//using Android.Telecom;
using DoitDoit.Models;
//using Java.Security.Spec;
//using Org.Apache.Http.Client.Methods;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
/*
 FB에 있지만, 영양섭취량에 포함안되는정보
 지방-탄수화물
     */
namespace DoitDoit.Nutrition
{
    public class nutBases
    {
        #region nutBase프로퍼티
        public BITAMIN_C bitamin_c { get; set; } public THIAMIN thiamin { set; get; } public RIBOFLAVIN riboflavin { set; get; }
        public NIACIN niacin { set; get; }
        public BITAMIN_B6 bitamin_b6 { set; get; }
        public BITAMIN_B12 bitamin_b12 { set; get; }
        public BITAMIN_A bitamin_a { set; get; }
        public BITAMIN_D bitamin_d { set; get; }
        public BITAMIN_E bitamin_e { set; get; }
        public BITAMIN_K bitamin_k { set; get; }
        public IRON iron; public ZINC zinc { set; get; }
        public COOPER cooper { set; get; }
        public FLUORINE fluorine { set; get; }
        public CHLORINE chlorine { set; get; }
        public KALIUM kalium { set; get; }
        public MAGNESIUM magnesium { set; get; }
        public MANGANESE manganese { set; get; }
        public IODINE iodine { set; get; }
        public SELENIUM selenium { set; get; }
        public NATRIUM natrium { set; get; }
        public PHOSPHORUS phosphorus { set; get; }
        public CALCIUM calcium { set; get; }
        public CALORIE calorie { set; get; }
        public PROTEIN protein { set; get; }
        #endregion
        public nutBases(Boolean sex, int age)
        {
            bitamin_b6 = new BITAMIN_B6(sex, age);
            bitamin_c = new BITAMIN_C(sex, age);
            thiamin = new THIAMIN(sex, age);
            riboflavin = new RIBOFLAVIN(sex, age);
            niacin = new NIACIN(sex, age);
            bitamin_b12 = new BITAMIN_B12(sex, age);
            bitamin_a = new BITAMIN_A(sex, age);
            bitamin_d = new BITAMIN_D(sex, age);
            bitamin_e = new BITAMIN_E(sex, age);
            bitamin_k = new BITAMIN_K(sex, age);
            iron = new IRON(sex, age);
            zinc = new ZINC(sex, age);
            cooper = new COOPER(sex, age);
            fluorine = new FLUORINE(sex, age);
            chlorine = new CHLORINE(sex, age);
            kalium = new KALIUM(sex, age);
            magnesium = new MAGNESIUM(sex, age);
            manganese = new MANGANESE(sex, age);
            iodine = new IODINE(sex, age);
            selenium = new SELENIUM(sex, age);
            natrium = new NATRIUM(sex, age);
            phosphorus = new PHOSPHORUS(sex, age);
            calcium = new CALCIUM(sex, age);
            calorie = new CALORIE(sex, age);
            protein = new PROTEIN(sex, age);
        }

    }

