using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserMenu : ContentPage
    {
        public DateTime dateTime { get; set; }
        String selectTime;
        public AddUserMenu()
        {
            InitializeComponent();
            SearchText.Completed += searchText_Completed;
            addMenu_ClockTime.Time = DateTime.Now.TimeOfDay;
           
        }
        private void ContentPage_Appearing(object sender, EventArgs e) {
            selectTime = dateTime.ToString("yyyyMMdd");
            this.addMenu_DayTime.Date = dateTime;
        }

        public void searchText_Completed(object sender, EventArgs e)
        {
            SearchText.Focus();
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