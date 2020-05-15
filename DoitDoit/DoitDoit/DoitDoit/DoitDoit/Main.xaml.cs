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

namespace DoitDoit
{
    /// <summary>
    /// 메인 화면
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
        private UserModel usermodel = UserModel.GetInstance;

        /// <summary>
        /// 서버에 있는 공유 식단 게시글 리스트를 가져옵니다.
        /// </summary>
        private async void GetPostData() {
            Network.FirebaseServer server = Network.FirebaseServer.Server;

            string result = await server.FirebaseRequest("GetPostData", new Dictionary<string, string>());

            // UI 스레드에서 실행
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                ObservableCollection<Models.Post> posts =
                JsonConvert.DeserializeObject<ObservableCollection<Models.Post>>(result);
                UserModel.GetInstance.Posts = posts;
            });
        }

        public Main ()
		{
            InitializeComponent ();

            // 게시글 리스트 비동기로 가져옴
            Task.Run(this.GetPostData);
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
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            
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
        #endregion
    } // END Of Main CLASS
}