using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DoitDoit.Models;
using Newtonsoft.Json;
using DoitDoit.Network;

namespace DoitDoit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoEntry : ContentPage
	{
		private readonly UserModel usermodel;
		public Layout.MasterDetailLayout MasterDetail { get; set; } = null;

		public bool Mode { get; set; } = false;

		public InfoEntry(Layout.MasterDetailLayout md)
		{
			InitializeComponent ();
			usermodel = UserModel.GetInstance;
			/// 회원가입 화면에서 왔을 경우 Cancel 버튼 비활성화
			this.CanelButton.IsVisible = !this.Mode;
			this.MasterDetail = md;
		}

		public InfoEntry() {
			InitializeComponent();
			usermodel = UserModel.GetInstance;
			/// 회원가입 화면에서 왔을 경우 Cancel 버튼 비활성화
			this.CanelButton.IsVisible = !this.Mode;
		}

		/// <summary>
		/// OK 버튼 눌렀을 때의 이벤트 처리기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void OK_Clicked(object sender, EventArgs e)
        {
            bool accept = await DisplayAlert("안내", "정보를 수정할까요?", "OK", "Cancel");

			if (accept) {
				#region SET USERMODEL & POST DATA
				Dictionary<string, string> post = new Dictionary<string, string> {
					["ID"] = usermodel.Id,
					["Password"] = usermodel.Password,
					["Height"] = (usermodel.Height = Convert.ToSingle(this.HEIGHT.Text)).ToString(),
					["Weight"] = (usermodel.Weight = Convert.ToSingle(this.WEIGHT.Text)).ToString(),
					["Name"] = (usermodel.Name = NAME.Text),
					["Gender"] = (usermodel.Gender = this.SWITCH.IsToggled).ToString(),
					["Age"] = (usermodel.Age = Convert.ToInt32(AGE.Text)).ToString()
				};
				#endregion

#pragma warning disable 4014
				/// 서버에 유저 정보를 보낸다.
				Task.Run(async () => {
					FirebaseServer server = FirebaseServer.Server;
					//string result = await server.FirebaseRequest("SetUserData", post);
					bool result = await server.SetUserData(post);
				});
#pragma warning restore 4014

				usermodel.Bases = new Nutrition.nutBases(usermodel.Gender, usermodel.Age);

				/// 회원가입에서 왔을 경우 -> Main 화면
				/// 사이드 메뉴에서 왔을 경우 -> Back
				if (this.Mode) {
					await Navigation.PushModalAsync(new Layout.MasterDetailLayout(new Main()));
				}
				else {
					//if (this.MasterDetail is null) {
						this.OnBackButtonPressed();
					//}
					//else {
						//this.MasterDetail.ToMain();
					//}
				}
			}
		}

		private void Switch_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value == true) {
				labelGender.Text = "남자";
			//	gender = "true";
			}
			else {
				labelGender.Text = "여자";
			//	gender = "false";
			}
		}

		private void Cancel_Clicked(object sender, EventArgs e)
		{
			//if (this.MasterDetail is null) {
				this.OnBackButtonPressed();
			//}
			//else {
				//this.MasterDetail.ToMain();
			//}
		}
	}
}