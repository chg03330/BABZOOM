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

        private bool checkPw() {
            return this.PASSWORD.Text.Equals(this.PWCHECK.Text);
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

            if (checkPw())
            {
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
            }
            else
            {
                await DisplayAlert("실패","비밀번호를 확인하세요", "취소");
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

            bool result = await FirebaseServer.Server.IsUserExist(this.ID.Text);

            if (result is true) {
                await this.DisplayAlert("안내", "아이디가 이미 존재합니다.", "OK");
            }

            this.checkId.IsChecked = !result;
        }
    }
}