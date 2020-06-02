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
using Android.App;

namespace DoitDoit
{
    //첫번째 로그인 화면
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 로그인 버튼 클릭했을 때의 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
<<<<<<< Updated upstream
        private async void Login_Clicked(object sender, EventArgs e) {
            String id = this.Entry_ID.Text;
            String pw = this.PASSWORD.Text;

            FirebaseServer server = FirebaseServer.Server;

            UserModel model = UserModel.GetInstance;

            bool result = await server.SignIn(id, pw);
=======
        private async void Login_Clicked(object sender, EventArgs e)
        {
            Button a = sender as Button;
            a.IsEnabled = false;
                String id = this.Entry_ID.Text;
                String pw = this.PASSWORD.Text;
                
                    Dictionary<string, string> post = new Dictionary<string, string>();
                    post["ID"] = id;
                    post["Password"] = pw;

                    FirebaseServer server = FirebaseServer.Server;
                    string result = await server.FirebaseRequest("SignIn", post);

                    Packet packet = JsonConvert.DeserializeObject<Packet>(result);



            if (packet.Result)
            {
                UserModel usermodel = UserModel.GetInstance;

                Dictionary<string, string> resultdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(packet.Context);

                usermodel.Id = id;
                usermodel.Password = pw;

                usermodel.Name = resultdic["name"];
                usermodel.Age = int.Parse(resultdic["age"]);
                usermodel.Height = float.Parse(resultdic["height"]);
                usermodel.Weight = float.Parse(resultdic["weight"]);
                usermodel.Gender = Convert.ToBoolean(resultdic["gender"]);

                usermodel.Bases = new Nutrition.nutBases(usermodel.Gender, usermodel.Age);
>>>>>>> Stashed changes

            if (result) {
                Dictionary<string, string> req = new Dictionary<string, string>();
                req["ID"] = model.Id;

                await server.GetMenuData();

                //string result1 = await server.FirebaseRequest("GetMenuData", req);

                //ObservableCollection<FoodViewModel> list = new ObservableCollection<FoodViewModel>();

                //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<FoodViewModel>>(result1);

                //model.FoodViewModels = list;

                OnBackButtonPressed();
                await Navigation.PushModalAsync(new Main());
            }
<<<<<<< Updated upstream
            else {

=======
            else
            {
                await DisplayAlert(packet.Result.ToString(), packet.Context, "Cancel") ;
                a.IsEnabled = true;
>>>>>>> Stashed changes
            }
                
        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignUp());
        }
        
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            // 해당 화면에서 취소버튼을 누를 경우
            // 어플 종료를 한다
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}
