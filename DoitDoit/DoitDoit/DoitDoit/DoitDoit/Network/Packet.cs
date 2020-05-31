using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DoitDoit.Network {
    [Serializable]
    class Packet {
        private static IEnumerator<int> IDEnumerator = Packet.GetId();
        static public int IDCreator {
            get {
                if (Packet.IDEnumerator.MoveNext()) {
                    return Packet.GetId().Current;
                }
                else return -1;
            }
        }
        public string Command { get; set; }
        public bool Result { get; set; }
        public string Context { get; set; }
        public int ID { get; set; } = -1;
        public int FragmentMax { get; set; } = 1;
        public int FragmentID { get; set; } = 1;

        static private IEnumerator<int> GetId() {
            for (int i = 1; i < int.MaxValue; i++) {
                yield return i;
            }
        }

        public byte[] ToBytes() {
            string json = JsonConvert.SerializeObject(this);

            byte[] buf = Encoding.UTF8.GetBytes(json);

            return buf;
        }
    } // END OF Packet CLASS
}
