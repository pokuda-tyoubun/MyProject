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
    /// 検索条件設定ビジネスロジック
    /// </summary>
    public class SearchConditionModel {

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
        public SearchConditionModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// 検索条件初期設定を取得
        /// </summary>
        /// <returns></returns>
        public DataSet GetSearchItemTables(HttpRequestMessage req, string loginId) {
            DataSet ret = new DataSet();
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);

            DataSet ds = JsonConvert.DeserializeObject<DataSet>(req.Content.ReadAsStringAsync().Result);
            DataTable dt = ds.Tables[0];

            try {
                db.Open(_connectString);

                string windowName = StringUtil.NullToBlank(dt.Rows[0][CommonConsts.window_name]);
                string gridName = StringUtil.NullToBlank(dt.Rows[0][CommonConsts.grid_name]);
                param.Add(new SqlParameter("@window_name", windowName));
                param.Add(new SqlParameter("@grid_name", gridName));
                param.Add(new SqlParameter("@login_id", loginId));
                DataSet ds1 = db.ExecSelect(SQLSrc.t_search_condition.SELECT_CONDITION, param.ToArray());
                if (ds1.Tables[0].Rows.Count == 0) {
                    //自分の設定がない場合は、デフォルト設定を取得
                    List<SqlParameter> param2 = new List<SqlParameter>();
                    param2.Add(new SqlParameter("@window_name", windowName));
                    param2.Add(new SqlParameter("@grid_name", gridName));
                    param2.Add(new SqlParameter("@login_id", ""));
                    ds1 = db.ExecSelect(SQLSrc.t_search_condition.SELECT_CONDITION, param2.ToArray());
                }
                ds1.Tables[0].TableName = CommonConsts.SearchItemTbl;
                ret.Tables.Add(ds1.Tables[CommonConsts.SearchItemTbl].Copy());

                //プルダウンの候補をセット
                SetPulldownCandidate(db, ret);

                return ret;
            } finally {
                db.Close();
            }
        }
        /// <summary>
        /// 検索条件初期設定を更新(Delete-Insert)
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateSearchConditionInfo(DataTable dt, string loginId) {
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
                    db.ExecuteNonQuery(SQLSrc.t_search_condition.DELETE, delParam.ToArray());

                    //Insert
                    foreach (DataRow dr in dt.Rows) {
                        List<SqlParameter> insParam = new List<SqlParameter>();
                        insParam.Add(new SqlParameter("@window_name", dr[CommonConsts.window_name]));
                        insParam.Add(new SqlParameter("@grid_name", dr[CommonConsts.grid_name]));
                        insParam.Add(new SqlParameter("@db_name", dr[CommonConsts.db_name]));
                        insParam.Add(new SqlParameter("@login_id", loginId));
                        insParam.Add(new SqlParameter("@ctl_type", dr[CommonConsts.ctl_type]));
                        insParam.Add(new SqlParameter("@value1", dr[CommonConsts.value1]));
                        insParam.Add(new SqlParameter("@value2", dr[CommonConsts.value2]));
                        insParam.Add(new SqlParameter("@visible", dr[CommonConsts.visible]));
                        insParam.Add(new SqlParameter("@disp_order", dr[CommonConsts.disp_order]));

                        db.ExecuteNonQuery(SQLSrc.t_search_condition.INSERT, insParam.ToArray());
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

        #region ProtectedMethods
        /// <summary>
        /// プルダウンの候補をセット
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ds"></param>
        protected virtual void SetPulldownCandidate(SQLDBUtil db, DataSet ds) {
            foreach (DataRow r in ds.Tables[CommonConsts.SearchItemTbl].Rows) {
                if (r[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypePulldownlist) {
                    //プルダウンリストの候補を取得
                    DataSet dsTmp = db.ExecSelect(r[CommonConsts.value1].ToString());
                    dsTmp.Tables[0].TableName = r[CommonConsts.db_name].ToString();
                    ds.Tables.Add(dsTmp.Tables[r[CommonConsts.db_name].ToString()].Copy());
                }
            }
        }
        #endregion ProtectedMethods

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