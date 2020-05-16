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

            //UserModel.GetInstance.Posts.OrderBy(n => n.Date);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;
            if (!(button.BindingContext is Models.Post postdata)) return;

            RecoList_Post post = new RecoList_Post();
            post.PostData = postdata;

            Navigation.PushModalAsync(post);
        }
    }
}