using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;

namespace FxCommonLib.Models.ServiceSide {
    /// <summary>
    /// グリッド設定情報を取得
    /// </summary>
    public class GridConfigModel {

        #region MemberVariables
        /// <summary>ロックオブジェクト</summary>
        private static Object _lockObj = new Object();

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
        public GridConfigModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// グリッド設定情報を取得
        /// </summary>
        /// <returns></returns>
        public DataSet GetGridConfig(HttpRequestMessage req, string loginId) {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(req.Content.ReadAsStringAsync().Result);
            DataTable dt = ds.Tables[0];
            string windowName = StringUtil.NullToBlank(dt.Rows[0][CommonConsts.window_name]);
            string gridName = StringUtil.NullToBlank(dt.Rows[0][CommonConsts.grid_name]);

            return GetGridConfig(windowName, gridName, loginId);
        }
        /// <summary>
        /// グリッド設定情報を取得
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="gridName"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public DataSet GetGridConfig(string windowName, string gridName, string loginId) {
            DataSet ret = null;
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);
            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@window_name", windowName));
                param.Add(new SqlParameter("@grid_name", gridName));
                param.Add(new SqlParameter("@login_id", loginId));
                ret = db.ExecSelect(SQLSrc.t_grid_config.SELECT_CONDITION, param.ToArray());
                if (ret.Tables[0].Rows.Count == 0) {
                    //自分の設定がない場合は、デフォルト設定を取得
                    List<SqlParameter> param2 = new List<SqlParameter>();
                    param2.Add(new SqlParameter("@window_name", windowName));
                    param2.Add(new SqlParameter("@grid_name", gridName));
                    param2.Add(new SqlParameter("@login_id", ""));
                    ret = db.ExecSelect(SQLSrc.t_grid_config.SELECT_CONDITION, param2.ToArray());
                }
                ret.Tables[0].TableName = "GridConfig";

                return ret;
            } finally {
                db.Close();
            }
        }
        /// <summary>
        /// グリッド設定を更新(Delete-Insert)
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateGridConfigInfo(DataTable dt, string loginId) {
            SQLDBUtil db = new SQLDBUtil(_logger);

            SetNewDispOrder(dt);

            lock (_lockObj) {
                try {
                    db.Open(_connectString);
                    db.BeginTransaction();

                    //Delete
                    DataRow r = dt.Rows[0];
                    List<SqlParameter> delParam = new List<SqlParameter>();
                    delParam.Add(new SqlParameter("@window_name", r[CommonConsts.window_name]));
                    delParam.Add(new SqlParameter("@grid_name", r[CommonConsts.grid_name]));
                    delParam.Add(new SqlParameter("@login_id", loginId));
                    db.ExecuteNonQuery(SQLSrc.t_grid_config.DELETE, delParam.ToArray());

                    //Insert
                    foreach (DataRow dr in dt.Rows) {
                        List<SqlParameter> insParam = new List<SqlParameter>();
                        insParam.Add(new SqlParameter("@window_name", dr[CommonConsts.window_name]));
                        insParam.Add(new SqlParameter("@grid_name", dr[CommonConsts.grid_name]));
                        insParam.Add(new SqlParameter("@db_name", dr[CommonConsts.db_name]));
                        insParam.Add(new SqlParameter("@login_id", loginId));
                        insParam.Add(new SqlParameter("@width", dr[CommonConsts.width]));
                        insParam.Add(new SqlParameter("@height", dr[CommonConsts.height]));
                        insParam.Add(new SqlParameter("@conf_editable", dr[CommonConsts.conf_editable]));
                        insParam.Add(new SqlParameter("@visible", dr[CommonConsts.visible]));
                        insParam.Add(new SqlParameter("@editable", dr[CommonConsts.editable]));
                        insParam.Add(new SqlParameter("@disp_order", dr[CommonConsts.disp_order]));
                        insParam.Add(new SqlParameter("@col_fixed", dr[CommonConsts.col_fixed]));
                        insParam.Add(new SqlParameter("@data_type", dr[CommonConsts.data_type]));
                        insParam.Add(new SqlParameter("@max_length", dr[CommonConsts.max_length]));
                        insParam.Add(new SqlParameter("@required", dr[CommonConsts.required]));
                        insParam.Add(new SqlParameter("@primary_key", dr[CommonConsts.primary_key]));
                        insParam.Add(new SqlParameter("@password_char", dr[CommonConsts.password_char]));
                        insParam.Add(new SqlParameter("@note", dr[CommonConsts.note]));

                        db.ExecuteNonQuery(SQLSrc.t_grid_config.INSERT, insParam.ToArray());
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

        #region PrivateMethods
        /// <summary>
        /// ユーザ入力を反映させて表示順を再設定
        /// </summary>
        /// <param name="dt"></param>
        private void SetNewDispOrder(DataTable dt) {
            //列を追加
            dt.Columns.Add("disp_order2", Type.GetType("System.Double"));
            dt.Columns.Add("disp_order3", Type.GetType("System.Double"));

            int i = 1;
            foreach (DataRow dr in dt.Rows) {
                if (dr["disp_order"].ToString() == "") {
                    dr["disp_order2"] = double.MaxValue.ToString();
                } else {
                    dr["disp_order2"] = double.Parse(dr["disp_order"].ToString());
                }
                dr["disp_order3"] = i;
                i++;
            }

            //ソートして採番
            DataRow[] rows = (
                from row in dt.AsEnumerable()
                let dispOrder2 = row.Field<double>("disp_order2")
                let dispOrder3 = row.Field<double>("disp_order3")
                orderby dispOrder2, dispOrder3
                select row).ToArray();
            i = 1;
            foreach (DataRow dr in rows) {
                dr["disp_order"] = i;
                i++;
            }
        }
        #endregion PrivateMethods
    }
}