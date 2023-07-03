using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1_PersOto
{
    internal class Temizle
    {
        public static string GetEnumDisplayName(Unvan value)//x
        {
            var fieldinfo = value.GetType().GetField(value.ToString());
            var attibute = fieldinfo.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();
            return attibute?.Name ?? value.ToString();
        }
    }
}
