using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BABZOOM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
		public Main ()
		{
			InitializeComponent();
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
	}
}