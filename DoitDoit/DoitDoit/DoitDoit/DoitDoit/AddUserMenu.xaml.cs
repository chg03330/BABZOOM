using DoitDoit.Models;
using DoitDoit.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoitDoit.Models;
using DoitDoit.Network;
using System.Collections.ObjectModel;
//using Android.Database;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserMenu : ContentPage
    {
        public DateTime dateTime { get; set; }
        public ObservableCollection<FoodData> Foods { get; set; }
        public FoodData food;
        public String selectItem { get; set; }
        String selectTime;
        public AddUserMenu()
        {
            InitializeComponent();
            SearchText.Completed += searchText_Completed;
            addMenu_ClockTime.Time = DateTime.Now.TimeOfDay;
            Foods = new ObservableCollection<FoodData>();

            BindableLayout.SetItemsSource(this.addList, Foods);

        }
        private void ContentPage_Appearing(object sender, EventArgs e) {
            selectTime = dateTime.ToString("yyyyMMdd");
            this.addMenu_DayTime.Date = dateTime;
            SearchText.Text = "";
            DisplayAlert("확인", ""+Foods.Count(), "확인");
            
        }

        public async void addItem(String a) {
            FirebaseServer server = new FirebaseServer();
            food = await server.SpecificFood(a);
            Foods.Add(food);
        }

        public void searchText_Completed(object sender, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                SearchFood sf = new SearchFood();
                sf.text = SearchText.Text;
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
            int year = this.dateTime.Year;
            int month = this.dateTime.Month;
            int day = this.dateTime.Day;
            int hour = this.addMenu_ClockTime.Time.Hours;
            int minute = this.addMenu_ClockTime.Time.Minutes;

            DateTime date = new DateTime(year, month, day, hour, minute, 0);

            var foods = from fooddata in this.Foods
                        select new Food() { Code = fooddata.식품코드, Quantity = fooddata.내용량.ToString(),
                         Data = fooddata, Unit = fooddata.내용량_단위};

            FoodViewModel menu = new FoodViewModel {
                UserID = UserModel.GetInstance.Id,
                Foods = foods.ToArray(),
                Code = date.ToString("yyyyMMddhhmmss")
            };

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                UserModel.GetInstance.FoodViewModels.Add(menu);
            });

            FirebaseServer server = new FirebaseServer();
            server.FirebaseRequest("SetMenuData", menu);

            this.OnBackButtonPressed();
        }

        private void addMenu_DayTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //var date = e.NewDate;
            //DisplayAlert("선택한 날짜", date.ToString("yyyy년 MM월 dd일"), "OK");
        }
    }
}