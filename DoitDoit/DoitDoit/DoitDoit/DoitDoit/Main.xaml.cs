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

using Microcharts.Forms;
using Microcharts;
using SkiaSharp;
using SkiaSharp.Views;
using System.ComponentModel;

namespace DoitDoit
{
    /// <summary>
    /// 메인 화면
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
        private UserModel usermodel = UserModel.GetInstance;

        private double GetMenusSum(IEnumerable<FoodViewModel> menus, string NutCode) {
            var sum = menus.Sum(menu =>
            menu.Foods.Sum(food =>
            food.Data.영양소.Where(nut =>
            nut.Code == NutCode).First().Quantity));

            return sum;
        }

        /// <summary>
        /// 서버에 있는 공유 식단 게시글 리스트를 가져옵니다.
        /// </summary>
        private async void GetPostData() {
            Network.FirebaseServer server = Network.FirebaseServer.Server;

            string result = await server.FirebaseRequest("GetPostData", new Dictionary<string, string>());

            // UI 스레드에서 실행
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                Models.Post[] posts =
                JsonConvert.DeserializeObject<Models.Post[]>(result);

                var ps = (from p in posts
                          where !UserModel.GetInstance.Posts.Any(post => post.Code == p.Code)
                          select p);

                foreach (Models.Post p in ps) UserModel.GetInstance.Posts.Add(p);

                UserModel.GetInstance.SortPosts();
            });
        }

        public Main ()
		{
            InitializeComponent ();

            // 게시글 리스트 비동기로 가져옴
            Task.Run(this.GetPostData);

            UserModel.GetInstance.PropertyChanged += this.PostsChanged;
        }

        ~Main() {
            UserModel.GetInstance.PropertyChanged -= this.PostsChanged;
        }

        #region 이벤트 처리기
        private void Reco_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Post());
        }

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
            Navigation.PushModalAsync(new SideMenu());
        }

        /// <summary>
        /// 메인 페이지 전부 로드 되었을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            SKTypeface.FromFamilyName("MalgunGothic");

            DateTime now = DateTime.Now;

            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();

            double total = 0;

            for (int i = 0; i < 5; i++) {
                DateTime date = now.AddMonths(-i);

                var menus = UserModel.GetInstance.GetMenuGroup(date, 1);
                double calsum = Math.Floor(this.GetMenusSum(menus, "N00001"));
                total += calsum;

                Microcharts.Entry entry = new Microcharts.Entry(Convert.ToSingle(calsum));
                entry.Color = SKColor.Parse("#FF0000");
                entry.Label = date.ToString("yyyy년 MM월");
                entry.ValueLabel = calsum.ToString();

                entries.Add(entry);
            }

            this.TotalKcal.Text = $"5개월간 총 {total}Kcal";

            entries.Reverse();

            this.ChartView.Chart = new Microcharts.LineChart() 
            { 
                Entries = entries, LineMode = LineMode.Straight, LabelTextSize = 30,
            };

            this.ChartView.HeightRequest = 300;
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
                var list = (from post in UserModel.GetInstance.Posts
                         orderby post.Date descending
                         select post).Take(4);

                BindableLayout.SetItemsSource(this.PostList, list);
            }
        }
        #endregion
    } // END Of Main CLASS
}