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

        /// <summary>
        /// 로그인 버튼 클릭했을 때의 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Login_Clicked(object sender, EventArgs e) {
            String id = this.Entry_ID.Text;
            String pw = this.PASSWORD.Text;

            FirebaseServer server = FirebaseServer.Server;

            UserModel model = UserModel.GetInstance;

            bool result = await server.SignIn(id, pw);

            if (result) {
                Dictionary<string, string> req = new Dictionary<string, string>();
                req["ID"] = model.Id;

                string result1 = await server.FirebaseRequest("GetMenuData", req);

                ObservableCollection<FoodViewModel> list = new ObservableCollection<FoodViewModel>();

                list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<FoodViewModel>>(result1);

                model.FoodViewModels = list;

                await Navigation.PushModalAsync(new Main());
            }
            else {

            }
        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignUp());
        }
        
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
    }
}
