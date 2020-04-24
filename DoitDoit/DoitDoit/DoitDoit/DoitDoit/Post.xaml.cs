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

	//게시글 화면
	public partial class Post : ContentPage
	{
		public Post ()
		{
			InitializeComponent ();
		}
	}
}