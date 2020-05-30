using DoitDoit.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using Microcharts;

namespace DoitDoit.ExMethod {
    static class MenuLINQ {
        public static double GetMenusSum(this IEnumerable<FoodData> foods, string NutCode) {
            var sum = foods.Sum(food => {
                Nut nut = food.영양소.Where(n => n.Code == NutCode).FirstOrDefault();
                if (nut is null) return 0;

                return nut.Quantity;
            });

            return sum;
        }

        public static double GetMenusSum(this IEnumerable<FoodData> foods, string NutCode, out Entry entry) {
            var sum = foods.GetMenusSum(NutCode);
            entry = Nut.GetEntry(NutCode, sum);
            return sum;
        }

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
            entry = Nut.GetEntry(NutCode, sum);
            return sum;
        }
    
        public static Entry[] GetEntry(this IEnumerable<FoodViewModel> menus) {
            List<Microcharts.Entry> resultentries = new List<Microcharts.Entry>();

            for (int i = 0; i < Nut.NutInfo.Length; i++) {
                string nutcode = "N000" + (i + 1).ToString().PadLeft(2, '0');
                ExMethod.MenuLINQ.GetMenusSum(menus, nutcode, out Microcharts.Entry entry);
                resultentries.Add(entry);
            }

            return resultentries.ToArray();
        }

        public static Entry[] GetEntry(this IEnumerable<FoodData> foods) {
            List<Microcharts.Entry> resultentries = new List<Microcharts.Entry>();

            for (int i = 0; i < Nut.NutInfo.Length; i++) {
                string nutcode = "N000" + (i + 1).ToString().PadLeft(2, '0');
                ExMethod.MenuLINQ.GetMenusSum(foods, nutcode, out Microcharts.Entry entry);
                resultentries.Add(entry);
            }

            return resultentries.ToArray();
        }
    }
}