    public class BITAMIN_A : nutBase
    {
        public BITAMIN_A(Boolean sex, int age) 
        {
            NUT_UNIT = "㎍RAE";
            if (age < 3)
            {
                AVG_INTAKE = 200;
                HOP_INTAKE = 300;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 230;
                HOP_INTAKE = 350;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 320;
                            HOP_INTAKE = 450;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 420;
                            HOP_INTAKE = 600;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 540;
                            HOP_INTAKE = 750;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 620;
                            HOP_INTAKE = 850;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 570;
                            HOP_INTAKE = 800;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 550;
                            HOP_INTAKE = 750;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 530;
                            HOP_INTAKE = 750;
                            break;
                        default:
                            AVG_INTAKE = 500;
                            HOP_INTAKE = 700;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 290;
                            HOP_INTAKE = 400;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 380;
                            HOP_INTAKE = 550;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 470;
                            HOP_INTAKE = 650;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 440;
                            HOP_INTAKE = 600;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 460;
                            HOP_INTAKE = 650;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 450;
                            HOP_INTAKE = 650;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 430;
                            HOP_INTAKE = 600;
                            break;
                        default:
                            AVG_INTAKE = 410;
                            HOP_INTAKE = 550;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_C :nutBase
    {
        public BITAMIN_C(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3) 
            {
                AVG_INTAKE = 30;
                HOP_INTAKE = 35;
            }
            else if (age < 6) 
            {
                AVG_INTAKE = 30;
                HOP_INTAKE = 40;
            }
            else 
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 40;
                            HOP_INTAKE = 55;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 70;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 70;
                            HOP_INTAKE = 90;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 80;
                            HOP_INTAKE = 105;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        default:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 45;
                            HOP_INTAKE = 60;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 60;
                            HOP_INTAKE = 80;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 70;
                            HOP_INTAKE = 95;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        default:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_B6 : nutBase
    {
        public BITAMIN_B6(Boolean sex, int age) 
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.5f;
                HOP_INTAKE = 0.6f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.6f;
                HOP_INTAKE = 0.7f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.7f;
                            HOP_INTAKE = 0.9f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.9f;
                            HOP_INTAKE = 1.1f;
                            break;
                        default:
                            AVG_INTAKE = 1.3f;
                            HOP_INTAKE = 1.5f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.7f;
                            HOP_INTAKE = 0.9f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.9f;
                            HOP_INTAKE = 1.1f;
                            break;
                        default:
                            AVG_INTAKE = 1.2f;
                            HOP_INTAKE = 1.4f;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_B12 : nutBase
    {
        public BITAMIN_B12(Boolean sex, int age)
        {
            NUT_UNIT = "㎍";
            if (age < 3)
            {
                AVG_INTAKE = 0.8f;
                HOP_INTAKE = 0.9f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.9f;
                HOP_INTAKE = 1.1f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.1f;
                            HOP_INTAKE = 1.3f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1.5f;
                            HOP_INTAKE = 1.7f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 1.9f;
                            HOP_INTAKE = 2.3f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 2.2f;
                            HOP_INTAKE = 2.7f;
                            break;
                        default:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.4f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.1f;
                            HOP_INTAKE = 1.3f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1.5f;
                            HOP_INTAKE = 1.7f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 1.9f;
                            HOP_INTAKE = 2.3f;
                            break;
                        default:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.4f;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_D : nutBase
    {
        public BITAMIN_D(Boolean sex, int age) 
        {
            NUT_UNIT = "㎍";
            if (age < 3)
            {
                AVG_INTAKE = 5;
                HOP_INTAKE = 5;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 5;
                HOP_INTAKE = 5;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 5;
                            HOP_INTAKE = 5;
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 10;
                            HOP_INTAKE = 10;
                            break;
                        default:
                            AVG_INTAKE = 15;
                            HOP_INTAKE = 15;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 5;
                            HOP_INTAKE = 5;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 10;
                            HOP_INTAKE = 10;
                            break;
                        default:
                            AVG_INTAKE = 15;
                            HOP_INTAKE = 15;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_E : nutBase
    {
        public BITAMIN_E(Boolean sex, int age)
        {
            NUT_UNIT = "㎎α-TE";
            if (age < 3)
            {
                AVG_INTAKE = 5;
                HOP_INTAKE = 5;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 6;
                HOP_INTAKE = 6;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 7;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 9;
                            HOP_INTAKE = 9;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 10;
                            HOP_INTAKE = 10;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 11;
                            break;
                        default:
                            AVG_INTAKE = 12;
                            HOP_INTAKE = 12;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 7;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 9;
                            HOP_INTAKE = 9;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 10;
                            HOP_INTAKE = 10;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 11;
                            break;
                        default:
                            AVG_INTAKE = 12;
                            HOP_INTAKE = 12;
                            break;
                    }
                }
            }
        }
    }
    public class BITAMIN_K : nutBase
    {
        public BITAMIN_K(Boolean sex, int age)
        {
            NUT_UNIT = "㎍";
            if (age < 3)
            {
                AVG_INTAKE = 25;
                HOP_INTAKE = 25;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 30;
                HOP_INTAKE = 30;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 45;
                            HOP_INTAKE = 45;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 55;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 70;
                            HOP_INTAKE = 70;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 80;
                            HOP_INTAKE = 80;
                            break;
                        default:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 75;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 45;
                            HOP_INTAKE = 45;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 55;
                            break;
                        default:
                            AVG_INTAKE = 65;
                            HOP_INTAKE = 65;
                            break;
                    }
                }
            }
        }
    }


    public class THIAMIN : nutBase 
    {
        public THIAMIN(Boolean sex, int age) 
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.4f;
                HOP_INTAKE = 0.5f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.4f;
                HOP_INTAKE = 0.5f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.6f;
                            HOP_INTAKE = 0.7f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.7f;
                            HOP_INTAKE = 0.9f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.1f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 1.1f;
                            HOP_INTAKE = 1.3f;
                            break;
                        default:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.2f;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.6f;
                            HOP_INTAKE = 0.7f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.7f;
                            HOP_INTAKE = 0.9f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 0.9f;
                            HOP_INTAKE = 1.1f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.2f;
                            break;
                        default:
                            AVG_INTAKE = 0.9f;
                            HOP_INTAKE = 1.1f;
                            break;
                    }
                }
            }
        }
    } //티아민 (비타민B1)
    public class RIBOFLAVIN : nutBase
    {
        public RIBOFLAVIN(Boolean sex, int age) 
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.5f;
                HOP_INTAKE = 0.5f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.5f;
                HOP_INTAKE = 0.6f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.7f;
                            HOP_INTAKE = 0.9f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.2f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 1.2f;
                            HOP_INTAKE = 1.5f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 1.4f;
                            HOP_INTAKE = 1.7f;
                            break;
                        default:
                            AVG_INTAKE = 1.3f;
                            HOP_INTAKE = 1.5f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.6f;
                            HOP_INTAKE = 0.8f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.8f;
                            HOP_INTAKE = 1.0f;
                            break;
                        default:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.2f;
                            break;
                    }
                }
            }
        }
    } // 리보플라빈
    public class NIACIN : nutBase
    {
        public NIACIN(Boolean sex, int age) 
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 4;
                HOP_INTAKE = 6;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 5;
                HOP_INTAKE = 7;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 9;
                            HOP_INTAKE = 12;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 15;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 13;
                            HOP_INTAKE = 17;
                            break;
                        default:
                            AVG_INTAKE = 12;
                            HOP_INTAKE = 16;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 9;
                            HOP_INTAKE = 12;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 15;
                            break;
                        default:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 14;
                            break;
                    }
                }
            }
        }
    } // 니아신 (비타민B3)
    public class IRON : nutBase 
    {
        public IRON(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 4;
                HOP_INTAKE = 6;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 5;
                HOP_INTAKE = 6;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 8;
                            HOP_INTAKE = 10;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 14;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 8;
                            HOP_INTAKE = 10;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 10;
                            break;
                        default:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 6;
                            HOP_INTAKE = 8;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 10;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 13;
                            HOP_INTAKE = 16;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 11;
                            HOP_INTAKE = 14;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 6;
                            HOP_INTAKE = 8;
                            break;
                        default:
                            AVG_INTAKE = 5;
                            HOP_INTAKE = 7;
                            break;
                    }
                }
            }
        }
    } // 철
    public class ZINC : nutBase
    {
        public ZINC(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 2;
                HOP_INTAKE = 3;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 3;
                HOP_INTAKE = 4;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 5;
                            HOP_INTAKE = 6;
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 8;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 8;
                            HOP_INTAKE = 10;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 8;
                            HOP_INTAKE = 9;
                            break;
                        default:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 4;
                            HOP_INTAKE = 5;
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 6;
                            HOP_INTAKE = 8;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 9;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 7;
                            HOP_INTAKE = 8;
                            break;
                        default:
                            AVG_INTAKE = 6;
                            HOP_INTAKE = 7;
                            break;
                    }
                }
            }
        }
    } // 아연
    public class COOPER : nutBase
    {
        public COOPER(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.22f;
                HOP_INTAKE = 0.28f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.25f;
                HOP_INTAKE = 0.32f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.34f;
                            HOP_INTAKE = 0.44f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.44f;
                            HOP_INTAKE = 0.58f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 0.57f;
                            HOP_INTAKE = 0.74f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 0.65f;
                            HOP_INTAKE = 0.84f;
                            break;
                        default:
                            AVG_INTAKE = 0.6f;
                            HOP_INTAKE = 0.8f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.34f;
                            HOP_INTAKE = 0.44f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.44f;
                            HOP_INTAKE = 0.58f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 0.57f;
                            HOP_INTAKE = 0.74f;
                            break;
                        default:
                            AVG_INTAKE = 0.6f;
                            HOP_INTAKE = 0.8f;
                            break;
                    }
                }
            }
        }
    }//구리
    public class FLUORINE : nutBase
    {
        public FLUORINE(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.6f;
                HOP_INTAKE = 0.6f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.8f;
                HOP_INTAKE = 0.8f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.0f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.0f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 2.5f;
                            HOP_INTAKE = 2.5f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 3.0f;
                            HOP_INTAKE = 3.0f;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 3.5f;
                            HOP_INTAKE = 3.5f;
                            break;
                        default:
                            AVG_INTAKE = 3.0f;
                            HOP_INTAKE = 3.0f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.0f;
                            HOP_INTAKE = 1.0f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.0f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 2.5f;
                            HOP_INTAKE = 2.5f;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 3.0f;
                            HOP_INTAKE = 3.0f;
                            break;
                        default:
                            AVG_INTAKE = 2.5f;
                            HOP_INTAKE = 2.5f;
                            break;
                    }
                }
            }
        }
    }// 플루오린-불소
    public class CHLORINE : nutBase
    {
        public CHLORINE(Boolean sex, int age)
        {
            NUT_UNIT = "g";
            if (age < 3)
            {
                AVG_INTAKE = 1.3f;
                HOP_INTAKE = 1.3f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 1.5f;
                HOP_INTAKE = 1.5f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.9f;
                            HOP_INTAKE = 1.9f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 2.1f;
                            HOP_INTAKE = 2.1f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 2.3f;
                            HOP_INTAKE = 2.3f;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.0f;
                            break;
                        default:
                            AVG_INTAKE = 1.7f;
                            HOP_INTAKE = 1.7f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1.9f;
                            HOP_INTAKE = 1.9f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 2.1f;
                            HOP_INTAKE = 2.1f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 2.3f;
                            HOP_INTAKE = 2.3f;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 2.0f;
                            HOP_INTAKE = 2.0f;
                            break;
                        default:
                            AVG_INTAKE = 1.7f;
                            HOP_INTAKE = 1.7f;
                            break;
                    }
                }
            }
        }
    }// 클로린-염소
    public class KALIUM : nutBase 
    {
        public KALIUM(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 2000f;
                HOP_INTAKE = 2000f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 2300f;
                HOP_INTAKE = 2300f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 2600f;
                            HOP_INTAKE = 2600f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 3000f;
                            HOP_INTAKE = 3000f;
                            break;
                        default:
                            AVG_INTAKE = 3500f;
                            HOP_INTAKE = 3500f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 2600f;
                            HOP_INTAKE = 2600f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 3000f;
                            HOP_INTAKE = 3000f;
                            break;
                        default:
                            AVG_INTAKE = 3500f;
                            HOP_INTAKE = 3500f;
                            break;
                    }
                }
            }
        }

    }// 칼륨
    public class MAGNESIUM : nutBase 
    {
        public MAGNESIUM(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 65;
                HOP_INTAKE = 80;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 85;
                HOP_INTAKE = 100;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 135;
                            HOP_INTAKE = 160;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 190;
                            HOP_INTAKE = 230;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 265;
                            HOP_INTAKE = 320;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 335;
                            HOP_INTAKE = 400;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 295;
                            HOP_INTAKE = 350;
                            break;
                        default:
                            AVG_INTAKE = 305;
                            HOP_INTAKE = 370;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 125;
                            HOP_INTAKE = 150;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 180;
                            HOP_INTAKE = 210;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 245;
                            HOP_INTAKE = 290;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 285;
                            HOP_INTAKE = 340;
                            break;
                        default:
                            AVG_INTAKE = 235;
                            HOP_INTAKE = 280;
                            break;
                    }
                }
            }
        }
    } // 마그네슘

    public class MANGANESE : nutBase
    {
        public MANGANESE(Boolean sex, int age) 
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 1.5f;
                HOP_INTAKE = 1.5f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 2.0f;
                HOP_INTAKE = 2.0f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 2.5f;
                            HOP_INTAKE = 2.5f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 3.0f;
                            HOP_INTAKE = 3.0f;
                            break;
                        default:
                            AVG_INTAKE = 4.0f;
                            HOP_INTAKE = 4.0f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 2.5f;
                            HOP_INTAKE = 2.5f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 3.0f;
                            HOP_INTAKE = 3.0f;
                            break;
                        default:
                            AVG_INTAKE = 3.5f;
                            HOP_INTAKE = 3.5f;
                            break;
                    }
                }
            }
        }
    }// 망간
    public class IODINE : nutBase
    {
        public IODINE(Boolean sex, int age)
        {
            NUT_UNIT = "㎍";
            if (age < 3)
            {
                AVG_INTAKE = 55;
                HOP_INTAKE = 80;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 65;
                HOP_INTAKE = 90;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 85;
                            HOP_INTAKE = 110;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 90;
                            HOP_INTAKE = 130;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 95;
                            HOP_INTAKE = 130;
                            break;
                        default:
                            AVG_INTAKE = 95;
                            HOP_INTAKE = 150;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 75;
                            HOP_INTAKE = 100;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 85;
                            HOP_INTAKE = 110;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 90;
                            HOP_INTAKE = 130;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 95;
                            HOP_INTAKE = 130;
                            break;
                        default:
                            AVG_INTAKE = 95;
                            HOP_INTAKE = 150;
                            break;
                    }
                }
            }
        }
    }// 요오드
    public class SELENIUM : nutBase
    {
        public SELENIUM(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 0.019f;
                HOP_INTAKE = 0.023f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 0.022f;
                HOP_INTAKE = 0.025f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.03f;
                            HOP_INTAKE = 0.035f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.039f;
                            HOP_INTAKE = 0.045f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 0.049f;
                            HOP_INTAKE = 0.06f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 0.055f;
                            HOP_INTAKE = 0.065f;
                            break;
                        default:
                            AVG_INTAKE = 0.05f;
                            HOP_INTAKE = 0.06f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 0.03f;
                            HOP_INTAKE = 0.035f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 0.039f;
                            HOP_INTAKE = 0.045f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 0.049f;
                            HOP_INTAKE = 0.06f;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 0.055f;
                            HOP_INTAKE = 0.065f;
                            break;
                        default:
                            AVG_INTAKE = 0.05f;
                            HOP_INTAKE = 0.06f;
                            break;
                    }
                }
            }
        }
    }//셀레늄
    public class NATRIUM : nutBase
    {
        public NATRIUM(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 900f;
                HOP_INTAKE = 900f;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 1000f;
                HOP_INTAKE = 1000f;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1200f;
                            HOP_INTAKE = 1200f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1400f;
                            HOP_INTAKE = 1400f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 1500f;
                            HOP_INTAKE = 1500f;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 1300f;
                            HOP_INTAKE = 1300f;
                            break;
                        default:
                            AVG_INTAKE = 1100f;
                            HOP_INTAKE = 1100f;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1200f;
                            HOP_INTAKE = 1200f;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1400f;
                            HOP_INTAKE = 1400f;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 1500f;
                            HOP_INTAKE = 1500f;
                            break;
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                            AVG_INTAKE = 1300f;
                            HOP_INTAKE = 1300f;
                            break;
                        default:
                            AVG_INTAKE = 1100f;
                            HOP_INTAKE = 1100f;
                            break;
                    }
                }
            }
        }
    }// 나트륨
    public class PHOSPHORUS : nutBase
    {
        public PHOSPHORUS(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 450;
                HOP_INTAKE = 450;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 550;
                HOP_INTAKE = 550;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 600;
                            HOP_INTAKE = 600;
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 1200;
                            HOP_INTAKE = 1200;
                            break;
                        default:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;
                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 550;
                            HOP_INTAKE = 550;
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 1200;
                            HOP_INTAKE = 1200;
                            break;
                        default:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;
                    }
                }
            }
        }
    }//포스퍼러스 - 인
    public class CALCIUM : nutBase
    {
        public CALCIUM(Boolean sex, int age)
        {
            NUT_UNIT = "mg";
            if (age < 3)
            {
                AVG_INTAKE = 500;
                HOP_INTAKE = 500;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 600;
                HOP_INTAKE = 600;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 800;
                            HOP_INTAKE = 800;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 1000;
                            HOP_INTAKE = 1000;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 900;
                            HOP_INTAKE = 900;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 800;
                            HOP_INTAKE = 800;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 750;
                            HOP_INTAKE = 750;
                            break;
                        default:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 800;
                            HOP_INTAKE = 800;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 900;
                            HOP_INTAKE = 900;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 800;
                            HOP_INTAKE = 800;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 700;
                            HOP_INTAKE = 700;
                            break;
                        default:
                            AVG_INTAKE = 800;
                            HOP_INTAKE = 800;
                            break;
                    }
                }
            }
        }
    }// 칼슘
    public class CALORIE : nutBase
    {
        public CALORIE(Boolean sex, int age)
        {
            NUT_UNIT = "Kcal";
            if (age < 3)
            {
                AVG_INTAKE = 1000;
                HOP_INTAKE = 1000;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 1400;
                HOP_INTAKE = 1400;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1700;
                            HOP_INTAKE = 1700;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 2100;
                            HOP_INTAKE = 2100;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 2500;
                            HOP_INTAKE = 2500;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 2700;
                            HOP_INTAKE = 2700;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 2600;
                            HOP_INTAKE = 2600;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 2400;
                            HOP_INTAKE = 2400;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 2200;
                            HOP_INTAKE = 2200;
                            break;
                        default:
                            AVG_INTAKE = 2000;
                            HOP_INTAKE = 2000;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 1500;
                            HOP_INTAKE = 1500;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 1800;
                            HOP_INTAKE = 1800;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 2000;
                            HOP_INTAKE = 2000;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 2100;
                            HOP_INTAKE = 2100;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            AVG_INTAKE = 1900;
                            HOP_INTAKE = 1900;
                            break;
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 1800;
                            HOP_INTAKE = 1800;
                            break;
                        default:
                            AVG_INTAKE = 1600;
                            HOP_INTAKE = 1600;
                            break;
                    }
                }
            }
        }
    }//칼로리
    public class PROTEIN: nutBase
    {
        public PROTEIN(Boolean sex, int age)
        {
            NUT_UNIT = "g";
            if (age < 3)
            {
                AVG_INTAKE = 15;
                HOP_INTAKE = 15;
            }
            else if (age < 6)
            {
                AVG_INTAKE = 20;
                HOP_INTAKE = 20;
            }
            else
            {
                if (sex)
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 30;
                            HOP_INTAKE = 30;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 40;
                            HOP_INTAKE = 40;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 55;
                            break;
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 65;
                            HOP_INTAKE = 65;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 60;
                            HOP_INTAKE = 60;
                            break;
                        default:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 55;
                            break;

                    }
                }
                else
                {
                    switch (age)
                    {
                        case 6:
                        case 7:
                        case 8:
                            AVG_INTAKE = 25;
                            HOP_INTAKE = 25;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            AVG_INTAKE = 40;
                            HOP_INTAKE = 40;
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            AVG_INTAKE = 50;
                            HOP_INTAKE = 50;
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            AVG_INTAKE = 55;
                            HOP_INTAKE = 55;
                            break;
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                            AVG_INTAKE = 50;
                            HOP_INTAKE = 50;
                            break;
                        default:
                            AVG_INTAKE = 45;
                            HOP_INTAKE = 450;
                            break;
                    }
                }
            }
        }
    }//단백질
}
