using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using DoitDoit.Models;
using Newtonsoft.Json;

namespace DoitDoit.Network {
    /// <summary>
    /// SINGLETON
    /// 파이어베이스 서버 연결
    /// </summary>
    class FirebaseServer {
        public static FirebaseServer Server { get; private set; } = new FirebaseServer();
        
        private const string src = "https://us-central1-babzoom-7cae1.cloudfunctions.net/";
        private readonly HttpClient client = new HttpClient();

        private const string SEARCHSERVERADDRESS = "140.238.7.47";
        private const int SEARCHSERVERPORT = 24243;
        private readonly System.Net.Sockets.TcpClient searchserver = null;

        private FirebaseServer() {
            this.searchserver = this.GetServer();
        }

        ~FirebaseServer() {
            this.searchserver.Close();
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

        public async Task<List<string>> SearchFood(string foodname) {
            Packet packet = new Packet() { Command = "SearchFood", Context = foodname };

            System.Net.Sockets.TcpClient server = this.searchserver;

            System.Net.Sockets.NetworkStream stream = server.GetStream();

            byte[] packetbuf = packet.ToBytes();
            stream.Write(packetbuf, 0, packetbuf.Length);

            byte[] buf = new byte[1024 * 10];

            string resultjson = "";
            int count = 0;

            do {
                count = stream.Read(buf, 0, buf.Length);
                resultjson += Encoding.UTF8.GetString(buf, 0, count);
            } while (stream.DataAvailable);

            List<string> foods = JsonConvert.DeserializeObject<List<string>>(resultjson);

            return foods;
        }

        public async Task<FoodData> SpecificFood(string foodname) {
            Packet packet = new Packet() { Command = "SpecificFood", Context = foodname };

            System.Net.Sockets.TcpClient server = this.searchserver;

            System.Net.Sockets.NetworkStream stream = server.GetStream();

            byte[] packetbuf = packet.ToBytes();
            stream.Write(packetbuf, 0, packetbuf.Length);

            byte[] buf = new byte[1024 * 10];

            string resultjson = "";
            int count = 0;

            do {
                count = stream.Read(buf, 0, buf.Length);
                resultjson += Encoding.UTF8.GetString(buf, 0, count);
            } while (stream.DataAvailable);

            FoodData food = JsonConvert.DeserializeObject<FoodData>(resultjson);

            return food;
        }
    } // END OF FirebaseServer
}
