using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoitDoit.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay : ContentPage
    {
        FoodViewModel viewModel = new FoodViewModel("a");
        ObservableCollection<MenuModel> list;


        public UserCalenderDay()
        {
            InitializeComponent();

            viewModel.Foods.Add(new Food() { Name = "1", Code = "1", Quantity = "1", Unit = "g" });

            
        }

        private void addUserMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddUserMenu());
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Food selectedItem = e.SelectedItem as Food;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Food tappedItem = e.Item as Food;
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            DoitDoit.Network.FirebaseServer firebase = new Network.FirebaseServer();
            Dictionary<string, string> req = new Dictionary<string, string>();
            req["ID"] = UserModel.GetInstance.Id;

            string result = await firebase.FirebaseRequest("GetMenuData", req);

            list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<MenuModel>>(result);

            //list = new ObservableCollection<MenuModel>();

            //listview.ItemsSource = list;//viewModel.Foods;
        }
    }
}