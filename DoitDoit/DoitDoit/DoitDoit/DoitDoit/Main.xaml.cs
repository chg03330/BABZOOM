using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DoitDoit.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    // 메인화면
	public partial class Main : ContentPage
	{
        UserModel a = UserModel.GetInstance;

        public Main ()
		{
            InitializeComponent ();

            Task.Run(async () => {
                Network.FirebaseServer server = new Network.FirebaseServer();

                string result = await server.FirebaseRequest("GetPostData", new Dictionary<string, string>());

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                    ObservableCollection<Models.Post> posts = JsonConvert.DeserializeObject<ObservableCollection<Models.Post>>(result);
                    UserModel.GetInstance.Posts = posts;
                });
            });
        }

        private void Reco_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Post());
        }

        private void RecoMenu_Clicked(Object sender, EventArgs e)
        {

        }

        private void Reco2_Clicked(Object sender, EventArgs e)
        {

        }

        private void RecoMenu2_Clicked(Object sender, EventArgs e)
        {

        }

        private void SideMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SideMenu());
        }
        /// <summary>
        /// 메인 페이지 전부 로드 되었을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToRecoListButton_Clicked(object sender, EventArgs e) {
            RecoList recolist = new RecoList();
            Navigation.PushModalAsync(recolist);
        }
    } // END Of Main CLASS
}