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
        public AddUserMenu AM { get; set; }
        string selectItem;
        public SearchFood() {
            InitializeComponent();
           
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)  {

            s_bar.Text = text;
            selectItem = "";

            #region 검색                   
            FirebaseServer server = new FirebaseServer();
            List<String> fd = new List<String>();
            fd = await server.SearchFood(text);

            lvFood.ItemsSource = fd;
            #endregion
        }

        private void Register_Clicked(object sender, EventArgs e) {
            if ("".Equals(selectItem))
                DisplayAlert("오류", "선택한 식품이 없습니다.", "확인");
            else
            {
                AM.addItem(selectItem);
                AM.selectItem = selectItem;
                OnBackButtonPressed();
            }
        }

        private void lvFood_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectItem = e.SelectedItem.ToString();
            DisplayAlert("a", selectItem, "a");
        }
    }
}