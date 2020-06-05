using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DoitDoit.Models;

namespace DoitDoit {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoList : ContentPage {
        public RecoList() {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;
            if (!(button.BindingContext is Models.Post postdata)) return;

            RecoList_Post post = new RecoList_Post();
            post.PostData = postdata;

            Navigation.PushModalAsync(post);
        }

        private async void RefreshButton_Clicked(object sender, EventArgs e) {
            if (!(sender is Button button)) return;

            button.IsEnabled = false;

            Network.FirebaseServer server = Network.FirebaseServer.Server;

            //string result = await server.FirebaseRequest("GetPostData", new Dictionary<string, string>());

            Models.Post[] posts = await server.GetPostData();

            // UI 스레드에서 실행
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                var ps = (from p in posts
                          where !UserModel.GetInstance.Posts.Any(post => post.Code == p.Code)
                          select p);

                foreach (Models.Post p in ps) {
                    UserModel.GetInstance.Posts.Add(p);
                }

                UserModel.GetInstance.SortPosts();

                button.IsEnabled = true;
            });
        }
    }
}