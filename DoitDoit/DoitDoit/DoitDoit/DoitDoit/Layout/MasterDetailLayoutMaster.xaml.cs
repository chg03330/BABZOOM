using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit.Layout {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailLayoutMaster : ContentPage {
        public ListView ListView;

        public MasterDetailLayoutMaster() {
            InitializeComponent();

            BindingContext = new MasterDetailLayoutMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailLayoutMasterViewModel : INotifyPropertyChanged {
            public ObservableCollection<MasterDetailLayoutMasterMenuItem> MenuItems { get; set; }

            public MasterDetailLayoutMasterViewModel() {
                MenuItems = new ObservableCollection<MasterDetailLayoutMasterMenuItem>(new[]
                {
                    new MasterDetailLayoutMasterMenuItem { Id = 0, Title = "메인", TargetType = typeof(DoitDoit.Main) },
                    new MasterDetailLayoutMasterMenuItem { Id = 1, Title = "정보수정", TargetType = typeof(DoitDoit.InfoEntry) },
                    new MasterDetailLayoutMasterMenuItem { Id = 2, Title = "달력", TargetType = typeof(DoitDoit.Calender) },
                    new MasterDetailLayoutMasterMenuItem { Id = 3, Title = "식단작성", TargetType = typeof(DoitDoit.UserCalenderDay) },
                    new MasterDetailLayoutMasterMenuItem { Id = 4, Title = "로그아웃" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "") {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}