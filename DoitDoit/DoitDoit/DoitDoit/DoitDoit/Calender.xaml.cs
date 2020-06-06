﻿using DoitDoit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

using DoitDoit.ExMethod;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Calender : ContentPage
    {
        public EventCollection Events { get; set; }
        UserModel usermodel;
        DateTime now;

        public Calender()
        {
            InitializeComponent();
            now = DateTime.Now;
        }

        private void ContentPage_Appearing(object sender, EventArgs e) {
             usermodel = UserModel.GetInstance;

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

            SetMKcal(now);
            SetDKcal(now);
        }

        private void AddButton_Clicked(object sender, EventArgs e) {

        }

        /// <summary>
        /// 현재 달을 가져와서 그 달에 포함된 식단 그룹화
        /// 그 식단 그룹 내에 있는 모든 음식의 에너지 합
        /// </summary>
        /// <param name="now"></param>
        private void SetMKcal(DateTime now) { 
            var thismonthmenus = usermodel.GetMenuGroup(now, 1);
            var mkcal = thismonthmenus.GetMenusSum("N00001");
            this.MKcal.Text = mkcal.ToString();
        }
        /// <summary>
        ///날짜에 대한 식단을 가져와 에너지합을 구함
        /// </summary>
        /// <param name="now"></param>
        private void SetDKcal(DateTime now)
        {
            var thisdaymenus = usermodel.GetMenuGroup(now, 0);
            var dkcal = thisdaymenus.GetMenusSum("N00001");
            this.DKcal.Text = dkcal.ToString();
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
                    now = cal.SelectedDate;
                    Navigation.PushModalAsync(dayform);
                });
            }
            else if (e.PropertyName is "Month") {
                //DisplayAlert("a", cal.Month.ToString(), "Close");
                //DisplayAlert("b", cal.MonthYear.ToString(), "Close"); //캘린더의 설정된 year,month값을 가져옴.
                SetMKcal(cal.MonthYear);
            }
        }
    } // END OF Calender CLASS
}