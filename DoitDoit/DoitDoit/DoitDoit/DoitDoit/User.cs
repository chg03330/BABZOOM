using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit
{
   
    // 싱글톤 Pattern
   

    public class User
    {
        /************************* 유저정보 ********************************/
        private String id;
        private String pw;
        private float height;
        private float weight;
        private int age;
        private bool gender;
        private String name;
        //**                         **                  ***                */
        private static User user;
        /*******************************************************************/


        /************************** 생성자**********************************/
        private User() {
        }

        public static User GenInstance() {
            if (user == null) user = new User();
            return user;
        }
        /*******************************************************************/


        /************************** 데이터 셋 *******************************/
        public void setData_Id(String a) { this.id = a; }
        public void setData_Pw(String a) { this.pw = a; }
        public void setData_Height(float a) { this.height = a; }
        public void setData_Weight(float a) { this.weight = a; }
        public void setData_Age(int a) { this.age = a; }
        public void setData_Gender(bool a) { this.gender = a; }
        public void setData_Name(String a) { this.name = a; }
        /*******************************************************************/

        /************************** 데이터 겟 ******************************/
        public String getData_Id() { return this.id; }
        public String getData_Pw() { return this.pw; }
        public float getData_Height() { return this.height; }
        public float getData_Weight() { return this.weight; }
        public int getData_Age() { return this.age; }
        public bool getData_Gender() { return this.gender; }
        public String getData_Name() { return this.name; }

        /*******************************************************************/

    }
}
