using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class IDataReaderExtensions
    {
        /// <summary>
        /// 确定IDataReader是否包含指定的列
        /// </summary>
        /// <param name="col"></param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static bool ContainsColumn(this IDataReader col, string columnName)
        {
            bool containsColumn = false;
            DataTable schemaTable = col.GetSchemaTable();

            foreach (DataRow row in schemaTable.Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                {
                    containsColumn = true;
                    break;
                }
            }

            return containsColumn;
        }

        /// <summary>
        /// 根据列名取得DataReader中的记录，并转换为指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T Get<T>(this IDataReader col, string columnName, T defaultValue)
        {
            return col[columnName].As<T>(defaultValue);
        }
        public static T Get<T>(this IDataReader col, string columnName)
        {
            return col.Get<T>(columnName, default(T));
        }
    }
}
