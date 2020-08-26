using FxCommonLib.Consts;
using FxCommonLib.Consts.IM;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Models.IM {
    /// <summary>
    /// m_user
    /// (Userは予約語なのでIMUserとした)
    /// </summary>
    public class IMUser : BaseUser {
        /// <summary>ユーザID</summary>
        public string UserId { get; set; }
        /// <summary>ユーザ名</summary>
        public string UserName { get; set; }
        /// <summary>カナ</summary>
        public string UserKana { get; set; }
        /// <summary>権限コード</summary>
        public string AuthorityCode { get; set; }
        /// <summary>備考</summary>
        public string Note { get; set; }

        public override string LoginId {
            get {
                return UserId;
            }
        }
        public override string LoginName {
            get {
                return UserName;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IMUser() {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ds"></param>
        public IMUser(DataSet ds) {
            DataRow dr = ds.Tables[0].Rows[0];
            DataColumnCollection columns = ds.Tables[0].Columns;
            
            this.UserId = dr[IMConsts.user_id].ToString();
            this.UserName = dr[IMConsts.user_name].ToString();
            this.UserKana = dr[IMConsts.user_kana].ToString();
            this.AuthorityCode = dr[IMConsts.authority_code].ToString();
            this.Note = dr[CommonConsts.note].ToString();
            this.Password = dr[IMConsts.password].ToString();
            this.PasswordUpdateId = dr[IMConsts.pwd_up_id].ToString();
            this.PasswordUpdateDate = DateTimeUtil.ParseEx(dr[IMConsts.pwd_up_date].ToString());
            this.LastLoginDate = DateTimeUtil.ParseEx(dr[IMConsts.last_login_date].ToString());
            if (columns.Contains(CommonConsts.session_id)) {
                this.SessionId = dr[CommonConsts.session_id].ToString();
            } else {
                this.SessionId = "";
            }
            if (columns.Contains(IMConsts.admin_menu_lock)) {
                this.AdminMenuLock = StringUtil.ParseBool(dr[IMConsts.admin_menu_lock].ToString());
            } else {
                this.AdminMenuLock = false;
            }
        }

        /// <summary>
        /// データセットに変換
        /// </summary>
        /// <returns></returns>
        public DataSet ToDataSet() {
            DataTable dt = CreateBlankTable();
            AppendDataRow(dt);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// ブランクテーブル作成
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateBlankTable() {
            DataTable dt = new DataTable();
            dt.TableName = CommonConsts.UpdateType.RequestInfo.ToString();

            dt.Columns.Add(IMConsts.user_id, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.user_name, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.user_kana, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.authority_code, Type.GetType("System.String"));
            dt.Columns.Add(CommonConsts.note, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.password, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.pwd_up_id, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.pwd_up_date, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.last_login_date, Type.GetType("System.String"));
            dt.Columns.Add(CommonConsts.session_id, Type.GetType("System.String"));
            dt.Columns.Add(IMConsts.admin_menu_lock, Type.GetType("System.String"));

            return dt;
        }
        /// <summary>
        /// DataRowとして追加
        /// </summary>
        /// <param name="dt"></param>
        public void AppendDataRow(DataTable dt) {
            DataRow dr = dt.NewRow();
            dr[IMConsts.user_id] = this.UserId;
            dr[IMConsts.user_name] = this.UserName;
            dr[IMConsts.user_kana] = this.UserKana;
            dr[IMConsts.authority_code] = this.AuthorityCode;
            dr[CommonConsts.note] = this.Note;
            dr[IMConsts.password] = this.Password;
            dr[IMConsts.pwd_up_id] = this.PasswordUpdateId;
            dr[IMConsts.pwd_up_date] = this.PasswordUpdateDate;
            dr[IMConsts.last_login_date] = this.LastLoginDate;
            dr[CommonConsts.session_id] = this.SessionId;
            dr[IMConsts.admin_menu_lock] = this.AdminMenuLock;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        /// <summary>
        /// ステータスバーのログイン情報文字列
        /// </summary>
        /// <returns></returns>
        public string GetLoginInfoString(MultiLangUtil mlu) {
            string auth = "";
            if (AuthorityCode == IMConsts.SystemAuthorityAdmin) {
                auth = mlu.GetMsg(IMConsts.AUTH_TYPE_ADMIN);
            } else if (AuthorityCode == IMConsts.SystemAuthorityPlanner) {
                auth = mlu.GetMsg(IMConsts.AUTH_TYPE_PLANNER);
            } else if (AuthorityCode == IMConsts.SystemAuthorityWorker) {
                auth = mlu.GetMsg(IMConsts.AUTH_TYPE_WORKER);
            }

            return UserId + "／" + UserName + " （" + auth + "）";
        }
    }
}
