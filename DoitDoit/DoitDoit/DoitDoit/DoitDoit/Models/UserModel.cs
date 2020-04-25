using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit.Models
{

    // 싱글톤 Pattern


    public class UserModel
    {
        /********************** 유저정보 / 필드 ***************************/
        private String id = "초기값"; // Field
        private String pw;
        private float height;
        private float weight;
        private int age;
        private bool gender;
        private String name;
        //**                         **                  ***                */
        private static UserModel userModel;
        /*******************************************************************/

        /******************** 유저정보 / Property **************************/
        public String Id { get => this.id; set { this.id = value; } } // Property
        public String Password { get => this.pw; set { this.pw = value; } }
        public float Height { get => this.height; set { this.height = value; } }
        public float Weight { get => this.weight; set { this.weight = value; } }
        public int Age { get => this.age; set { this.age = value; } }
        public bool Gender { get => this.gender; set { this.gender = value; } }
        public String Name { get => this.name; set { this.name = value; } }


        /*******************************************************************/

        /************************** 생성자**********************************/
        private UserModel() {
        }

        public static UserModel GetInstance
        {
            get
            {
                if (userModel == null)
                    userModel = new UserModel(); 
                return userModel;
            }
        }

        /*******************************************************************/


    }
}
