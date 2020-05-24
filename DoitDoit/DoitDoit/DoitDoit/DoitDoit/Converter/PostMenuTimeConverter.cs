using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace DoitDoit.Converter {
    class PostMenuTimeConverter : IValueConverter {
        public bool Type { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string time = value as string;
            if (String.IsNullOrWhiteSpace(time)) return "";

            int hour = 0;
            try {
                hour = System.Convert.ToInt32(time.Substring(8, 2));
            }
            catch(Exception) {}

            string minute = "0";
            try {
                minute = time.Substring(10, 2);
            }
            catch (Exception) { }
            
            string noon = "오전";
            
            if (hour > 12) {
                hour -= 12;
                noon = "오후";
            }

            string result = $"{noon} {hour}시 {minute}분";

            if (this.Type) result += " 식단";

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return new DateTime();
        }
    }
}
