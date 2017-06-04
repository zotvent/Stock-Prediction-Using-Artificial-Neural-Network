using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionUtils.Extensions
{
    public static class DateTimeExtension
    {
        public static long Days(this DateTime time)
        {
            var days = time.Ticks / TimeSpan.TicksPerDay;
            return days;
        }
    }
}
