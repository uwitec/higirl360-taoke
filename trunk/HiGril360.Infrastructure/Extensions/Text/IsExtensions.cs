using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class IsExtensions
    {
        #region 数字字符串检查

        /// <summary>
        /// 是否为整数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static bool IsInteger(this string input)
        {
            return IsExtensions.IsMatch(input, "^[0-9]+$");
        }

        /// <summary>
        /// 是否为带正负号的整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsSignedInteger(this string input)
        {
            return IsExtensions.IsMatch(input, "^[+-]?[0-9]+$");
        }


        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(this string input)
        {
            return IsExtensions.IsMatch(input, "^[0-9]+[.]?[0-9]+$");
        }

        /// <summary>
        /// 是否是带正负号的浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsSignedDecimal(this string input)
        {
            return IsExtensions.IsMatch(input, "^[+-]?[0-9]+[.]?[0-9]+$");
        }

        #endregion

        #region 是否为英文字母

        /// <summary>
        /// 是否为a-z的英文字母
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static bool IsLetter(this string input)
        {
            return IsExtensions.IsMatch(input, "^[a-zA-Z]+$");
        }

        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsChinese(this char input)
        {
            return IsExtensions.IsMatch(input.ToString(), "^[\u4e00-\u9fa5]$");
        }

        //public static bool IsChineseOrSBC(this char input)
        //{
        //    return IsExtensions.IsMatch(input.ToString(), "^[\u4e00-\u9fa5（）《》——；，。“”]$");
        //}


        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(this string input)
        {
            return IsExtensions.IsMatch(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        #endregion

        #region 是否是Url
        /// <summary>
        /// 是否是网络地址
        /// </summary>
        /// kevin 12.12
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsUrl(this string input)
        {
            return IsExtensions.IsMatch(input, "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        }
        #endregion

        #region 是否是中国手机号码
        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// kevin 12.12
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseMobilePhone(this string input)
        {
            return IsExtensions.IsMatch(input, @"^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}$");
        }
        #endregion

        #region 是否为IP
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string input)
        {
            return IsExtensions.IsMatch(input, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region Url部件

        /// <summary>
        /// 判断字符串是否可做为Url的部件。
        /// 是否符合Url规则
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static bool IsUrlSegment(this string segment)
        {
            return Regex.IsMatch(segment, @"^[^/?#[\]@""^{}|`<>\s]+$");
        }

        #endregion

        public static bool IsMatch(this string input, string pattern)
        {
            return IsExtensions.IsMatch(input, pattern, RegexOptions.None);
        }
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return string.IsNullOrEmpty(input)
                ? false
                : Regex.IsMatch(input, pattern, options);
        }
    }
}
