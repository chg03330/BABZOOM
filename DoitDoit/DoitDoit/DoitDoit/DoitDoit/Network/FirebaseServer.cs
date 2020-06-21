using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using DoitDoit.Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Linq;

namespace DoitDoit.Network {
    class Token {
        public Packet packet { get; set; } = null;
        public int ID => this.sendpacket.ID;

        public event Action<Packet> ReRequested;

        private Packet sendpacket = null;
        private int Requested = 0;
        private int RequestNum = 3;

        public Token(Packet sendpacket) {
            this.sendpacket = sendpacket;
        }

        public async Task<Packet> GetPacket() {
            while (this.Requested < this.RequestNum) {
                var task = Task.Run(this.Wait);

                if (task.Wait(5000)) {
                    return this.packet;
                }
                else {
                    if (this.Requested < this.RequestNum) {
                        Console.WriteLine("ReRequested");
                        this.ReRequested?.Invoke(this.sendpacket);
                        this.Requested++;
                        continue;
                    }
                }
            }

            return new Packet() { Result = false, Context = "Response Timeout" };
        }

        public async Task Wait() {
            while (this.packet is null) {
                await Task.Delay(100);
            }
        }
    }

    /// <summary>
    /// SINGLETON
    /// 파이어베이스 서버 연결
    /// </summary>
    class FirebaseServer {
        private static FirebaseServer server = new FirebaseServer();
        public static FirebaseServer Server => FirebaseServer.server;
        
        private const string src = "https://us-central1-babzoom-7cae1.cloudfunctions.net/";
        private readonly HttpClient client = new HttpClient();

        private const string SEARCHSERVERADDRESS = 
            "140.238.7.47";
            //"127.0.0.1";
        private const int SEARCHSERVERPORT = 24243;
        private readonly System.Net.Sockets.TcpClient searchserver = null;

        private readonly object locker = new object();

        private readonly List<Packet> fpackets = new List<Packet>();
        private readonly List<Token> tokens = new List<Token>();

        private FirebaseServer() {
            try {
                this.searchserver = this.GetServer();
                Task.Run(() => { this.GetServerData(); });
            }
            catch(Exception) {}
        }

        ~FirebaseServer() {
            this.searchserver?.Close();
        }

        private void GetServerData() {
            TcpClient server = this.searchserver;
            NetworkStream stream = server.GetStream();

            byte[] buf = new byte[1024 * 100];

            int pointer = 0;
            bool flag = false;
            string stbuf = "";
            int left = 0;

        RERUN: while (true) {
                //string resultjson = "";
                int count = stream.Read(buf, 0, buf.Length);
                if (count is 0) continue;

                //Console.WriteLine(Encoding.UTF8.GetString(buf, 0, count));

                while (pointer < count) {
                    if (flag is false) {
                        int length = BitConverter.ToInt32(new ReadOnlySpan<byte>(buf, pointer, 4).ToArray(), 0);
                        pointer += 4;

                        if (pointer + length > count) {
                            stbuf += Encoding.UTF8.GetString(buf, pointer, count - pointer);
                            left = pointer + length - count;
                            flag = true;
                            pointer = 0;
                            goto RERUN;
                        }
                        else {
                            string result = Encoding.UTF8.GetString(buf, pointer, length);
                            pointer += length;

                            Console.WriteLine($"{length} {pointer} {count}" + "\n" + result);

                            this.CommandPacket(result);
                        }
                    }
                    else {
                        if (left > count) {
                            stbuf += Encoding.UTF8.GetString(buf, pointer, count);
                            left -= count;
                            flag = true;
                            pointer = 0;
                            goto RERUN;
                        }
                        else {
                            stbuf += Encoding.UTF8.GetString(buf, pointer, left);
                            pointer += left;
                            string result = String.Copy(stbuf);

                            stbuf = "";
                            left = 0;
                            flag = false;

                            this.CommandPacket(result);
                        }
                    }

                    if (pointer >= count) {
                        pointer = 0;
                        break;
                    }
                } // PARSE WHILE LOOP
            } // READ WHILE LOOP
        }

        private void CommandPacket(string resultjson) {
            Packet packet = JsonConvert.DeserializeObject<Packet>(resultjson);

            if (packet.FragmentMax > 1) {
                fpackets.Add(packet);
                List<Packet> ftp = new List<Packet>(fpackets);

                var fs = from f in ftp
                         where f.ID == packet.ID
                         orderby f.FragmentID ascending
                         select f;
                if (fs.Count() == packet.FragmentMax) {
                    string context = "";

                    foreach (Packet f in fs) {
                        context += f.Context;
                        fpackets.Remove(f);
                    }
                    packet.Context = context;

                    Task.Run(() => { this.CommandPacket(packet); });
                }
            }
            else {
                Task.Run(() => { this.CommandPacket(packet); });
            }
        }

