using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Collections.ObjectModel;

using System.Linq;

using Newtonsoft.Json;

namespace DoitDoit.Models
{
    // 싱글톤 Pattern
    /// <summary>
    /// SINGLETON
    /// 유저 정보를 가지고 있는 객체
    /// </summary>
    [Serializable]
    public class UserModel : INotifyPropertyChanged
    {
        #region 싱글톤
        private static UserModel userModel;
        public static UserModel GetInstance
        {
            get
            {
                if (userModel == null)
                    userModel = new UserModel();
                return userModel;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #region 유저 정보 Field
        private String id;
        private String pw;
        private float height;
        private float weight;
        private int age;
        private bool gender;
        private String name;
        private Nutrition.nutBases bases;
        private ObservableCollection<FoodViewModel> foodViewModels;
        private ObservableCollection<Post> posts = new ObservableCollection<Post>();
        #endregion

        #region 유저 정보 Property
        public String Id {
            get => this.id;
            set {
                this.id = value;
                this.OnPropertyChanged(nameof(this.id));
            }
        }
        public String Password { get => this.pw;
            set {
                this.pw = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }
        public float Height {
            get => this.height;
            set {
                this.height = value;
                this.OnPropertyChanged(nameof(this.Height));
            }
        }
        public float Weight {
            get => this.weight;
            set {
                this.weight = value;
                this.OnPropertyChanged(nameof(this.Weight));
            }
        }
        public int Age {
            get => this.age;
            set {
                this.age = value;
                this.OnPropertyChanged(nameof(this.Age)); }
        }
        public bool Gender {
            get => this.gender;
            set {
                this.gender = value;
                this.OnPropertyChanged(nameof(this.Gender));
            }
        }
        public String Name {
            get => this.name;
            set {
                this.name = value;
                this.OnPropertyChanged(nameof(this.Name));
            }
        }
        public Nutrition.nutBases Bases {
            get => this.bases;
            set {
                this.bases = value;
                this.OnPropertyChanged(nameof(this.Bases));
            }
        }
        public ObservableCollection<FoodViewModel> FoodViewModels {
            get => foodViewModels;
            set {
                this.foodViewModels = value;
                this.OnPropertyChanged(nameof(this.FoodViewModels));
            }
        }
        public ObservableCollection<Post> Posts {
            get => this.posts;
            set {
                this.posts = value;
                this.OnPropertyChanged("Posts");
            }
        }
        #endregion

        /// <summary>
        /// 현재 가지고 있는 식단 목록중에서 해당 일 / 월 / 년 에 해당하는 식단 그룹을 가져옵니다.
        /// </summary>
        /// <param name="val">
        /// 
        /// </param>
        /// <param name="mode">
        /// 0 = 일
        /// 1 = 월
        /// 2 = 년
        /// </param>
        /// <returns></returns>
        public IEnumerable<FoodViewModel> GetMenuGroup(DateTime time, int mode = 0) {
            if (mode > 2 || mode < 0) mode = 0;

            var result = this.FoodViewModels.Where(menu => {
                string Code = "";

                int day = Convert.ToInt32(menu.Code.Substring(6, 2));
                int month = Convert.ToInt32(menu.Code.Substring(4, 2));
                int year = Convert.ToInt32(menu.Code.Substring(0, 4));

                DateTime codetime = new DateTime(year, month, day);

                if (time.Year == codetime.Year) {
                    if (mode is 2) return true;
                    if (time.Month == codetime.Month) {
                        if (mode is 1) return true;
                        if (time.Day == codetime.Day) {
                            if (mode is 0) return true;
                        }
                    }
                }

                return false;
            });

            return result;
        }

        public IEnumerable<FoodViewModel> GetMenuGroup(DateTime time, int mode, out Microcharts.Entry[] nutentries) {
            var result = this.GetMenuGroup(time, mode);

            nutentries = ExMethod.MenuLINQ.GetEntry(result);

            return result;
        }

        /// <summary>
        /// 공유 식단 데이터를 날짜별로 정렬
        /// 꼭 UI 스레드에서 실행 되어야 함
        /// </summary>
        public void SortPosts() {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                this.Posts = new ObservableCollection<Post>(this.Posts.OrderByDescending(post => post.Date));
            });
        }

        /************************** 생성자**********************************/
        private UserModel() {}
        /*******************************************************************/
    }
}
