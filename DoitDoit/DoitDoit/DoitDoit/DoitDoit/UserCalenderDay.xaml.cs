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
        ObservableCollection<FoodViewModel> list;


        public UserCalenderDay()
        {
            InitializeComponent();
         
            
        }

        private void addUserMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddUserMenu());
        }


        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            DoitDoit.Network.FirebaseServer firebase = new Network.FirebaseServer();
            Dictionary<string, string> req = new Dictionary<string, string>();
            req["ID"] = UserModel.GetInstance.Id;

            string result = await firebase.FirebaseRequest("GetMenuData", req);

            list = new ObservableCollection<FoodViewModel>();

            list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<FoodViewModel>>(result);


            listview.ItemsSource = list;//viewModel.Foods;
            BindableLayout.SetItemsSource(this.liststack, list);
        }
    }
}