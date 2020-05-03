using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


using DoitDoit.Network;
using Newtonsoft.Json;
using DoitDoit.Models;
using System.Collections.ObjectModel;

namespace DoitDoit
{
    //첫번째 로그인 화면
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            String id = Entry_ID.Text;
            String pw = PASSWORD.Text;

            Dictionary<string, string> post = new Dictionary<string, string>();
            post["ID"] = id;
            post["Password"] = pw;

            FirebaseServer server = new FirebaseServer();
            string result = await server.FirebaseRequest("SignIn", post);

            Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);


            if ("true".Equals(resultdic["Result"]))
            {
                UserModel a = UserModel.GetInstance;
                a.Id=id;
                a.Password=pw;

                
                Dictionary<string, string> req = new Dictionary<string, string>();
                req["ID"] = a.Id;

                string result1 = await server.FirebaseRequest("GetMenuData", req);

                ObservableCollection<FoodViewModel> list = new ObservableCollection<FoodViewModel>();

                list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<FoodViewModel>>(result1);

                a.FoodViewModels = list;

                await Navigation.PushModalAsync(new Main());
            }
            else {
                await DisplayAlert(resultdic["Result"], resultdic["Context"], "Cancel");
            }
        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignUp());
        }
        
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new UserCalenderDay());
        }
    }
}
