using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay : ContentPage
    {
        public UserCalenderDay()
        {
            InitializeComponent();
        }

        private void addUserMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddUserMenu());
        }
    }
}