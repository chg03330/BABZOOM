using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoitDoit.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoitDoit
{
    public class ButtonWithTag : Button
    {
        public object Tag
        {
            get { return (object)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public static readonly BindableProperty TagProperty =
            BindableProperty.Create(nameof(Tag), typeof(object), typeof(ButtonWithTag), null);
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay : ContentPage, INotifyPropertyChanged
    {
        ObservableCollection<FoodViewModel> list = new ObservableCollection<FoodViewModel>();
        UserModel usermodel = UserModel.GetInstance;
        DateTime datetime = DateTime.Now;
        public DateTime dateTime {
            get => this.datetime;
            set {
                this.datetime = value;
                this.OnPropertyChanged(nameof(this.dateTime));
            }
        }
        String nowTime;


        public UserCalenderDay()
        {
            InitializeComponent();
            this.CalenderDayTime.BindingContext = this;
            nowTime = dateTime.ToString("yyyyMMdd");
        }

        
        private void addUserMenu_Clicked(object sender, EventArgs e)
        {
            AddUserMenu ad = new AddUserMenu();
            ad.dateTime = this.dateTime;
            Navigation.PushModalAsync(ad);
        }


        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            var fvm = usermodel.FoodViewModels.Where(model => {
                return model.Code.Contains(this.dateTime.ToString("yyyyMMdd"));
            });

            list = new ObservableCollection<FoodViewModel>();
            foreach (FoodViewModel f in fvm) {
                f.Code = f.Code.Substring(0, 8);
                list.Add(f);
            }

            BindableLayout.SetItemsSource(this.liststack, list);
        }

        /*
        private void Button_Clicked(object sender, EventArgs e)
        {
            ButtonWithTag button = sender as ButtonWithTag;
            Food a = button.BindingContext as Food;
            FoodData foodData = a.Data;
            FoodViewModel menu = button.Tag as FoodViewModel;

            DisplayAlert("확인용", "값:" + menu.Code, foodData.식품코드) ;
        }
        */
        private void CalenderDayTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.dateTime = e.NewDate;
            var fvm = usermodel.FoodViewModels.Where(model => {
                return model.Code.Contains(e.NewDate.ToString("yyyyMMdd"));
            });

            list.Clear();
            if (fvm.Count() == 0) {
                DisplayAlert("알림", "해당 날짜의 식단이 존재하지 않습니다.", "확인");
            }
            else {
                foreach (FoodViewModel f in fvm) {
                    f.Code = f.Code.Substring(0, 8);
                    list.Add(f);
                }
            }
        }
    }

}