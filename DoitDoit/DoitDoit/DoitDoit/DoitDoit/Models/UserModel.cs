using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Collections.ObjectModel;

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
        private ObservableCollection<Post> posts;
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
                this.OnPropertyChanged(nameof(this.Posts));
            }
        }
        #endregion

        /************************** 생성자**********************************/
        private UserModel() {}
        /*******************************************************************/
    }
}
