using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit.Layout {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailLayout : MasterDetailPage {
        private Main mainpage = null;

        public MasterDetailLayout(Page page) {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            if (page is Main mainpage) this.mainpage = mainpage;

            page.Title = "Detail";
            this.Detail = page;
        }

        public void ToMain() {
            if (this.mainpage is null) return;

            this.mainpage.Title = "Detail";
            this.Detail = mainpage;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            var item = e.SelectedItem as MasterDetailLayoutMasterMenuItem;
            if (item == null)
                return;

            Page page = null;

            if (item.TargetType is null) {
                return;
            }

            if (item.TargetType == typeof(DoitDoit.Main)) {
                page = new DoitDoit.Main();
            }
            else if (item.TargetType == typeof(DoitDoit.InfoEntry)) {
                page = (Page)Activator.CreateInstance(item.TargetType, this);
                await this.Navigation.PushModalAsync(page);
                return;
            }
            else {
                page = (Page)Activator.CreateInstance(item.TargetType);
            }

            if (page is null) return;

            page.Title = "Detail";
            Detail = page;
            
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}