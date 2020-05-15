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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is ButtonWithTag button) {
                RecoList_Post post = new RecoList_Post();
                string postcode = (String)button.Tag;

                await DisplayAlert("", postcode, "close");

                Navigation.PushModalAsync(post);
            }
        }
    }

    public class CommentCountConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return $"댓글 {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return 0;
        }
    }
}