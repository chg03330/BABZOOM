using System;
using System.Collections.Generic;
using System.Linq;
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

        public double GetNutQuantity(string nutcode) {
            Nut nut = this.영양소.Where(n => n.Code == nutcode).FirstOrDefault();
            if (nut is null) return 0;

            return nut.Quantity;
        }
    }

    public class Nut {
        public double Quantity { get; set; } = 0;
        public string Unit { get; set; } = "";
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";

        public static (string, string)[] NutInfo { get; private set; } = {
            ("에너지", "Kcal"), //ㅇ
            ("탄수화물", "g"), //ㅇ
            ("지방", "g"),
            ("단백질","g"), //ㅇ
            ("비타민C", "mg"), //ㅇ
            ("비타민B6", "mg"), //ㅇ
            ("비타민B12", "㎍"), //ㅇ
            ("비타민A", "㎍RAE"), // A보단 레티놀 ㎍ 값이 존재.0
            ("비타민D","㎍"), //ㅇ
            ("비타민E","㎎α-TE"), //ㅇ
            ("비타민K","㎍"), //ㅇ k1임
            ("칼슘","mg"), //ㅇ
            ("인","mg"), //ㅇ
            ("나트륨","mg"), //g->mg수정
            ("염소","g"),
            ("칼륨", "mg"), //g->mg
            ("마그네슘","mg"), //ㅇ
            ("철","mg"), //ㅇ
            ("아연","mg"), //ㅇ
            ("구리","mg"), // ㎍->mg
            ("불소","mg"),
            ("망간","mg"), //ㅇ
            ("요오드","㎍"), //ㅇ
            ("셀레늄","mg") // ㎍->mg
        };

        /// <summary>
        /// 영양소 코드를 받아서, 영양소 이름에 따른 단위값을 받음./
        /// 받은걸로 차트에 표시될 개체(Entry)생성 /
        /// 각 개체의 색은 랜덤 /
        /// 칼로리,탄수화물,단백질,지방 는 고유 색깔을 가짐
        /// </summary>
        /// <param name="nutcode">영양소 코드</param>
        /// <param name="val">영양소 량</param>
        /// <returns></returns>
        public static Microcharts.Entry GetEntry(string nutcode, double val) {
            Random rnd = new Random();

            string code = nutcode.TrimStart('N');
            int codenum = Convert.ToInt32(code);

            (string, string) nameunit = Nut.NutInfo[codenum - 1];

            Entry result = new Entry(Convert.ToSingle(val));
            if (code.Equals("00001"))
            {
                result.Color = SkiaSharp.SKColor.Parse("#D358F7");
            }
            else if (code.Equals("00002"))
            {
                result.Color = SkiaSharp.SKColor.Parse("#5858FA");
            }
            else if (code.Equals("00003"))
            {
                result.Color = SkiaSharp.SKColor.Parse("#81DAF5");
            }
            else if (code.Equals("00004"))
            {
                result.Color = SkiaSharp.SKColor.Parse("#FE2E64");
            }
            else
            {
                result.Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
            }
            result.Label = $"{nameunit.Item1}({nameunit.Item2})";
            
            return result;
        }
    } // END OF Nut CLASS
}
