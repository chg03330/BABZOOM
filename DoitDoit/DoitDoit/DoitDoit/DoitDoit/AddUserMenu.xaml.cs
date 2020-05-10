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
using Android.Database;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserMenu : ContentPage
    {
        public DateTime dateTime { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Food food;
        public String selectItem { get; set; }
        String selectTime;
        public AddUserMenu()
        {
            InitializeComponent();
            SearchText.Completed += searchText_Completed;
            addMenu_ClockTime.Time = DateTime.Now.TimeOfDay;
            Foods = new ObservableCollection<Food>();

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
            SearchFood sf = new SearchFood();
            sf.text = SearchText.Text;
            sf.AM = this;
            Navigation.PushModalAsync(sf);
        }

   
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            
        }

        private void addMenu_DayTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //var date = e.NewDate;
            //DisplayAlert("선택한 날짜", date.ToString("yyyy년 MM월 dd일"), "OK");
        }
    }
}