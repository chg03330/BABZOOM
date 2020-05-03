using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DoitDoit.Models
{
    public class FoodViewModel
    {
        //private ObservableCollection<Food> foods = new ObservableCollection<Food>();
        //public ObservableCollection<Food> Foods
        //{
        //    get {
        //        return foods;
        //    }
        //    set {
        //        foods = value;
        //    }
        //}
        public String Code { get; set; } = ""; // 이게 날짜값 2020042004500이런거
        public String UserID { get; set; } = "";
        public Food[] Foods { get; set; } = null;

        public FoodViewModel() {
        }

    }
}
