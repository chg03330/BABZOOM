using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit.Models
{
    public class Food
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }

        public FoodData Data { get; set; } = null;
    }

    public class FoodData
    {
        public string 식품명 { get; set; }
        public String DB군 { get; set; }
        public String 식품코드 { get; set; }
    }
}
