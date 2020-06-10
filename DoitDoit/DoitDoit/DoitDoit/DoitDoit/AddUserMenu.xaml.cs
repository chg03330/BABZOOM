using DoitDoit.Models;
using DoitDoit.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microcharts;
using DoitDoit.ExMethod;
//using Android.Database;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserMenu : ContentPage, INotifyPropertyChanged
    {
        public DateTime dateTime { get; set; }
        public ObservableCollection<FoodData> Foods { get; set; }
        public FoodData food;
        public String selectItem { get; set; }
        String selectTime;
        
        public FoodViewModel ModifyMenu { get; set; }
        
        public AddUserMenu()
        {
            InitializeComponent();
            this.Initialize();
        }

        public AddUserMenu(FoodViewModel menu) {
            InitializeComponent();
            this.Initialize();

            if (menu is null) return;

            this.ModifyMenu = menu;
            this.addMenu_DayTime.IsEnabled = false;
            this.addMenu_ClockTime.IsEnabled = false;
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                if (this.Foods is null) return;

                this.Foods.Clear();
                foreach (Food food in this.ModifyMenu.Foods) {
                    this.Foods.Add(food.Data);
                }
            });
        }

        public void Initialize() {
            SearchText.SearchButtonPressed += searchText_Completed;
            addMenu_ClockTime.Time = DateTime.Now.TimeOfDay;
            Foods = new ObservableCollection<FoodData>();

            BindableLayout.SetItemsSource(this.addList, Foods);
        }

        private void ContentPage_Appearing(object sender, EventArgs e) {
            selectTime = dateTime.ToString("yyyyMMdd");
            this.addMenu_DayTime.Date = dateTime;
            this.addMenu_ClockTime.Time = dateTime.TimeOfDay;
            SearchText.Text = "";
        }

        public async void addItem(String a) {
            FirebaseServer server = FirebaseServer.Server;
            food = await server.SpecificFood(a);
            Foods.Add(food);
        }

        public void searchText_Completed(object sender, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                SearchFood sf = new SearchFood();
                sf.text = SearchText.Text.Trim();
                sf.AM = this;
                await Navigation.PushModalAsync(sf);
            });
        }

   
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            this.OnBackButtonPressed();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            if (Foods.Count != 0) { 
                int year = this.dateTime.Year;
                int month = this.dateTime.Month;
                int day = this.dateTime.Day;
                int hour = this.addMenu_ClockTime.Time.Hours;
                int minute = this.addMenu_ClockTime.Time.Minutes;

                DateTime date = new DateTime(year, month, day, hour, minute, 0);

                var foods = from fooddata in this.Foods
                            select new Food()
                            {
                                Code = fooddata.식품코드,
                                Quantity = fooddata.내용량.ToString(),
                                Data = fooddata,
                                Unit = fooddata.내용량_단위
                            };

                FoodViewModel menu = null;
                
                if (this.ModifyMenu is null) {
                    menu = new FoodViewModel {
                        UserID = UserModel.GetInstance.Id,
                        Foods = foods.ToArray(),
                        Code = date.ToString("yyyyMMddHHmmss") + UserModel.GetInstance.Id
                    };

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        UserModel.GetInstance.FoodViewModels.Add(menu);
                    });
                }
                else {
                    this.ModifyMenu.Foods = foods.ToArray();
                    menu = this.ModifyMenu;
                }
                
                FirebaseServer server = FirebaseServer.Server;
                //server.FirebaseRequest("SetMenuData", menu);
                Task<bool> isok = server.SetMenuData(menu);

                this.OnBackButtonPressed();
            }
            else {
                DisplayAlert("안내", "등록한 식단이 없습니다.", "확인");
            }
        }

        private void addMenu_DayTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //var date = e.NewDate;
            //DisplayAlert("선택한 날짜", date.ToString("yyyy년 MM월 dd일"), "OK");
        }

        private void Del_Clicked(object sender, EventArgs e)
        {
            if (!(sender is ImageButton button)) return;
            if (!(button.BindingContext is FoodData foodData)) return;

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                this.Foods.Remove(foodData);
            });
        }

        //private void updateChart() {
        //    List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
        //    calNut(entries);                                                                //칼로리계산
        //    DonutChart dc = new DonutChart() { Entries = entries, LabelTextSize = 40f };             //차트생성
        //    Chart1.Chart = dc;                                                      //차트 바인딩
        //}

        //#region calNut()함수 / 칼로리계산, List<Entry>에 Entry개체추가
        //private void calNut(List<Microcharts.Entry> entries) {
        //    Microcharts.Entry[] nutentries = this.Foods.GetEntry();

        //    for (int i = 0; i < 4; i++) {
        //        if (!(i is 0)) {
        //            entries.Add(nutentries[i]);
        //        }
        //    }

        //    laCal.Text = nutentries[0].Value.ToString();
        //    laTan.Text = nutentries[1].Value.ToString();
        //    laJi.Text = nutentries[2].Value.ToString();
        //    laDan.Text = nutentries[3].Value.ToString();
        //}
        //#endregion
    }
}