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
        public int ID { get; private set; } = -1;

        public Token(int id) {
            this.ID = id;
        }

        public async Task<Packet> GetPacket() {
            var task = Task.Run(this.Wait);
            
            if (task.Wait(5000)) {
                return this.packet;
            }
            else {
                return null;
            }
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

            List<Packet> fpackets = new List<Packet>();

            while (true) {
                string resultjson = "";
                byte[] buf = new byte[1024 * 10];

                do {
                    int count = stream.Read(buf, 0, buf.Length);
                    resultjson += Encoding.UTF8.GetString(buf, 0, count);
                } while (stream.DataAvailable);

                Console.WriteLine(resultjson);

                Packet packet = JsonConvert.DeserializeObject<Packet>(resultjson);

                if (packet.FragmentMax > 1) {
                    fpackets.Add(packet);

                    var fs = from f in fpackets
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
                        this.CommandPacket(packet);
                    }
                }
                else {
                    this.CommandPacket(packet);
                }
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

            Token token = null;
            if (response) {
                packet.ID = Packet.IDCreator;

                token = new Token(packet.ID);
                this.tokens.Add(token);
            }

            TcpClient server = this.searchserver;
            NetworkStream stream = server.GetStream();

            lock (this.locker) {
                byte[] buf = packet.ToBytes();
                stream.Write(buf, 0, buf.Length);
            }

            if (token is null) {
                return null;
            }
            else {
                return await token.GetPacket();
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
            else if (recvpacket.Result == false) return false;

            Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(recvpacket.Context);

            if (recvpacket.Result) {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                    usermodel.Id = id;
                    usermodel.Password = pw;

                    usermodel.Name = resultdic["name"];
                    usermodel.Age = int.Parse(resultdic["age"]);
                    usermodel.Height = float.Parse(resultdic["height"]);
                    usermodel.Weight = float.Parse(resultdic["weight"]);
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

            List<FoodViewModel> menus = JsonConvert.DeserializeObject<List<FoodViewModel>>(recvpacket.Context);

            foreach (var menu in menus) {
                UserModel.GetInstance.FoodViewModels.Add(menu);
            }
        }
    } // END OF FirebaseServer
}
