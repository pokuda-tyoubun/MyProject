using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Models {
    /// <summary>
    /// 共通設定ビジネスロジッククラス
    /// </summary>
    public class DataDictionaryModel {

        /// <summary>
        /// データディクショナリ情報取得
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataDictionary(ILog logger, string connectString) {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(logger);

            try {
                db.Open(connectString);

                DataSet ret = db.ExecSelect(SQLSrc.m_data_dictionary.SELECT_ALL);
                ret.Tables[0].TableName = CommonConsts.TableNameMDataDictionary;

                return ret;
            } finally {
                db.Close();
            }
        }
    }
}