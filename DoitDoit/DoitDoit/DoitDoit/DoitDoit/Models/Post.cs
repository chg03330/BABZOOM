using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using System.Linq;

namespace DoitDoit.Models {
    [Serializable]
    public class Post : NotifyableObject {
        private string code = "";
        public string Code {
            get => this.code;
            set {
                this.code = value;
                this.OnPropertyChanged(nameof(this.Code));
            }
        }

        private string userid = "";
        public string UserID {
            get => this.userid;
            set {
                this.userid = value;
                this.OnPropertyChanged(nameof(this.UserID));
            }
        }

        private string context = "";
        public string Context {
            get => this.context;
            set {
                this.context = value;
                this.OnPropertyChanged(nameof(this.Context));
            }
        }

        private DateTime date;
        public DateTime Date {
            get => this.date;
            set {
                this.date = value;
                this.OnPropertyChanged(nameof(this.Date));
            }
        }
        public double DateTime {
            get {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                TimeSpan span = this.Date - dt1970;

                return Math.Floor(span.TotalMilliseconds);
            }
        }

        private DateTime mdate;
        public DateTime MDate {
            get => this.mdate;
            set {
                this.mdate = value;
                this.OnPropertyChanged(nameof(this.MDate));
            }
        }
        public double MDateTime {
            get {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                TimeSpan span = this.MDate - dt1970;

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

        private ObservableCollection<FoodViewModel> menus = new ObservableCollection<FoodViewModel>();
        public ObservableCollection<FoodViewModel> Menus {
            get => this.menus;
            set {
                this.menus = value;
                this.OnPropertyChanged(nameof(this.Menus));
            }
        }

        private ObservableCollection<Comment> comments = new ObservableCollection<Comment>();
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
