using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoitDoit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Post_com : ContentPage
    {
        private Models.Post postdata = null;

        public Models.Post PostData {
            get => this.postdata;
            set {
                this.postdata = value;
                this.OnPropertyChanged(nameof(this.PostData));
            }
        }

        public Post_com()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 댓글 페이지 로드 되었을 때의 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPage_Appearing(object sender, EventArgs e) {
            this.SetMenuDay();
            BindableLayout.SetItemsSource(this.CommentStackLayout, this.PostData.Comments);
        }

        private void SetMenuDay()
        {
            if (this.PostData is null) return;

            //밥줌님의 2020년 5월 4일 식단
            Label label = this.PostMenuDay;
            string day = "";
            if (this.PostData.Menus.Count > 0)
            {
                string date = this.PostData.Menus.First().Code;
                string year = date.Substring(0, 4);
                string month = date.Substring(4, 2);
                string tday = date.Substring(6, 2);

                day = $"{year}년 {month}월 {tday}일 ";
            }
            string context = $"{this.PostData.UserID}님의 {day}식단";

            label.Text = context;
        }

        /// <summary>
        /// 작성 버튼 눌렀을 때의 이벤트 처리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e) {
            Models.Comment comment = new Models.Comment();
            comment.ID = Models.UserModel.GetInstance.Id;
            comment.Date = DateTime.Now;
            comment.PostID = this.PostData.Code;
            comment.Context = this.CommentWrite.Text;

            bool accept = await DisplayAlert("안내", "댓글을 등록하시겠습니까?", "OK", "Cancel");

            if (accept) {
                Network.FirebaseServer server = Network.FirebaseServer.Server;
                string resultjson = await server.FirebaseRequest("AddCommentData", comment);

                Network.Packet packet = Newtonsoft.Json.JsonConvert.DeserializeObject<Network.Packet>(resultjson);

                if (!(packet is null) && packet.Result) {
                    comment.Code = packet.Context;
                    this.PostData.Comments.Add(comment);
                    this.CommentWrite.Text = "";
                }
            }
        } // END OF Button_Clicked EVENT LISTENER


        /// <summary>
        /// 덧글 삭제버튼 누를시 기능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DelPost_Clicked(object sender, EventArgs e) {
            if (sender is Button btn) {
                if (btn.BindingContext is Models.Comment comment) {
                    bool result = await DisplayAlert("안내", "댓글을 삭제하시겠습니까?", "OK", "Cancel");

                    if (result) {
                        bool qresult = await Network.FirebaseServer.Server.DeleteCommentData(comment.Code, this.PostData.Code);

                        if (qresult) {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                                this.PostData.Comments.Remove(comment);
                            });
                        }
                    }
                }
            }
        } //

        private void CommentStackLayout_ChildAdded(object sender, ElementEventArgs e) {
            Button btn = e.Element.FindByName<Button>("DelPost");

            if (btn is null) return;

            if (btn.BindingContext is DoitDoit.Models.Comment comment) {
                btn.IsVisible = (Models.UserModel.GetInstance.Id == comment.ID);
            }
        }
    }
}