        private void CommandPacket(Packet packet) {
            if (packet.ID >= 0) {
                Token token = (from t in this.tokens
                               where t.ID == packet.ID
                               select t).FirstOrDefault();
                if (!(token is null)) {
                    token.packet = packet;
                    this.tokens.Remove(token);
                }
            }
            else {

            }
        }

        private async Task<Packet> SendPacketData(Packet packet, bool response = false) {
            if (packet is null) return null;

            TcpClient server = this.searchserver;
            NetworkStream stream = server.GetStream();

            Token token = null;
            if (response) {
                packet.ID = Packet.IDCreator;

                token = new Token(packet);

                token.ReRequested += new Action<Packet>(repacket => {
                    lock (this.fpackets) {
                        this.fpackets.RemoveAll(rmpacket => rmpacket.ID == repacket.ID);
                    }

                    lock (this.locker) {
                        byte[] buf = repacket.ToBytes();
                        stream.Write(buf, 0, buf.Length);
                    }
                });

                this.tokens.Add(token);
            }

            lock (this.locker) {
                byte[] buf = packet.ToBytes();
                stream.Write(buf, 0, buf.Length);
            }

            if (token is null) {
                return null;
            }
            else {
                Packet resultpacket = await token.GetPacket();

                this.tokens.Remove(token);

                return resultpacket;
            }
        }

        #region FIREBASE REQUEST
        public async Task<string> FirebaseRequest(string function, Dictionary<string, string> values) {
            FormUrlEncodedContent requestcontent = new FormUrlEncodedContent(values);
            return await this.Request(function, requestcontent);
        }

        public async Task<string> FirebaseRequest(string function, String json) {
            StringContent requestcontent = new StringContent(json, Encoding.UTF8, "application/json");
            return await this.Request(function, requestcontent);
        }

        public async Task<string> FirebaseRequest(string function, object obj) {
            return await this.FirebaseRequest(function, JsonConvert.SerializeObject(obj));
        }
        #endregion

        private async Task<string> Request(string function, HttpContent content) {
            HttpResponseMessage response = await client.PostAsync(String.Concat(src, function), content);
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private System.Net.Sockets.TcpClient GetServer() {
            System.Net.Sockets.TcpClient server = new System.Net.Sockets.TcpClient();
            server.Connect(System.Net.IPAddress.Parse(SEARCHSERVERADDRESS), SEARCHSERVERPORT);

            return server;
        }

        public async Task<bool> SignIn(string id, string pw) {
            UserModel usermodel = UserModel.GetInstance;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["ID"] = id;
            dic["Password"] = pw;

            Packet packet = new Packet() { Command = "SignIn", Context = JsonConvert.SerializeObject(dic) };
            Packet recvpacket = await this.SendPacketData(packet, true);
            if (recvpacket is null) return false;

            Console.WriteLine(recvpacket.Context);

            if (recvpacket.Result == false) return false;

            Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(recvpacket.Context);

            if (recvpacket.Result) {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                    usermodel.Id = id;
                    usermodel.Password = pw;

                    if (resultdic.ContainsKey("name"))
                        usermodel.Name = resultdic["name"];
                    if (resultdic.ContainsKey("age"))
                        usermodel.Age = int.Parse(resultdic["age"]);
                    if (resultdic.ContainsKey("height"))
                        usermodel.Height = float.Parse(resultdic["height"]);
                    if (resultdic.ContainsKey("weight"))
                        usermodel.Weight = float.Parse(resultdic["weight"]);
                    if (resultdic.ContainsKey("gender"))
                        usermodel.Gender = Convert.ToBoolean(resultdic["gender"]);

                    usermodel.Bases = new Nutrition.nutBases(usermodel.Gender, usermodel.Age);
                });
            }

            return recvpacket.Result;
        }

        public async Task<List<string>> SearchFood(string foodname) {
            Packet packet = new Packet() { Command = "SearchFood", Context = foodname };
            Packet recvpacket = await this.SendPacketData(packet, true);

            List<string> foods = JsonConvert.DeserializeObject<List<string>>(recvpacket.Context);

            return foods;
        }

