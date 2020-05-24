using DoitDoit.ExMethod;
using DoitDoit.Network;
using Microcharts;
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
    public partial class RecoList_Post : ContentPage
    {
        private Models.Post postdata = null;
        private bool modifymode = false;

        public Models.Post PostData {
            get => this.postdata;
            set {
                this.postdata = value;
                this.OnPropertyChanged(nameof(this.PostData));
            }
        }
        public bool ModifyMode {
            get => this.modifymode;
            set {
                this.modifymode = value;
                this.OnPropertyChanged(nameof(this.ModifyMode));
            }
        }

        #region 영양소 property
        public Double Kcal { get; set; }
        public Double Tan { get; set; }
        public Double Dan { get; set; }
        public Double Ji { get; set; }
        #endregion
        DonutChart dc;
        Models.UserModel usermodel;

        private DateTime datetime = DateTime.Now;
        public DateTime dateTime {
            get => this.datetime;
            set {
                this.datetime = value;
                this.OnPropertyChanged(nameof(this.dateTime));
            }
        }

        public RecoList_Post()
        {
            InitializeComponent();
            this.usermodel = Models.UserModel.GetInstance;
        }

        /// <summary>
        /// 공유 식단 게시글이 로딩 완료 되었을 때 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPage_Appearing(object sender, EventArgs e) {
            this.SetMenuDay();
            Xamarin.Forms.BindableLayout.SetItemsSource(this.PostMenuList, this.PostData.Menus);

            #region ModifyMode예외처리
            this.BtnOk.IsVisible = this.ModifyMode;
            this.PostContextLabel.IsVisible = !this.ModifyMode;
            this.PostContextEditor.IsVisible = this.ModifyMode;
            this.BtnComment.IsVisible = !this.ModifyMode;
            #endregion

            updateChart();
        }

        private void SetMenuDay() {
            if (this.PostData is null) return;

            //밥줌님의 2020년 5월 4일 식단
            Label label = this.PostMenuDay;

            string day = "";
            if (this.PostData.Menus.Count > 0) {
                string date = this.PostData.Menus.First().Code;
                string year = date.Substring(0, 4);
                string month = date.Substring(4, 2);
                string tday = date.Substring(6, 2);

                day = $"{year}년 {month}월 {tday}일 ";
            }
            string context = $"{this.PostData.UserID}님의 {day}식단";

            label.Text = context;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            this.OnBackButtonPressed();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            postdata.Date = DateTime.Now;
            this.PostData.Context = this.PostContextEditor.Text;
            
            bool accept = await DisplayAlert("안내", "됐을까요?", "OK", "Cancel");

            if (accept) {
                FirebaseServer server = FirebaseServer.Server;
                string packetjson = await server.FirebaseRequest("SetPostData", postdata);
                Packet packet = Newtonsoft.Json.JsonConvert.DeserializeObject<Packet>(packetjson);

                if (packet.Result) {
                    this.PostData.Code = packet.Context;
                    Models.UserModel.GetInstance.Posts.Add(this.PostData);
                }

                this.OnBackButtonPressed();
            }
        } // END OF Ok_clicked EVENT LISTENER

        /// <summary>
        /// 댓글 버튼이 눌렸을 때의 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentButton_Clicked(object sender, EventArgs e) {
            Post_com comment = new Post_com {
                PostData = this.PostData
            };

            Navigation.PushModalAsync(comment);
        } // END OF CommentButton_Clicked EVENT LISTENER

        private void NutButton_Clicked(object sender, EventArgs e) {

        }

        private void updateChart() {
            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
            calNut(entries);                                                                //칼로리계산
            dc = new DonutChart() { Entries = entries, LabelTextSize = 40f };             //차트생성
            Chart1.Chart = dc;                                                      //차트 바인딩
        }

        #region calNut()함수 / 칼로리계산, List<Entry>에 Entry개체추가
        private void calNut(List<Microcharts.Entry> entries) {
            Random rnd = new Random();
            var thisdaymenus = this.PostData.Menus;
            String a;
            for (int i = 1; i < 5; i++) {
                a = $"N0000{i}";

                String b;
                switch (a) {
                    case "N00001":
                        b = "칼로리";
                        Kcal = thisdaymenus.GetMenusSum(a);
                        laCal.Text = Kcal.ToString() + " Kcal";
                        break;
                    case "N00002":
                        b = "탄수화물";
                        Tan = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Microcharts.Entry((float)Tan) {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laTan.Text = Tan.ToString() + " g";
                        break;
                    case "N00003":
                        b = "지방";
                        Ji = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Microcharts.Entry((float)Ji) {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laJi.Text = Ji.ToString() + " g";
                        break;
                    case "N00004":
                        b = "단백질";
                        Dan = thisdaymenus.GetMenusSum(a);
                        entries.Add(new Microcharts.Entry((float)Dan) {
                            Label = b,
                            Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)))
                        });
                        laDan.Text = Dan.ToString() + " g";
                        break;
                }
            }
            Kcal = Kcal - ((Dan * 4.1) + (Ji * 9.3) + (Tan * 4.1));
            entries.Add(new Microcharts.Entry((float)Kcal) { Label = "칼로리", Color = SkiaSharp.SKColor.Parse("#999999") });
        }
        #endregion
    }
}