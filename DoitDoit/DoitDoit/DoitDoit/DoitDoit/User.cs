using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit
{
    public class User
    {
        public User(String id, String pw) {
            this.id = id;
            this.pw = pw;
        }
        public String id { get; set; }
        public String pw { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public int age { get; set; }
        public Boolean gender { get; set; }
        public String name { get; set; }


    }
}