        public async Task<FoodData> SpecificFood(string foodname) {
            Packet packet = new Packet() { Command = "SpecificFood", Context = foodname };
            Packet recvpacket = await this.SendPacketData(packet, true);

            FoodData food = JsonConvert.DeserializeObject<FoodData>(recvpacket.Context);

            return food;
        }

        public async Task GetMenuData() {
            Packet packet = new Packet() { Command = "GetMenuData" };
            Packet recvpacket = await this.SendPacketData(packet, true);
            if (recvpacket.Result is false) return;

            List<FoodViewModel> menus = JsonConvert.DeserializeObject<List<FoodViewModel>>(recvpacket.Context);

            foreach (var menu in menus) {
                UserModel.GetInstance.FoodViewModels.Add(menu);
            }
        }

        public async Task<DoitDoit.Models.FoodViewModel[]> GetSpecificPostData(string postcode) {
            Packet packet = new Packet() { Command = "GetSpecificPostData", Context = postcode };
            Packet recvpacket = await this.SendPacketData(packet, true);
            if (recvpacket.Result is false) return new DoitDoit.Models.FoodViewModel[] { };

            Console.WriteLine(recvpacket.Context);

            DoitDoit.Models.FoodViewModel[] menus = JsonConvert.DeserializeObject<DoitDoit.Models.FoodViewModel[]>(recvpacket.Context);

            return menus;
        }

        public async Task<DoitDoit.Models.Comment[]> GetPostCommentData(string postcode) {
            Packet packet = new Packet() { Command = "GetPostCommentData", Context = postcode };
            Packet recvpacket = await this.SendPacketData(packet, true);
            if (recvpacket.Result is false) return new DoitDoit.Models.Comment[] { };

            DoitDoit.Models.Comment[] comments = JsonConvert.DeserializeObject<DoitDoit.Models.Comment[]>(recvpacket.Context);

            return comments;
        }

        public async Task<DoitDoit.Models.Post[]> GetPostData() {
            Packet packet = new Packet() { Command = "GetPostData" };
            Packet recvpacket = await this.SendPacketData(packet, true);
            if (recvpacket.Result is false) return new DoitDoit.Models.Post[] { };

            DoitDoit.Models.Post[] posts
                = JsonConvert.DeserializeObject<DoitDoit.Models.Post[]>(recvpacket.Context);

            return posts;
        }

        public async Task<DoitDoit.Models.Post> IsPostExist(string userid, string mdate) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["UserID"] = userid;
            dic["MDate"] = mdate;

            Packet packet = new Packet() {
                Command = "IsPostExist",
                Context = JsonConvert.SerializeObject(dic)
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return JsonConvert.DeserializeObject<DoitDoit.Models.Post>(recvpacket.Context);
        }

        public async Task DeletePostData(string postcode) {
            Packet packet = new Packet() {
                Command = "DeletePostData",
                Context = postcode
            };

            this.SendPacketData(packet, true);
        }

        public async Task<bool> DeleteCommentData(string comcode, string postcode) {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["Post"] = postcode;
            param["Code"] = comcode;

            Packet packet = new Packet() {
                Command = "DeleteCommentData",
                Context = JsonConvert.SerializeObject(param)
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }

        public async Task<bool> DeleteMenuData(string menucode) {
            Packet packet = new Packet() {
                Command = "DeleteMenuData",
                Context = menucode
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }

        public async Task<bool> SetMenuData(FoodViewModel menu) {
            Packet packet = new Packet() {
                Command = "SetMenuData",
                Context = JsonConvert.SerializeObject(menu)
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }

        public async Task<bool> IsUserExist(string id) {
            Packet packet = new Packet() {
                Command = "IsUserExist",
                Context = id
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }

        public async Task<FoodData[]> GetRecommendMenuData() {
            Packet packet = new Packet() {
                Command = "GetRecommendMenuData"
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            FoodData[] result = JsonConvert.DeserializeObject<FoodData[]>(recvpacket.Context);

            return result;
        }

        public async Task<bool> SignUp(Dictionary<string, string> data) {
            Packet packet = new Packet() {
                Command = "SignUp",
                Context = JsonConvert.SerializeObject(data)
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }

        public async Task<bool> SetUserData(Dictionary<string, string> data) {
            Packet packet = new Packet() {
                Command = "SetUserData",
                Context = JsonConvert.SerializeObject(data)
            };

            Packet recvpacket = await this.SendPacketData(packet, true);

            return recvpacket.Result;
        }
    } // END OF FirebaseServer
}
