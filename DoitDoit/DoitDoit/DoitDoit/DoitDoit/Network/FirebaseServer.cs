﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using DoitDoit.Models;
using Newtonsoft.Json;

namespace DoitDoit.Network {
    class FirebaseServer {
        private const string src = "https://us-central1-babzoom-7cae1.cloudfunctions.net/";
        HttpClient client = new HttpClient();


        public async Task<string> FirebaseRequest(string function, Dictionary<string, string> values) {
            FormUrlEncodedContent requestcontent = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await client.PostAsync(String.Concat(src, function), requestcontent);
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private System.Net.Sockets.TcpClient GetServer() {
            System.Net.Sockets.TcpClient server = new System.Net.Sockets.TcpClient();
            server.Connect(System.Net.IPAddress.Parse("140.238.7.47"), 24243);

            return server;
        }

        public async Task<List<string>> SearchFood(string foodname) {
            Packet packet = new Packet() { Command = "SearchFood", Context = foodname };

            System.Net.Sockets.TcpClient server = this.GetServer();

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

            server.Close();

            List<string> foods = JsonConvert.DeserializeObject<List<string>>(resultjson);

            return foods;
        }

        public async Task<Food> SpecificFood(string foodname) {
            Packet packet = new Packet() { Command = "SpecificFood", Context = foodname };

            System.Net.Sockets.TcpClient server = this.GetServer();

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

            server.Close();

            Food food = JsonConvert.DeserializeObject<Food>(resultjson);

            return food;
        }
    } // END OF FirebaseServer
}
