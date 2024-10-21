using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Tests.Utils
{
    public class ParseUtils
    {
        public static DateTime ParseTime(string str)
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }
    }
}
