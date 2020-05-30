using System;
using System.Collections.Generic;
using System.Text;

using Microcharts;

namespace DoitDoit.Models
{
    public class Food
    {
        public string Code { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }

        public FoodData Data { get; set; } = null;
    }

    public class FoodData
    {
        public string DB군 { get; set; } = "";
        public double 내용량 { get; set; } = 0;
        public string 내용량_단위 { get; set; } = "";
        public string 식품군명 { get; set; } = "";
        public string 식품명 { get; set; } = "";
        public string 식품코드 { get; set; } = "";

        public List<Nut> 영양소 { get; set; } = new List<Nut>();
    }

    public class Nut {
        public double Quantity { get; set; } = 0;
        public string Unit { get; set; } = "";
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
    }
}
