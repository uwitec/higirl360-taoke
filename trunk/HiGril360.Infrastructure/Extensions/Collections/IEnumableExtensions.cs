using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class IEnumableExtensions
    {
        #region Foreach

        /// <summary>
        /// 遍历集合，并通过委托的itemHandler方法对集合元素进行操作。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="cols">集合</param>
        /// <param name="itemHandler">操作集合元素的委托方法</param>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> cols, Action<T> itemHandler)
        {
            return cols.Foreach<T>(delegate(T item, int index) { itemHandler(item); });
        }
        /// <summary>
        /// 遍历集合，并通过委托的itemHandler方法对集合元素进行操作。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="cols">集合</param>
        /// <param name="itemHandler">操作集合元素的委托方法,int为元素在集合中的索引</param>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> cols, Action<T, int> itemHandler)
        {
            //因为Foreach有两个重载参数分别是Action<T> itemHandler，和Action<T,int> itemHandler
            //所以Foreach无法传入null类型的itemHandler参数。因为传入null参数时，将会出现不明确调用，编译器无法通过
            //if (itemHandler == null)
            //    throw new ArgumentNullException("itemHandler");

            if (cols != null)
            {
                int index = 0;
                foreach (T item in cols)
                {
                    itemHandler(item, index++);
                }
            }

            return cols;
        }

        #endregion

        #region CombineToString

        public static string CombineToString<T>(this IEnumerable<T> cols)
        {
            return cols.CombineToString('|');
        }
        public static string CombineToString<T>(this IEnumerable<T> cols, char separator)
        {
            return cols.CombineToString(separator, null);
        }
        public static string CombineToString<T>(this IEnumerable<T> cols, Func<T, string> itemHandler)
        {
            return cols.CombineToString('|', itemHandler);
        }
        /// <summary>
        ///  将集合中的元素根据分割符合并成字会串。
        ///  string.Join()具有类似的效果
        /// </summary>
        /// <typeparam name="T">集合中元素类型</typeparam>
        /// <param name="cols"></param>
        /// <param name="separator">分割符</param>
        /// <param name="itemHandler">对元素进行操作的委托方法</param>
        /// <returns></returns>
        public static string CombineToString<T>(this IEnumerable<T> cols, char separator, Func<T, string> itemHandler)
        {
            if (char.IsWhiteSpace(separator))
                throw new ArgumentNullException("separator");

            if (itemHandler == null && typeof(T).IsAssignableFrom(typeof(IConvertible)))
                throw new ArgumentNullException("itemHandler");

            if (cols == null || cols.Count() == 0)
            {
                return string.Empty;
            }

            StringBuilder resultStringBuilder = new StringBuilder();
            string itemStr = string.Empty;


            foreach (T item in cols)
            {
                itemStr = itemHandler != null ?
                    itemHandler(item)
                    : item.As<string>();

                resultStringBuilder.Append(itemStr);
                resultStringBuilder.Append(separator);
            }

            return resultStringBuilder.ToString().TrimEnd(separator);
        }

        #endregion

        #region Random

        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> cols)
        {
            return cols.GetRandom<T>(1);
        }
        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> cols, int count)
        {
            return cols.GetRandom<T>(count, true);
        }
        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> cols, int count, bool hasRepeat)
        {
            return cols.GetRandom<T>(count, count, hasRepeat);
        }
        /// <summary>
        /// 从数集合中随机取指定数量的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cols"></param>
        /// <param name="count"></param>
        /// <param name="hasRepeat">是否允许重复对象：true为充许，false为不充许</param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> cols, int minCount, int maxCount, bool hasRepeat)
        {
            if (maxCount < minCount)
            {
                maxCount = minCount;
            }

            var count = minCount == maxCount
                ? minCount
                : Utility.Random.Next(minCount, maxCount + 1);

            IList<T> result = new List<T>();

            if (cols == null || count <= 0)
            {
                return result;
            }

            if (count >= cols.Count() && !hasRepeat)
            {
                return cols;
            }

            while (result.Count < count)
            {
                var item = cols.ElementAt(Utility.Random.Next(0, cols.Count()));
                if (!hasRepeat && result.Contains(item))
                {
                    continue;
                }

                result.Add(item);
            }

            return result;
        }

        #endregion
    }
}
