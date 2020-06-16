using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DoitDoit.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts;
using SkiaSharp;
using SkiaSharp.Views;
using System.ComponentModel;
using DoitDoit.ExMethod;
using System.Collections.Specialized;

namespace DoitDoit
{
    /// <summary>
    /// 메인 화면
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
        private UserModel usermodel = UserModel.GetInstance;

        private double monthtotal = 0;
        private double weektotal = 0;

        /// <summary>
        /// 서버에 있는 공유 식단 게시글 리스트를 가져옵니다.
        /// </summary>
        private async void GetPostData() {
            if (UserModel.GetInstance.Posts != null && UserModel.GetInstance.Posts.Any()) {
                return;
            }

            Network.FirebaseServer server = Network.FirebaseServer.Server;

            Models.Post[] posts = await server.GetPostData();

            // UI 스레드에서 실행
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                var ps = (from p in posts
                          where !UserModel.GetInstance.Posts.Any(post => post.Code == p.Code)
                          select p);

                foreach (Models.Post p in ps) {
                    UserModel.GetInstance.Posts.Add(p);
                }

                UserModel.GetInstance.SortPosts();
            });
        }

        public Main ()
		{
            InitializeComponent ();

            // 게시글 리스트 비동기로 가져옴
            Task.Run(this.GetPostData);

            UserModel.GetInstance.PropertyChanged += this.PostsChanged;
            /*예시로 추가한 코드입니다.*/
            DisplayAlert("하이", UserModel.GetInstance.AvgHeight.ToString(),"ㅁㅁ");
        }

        ~Main() {
            UserModel.GetInstance.PropertyChanged -= this.PostsChanged;
        }

        private Microcharts.LineChart CreateChart(int term, int mode) {
            if (mode > 2 || mode < 0) mode = 0;

            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();

            DateTime now = DateTime.Now;

            for (int i = 0; i < term; i++) {
                string label = "";

                DateTime date;
                switch (mode) {
                    case 0:
                        date = now.AddDays(-i);
                        label = date.ToString("MM/dd");
                        break;
                    case 1:
                        date = now.AddMonths(-i);
                        label = date.ToString("yyyy/MM");
                        break;
                    case 2:
                        date = now.AddYears(-i);
                        label = date.ToString("yyyy");
                        break;
                    default:
                        date = DateTime.Now;
                        break;
                }

                var menus = UserModel.GetInstance.GetMenuGroup(date, mode);

                double calsum = Math.Floor(menus.GetMenusSum("N00001", out Microcharts.Entry entry));

                switch (mode) {
                    case 0:
                        this.weektotal += calsum;
                        break;
                    case 1:
                        this.monthtotal += calsum;
                        break;
                    case 2:

                        break;
                }
                

                entry.Color = SKColor.Parse("#FF0000");
                entry.Label = label;
                entry.ValueLabel = calsum.ToString();

                entries.Add(entry);
            }

            entries.Reverse();

            LineChart linechart = new LineChart() {
                Entries = entries,
                LineMode = LineMode.Straight,
                LabelTextSize = 30,
            };

            return linechart;
        }

        #region 이벤트 처리기
        /*private void Reco_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Post());
        }*/

        private void RecoMenu_Clicked(Object sender, EventArgs e)
        {

        }

        private void Reco2_Clicked(Object sender, EventArgs e)
        {

        }

        private void RecoMenu2_Clicked(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 사이드 메뉴 클릭했을 때 이벤트 처리기
        /// 사이드 메뉴 화면으로 넘어감
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SideMenu_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new SideMenu());
        }

        /// <summary>
        /// 메인 페이지 전부 로드 되었을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContentPage_Appearing(object sender, EventArgs e) {
            this.RefreshPosts();

            this.ChartView.Chart = this.CreateChart(5, 1);
            this.ChartView.HeightRequest = 300;

            this.WeekChartView.Chart = this.CreateChart(7, 0);
            this.WeekChartView.HeightRequest = 300;

            this.TotalKcal.Text = $"일주일간 총 {this.weektotal}Kcal";
        }

        /// <summary>
        /// 추천 게시글 버튼 클릭했을 때 이벤트 처리기
        /// 추천 식단 리스트 화면으로 넘어감
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToRecoListButton_Clicked(object sender, EventArgs e) {
            RecoList recolist = new RecoList();
            Navigation.PushModalAsync(recolist);
        }

        private void PostButton_Clicked(object sender, EventArgs e) {
            if (!(sender is Button button)) return;
            if (!(button.BindingContext is Models.Post postdata)) return;

            RecoList_Post post = new RecoList_Post();
            post.PostData = postdata;

            Navigation.PushModalAsync(post);
        }

        private void PostsChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName is "Posts") {
                this.RefreshPosts();
                UserModel.GetInstance.Posts.CollectionChanged += this.PostsItemChanged;
            }
        }

        private void PostsItemChanged(object sender, NotifyCollectionChangedEventArgs e) {
            this.RefreshPosts();
        }

        private void RefreshPosts() {
            if (UserModel.GetInstance.Posts is null || !UserModel.GetInstance.Posts.Any()) return;

            var list = (from post in UserModel.GetInstance.Posts
                        orderby post.Date descending
                        select post).Take(4);

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                BindableLayout.SetItemsSource(this.PostList, list);
            });
        }
        #endregion

        private void WeekButton_Clicked(object sender, EventArgs e) {
            this.MonthButton.IsEnabled = true;
            this.WeekButton.IsEnabled = false;

            this.WeekChartView.IsVisible = true;
            this.ChartView.IsVisible = false;

            this.TotalKcal.Text = $"일주일간 총 {this.weektotal}Kcal";
        }

        private void MonthButton_Clicked(object sender, EventArgs e) {
            this.MonthButton.IsEnabled = false;
            this.WeekButton.IsEnabled = true;

            this.WeekChartView.IsVisible = false;
            this.ChartView.IsVisible = true;

            this.TotalKcal.Text = $"5개월간 총 {this.monthtotal}Kcal";
        }
    } // END Of Main CLASS
}