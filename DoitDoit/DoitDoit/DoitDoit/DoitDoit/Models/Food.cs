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

        public static (string, string)[] NutInfo { get; private set; } = {
            ("에너지", "Kcal"),
            ("탄수화물", "g"),
            ("지방", "g"),
            ("단백질","g"),
            ("비타민C", "mg"),
            ("비타민B6", "mg"),
            ("비타민B12", "㎍"),
            ("비타민A", "㎍RAE"),
            ("비타민D","㎍"),
            ("비타민E","㎎α-TE"),
            ("비타민K","㎍"),
            ("칼슘","mg"),
            ("인","mg"),
            ("나트륨","g"),
            ("염소","g"),
            ("칼륨", "g"),
            ("마그네슘","mg"),
            ("철","mg"),
            ("아연","mg"),
            ("구리","㎍"),
            ("불소","mg"),
            ("망간","mg"),
            ("요오드","㎍"),
            ("셀레늄","㎍")
        };

        public static Microcharts.Entry GetEntry(string nutcode, double val) {
            Random rnd = new Random();

            string code = nutcode.TrimStart('N');
            int codenum = Convert.ToInt32(code);

            (string, string) nameunit = Nut.NutInfo[codenum - 1];

            Entry result = new Entry(Convert.ToSingle(val));
            result.Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
            result.Label = $"{nameunit.Item1}({nameunit.Item2})";
            
            return result;
        }
    } // END OF Nut CLASS
}
