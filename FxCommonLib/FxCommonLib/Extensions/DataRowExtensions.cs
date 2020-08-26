using System;
using System.Collections.Generic;
using System.Data;


namespace FxCommonLib.Extensions {
    public static class DataRowExtensions {

        /// <summary>
        /// 列名が存在すれば値を、存在しなければnullを返却
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static object GetValueIfColNameContains(this DataRow row, string colName) {
            return row.Table.Columns.Contains(colName) ? row[colName] : null;
        }

        /// <summary>
        /// 列名が存在すれば値をセット、存在しなければなにもしない
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static void SetValueIfColNameContains<T>(this DataRow row, string colName, T TValue) {
            
            if (row.Table.Columns.Contains(colName)) {
                row[colName] = TValue;
            }
        }

    }
}
