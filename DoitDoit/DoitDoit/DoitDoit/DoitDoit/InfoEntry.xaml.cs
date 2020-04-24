using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using DoitDoit.Network;

namespace DoitDoit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoEntry : ContentPage
	{
		//정보입력화면 -로그인 이후
		User a;
		public InfoEntry(User user)
		{
			InitializeComponent ();
			a = user;
		}

        private async void OK_Clicked(object sender, EventArgs e)
        {
			Dictionary<string, string> post = new Dictionary<string, string>();
			post["ID"] = a.id;
			post["Password"] = a.pw;
			post["Height"] = HEIGHT.Text;
			post["Weight"] = WEIGHT.Text;
			post["Name"] = "김덕배";
			post["Gender"] = "true";
			post["Age"] = AGE.Text;
			FirebaseServer server = new FirebaseServer();
			string result = await server.FirebaseRequest("SetUserData", post);

			Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
			await DisplayAlert("결과", "ㅇㅋ", "확인");
			Navigation.PushModalAsync(new Main());
        }
	}
}