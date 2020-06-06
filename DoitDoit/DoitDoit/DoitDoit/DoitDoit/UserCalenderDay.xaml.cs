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
        
        DonutChart dc;

        //String nowTime;

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
            //nowTime = dateTime.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 공유 식단 작성 버튼 눌렀을 때 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ShareButton_Clicked(object sender, EventArgs e) {
            if (this.list.Count is 0) {
                DisplayAlert("안내", "식단을 입력해 주세요", "Cancel");

                return;
            }

            RecoList_Post post = new RecoList_Post();

            FoodViewModel menu = this.list.FirstOrDefault();

            var posts = (from p in UserModel.GetInstance.Posts
                           where p.Menus.Where(m => m.Code == menu.Code).Any()
                           select p);

            Models.Post postdata;

            if (posts.Any()) {
                postdata = posts.FirstOrDefault();
                post.ModifyMode = false;
            }
            else {
                Post npost =
                    await Network.FirebaseServer.Server.IsPostExist(UserModel.GetInstance.Id, menu.Code);

                if (npost is null) {
                    postdata = new Models.Post();

                    foreach (FoodViewModel m in this.list) {
                        postdata.Menus.Add(m);
                    }

                    post.ModifyMode = true;
                }
                else {
                    postdata = npost;

                    post.ModifyMode = false;
                }
            }

            post.PostData = postdata;
            post.PostData.UserID = Models.UserModel.GetInstance.Id;

            await Navigation.PushModalAsync(post);
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
            list.Clear();
            if (fvm.Count() == 0)
            {
                DisplayAlert("알림", "해당 날짜의 식단이 존재하지 않습니다.1", "확인"); 
                nutSL.IsVisible = false;
            }
            else
            {
                list = new ObservableCollection<FoodViewModel>();
                foreach (FoodViewModel f in fvm)
                {
                    list.Add(f);
                }
                nutSL.IsVisible = true;
                updateChart();
            }
            
            /*
            var fvm = usermodel.FoodViewModels.Where(model => {
                return model.Code.Contains(e.NewDate.ToString("yyyyMMdd"));
            });

            list.Clear();
            if (fvm.Count() == 0)
            {
                DisplayAlert("알림", "해당 날짜의 식단이 존재하지 않습니다.2", "확인");
                nutSL.IsVisible = false;
            }
            else
            {
                foreach (FoodViewModel f in fvm)
                {
                    list.Add(f);
                }
                nutSL.IsVisible = true;
            }*/

            BindableLayout.SetItemsSource(this.liststack, list);


        }
        private void updateChart() {
            List<Entry> entries = new List<Entry>();
            calNut(entries);                                                                //칼로리계산
            //dc.Entries = null;
            dc = new DonutChart() { Entries = entries, LabelTextSize = 40f };             //차트생성
            Chart1.Chart = dc;                                                      //차트 바인딩
        }

        #region calNut()함수 / 칼로리계산, List<Entry>에 Entry개체추가
        private void calNut(List<Entry> entries) {
            var thisdaymenus = usermodel.GetMenuGroup(dateTime, 0, out Entry[] nutentries);
            for (int i = 0; i < 4; i++) {
                if (!(i is 0)) {
                    entries.Add(nutentries[i]);
                }
            }

            laCal.Text = nutentries[0].Value.ToString();
            laTan.Text = nutentries[1].Value.ToString();
            laJi.Text = nutentries[2].Value.ToString();
            laDan.Text = nutentries[3].Value.ToString();
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
            /*
            var fvm = usermodel.FoodViewModels.Where(model => {
                return model.Code.Contains(e.NewDate.ToString("yyyyMMdd"));
            });

            list.Clear();
            if (fvm.Count() == 0) {
                DisplayAlert("알림", "해당 날짜의 식단이 존재하지 않습니다.2", "확인");
                nutSL.IsVisible = false;
            }
            else {
                foreach (FoodViewModel f in fvm) {
                    //f.Code = f.Code.Substring(0, 8);
                    list.Add(f);
                }
                nutSL.IsVisible = true;
            }
            */
            this.ContentPage_Appearing(sender,(EventArgs)e);
        }



        /// <summary>
        /// 누를경우 영양소 상세 소개 페이지
        /// </summary>
        /// <param name="sender">상세버튼</param>
        /// <param name="e"></param>
        private void NutButton_Clicked(object sender, EventArgs e) {
            UserCalenderDay_Nut nut = new UserCalenderDay_Nut();
            if (!(this.list is null)) {
                nut.Menus = this.list.ToArray();
            }
            Navigation.PushModalAsync(nut);
        }
    } // END OF UserCalenderDay CLASS

}