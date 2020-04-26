using System;
using System.Collections.Generic;
using System.Text;

namespace DoitDoit.Models
{
    public class MenuModel
    {
        public String Code { get; set; } = "";
        public String UserID { get; set; } = "";
        public Food[] Foods { get; set; } = null; 
    }
}
