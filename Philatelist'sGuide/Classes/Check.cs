using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    internal class Check
    {
        public static bool Check_int(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) 
                && int.TryParse(value, out int value_int))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Check_string(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) 
                && !int.TryParse(value, out int value_int))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Check_double(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) 
                && double.TryParse(value, out double value_double))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}