using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static TReturn FuncByNotNull<T, TReturn>(this T col, Func<T, TReturn> func)
        {
            return FuncByNotNull<T, TReturn>(col, func, default(TReturn));
        }
        public static TReturn FuncByNotNull<T, TReturn>(this T col, Func<T, TReturn> func, TReturn defaultValue)
        {
            if (col == null)
            {
                return defaultValue;
            }

            return func(col);
        }

        public static void FuncByNotNull<T>(this T col, Action<T> func)
        {
            if (col != null)
            {
                func(col);
            }
        }
    }
}
