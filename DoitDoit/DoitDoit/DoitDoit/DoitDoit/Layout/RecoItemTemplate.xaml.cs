using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit.Layout {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoItemTemplate : ContentView {
        public RecoItemTemplate() {
            InitializeComponent();
        }

        private void PostButton_Clicked(object sender, EventArgs e) {

        }
    }
}