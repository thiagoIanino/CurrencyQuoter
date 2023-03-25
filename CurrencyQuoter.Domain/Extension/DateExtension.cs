using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Extension
{
    public static class DateExtension
    {
        public static DateTime ToDateTime(this long time) 
        {
            return DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
        }
    }
}
