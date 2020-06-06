using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit.Layout {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NutChartTemplate : Frame {
        public NutChartTemplate() {
            InitializeComponent();
        }

        private void NutButton_Clicked(object sender, EventArgs e) {

        }
    }
}