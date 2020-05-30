using DoitDoit.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using Microcharts;

namespace DoitDoit.ExMethod {
    static class MenuLINQ {
        public static double GetMenusSum(this IEnumerable<FoodViewModel> menus, string NutCode) {
            var sum = menus.Sum(menu =>
            menu.Foods.Sum(food => {
                Nut nut = food.Data.영양소.Where(n => n.Code == NutCode).FirstOrDefault();
                if (nut is null) return 0;

                return nut.Quantity;
            }));

            return sum;
        }

        public static double GetMenusSum(this IEnumerable<FoodViewModel> menus, string NutCode, out Entry entry) {
            var sum = MenuLINQ.GetMenusSum(menus, NutCode);

            Random rnd = new Random();

            string name = "";
            string unit = "";

            string code = NutCode.TrimStart('L');
            int codenum = Convert.ToInt32(code);

            switch (codenum) {
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

            Entry result = new Entry(Convert.ToSingle(sum));
            result.Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
            result.Label = $"{name}({unit})";
            entry = result;

            return sum;
        }
    }
}
