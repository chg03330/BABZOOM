using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using Microcharts.Forms;
using Entry = Microcharts.Entry;
using DoitDoit.ExMethod;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay_Nut : ContentPage
    {
        List<Entry> entries;
        List<nutClass> nutList;
        public Models.FoodViewModel[] Menus { get; set; }

        public UserCalenderDay_Nut()
        {
            InitializeComponent();
            entries = new List<Entry>();
            nutList = new List<nutClass>();

            /*
             * entries.Add(new Entry(100) {  });
            entries.Add(new Entry(50) {  });

            var chart = new BarChart() { Entries = entries };

            this.CharView.Chart = chart;
            this.CharView.HeightRequest = 300;*/
        }

        public class nutClass
        {
            public String nutName { set; get; }
            public double nutQ { set; get; }
            public String nutUnit { set; get; }
            public nutClass() { 
            }
        }

        public nutClass GetNutName(nutClass nC,int a) {
            nutClass rnC = nC;
            String name="";
            String unit = "";
            switch (a)
            {
                case 1:
                    name = "에너지";
                    unit = "Kcal";
                    break;
                case 2:
                    name = "탄수화물";
                    unit = "g";
                    break;
                case 3:
                    name = "지방";
                    unit = "g";
                    break;
                case 4:
                    name = "단백질";
                    unit = "g";
                    break;
                case 5:
                    name = "비타민C";
                    unit = "mg";
                    break;
                case 6:
                    name = "비타민B6";
                    unit = "mg";
                    break;
                case 7:
                    name = "비타민B12";
                    unit = "㎍";
                    break;
                case 8:
                    name = "비타민A";
                    unit = "㎍RAE";
                    break;
                case 9:
                    name = "비타민D";
                    unit = "㎍";
                    break;
                case 10:
                    name = "비타민E";
                    unit = "㎎α-TE";
                    break;
                case 11:
                    name = "비타민K";
                    unit = "㎍";
                    break;
                case 12:
                    name = "칼슘";
                    unit = "mg";
                    break;
                case 13:
                    name = "인";
                    unit = "mg";
                    break;
                case 14:
                    name = "나트륨";
                    unit = "g";
                    break;
                case 15:
                    name = "염소";
                    unit = "g";
                    break;
                case 16:
                    name = "칼륨";
                    unit = "g";
                    break;
                case 17:
                    name = "마그네슘";
                    unit = "mg";
                    break;
                case 18:
                    name = "철";
                    unit = "mg";
                    break;
                case 19:
                    name = "아연";
                    unit = "mg";
                    break;
                case 20:
                    name = "구리";
                    unit = "㎍";
                    break;
                case 21:
                    name = "불소";
                    unit = "mg";
                    break;
                case 22:
                    name = "망간";
                    unit = "mg";
                    break;
                case 23:
                    name = "요오드";
                    unit = "㎍";
                    break;
                case 24:
                    name = "셀레늄";
                    unit = "㎍";
                    break;
            }
            rnC.nutName = name;
            rnC.nutUnit = unit;
            return rnC;
        }
        public void nutAdd(int a) 
        {
            nutClass nac = new nutClass();
            String str = "";
            if (a < 10) { str = "N0000"; } else { str = "N000"; }
            nac = GetNutName(nac, a);
            str += a.ToString();
            nac.nutQ = Menus.GetMenusSum(str);
            nutList.Add(nac);
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (Menus.Length == 0)
            {
                DisplayAlert("알림", "상세 표시할 영양소가 없습니다.", "확인");
            }
            else 
            {
                //List<Task> tasks = new List<Task>();

                //비동기식 nutAdd()실행
                for (int i = 1; i < 25; i++) 
                {
                    //Task task =
                    nutAdd(i);
                    //tasks.Add(task);
                }
                //Task.WaitAll(tasks.ToArray());

                BindableLayout.SetItemsSource(this.nutSL, nutList);
            }

        }
    }
}