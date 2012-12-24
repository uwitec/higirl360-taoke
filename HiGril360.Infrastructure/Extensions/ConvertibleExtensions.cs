using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class ConvertibleExtensions
    {
        ///// <summary>
        ///// 将对象转换为指定类型， 
        /////对像为null、DBNull.Value或string.Empty时近回default(T)
        ///// </summary>
        ///// <param name="col">要转换的对象</param>
        ///// <returns></returns>
        public static T As<T>(this object col)
        {
            var resultType = typeof(T);

            return (T)col.As(resultType, default(T));
        }
        public static T As<T>(this object col, T defaultValue)
        {
            var resultType = typeof(T);

            return (T)col.As(resultType, defaultValue);
        }


        ///// <summary>
        ///// 将对象转换为指定类型， 
        /////对像为null、DBNull.Value或string.Empty时近回default(T)
        ///// </summary>
        ///// <param name="col">要转换的对象</param>
        ///// <returns></returns>
        public static object As(this object col, Type targetType)
        {
            return col.As(targetType, () => BuildDefaultValue(targetType));
        }
        public static object As(this object col, Type targetType, object defaultValue)
        {
            return col.As(targetType, () => defaultValue);
        }
        private static object As(this object col, Type targetType, Func<object> acquireDefaultValue)
        {
            //无转换时，直接返回默认值
            if (col == null
                || col == DBNull.Value
                || (col is string && string.IsNullOrEmpty((string)col))
                || targetType == null)
            {
                return acquireDefaultValue();
            }

            Type colType = col.GetType();
            Type resultType = targetType;
            // 如果为Nullable<T>类型，则将targetType更改为Nullable<T>.UnderlyingType
            Type underlyingType = Nullable.GetUnderlyingType(resultType);
            if (underlyingType != null)
            {
                resultType = underlyingType;
            }

            //如果当前类型和要转换到的类型相同，则直接返回对象
            if (colType == resultType)
            {
                return col;
            }

            //当是从子类转为父类时，直接转换。
            if (resultType.IsAbstract && colType.IsSubclassOf(resultType))
            {
                return col;
            }

            //当从继承转类转换为接口时，直接转换
            if (resultType.IsInterface && colType.GetInterface(resultType.Name) != null)
            {
                return col;
            }

            ///将对象转换为字符串
            if (resultType == typeof(string))
            {
                return col.ToString();
            }

            if (resultType.IsEnum)
            {
                return Enum.Parse(resultType, col.ToString(), true);
            }

            if (col is string)
            {
                return ConvertibleExtensions.As((string)col, resultType, acquireDefaultValue);
            }

            try
            {
                return Convert.ChangeType(col, resultType);
            }
            catch
            {
                return acquireDefaultValue();
            }
        }
        public static object As(this string col, Type targetType, object acquireDefaultValue)
        {
            return col.As(targetType, () => acquireDefaultValue);
        }
        private static object As(this string col, Type targetType, Func<object> acquireDefaultValue)
        {
            try
            {
                if (targetType == typeof(Guid))
                {
                    return new Guid(col);
                }

                if (targetType == typeof(Uri))
                {
                    return new Uri(col);
                }

                if (targetType == typeof(TimeSpan))
                {
                    return TimeSpan.Parse(col);
                }

                if (targetType == typeof(DateTime))
                {
                    if (col.IsInteger() && (col.Length == 4 || col.Length == 6 || col.Length == 8))
                    {
                        col = col.Insert(4, "-");

                        if (col.Length > 6)
                        {
                            col = col.Insert(7, "-");
                        }
                        else
                        {
                            col += "01-";
                        }

                        if (col.Length <= 8)
                        {
                            col += "01";
                        }
                    }

                    return DateTime.Parse(col);
                }

                if (targetType == typeof(DateTimeOffset))
                {
                    return DateTimeOffset.Parse(col);
                }

                //将字符串转换为Type
                if (targetType.FullName == typeof(Type).FullName)
                {
                    return Type.GetType(col);
                }

                if (targetType == typeof(bool))
                {
                    int number;
                    if (int.TryParse(col, out number))
                    {
                        return number > 0;
                    }

                    return Convert.ToBoolean(col);
                }

                return Convert.ChangeType(col, targetType);
            }
            catch
            {
                return acquireDefaultValue();
            }
        }

        private static object BuildDefaultValue(Type targetType)
        {
            if (targetType == typeof(string))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    return Activator.CreateInstance(targetType);
                }
                catch (MissingMethodException)
                {
                    return null;
                }
            }
        }
    }
}
