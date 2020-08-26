using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.IM;
using FxCommonLib.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Models.ServiceSide {
    /// <summary>
    /// ウィンドウ表示状態管理ビジネスロジック
    /// </summary>
    public class WindowStateModel {

        #region MemberVariables
        /// <summary>ロックオブジェクト</summary>
        private static Object _lockObj = new Object();
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
        public WindowStateModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// ウィンドウ表示状態取得
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public DataTable GetWindowState(string loginId, string windowName) {
            SQLDBUtil db = new SQLDBUtil(_logger);
            DataTable ret = null;
            DataSet tmp = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@window_name", windowName));
                param.Add(new SqlParameter("@login_id", ""));
                tmp = db.ExecSelect(SQLSrc.t_window_state.SELECT_STATE, param.ToArray());
                ret = tmp.Tables[0].Clone();
                foreach (DataRow dr in tmp.Tables[0].Rows) {
                    string ctlName = dr[CommonConsts.control_name].ToString();
                    DataTable tmpTbl = GetWindowState(loginId, windowName, ctlName);
                    DataRow newRow = ret.NewRow();
                    newRow.ItemArray = tmpTbl.Rows[0].ItemArray;
                    ret.Rows.Add(newRow);
                }
                ret.TableName = "WindowState";

                return ret;
            } finally {
                db.Close();
            }
        }
        /// <summary>
        /// ウィンドウ表示状態取得
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="windowName"></param>
        /// <param name="ctlName"></param>
        /// <returns></returns>
        public DataTable GetWindowState(string loginId, string windowName, string ctlName) {
            SQLDBUtil db = new SQLDBUtil(_logger);
            DataSet ret = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@window_name", windowName));
                param.Add(new SqlParameter("@control_name", ctlName));
                param.Add(new SqlParameter("@login_id", loginId));
                ret = db.ExecSelect(SQLSrc.t_window_state.SELECT_STATE_ONE, param.ToArray());
                if (ret.Tables[0].Rows.Count == 0) {
                    List<SqlParameter> param2 = new List<SqlParameter>();
                    param2.Add(new SqlParameter("@window_name", windowName));
                    param2.Add(new SqlParameter("@control_name", ctlName));
                    param2.Add(new SqlParameter("@login_id", ""));
                    ret = db.ExecSelect(SQLSrc.t_window_state.SELECT_STATE_ONE, param2.ToArray());
                }
                ret.Tables[0].TableName = "WindowState";

                return (DataTable)_dcu.DeepCopy(ret.Tables[0]);
            } finally {
                db.Close();
            }
        }

        /// <summary>
        /// Window状態を更新(Delete-Insert)
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateWindowStateInfo(DataTable dt, string loginId) {
            SQLDBUtil db = new SQLDBUtil(_logger);

            lock (_lockObj) {
                try {
                    db.Open(_connectString);
                    db.BeginTransaction();

                    DataRow r = dt.Rows[0];

                    foreach (DataRow dr in dt.Rows) {
                        //Delete
                        List<SqlParameter> delParam = new List<SqlParameter>();
                        delParam.Add(new SqlParameter("@window_name", r[CommonConsts.window_name]));
                        delParam.Add(new SqlParameter("@control_name", dr[CommonConsts.control_name]));
                        delParam.Add(new SqlParameter("@login_id", loginId));
                        db.ExecuteNonQuery(SQLSrc.t_window_state.DELETE, delParam.ToArray());

                        //Insert
                        List<SqlParameter> insParam = new List<SqlParameter>();
                        insParam.Add(new SqlParameter("@window_name", dr[CommonConsts.window_name]));
                        insParam.Add(new SqlParameter("@control_name", dr[CommonConsts.control_name]));
                        insParam.Add(new SqlParameter("@login_id", loginId));
                        insParam.Add(new SqlParameter("@conf_value", dr[CommonConsts.conf_value]));

                        db.ExecuteNonQuery(SQLSrc.t_window_state.INSERT, insParam.ToArray());
                    }

                    db.Commit();
                } catch (Exception) {
                    db.Rollback();
                    throw;
                } finally {
                    db.Close();
                }
            }
        }
        #endregion PublicMethods
    }
}