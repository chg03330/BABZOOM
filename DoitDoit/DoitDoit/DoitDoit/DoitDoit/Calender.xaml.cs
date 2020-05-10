using DoitDoit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Calender : ContentPage
    {
        public EventCollection Events { get; set; }

        public Calender()
        {
            InitializeComponent();

        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            UserModel a = UserModel.GetInstance;

            Events = new EventCollection();
            for (int i = 0; i < a.FoodViewModels.Count; i++) 
            {
                string b = String.Format("yyyyMMdd", a.FoodViewModels[i].Code.Substring(0, 8));
                Events.Add((DateTime.Parse(b)),a.FoodViewModels[i].Foods);
                
            }

            
            
        }              

            
        
    }
}