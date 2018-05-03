using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class Language
    {
        public static string Numeral(int n)
        {
            string s = "a";
            if (n == 11 || n == 8 || n == 18)
                s += "n ";
            else
                s += " ";

            s += NumSuffix(n);
            
            return s;
        }
        
        public static string NumSuffix(int n)
        {
            switch (n)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                default:
                    return n + "th";
            }
        }
    }
}
