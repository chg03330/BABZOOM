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
	public partial class InfoEntry : ContentPage
	{
		public InfoEntry ()
		{
			InitializeComponent ();
		}

        private void OK_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Main());
        }
	}
}