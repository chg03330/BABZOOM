using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DoitDoit.Models
{

    // 싱글톤 Pattern


    public class UserModel
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

        #region 유저 정보 Field
        private String id;
        private String pw;
        private float height;
        private float weight;
        private int age;
        private bool gender;
        private String name;
        private ObservableCollection<FoodViewModel> foodViewModels;
        private ObservableCollection<Post> posts;
        #endregion
        
        #region 유저 정보 Property
        public String Id { get => this.id; set { this.id = value; } }
        public String Password { get => this.pw; set { this.pw = value; } }
        public float Height { get => this.height; set { this.height = value; } }
        public float Weight { get => this.weight; set { this.weight = value; } }
        public int Age { get => this.age; set { this.age = value; } }
        public bool Gender { get => this.gender; set { this.gender = value; } }
        public String Name { get => this.name; set { this.name = value; } }
        public ObservableCollection<FoodViewModel> FoodViewModels { get => foodViewModels; set { this.foodViewModels = value; } }
        public ObservableCollection<Post> Posts {
            get => this.posts;
            set {
                this.posts = value;
            }
        }
        #endregion

        /************************** 생성자**********************************/
        private UserModel() {
        }
        /*******************************************************************/


    }
}
