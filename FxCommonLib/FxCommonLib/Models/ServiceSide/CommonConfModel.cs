using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCommonLib.Models.ServiceSide {
    /// <summary>
    /// 共通設定ビジネスロジッククラス
    /// </summary>
    public class CommonConfModel {

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
        public CommonConfModel(ILog logger, string connectString) {
            _logger = logger;
            _connectString = connectString;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// 共通設定情報取得(1項目)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataSet GetCommonConfItem(string key) {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);

            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@key", key));
                DataSet ret = db.ExecSelect(SQLSrc.m_common_conf.SELECT_ITEM, param.ToArray());
                ret.Tables[0].TableName = CommonConsts.TableNameMCommonConf;

                return ret;
            } finally {
                db.Close();
            }
        }

        /// <summary>
        /// 共通設定情報取得
        /// </summary>
        /// <returns></returns>
        public DataSet GetCommonConf() {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);

            try {
                db.Open(_connectString);

                DataSet ret = db.ExecSelect(SQLSrc.m_common_conf.SELECT_ALL);
                ret.Tables[0].TableName = CommonConsts.TableNameMCommonConf;

                return ret;
            } finally {
                db.Close();
            }
        }
        /// <summary>
        /// 共通設定情報更新(1項目)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetCommonConfItem(string key, string value, string note) {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);

            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@key", key));
                param.Add(new SqlParameter("@value", value));
                param.Add(new SqlParameter("@note", note));
                db.ExecuteNonQuery(SQLSrc.m_common_conf.MERGE, param.ToArray());

                db.Commit();
            } catch (Exception) {
                db.Rollback();
                throw;
            } finally {
                db.Close();
            }
        }
        /// <summary>
        /// 共通設定情報更新(1項目)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetCommonConfItem(string key, string value) {
            List<SqlParameter> param = new List<SqlParameter>();
            SQLDBUtil db = new SQLDBUtil(_logger);

            try {
                db.Open(_connectString);

                param.Add(new SqlParameter("@key", key));
                param.Add(new SqlParameter("@value", value));
                db.ExecuteNonQuery(SQLSrc.m_common_conf.MERGE_KEY_VALUE, param.ToArray());

                db.Commit();
            } catch (Exception) {
                db.Rollback();
                throw;
            } finally {
                db.Close();
            }
        }
        #endregion PublicMethods
    }
}