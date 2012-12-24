using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 取得枚举的描述信息
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((EnumDescriptionAttribute)attrs[0]).Text;
            }

            return enumeration.ToString();
        }

        /// <summary>
        /// 判断值是否存在于集合中，主要用于位枚举的判断
        /// </summary>
        /// <param name="col"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(this Enum col, Enum value)
        {
            if (col.GetType() != value.GetType())
            {
                return false;
            }

            if (!col.Equals(value))
            {
                return (col.As<int>() & value.As<int>()) != 0;
            }

            return true;
        }
    }

    public class EnumDescriptionAttribute : Attribute
    {
        public string Text { get; private set; }

        public EnumDescriptionAttribute(string text)
        {
            this.Text = text;
        }
    }
}
