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
using SkiaSharp;
using DoitDoit.Models;
using DoitDoit.Nutrition;
using System.ComponentModel;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCalenderDay_Nut : ContentPage
    {
        UserModel us;
        List<Entry> entries;            //MainChart의 Entry집합
        List<nutClass> nutList;
        nutBases baseNut;
        public Models.FoodViewModel[] Menus { get; set; }

        public UserCalenderDay_Nut()
        {
            InitializeComponent();
            us = UserModel.GetInstance;
            baseNut = new nutBases(us.Gender, us.Age);
            entries = new List<Entry>();
            nutList = new List<nutClass>();

            /*
            var chart = new BarChart() { Entries = entries, LabelTextSize = 30f};
            this.CharView.Chart = chart;
            */
            
        }

        public class nutClass
        {
            public String nutName { set; get; }
            public double nutQ { set; get; }
            public String nutUnit { set; get; }
            public Entry entry { set; get; }
            public SKColor Color { set; get; }
            public Chart nutChart { set; get; }
            public nutClass() { 
            }
        }

        public String addNutCode(int a) {
            if (a < 10)
                return "N0000" + a.ToString();
            else
                return "N000" + a.ToString();
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
        
        /*
            int a 는 참조값..
             */
        public nutClass SetNutChart(nutClass nC,int a) {

            nC.nutChart = new DonutChart() {
                Entries = new List<Entry>() {
                    new Entry(getHopIntake(a)-(float)nC.nutQ) { Label = "일일권장량", Color = SkiaSharp.SKColor.Parse("#999999") },
                    nC.entry
                },
                LabelTextSize = 35f,
                BackgroundColor = SkiaSharp.SKColors.Transparent, HoleRadius = 0.5f
            };
            return nC;
        }

        public float getHopIntake(int a) {
            float nutQ=0;
            switch (a)
            {
                case 1:
                    nutQ = baseNut.calorie.HOP_INTAKE;
                    break;
                case 2:
                    nutQ = baseNut.calorie.HOP_INTAKE / 3;
                    break;
                case 3:
                    nutQ = 50;
                    break;
                case 4:
                    nutQ = baseNut.protein.HOP_INTAKE;
                    break;
                case 5:
                    nutQ = baseNut.bitamin_c.HOP_INTAKE;
                    break;
                case 6:
                    nutQ = baseNut.bitamin_b6.HOP_INTAKE;
                    break;
                case 7:
                    nutQ = baseNut.bitamin_b12.HOP_INTAKE;
                    break;
                case 8:
                    nutQ = baseNut.bitamin_a.HOP_INTAKE;
                    break;
                case 9:
                    nutQ = baseNut.bitamin_d.HOP_INTAKE;
                    break;
                case 10:
                    nutQ = baseNut.bitamin_e.HOP_INTAKE;
                    break;
                case 11:
                    nutQ = baseNut.bitamin_k.HOP_INTAKE;
                    break;
                case 12:
                    nutQ = baseNut.calcium.HOP_INTAKE;
                    break;
                case 13:
                    nutQ = baseNut.phosphorus.HOP_INTAKE;
                    break;
                case 14:
                    nutQ = baseNut.natrium.HOP_INTAKE;
                    break;
                case 15:
                    nutQ = baseNut.chlorine.HOP_INTAKE;
                    break;
                case 16:
                    nutQ = baseNut.kalium.HOP_INTAKE;
                    break;
                case 17:
                    nutQ = baseNut.magnesium.HOP_INTAKE;
                    break;
                case 18:
                    nutQ = baseNut.iron.HOP_INTAKE;
                    break;
                case 19:
                    nutQ = baseNut.zinc.HOP_INTAKE;
                    break;
                case 20:
                    nutQ = baseNut.cooper.HOP_INTAKE;
                    break;
                case 21:
                    nutQ = baseNut.fluorine.HOP_INTAKE;
                    break;
                case 22:
                    nutQ = baseNut.manganese.HOP_INTAKE;
                    break;
                case 23:
                    nutQ = baseNut.iodine.HOP_INTAKE;
                    break;
                case 24:
                    nutQ = baseNut.selenium.HOP_INTAKE;
                    break;
            }
            return nutQ;
        }
        public nutClass addMainChartEntry(nutClass nC) {
            nC.entry = new Entry((float)nC.nutQ) { Label = nC.nutName, Color = nC.Color,ValueLabel=nC.nutUnit };
            entries.Add(nC.entry);
            return nC;
        }

        public void nutAdd(int a) 
        {
            Random rnd = new Random();      //색 랜덤
            nutClass nac = new nutClass();      //nutclass객체생성
            nac.Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
            String str = addNutCode(a);             //nut코드따기 ex)N00001
            nac = GetNutName(nac, a);               //nac의 이름,단위 따기            
            nac.nutQ = Menus.GetMenusSum(str);
            nac = addMainChartEntry(nac);
            nac = SetNutChart(nac,a);



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
                nutList.Clear();
                //비동기식 nutAdd()실행
                for (int i = 1; i < 25; i++) 
                {
                    //Task task =
                    nutAdd(i);
                    //tasks.Add(task);
                }
                //Task.WaitAll(tasks.ToArray());

                BindableLayout.SetItemsSource(this.nutSL, nutList);
                var chart = new BarChart() { Entries = entries, LabelTextSize = 30f };
                this.CharView.Chart = chart;
            }

        }
    }
}