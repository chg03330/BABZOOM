using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit.Models {
    [Serializable]
    public class Post {
        public string Code { get; set; } = "";
        public string UserID { get; set; } = "";
        public string Context { get; set; } = "";
        public DateTime Date { get; set; }
        public double DateTime {
            get {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                TimeSpan span = this.Date - dt1970;

                return Math.Floor(span.TotalMilliseconds);
            }
        }

        public string[] Menu { get; set; }

        public FoodViewModel[] Menus { get; set; } = new FoodViewModel[] { };
        public Comment[] Comments { get; set; } = new Comment[] { };
    }

    public class Comment {
        public string ID { get; set; } = "";
        public string PostID { get; set; } = "";
        public DateTime Date { get; set; }
        public double DateTime {
            get {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                TimeSpan span = this.Date - dt1970;

                return Math.Floor(span.TotalMilliseconds);
            }
        }

        public string Context { get; set; } = "";
    }
}
