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

        private double GetMenusSum(IEnumerable<FoodViewModel> menus, string NutCode) {
            var sum = menus.Sum(menu =>
            menu.Foods.Sum(food =>
            food.Data.영양소.Where(nut =>
            nut.Code == NutCode).First().Quantity));

            return sum;
        }

        private void ContentPage_Appearing(object sender, EventArgs e) {
            UserModel usermodel = UserModel.GetInstance;

            this.UserCalender.Events = new EventCollection() {};

            ///
            /// 싱글톤 내 usermodel 의 식단 정보를 년월일 (DateTime) struct로 그룹화
            ///
            var menus = usermodel.FoodViewModels.GroupBy(
                menu => {
                    int year = Convert.ToInt32(menu.Code.Substring(0, 4));      // 년
                    int month = Convert.ToInt32(menu.Code.Substring(4, 2));     // 월
                    int day = Convert.ToInt32(menu.Code.Substring(6, 2));       // 일

                    DateTime date = new DateTime(year, month, day);             // 데이트 객체 생성

                    return date;
                });

            foreach (IGrouping<DateTime, FoodViewModel> group in menus) {
                this.UserCalender.Events.Add(group.Key, group.ToList());
            }


            ////////////////////////////////////
            /// 현재 달을 가져와서 그 달에 포함된 식단 그룹화
            /// 그 식단 그룹 내에 있는 모든 음식의 에너지 합
            ///////////////////////////////////////
            DateTime now = DateTime.Now;

            var thismonthmenus = usermodel.GetMenuGroup(now.Month, 1);
            var mkcal = this.GetMenusSum(thismonthmenus, "N00001");
            this.MKcal.Text = mkcal.ToString();

            //////////////////////////////////////
            ///
            //////////////////////////////////////
            var thisdaymenus = usermodel.GetMenuGroup(now.Day, 0);
            var dkcal = this.GetMenusSum(thisdaymenus, "N00001");
            this.DKcal.Text = dkcal.ToString();
        }

        private void AddButton_Clicked(object sender, EventArgs e) {

        }

        /// <summary>
        /// 달력 내의 일자를 클릭하였을 때
        /// ( Calander 객체의 PropertyChanged 이벤트 -> SelectedDate 가 바뀌었을 때 동작 )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if (e.PropertyName is "Month") {

            }
        }
    } // END OF Calender CLASS
}