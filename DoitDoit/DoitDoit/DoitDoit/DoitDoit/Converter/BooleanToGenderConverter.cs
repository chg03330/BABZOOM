using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DoitDoit.Converter {
    class BooleanToGenderConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool gender = false;
            string result = "";

            try {
                gender = System.Convert.ToBoolean(value);
            }
            catch(Exception) {}

            if (gender) {
                result = "남자";
            }
            else {
                result = "여자";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string gender = value as string;
            if (gender is null) return false;

            bool result = false;

            if (gender is "남자") {
                result = true;
            }
            else if (gender is "여자") {
                result = false;
            }

            return result;
        }
    }
}
