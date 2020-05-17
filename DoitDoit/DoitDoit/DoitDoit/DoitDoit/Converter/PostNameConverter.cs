using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DoitDoit.Converter {
    class PostNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Models.Post post = value as Models.Post;
            if (post is null) return null;

            string day = "";
            if (post.Menus.Count > 0) {
                string date = post.Menus.First().Code;
                string year = date.Substring(0, 4);
                string month = date.Substring(4, 2);
                string tday = date.Substring(6, 2);

                day = $"{year}년 {month}월 {tday}일 ";
            }
            string context = $"{post.UserID}님의 {day}식단";

            return context;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
