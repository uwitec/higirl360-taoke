using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HiGirl360.Infrastructure.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 将多个表的colum合并成一个大表
        /// </summary>
        /// <param name="col"></param>
        /// <param name="tables"></param>
        public static void MergeDataTables(this DataTable col, params DataTable[] tables)
        {
            foreach (var table in tables)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];

                    foreach (DataColumn column in table.Columns)
                    {
                        if (!col.Columns.Contains(column.ColumnName))
                        {
                            col.Columns.Add(column.ColumnName, column.DataType);
                        }

                        if (col.Rows.Count <= i)
                        {
                            col.Rows.Add(col.NewRow());
                        }

                        col.Rows[i][column.ColumnName] = row[column.ColumnName];
                    }
                }
            }
        }

        public static DataTable AddColumns(this DataTable dt, params KeyValuePair<string, Func<DataRow, object>>[] columns)
        {
            return dt.AddColumns(columns.Select(item => new KeyValuePair<string, Func<DataRow, int, object>>(item.Key, (row, index) => item.Value(row))));
        }
        public static DataTable AddColumns(this DataTable dt, params KeyValuePair<string, Func<DataRow, int, object>>[] columns)
        {
            return dt.AddColumns((IEnumerable<KeyValuePair<string, Func<DataRow, int, object>>>)columns);
        }
        public static DataTable AddColumns(this DataTable dt, IEnumerable<KeyValuePair<string, Func<DataRow, int, object>>> columns)
        {
            if (dt == null || columns == null)
            {
                return dt;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (var pair in columns)
                {
                    if (!dt.Columns.Contains(pair.Key))
                    {
                        dt.Columns.Add(pair.Key);
                    }

                    dt.Rows[i][pair.Key] = pair.Value(dt.Rows[i], i);
                }
            }

            return dt;
        }

        public static void ForEach(this DataTable dt, params KeyValuePair<string, Func<DataRow, int, string, object>>[] valueHandler)
        {
            if (dt == null || dt.Rows.Count == 0 || valueHandler == null || valueHandler.Count() == 0)
            {
                return;
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                foreach (var handler in valueHandler)
                {
                    dt.Rows[i][handler.Key] = handler.Value(dt.Rows[i], i, handler.Key);
                }
            }
        }
    }
}
