using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetMonths(this DateTime fromDateTime, DateTime toDateTime)
        {
            if (fromDateTime == toDateTime)
            {
                return 0;
            }

            if (toDateTime == DateTime.MinValue || toDateTime == DateTime.MaxValue)
            {
                toDateTime = DateTime.Now;
            }

            var number = (toDateTime.Year - fromDateTime.Year) * 12 + toDateTime.Month - fromDateTime.Month;
            if ((toDateTime.Day - fromDateTime.Day) >= 15)
            {
                number++;
            }
            return number;
        }
    }
}
