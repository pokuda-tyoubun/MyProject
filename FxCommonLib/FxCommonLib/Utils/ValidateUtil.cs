using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 入力値検証ユーティリティ
    /// </summary>
    public class ValidateUtil {

        /// <summary>
        /// エラー情報テーブル作成
        /// </summary>
        /// <returns></returns>
        public DataTable CreateErrorTable() {
            DataTable et = new DataTable(CommonConsts.UpdateType.Error.ToString());

            et.Columns.Add("key", typeof(string));
            et.Columns.Add("message", typeof(string));

            return et;
        }

        /// <summary>
        /// 重複チェック（sqlパラメータ指定あり）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="errorTable"></param>
        /// <param name="keyList"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="msg"></param>
        public void CheckDuplicate(SQLDBUtil db, DataTable errorTable, List<string> keyList, string sql, SqlParameter[] param, string msg) {

            Dictionary<string, string> keyDic = new Dictionary<string, string>();
            //全キーを取得
            SqlDataReader sdr = db.ExecuteReader(sql, param);
            while (sdr.Read()) {
                keyDic.Add(sdr["key"].ToString(), "");
            }
            //重複チェック
            foreach (string key in keyList) {
                if (keyDic.ContainsKey(key)) {
                    DataRow dr = errorTable.NewRow();
                    dr["key"] = key;
                    dr["message"] = msg;
                    errorTable.Rows.Add(dr);
                } else {
                    keyDic.Add(key, "");
                }
            }
        }

        /// <summary>
        /// 重複チェック（sqlパラメータ指定なし）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="errorTable"></param>
        /// <param name="keyList"></param>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        public void CheckDuplicate(SQLDBUtil db, DataTable errorTable, List<string> keyList, string sql, string msg) {
            CheckDuplicate(db, errorTable, keyList, sql, null, msg);
        }
    }
}
