﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DoitDoit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    // 메인화면
	public partial class Main : ContentPage
	{
        UserModel a = UserModel.GetInstance;

        public Main ()
		{
            InitializeComponent ();
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

        private void SideMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SideMenu());
        }
    }
}