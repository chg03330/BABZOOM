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

        private async void ContentPage_Appearing(object sender, EventArgs e) {
            UserModel usermodel = UserModel.GetInstance;

            this.UserCalender.Events = new EventCollection() {};

            var menus = usermodel.FoodViewModels.GroupBy(
                menu => {
                    int year = Convert.ToInt32(menu.Code.Substring(0, 4));
                    int month = Convert.ToInt32(menu.Code.Substring(4, 2));
                    int day = Convert.ToInt32(menu.Code.Substring(6, 2));

                    DateTime date = new DateTime(year, month, day);

                    return date;
                });

            foreach (IGrouping<DateTime, FoodViewModel> group in menus) {
                this.UserCalender.Events.Add(group.Key, group.ToList());
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e) {

        }

        private void UserCalender_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            Xamarin.Plugin.Calendar.Controls.Calendar cal = sender as Xamarin.Plugin.Calendar.Controls.Calendar;
            if (cal is null || e is null || e.PropertyName is null) return;

            if (e.PropertyName is "SelectedDate") {
                //DisplayAlert("a", cal.SelectedDate.ToString(), "Close");
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                    UserCalenderDay dayform = new UserCalenderDay();
                    dayform.dateTime = cal.SelectedDate;
                    Navigation.PushModalAsync(dayform);
                });
            }
        }
    }
}