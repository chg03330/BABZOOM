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
    public partial class AddUserMenu : ContentPage
    {
        public AddUserMenu()
        {
            InitializeComponent();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}