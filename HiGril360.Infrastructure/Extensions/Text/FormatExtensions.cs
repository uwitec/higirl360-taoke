using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class FormatExtensions
    {
        /// <summary>
        /// 将对象格式化为指定格式的字符串
        /// 如：日期格式 yyyy-MM-dd
        /// </summary>
        /// <param name="col">要格式化的对象</param>
        /// <param name="format">要使用的格式</param>
        /// <returns></returns>
        public static string Format(this object col, string format)
        {
            if (null == col)
            {
                return string.Empty;
            }
            IFormattable formattable = col as IFormattable;
            if (null != formattable)
            {
                return formattable.ToString(format, null);
            }

            var colType = col.GetType();
            if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                object underlyingValue = Convert.ChangeType(col, Nullable.GetUnderlyingType(col.GetType()));
                return Format(underlyingValue, format);
            }

            return col.ToString();
        }
    }
}
