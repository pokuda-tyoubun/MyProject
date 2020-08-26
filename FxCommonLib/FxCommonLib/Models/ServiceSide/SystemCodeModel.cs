using FxCommonLib.Utils;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Models.ServiceSide {
    /// <summary>
    /// システムコード情報ビジネスロジック
    /// </summary>
    public class SystemCodeModel {

        #region MemberVariables
        /// <summary>ディープコピーユーティリティ</summary>
        private DeepCopyUtil _dcu = new DeepCopyUtil();
        /// <summary>接続文字列</summary>
        private string _connectString = "";
        /// <summary>ロガー</summary>
        private ILog _logger = null;
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="connectString"></param>
        public SystemCodeModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// システムコード情報を取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetSystemCodeInfo(string category) {
            SQLDBUtil db = new SQLDBUtil(_logger);
            DataSet ret = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@category", category));
                ret = db.ExecSelect(SQLSrc.m_system_code.SELECT_CANDIDATE, param.ToArray());
                ret.Tables[0].TableName = category;

                return (DataTable)_dcu.DeepCopy(ret.Tables[0]);
            } finally {
                db.Close();
            }
        }
        #endregion PublicMethods
    }
}