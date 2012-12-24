using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static T Get<T>(this IDictionary col, string key)
            where T : IConvertible
        {
            return col.Get<T>(key, default(T));
        }

        public static T Get<T>(this IDictionary col, string key, T defaultValue)
            where T : IConvertible
        {
            var result = defaultValue;

            if (col != null && col.Contains(key))
            {
                result = ((IConvertible)col[key]).As<T>();
            }

            return result;
        }
    }
}
