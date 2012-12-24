using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///  截取子字符串，每个中文字符和全角符号的长度为2。
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string SubstringByChineseRules(this string col, int length)
        {
            return col.SubstringByChineseRules(0, length);
        }

        private static Encoding coding = Encoding.GetEncoding("gb2312");
        public static string SubstringByChineseRules(this string col, int startIndex, int length)
        {
            if (length < 0)
            {
                return string.Empty;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            byte[] bytes = StringExtensions.coding.GetBytes(col);

            int caudaLength = bytes.Length - startIndex;
            if (caudaLength <= 0)
            {
                return string.Empty;
            }

            if (coding.GetString(bytes, 0, startIndex).EndsWith("?"))
            {
                startIndex += 1;
            }

            int subStringLength = caudaLength >= length
                ? length
                : caudaLength;

            return StringExtensions.coding.GetString(bytes, startIndex, subStringLength).TrimEnd('?');
        }

        /// <summary>
        /// 清除字符串中的空白区域
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(this string col)
        {
            bool changed = false;
            char[] output = col.ToCharArray();
            int cursor = 0;
            for (int i = 0, size = output.Length; i < size; i++)
            {
                char c = output[i];
                if (Char.IsWhiteSpace(c))
                {
                    changed = true;
                    continue;
                }

                output[cursor] = c;
                cursor++;
            }

            return changed ? new string(output, 0, cursor) : col;
        }

        /// <summary>
        /// 删除Html格式
        ///删除内容：html标签，script，<!---->注释，回车，换行，空格
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string col)
        {
            return Regex.Replace(col, @"<!--([\s\S])*?-->|<script([\s\S])*?/script>|<[^>]*>|&(nbsp|#160);|([\r\n])", "");
        }

        /// <summary>
        /// 根据分隔符切分字符串，并返回最后一个
        /// </summary>
        /// <returns></returns>
        public static string GetLastItemBy(this string col, char split)
        {
            string[] group = col.Split(split);

            return group[group.Length - 1];
        }

        /// <summary>
        /// 根据分隔符切分字符串，并返回第一个
        /// </summary>
        /// <returns></returns>
        public static string GetFirstItemBy(this string col, char split)
        {
            if (string.IsNullOrEmpty(col))
            {
                return string.Empty;
            }

            string[] group = col.Split(split);

            return group[0];
        }
    }
}
