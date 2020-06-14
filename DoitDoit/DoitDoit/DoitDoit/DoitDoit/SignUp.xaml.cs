using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using DoitDoit.Network;
using DoitDoit.Models;
using System.Text.RegularExpressions;
using Org.Apache.Http.Impl.Client;
using Android.OS;
using Android.Provider;
using Android.Views;

namespace DoitDoit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        UserModel usermodel;

        public SignUp() {
            InitializeComponent();
            this.usermodel = UserModel.GetInstance;
            this.ID.TextChanged += this.IDFieldChanged;
        }

       

        private void IDFieldChanged(object sender, TextChangedEventArgs e) {
            this.checkId.IsChecked = false;
        }

        private async void SignedUp_Clicked(object sender, EventArgs e)
        {
            if (this.checkId.IsChecked is false) {
                await DisplayAlert("안내", "아이디 중복 체크를 해주세요.", "OK");
                return;
            }
            else if (PASSWORD.Text==null || PWCHECK.Text == null) { 
                await DisplayAlert("안내", "빈칸을 모두 채워주세요.", "OK");
                return;
            }

            switch (Regex_checkPw()) {
                case 1:
                    Dictionary<string, string> post = new Dictionary<string, string>();
                    post["ID"] = this.ID.Text;
                    post["Password"] = this.PASSWORD.Text;
                    FirebaseServer server = FirebaseServer.Server;
                    string result = await server.FirebaseRequest("SignUp", post);

                    Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    if ("true".Equals(resultdic["Result"]))
                    {
                        this.usermodel.Id = this.ID.Text;
                        this.usermodel.Password = this.PASSWORD.Text;

                        await DisplayAlert("성공", "회원가입에 성공하셨습니다", "확인");

                        InfoEntry userinfo = new InfoEntry();
                        userinfo.Mode = true;

                        await Navigation.PushModalAsync(userinfo);
                    }
                    else
                    {
                        await DisplayAlert("실패", "회원가입에 실패했습니다.\n다시시도해주세요", "돌아가기");
                    }
                    break;
                case 2:
                    await DisplayAlert("실패", "비밀번호와 재입력한 비밀번호가 다릅니다. 다시 확인하세요", "돌아가기");
                    break;
                case 3:
                    await DisplayAlert("실패", "비밀번호 양식이 맞지않습니다. \n 영문+숫자+특문으로 8~12자 \n 특수문자종류 : !@#$%^&-_+=", "돌아가기");
                    break;
            }
        }

        private void Cancel2_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        /// <summary>
        /// 아이디 체크 버튼클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void checkId_Clicked(object sender, EventArgs e)
        {
            if (this.checkId.IsChecked is true) return;            
            else if (!Regex_checkId(this.ID.Text)) {
                await DisplayAlert("알림", "아이디는 영문/숫자만으로 이루어진 4~8자여야합니다.", "돌아가기");
                return;
            }

            bool result = await FirebaseServer.Server.IsUserExist(this.ID.Text);

            if (result is true) {
                await this.DisplayAlert("안내", "아이디가 이미 존재합니다.", "OK");
            }

            this.checkId.IsChecked = !result;
        }

        private bool Regex_checkId(String checkId) {
            Regex r = new Regex(@"^[0-9a-zA-Z]{4,8}$");
            //var id = / ^.*[A - Za - z0 - 9]{ 4,8}.*$/;
            if (r.IsMatch(checkId))
                return true;
            else
                return false;
        }
        private int Regex_checkPw()
        {
            String pw = PASSWORD.Text;
            String pwre = PWCHECK.Text;
            Regex reg = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&-_+=]).{8,12}$");
            if (reg.IsMatch(pw))
            {
                if (pw.Equals(pwre))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 3;
            }

        }
    }
}