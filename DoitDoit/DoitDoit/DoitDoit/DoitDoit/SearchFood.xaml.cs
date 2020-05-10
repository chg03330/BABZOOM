using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using DoitDoit.Network;
using DoitDoit.Models;
using System.Collections.ObjectModel;

namespace DoitDoit {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchFood : ContentPage {
        public String text { get; set; }
        string selectItem;
        public SearchFood() {
            InitializeComponent();
           
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)  {

            s_bar.Text = text;

            #region 검색
            Dictionary<string, string> search = new Dictionary<string, string>();
            search["Search"] = text;            
            FirebaseServer server = new FirebaseServer();
            List<String> fd = new List<String>();
            fd = await server.SearchFood(text);
            
            lvFood.ItemsSource = fd;
            #endregion
        }

        private void Register_Clicked(object sender, EventArgs e) {
            Navigation.PushModalAsync(new MainPage());
        }

        private void lvFood_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectItem = e.SelectedItem.ToString();
            DisplayAlert("a", selectItem, "a");
        }
    }
}