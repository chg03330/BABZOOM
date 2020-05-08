using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchFood : ContentPage {
        public SearchFood() {
            InitializeComponent();
        }

        private void Register_Clicked(object sender, EventArgs e) {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}