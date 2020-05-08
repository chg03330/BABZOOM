using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DoitDoit.Network {
    [Serializable]
    class Packet {
        public string Command { get; set; }
        public bool Result { get; set; }
        public string Context { get; set; }

        public byte[] ToBytes() {
            string json = JsonConvert.SerializeObject(this);

            byte[] buf = Encoding.UTF8.GetBytes(json);

            return buf;
        }
    } // END OF Packet CLASS
}
