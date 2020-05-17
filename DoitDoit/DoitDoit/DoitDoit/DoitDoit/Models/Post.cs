using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using System.Linq;

namespace DoitDoit.Models {
    [Serializable]
    public class Post : NotifyableObject {
        private ObservableCollection<FoodViewModel> menus = new ObservableCollection<FoodViewModel>();
        private ObservableCollection<Comment> comments = new ObservableCollection<Comment>();

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

        public string[] Menu {
            get {
                if (this.Menus is null) return null;
                var menus = from menu in this.Menus
                            select menu.Code;
                return menus.ToArray();
            }
        }

        public ObservableCollection<FoodViewModel> Menus {
            get => this.menus;
            set {
                this.menus = value;
                this.OnPropertyChanged(nameof(this.Menus));
            }
        }
        public ObservableCollection<Comment> Comments {
            get => this.comments;
            set {
                this.comments = value;
                this.OnPropertyChanged(nameof(this.Comments));
            }
        }
    }

    [Serializable]
    public class Comment {
        public string Code { get; set; } = "";
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
