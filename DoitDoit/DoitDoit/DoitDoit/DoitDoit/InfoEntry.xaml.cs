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
		UserModel a;
		String gender;
		public InfoEntry()
		{
			InitializeComponent ();
			a = UserModel.GetInstance;
			gender = "false";
			if (!"".Equals(a.Name))
				searchInfo();		

		}

		private async void searchInfo() {
			Dictionary<string, string> post = new Dictionary<string, string>();
			post["ID"] = a.Id;
			post["Password"] = a.Password;
			FirebaseServer server = FirebaseServer.Server;
			string result = await server.FirebaseRequest("GetUserData", post);
			Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
			if ("true".Equals(resultdic["gender"]))
			{
				a.Name = resultdic["name"];
				a.Age = int.Parse(resultdic["age"]);
				a.Height = float.Parse(resultdic["height"]);
				a.Weight = float.Parse(resultdic["weight"]);
				if ("true".Equals(resultdic["gender"]))
					a.Gender = true;
				else
					a.Gender = false;

				NAME.Text = a.Name;
				HEIGHT.Text = a.Height.ToString();
				WEIGHT.Text = a.Weight.ToString();
				AGE.Text = a.Age.ToString();
				if (a.Gender == true)
				{
					labelGender.Text = "남자";
					gender = "true";
					SWITCH.IsToggled = true;
				}
				else
				{
					labelGender.Text = "여자";
					gender = "false";
					SWITCH.IsToggled = false;
				}

			}
			else
			{
				await DisplayAlert("실패", "정보 불러오는게 실패했습니다.", "확인");
			}
		}

        private async void OK_Clicked(object sender, EventArgs e)
        {
			Dictionary<string, string> post = new Dictionary<string, string>();
			post["ID"] = a.Id;
			post["Password"] = a.Password;
			post["Height"] = HEIGHT.Text;
			post["Weight"] = WEIGHT.Text;
			post["Name"] = NAME.Text;
			post["Gender"] = gender;
			post["Age"] = AGE.Text;
			FirebaseServer server = FirebaseServer.Server;
			string result = await server.FirebaseRequest("SetUserData", post);

			Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
			await DisplayAlert("결과", "ㅇㅋ", "확인");
			Navigation.PushModalAsync(new Main());
        }

		private void Switch_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value == true) {
				labelGender.Text = "남자";
				gender = "true";
			}
			else {
				labelGender.Text = "여자";
				gender = "false";
			}
		}
	}
}