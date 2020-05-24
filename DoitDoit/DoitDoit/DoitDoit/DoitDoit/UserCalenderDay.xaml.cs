using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoitDoit.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Entry = Microcharts.Entry;
using Microcharts;
using DoitDoit.ExMethod;

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
        
        #region 영양소 property
        public Double Kcal { get; set; }
        public Double Tan { get; set; }
        public Double Dan { get; set; }
        public Double Ji { get; set; }
        #endregion
        DonutChart dc;

        String nowTime;

        public DateTime dateTime {
            get => this.datetime;
            set {
                this.datetime = value;
                this.OnPropertyChanged(nameof(this.dateTime));
            }
        }
        
        public UserCalenderDay()
        {
            InitializeComponent();
            this.CalenderDayTime.BindingContext = this;
            nowTime = dateTime.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 공유 식단 작성 버튼 눌렀을 때 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShareButton_Clicked(object sender, EventArgs e) {
            RecoList_Post post = new RecoList_Post();

            Models.Post postdata = new Models.Post();

            foreach (FoodViewModel menu in this.list) {
                postdata.Menus.Add(menu);
            }

            post.PostData = postdata;
            post.ModifyMode = true;
            post.PostData.UserID = Models.UserModel.GetInstance.Id;
            Navigation.PushModalAsync(post);
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
                //f.Code = f.Code.Substring(0, 8);
                list.Add(f);
                //foreach (Food food in f.Foods) {
                //    FoodData data = await Network.FirebaseServer.Server.SpecificFood(food.Data.식품명);
                    
                //}
            }
            BindableLayout.SetItemsSource(this.liststack, list);

            updateChart();

        }
        private void updateChart() {
            List<Entry> entries = new List<Entry>();
            calNut(entries);                                                                //칼로리계산
            dc = new DonutChart() { Entries = entries, LabelTextSize = 40f };             //차트생성
            Chart1.Chart = dc;                                                      //차트 바인딩
        }

        #region calNut()함수 / 칼로리계산, List<Entry>에 Entry개체추가
        private void calNut(List<Entry> entries) {
            Random rnd = new Random();
            var thisdaymenus = usermodel.GetMenuGroup(dateTime, 0);
            String a;
            for (int i = 1; i < 5; i++)
            {
                a = $"N0000{i}";

                String b;
                switch (a)
                {
                    case "N00001":
                        b = "칼로리";
                        Kcal = thisdaymenus.GetMenusSum(a);
                        laCal.Text = Kcal.ToString() + " Kcal";
                        break;
                    case "N00002":
                        b = "탄수화물";
                        Tan = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Entry((float)Tan)
                        {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laTan.Text = Tan.ToString() + " g";
                        break;
                    case "N00003":
                        b = "지방";
                        Ji = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Entry((float)Ji)
                        {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laJi.Text = Ji.ToString() +" g";
                        break;
                    case "N00004":
                        b = "단백질";
                        Dan = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Entry((float)Dan)
                        {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laDan.Text = Dan.ToString() + " g";
                        break;
                }
            }
            Kcal = Kcal - ((Dan * 4.1) + (Ji * 9.3) + (Tan * 4.1));
            entries.Add(new Entry((float)Kcal) { Label="칼로리", Color = SkiaSharp.SKColor.Parse("#999999") });
        }
        #endregion

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
                nutSL.IsVisible = false;
            }
            else {
                foreach (FoodViewModel f in fvm) {
                    //f.Code = f.Code.Substring(0, 8);
                    list.Add(f);
                }
                nutSL.IsVisible = true;
            }
        }

        private void NutButton_Clicked(object sender, EventArgs e) {
            UserCalenderDay_Nut nut = new UserCalenderDay_Nut();
            if (!(this.list is null)) {
                nut.Menus = this.list.ToArray();
            }
            Navigation.PushModalAsync(nut);
        }
    } // END OF UserCalenderDay CLASS

}