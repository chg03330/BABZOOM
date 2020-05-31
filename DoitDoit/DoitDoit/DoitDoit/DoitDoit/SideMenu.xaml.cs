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
    public partial class SideMenu : ContentPage
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void Main_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Main());
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new InfoEntry());
        }

        private void Calendar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Calender());
        }

        private void MenuMake_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new UserCalenderDay());
        }

        /*private void MenuShare_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1());
        }*/

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}