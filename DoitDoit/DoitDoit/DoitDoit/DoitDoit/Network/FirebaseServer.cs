using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
    } // END OF FirebaseServer
}
