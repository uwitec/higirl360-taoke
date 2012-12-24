using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 当对象为空，并且如果是字符类型为Empty时抛出异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static void ArgumentNullExceptionByNullOrEmpty(this object value, string name)
        {
            if (null == value)
            {
                throw new ArgumentNullException(name);
            }
            if (value is string && string.Empty.Equals(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// 对象为null或对象Count为0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cols"></param>
        /// <param name="name"></param>
        public static void ArgumentNullExceptionByNullOrZeroElement<T>(this IEnumerable<T> cols, string name)
        {
            if (cols == null || cols.Count() == 0)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
