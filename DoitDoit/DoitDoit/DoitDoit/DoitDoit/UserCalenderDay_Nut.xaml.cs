using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts;
using Microcharts.Forms;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay_Nut : ContentPage
    {
        public Models.FoodViewModel[] Menus { get; set; }

        public UserCalenderDay_Nut()
        {
            InitializeComponent();

            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();

            entries.Add(new Microcharts.Entry(100) {  });
            entries.Add(new Microcharts.Entry(50) {  });

            var chart = new BarChart() { Entries = entries };

            this.CharView.Chart = chart;
            this.CharView.HeightRequest = 300;
        }
    }
}