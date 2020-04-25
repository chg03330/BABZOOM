using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit.Models
{
    class Food
    {
        private string code, unit;
        private string quantity;

        public string Code { get => this.code; set { this.code = value; } }
        public string Unit { get => this.unit; set { this.unit = value; } }
        public string Quantity { get => this.quantity; set { this.quantity = value; } }

        public Food(String code, String quantity, String unit) {
            this.code = code;
            this.quantity = quantity;
            this.unit = unit;
        }
    }
}
