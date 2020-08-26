using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Exceptions;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FxCommonLib.Utils {
    /// <summary>
    /// SQLServer接続(ADO.Net)ユーティリティ
    /// </summary>
    public class SQLDBUtil : IDisposable {

        #region Constants
        /// <summary>主キー制約違反</summary>
        public const int PkeyViolation = 2627;
        /// <summary>NotNull制約違反</summary>
        public const int NullViolation = 515;
        #endregion Constants

        #region Properties
        /// <summary>トランザクションクラス</summary>
        private System.Data.SqlClient.SqlTransaction _trans;
        public SqlTransaction Transaction {
            get { return _trans; }
        }
        /// <summary>SqlCommandクラス</summary>
        private SqlCommand _cmd;
        public SqlCommand SqlCommand {
            get { return _cmd; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>ロガー</summary>
        private ILog _logger;
        /// <summary>コネクションクラス</summary>
        private SqlConnection _con = new SqlConnection();
        public SqlConnection Connection {
            get { return _con; }
        }
        /// <summary>接続文字列</summary>
        private string _connectString;
        /// <summary>SqlDataAdapterクラス</summary>
        private SqlDataAdapter _da;
        public SqlDataAdapter SqlDataAdapter {
            get { return _da; }
        }
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public SQLDBUtil(ILog logger) {
            _logger = logger;
        }
        #endregion Constractors

        #region Destractor
        /// <summary>
        /// デストラクタ
        /// </summary>
        ~SQLDBUtil() {
            Dispose(false);
        }
        #endregion Destractor

        #region PublicMethods
        /// <summary>
        /// Dispose処理
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="connectString"></param>
        /// <remarks>ODP.Netはデフォルトでコネクションプールが有効になっている。
        /// なので、使用前でOpenし、使用後にCloseする方がよい。</remarks>
        public void Open(string connectString) {
            _connectString = connectString;
            _con.ConnectionString = _connectString;
            try {
                _logger.Info("Open:" + connectString);
                _con.Open();
            } catch (Exception ex) {
                _logger.Error(ex.ToString() + Environment.NewLine + ex.StackTrace);
                Close();
                throw;
            } finally {
            }
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        /// <remarks></remarks>
        public void BeginTransaction() {
            _trans = _con.BeginTransaction();
            _logger.Info("BeginTransaction:" + _trans.ToString());
        }

        /// <summary>
        /// コミット
        /// </summary>
        /// <remarks></remarks>
        public void Commit() {
            if (_trans != null) {
                _trans.Commit();
                _logger.Info("Commit:" + _trans.ToString());
                _trans = null;
            }
        }

        /// <summary>
        /// ロールバック
        /// </summary>
        /// <remarks></remarks>
        public void Rollback() {
            if (_trans != null) {
                _trans.Rollback();
                _logger.Info("Rollback:" + _trans.ToString());
                _trans = null;
            }
        }

        /// <summary>
        /// DataSet を使用せずに、UPDATE、INSERT、または DELETE ステートメントを実行。
        /// </summary>
        /// <param name="sql"></param>
        /// <remarks></remarks>
        public int ExecuteNonQuery(string sql, SqlParameter[] param, CommandType cmdType = CommandType.Text, int timeout = 60) {
            try {
                _cmd = _con.CreateCommand();
                _cmd.CommandType = cmdType;
                _cmd.CommandTimeout = timeout;
                if (_trans != null) {
                    _cmd.Transaction = _trans;
                }
                _cmd.CommandText = sql;
                //引数セット
                if (param != null) {
                    _cmd.Parameters.AddRange(param);
                }
                _logger.Info("ExecuteNonQuery:" + Environment.NewLine + sql);
                if (param != null) {
                    _logger.Info("Params:" + ParamToString(param));
                }

                return _cmd.ExecuteNonQuery();
            } finally {
            }
        }

        /// <summary>
        /// DataAdapterのUpdateComandを自動生成
        /// </summary>
        /// <remarks></remarks>
        public void BuildUpdateCommand() {
            //UpdateCommandを作成
            SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(_da);
            sqlCmdBuilder.GetUpdateCommand();
        }

        /// <summary>
        /// SELECT文実行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet ExecSelect(string sql, SqlParameter[] param = null, int maxRow = 0, string maxErrMsg = "", int timeout = 60) {
            _cmd = new SqlCommand();
            _da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try {
                _cmd = _con.CreateCommand();
                _cmd.CommandText = sql;
                _cmd.CommandTimeout = timeout;
                if (_trans != null) {
                    _cmd.Transaction = _trans;
                }
                //引数セット
                if (param != null) {
                    _cmd.Parameters.AddRange(param);
                }
                _da.SelectCommand = _cmd;
                _logger.Info("ExecSelect:" + Environment.NewLine + sql);
                if (param != null) {
                    _logger.Info("Params:" + ParamToString(param));
                }

                _da.Fill(ds);
                if (maxRow > 0) {
                    if (ds.Tables[0].Rows.Count >= maxRow) {
                        throw new MaxRowsException(string.Format(maxErrMsg, maxRow));
                    }
                }
            } catch (Exception ex) {
                _logger.Error(ex.ToString() + Environment.NewLine + ex.StackTrace);
                throw;
            } finally {
            }

            return ds;
        }

        /// <summary>
        /// 読取専用クエリを実行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public SqlDataReader ExecuteReader(string sql, SqlParameter[] param, CommandType cmdType = CommandType.Text, int timeout = 60) {
            _cmd = new SqlCommand();
            SqlDataReader odr = null;

            try {
                _cmd = _con.CreateCommand();
                _cmd.CommandType = cmdType;
                _cmd.CommandText = sql;
                _cmd.CommandTimeout = timeout;
                if (_trans != null) {
                    _cmd.Transaction = _trans;
                }
                //引数セット
                if (param != null) {
                    _cmd.Parameters.AddRange(param);
                }
                _logger.Info("ExecuteReader:" + Environment.NewLine + sql);
                if (param != null) {
                    _logger.Info("Params:" + ParamToString(param));
                }

                odr = _cmd.ExecuteReader();
            } catch (Exception ex) {
                _logger.Error(ex.ToString() + Environment.NewLine + ex.StackTrace);
                throw;
            } finally {
            }

            return odr;
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        /// <remarks></remarks>
        public void Close() {
            _logger.Info("Close:" + _connectString);
            _con.Close();
        }

        /// <summary>
        /// パラメータの値を文字列に変換
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ParamToString(SqlParameter[] param) {
            StringBuilder sb = new StringBuilder();

            foreach (SqlParameter p in param) {
                sb.Append(p.ParameterName);
                sb.Append("=");
                sb.Append("[");
                sb.Append(StringUtil.NullToBlank(p.Value));
                sb.Append("]");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Where句生成
        /// HACK : PreparedStatementバージョンを作成する。(SQLキャッシュとSQLインジェクション対応のため)
        /// 暫定的にシングルクォートをエスケープで対応
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string ParamToWhere(DataTable dt, string prefix = "") {
            StringBuilder sb = new StringBuilder();

            foreach(DataRow dr in dt.Rows) {
                string val1 = StringUtil.NullToBlank(dr["value1"]);
                string val2 = "";
                if (dt.Columns.Contains("value2")) {
                    val2 = StringUtil.NullToBlank(dr["value2"]);
                }

                if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeNumericRange) {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" >= CAST('");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append("' AS REAL)");
                    }
                    if (val2 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" <= CAST('");
                        sb.Append(EscapeSqlChar(val2));
                        sb.Append("' AS REAL)");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeDateRange) {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" >= CAST('");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append(".00");
                        sb.Append("' AS DATETIME2(2))");
                    }
                    if (val2 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" <= CAST('");
                        sb.Append(EscapeSqlChar(val2));
                        sb.Append(".99");
                        sb.Append("' AS DATETIME2(2))");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeTimeRange) {
                    //時間だけの型がないので文字列比較として扱う
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" >= '");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append("'");
                    }
                    if (val2 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" <= '");
                        sb.Append(EscapeSqlChar(val2));
                        sb.Append("'");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeValidDate) {
                    // 基準日付が適用開始終了年月日の範囲内
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(" CAST('");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append(".00");
                        sb.Append("' AS DATETIME2(2))");
                        sb.Append(" between ");
                        sb.Append(" ISNULL(");
                        sb.Append(prefix);
                        sb.Append("[valid_start], '1970/01/01 00:00:00'");
                        sb.Append(" )");
                        sb.Append(" and ");
                        sb.Append(" ISNULL(");
                        sb.Append(prefix);
                        sb.Append("[valid_end], '9999/12/31 23:59:59'");
                        sb.Append(" )");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypePulldownlist) {
                    if (val2 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" like '");
                        sb.Append(EscapeSqlChar(val2));
                        sb.Append("'");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == MESConsts.CtlTypeGroupList) {
                    if (val1 == CommonConsts.AllCandidate) {
                        //条件をセットしない
                    } else {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" like '");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append("'");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeString) {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" like '%");
                        //'*'を任意の文字とし間*の検索を可能とする。
                        sb.Append(EscapeSqlChar(val1).Replace('*', '%'));
                        sb.Append("%'");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypeWildCardSpecifiableString) {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" like '");
                        //'*'を任意の文字とし間*の検索を可能とする。
                        sb.Append(EscapeSqlChar(val1).Replace('*', '%'));
                        sb.Append("'");
                    }
                } else if (StringUtil.NullToBlank(dr[CommonConsts.ctl_type]) == CommonConsts.CtlTypePerfectMatchString) {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" = '");
                        sb.Append(val1);
                        sb.Append("'");
                    }
                } else {
                    if (val1 != "") {
                        sb.Append(" and ");
                        sb.Append(prefix);
                        sb.Append("[");
                        sb.Append(StringUtil.NullToBlank(dr["db_name"]));
                        sb.Append("]");
                        sb.Append(" like '");
                        sb.Append(EscapeSqlChar(val1));
                        sb.Append("'");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// SQL特殊文字をエスケープ
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string EscapeSqlChar(string val) {
            val = val.Replace("[", "[[]");
            val = val.Replace("]", "[]]");
            val = val.Replace("%", "[%]");
            val = val.Replace("_", "[_]");
            val = val.Replace("'", "''");
            return val;
        }
        public string AppendTopLimit(string sql, int limit) {
            sql = StringUtil.ReplaceByForwardCount(sql, "SELECT", "SELECT TOP " + limit.ToString(), 1, true);
            return sql;
        }

        /// <summary>
        /// 値なしのPramToWhere用データテーブルを取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetBlankParam(string table_name) {
            DataTable dt = new DataTable(table_name);
            //dt.Columns.Add(CommonConsts.window_name, typeof(string));
            //dt.Columns.Add(CommonConsts.grid_name, typeof(string));
            dt.Columns.Add(CommonConsts.db_name, typeof(string));
            dt.Columns.Add(CommonConsts.ctl_type, typeof(string));
            dt.Columns.Add(CommonConsts.value1, typeof(string));
            dt.Columns.Add(CommonConsts.value2, typeof(string));

            dt.AcceptChanges();

            return dt;
        }
        #endregion PublicMethods

        #region ProtectedMethods
        /// <summary>
        /// Dispose処理
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // 管理（managed）リソースの破棄処理をここに記述します。
                if (_cmd != null) {
                    _cmd.Dispose();
                }
                if (_da != null) {
                    _da.Dispose();
                }
                _con.Dispose();
            }
            // 非管理（unmanaged）リソースの破棄処理をここに記述します。
        }
        #endregion ProtectedMethods
    }
}
