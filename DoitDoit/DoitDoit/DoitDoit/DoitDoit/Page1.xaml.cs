using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(220)
            {
               /*Color : 영역색상
                *Label : 정보
                *TextColor : 글자색
                *ValueLabel : 글자밑수치
                * 
                * 모든값은 없을수있음.
                * 하나의 값만 있을수있음.
                * Color사용시, SKColor.parse()를 이용하는거같음.
                * Entry(A) : A가 차트에서 나타낼 영역 수치
                * 
                * 아래는 1~10개의 테스트 케이스.
                * 
                 */

                Color=SKColor.Parse("#333333"),
                Label="1",
                ValueLabel="220"
            },
            new Entry(500)
            {
                 Color=SKColor.Parse("#777777"),
                
                ValueLabel="500"
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
                
                ValueLabel="200"
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
               
                ValueLabel="200"
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
               
                ValueLabel="200"
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
                Label="6",
                
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
                Label="7",
               
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
                Label="8",
                
            },
            new Entry(200)
            {
                Color=SKColor.Parse("#999999"),
                Label="9",
               
            },
            new Entry(200)
            {
                 Color=SKColor.Parse("#999999"),
                
            }
        };
        DonutChart dc;
        public Page1()
        {
            InitializeComponent();

            /* Entries = entries는 Chart선언시 꼭 필요
               글자크기가 개짝음. 
               수정방법 : LabelTextSize=Float수;
               차트자체의 크기는 XAML에서 건드려줘야함
             */
            dc= new DonutChart() 
            { 
                LabelTextSize = 40f,
                Entries = entries 
            };
        }

        /*
         Appearing안하면, 한번 로드후, Xaml수정시 다시 안나옴.
         Appearing으로 dc와 chart1 연결.
             */       
        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            chart1.Chart = dc;
        }
    }
}