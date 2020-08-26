using FxCommonLib.Utils;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Models.ServiceSide {
    /// <summary>
    /// メッセージ情報ビジネスロジック
    /// </summary>
    public class MessageModel {

        #region MemberVariables
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
        public MessageModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// メッセージ情報取得
        /// </summary>
        /// <returns></returns>
        public DataSet GetMessageInfo() {
            SQLDBUtil db = new SQLDBUtil(_logger);
            DataSet ret = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try {
                db.Open(_connectString);

                ret = db.ExecSelect(SQLSrc.m_message.SELECT_ALL, param.ToArray());
                ret.Tables[0].TableName = "MessageInfo";

                return ret;
            } finally {
                db.Close();
            }
        }
        #endregion PublicMethods
    }
}