using DoitDoit.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

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
    }
}
