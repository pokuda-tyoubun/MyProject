using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using System;
using System.Data;

namespace FxCommonLib.Models.MES {
    /// <summary>
    /// ユーザ情報
    /// </summary>
    public class Client : BaseUser {

        #region Constants
        #endregion Constants

        #region Properties

        /// <summary>ユーザID</summary>
        public string Id { get; set; }
        /// <summary>WindowsPC版利用権限</summary>
        public bool IsPc { get; set; }
        /// <summary>モバイル版利用権限</summary>
        public bool IsMobile { get; set; }
        /// <summary>パスワード</summary>
        //public string Password { get; set; }
        /// <summary>権限コード</summary>
        //public string AuthorityCd { get; set; }
        /// <summary>クライアント名</summary>
        public string ClientName { get; set; }
        /// <summary>クライアント備考1</summary>
        public string Note1 { get; set; }
        /// <summary>クライアント備考2</summary>
        public string Note2 { get; set; }
        /// <summary>クライアント備考3</summary>
        public string Note3 { get; set; }
        /// <summary>クライアント備考4</summary>
        public string Note4 { get; set; }
        /// <summary>クライアント備考5</summary>
        public string Note5 { get; set; }
        /// <summary>最終ログイン日時</summary>
        //public DateTime? LastLoginDate { get; set; }
        /// <summary>最終パスワード更新者</summary>
        //public string PasswordUpdateId { get; set; }
        /// <summary>最終パスワード更新日時</summary>
        //public DateTime? PasswordUpdateDate { get; set; }
        /// <summary>最終アクセス日時(httpリクエストがあったか)</summary>
        //public DateTime? LastAccessDate { get; set; }
        /// <summary>所属グループ</summary>
        public string MemberOf { get; set; }
        /// <summary>セッションID</summary>
        //public string SessionId { get; set; }
        /// <summary>管理者メニューロック</summary>
        //public bool AdminMenuLock { get; set; }

        public override string LoginId {
            get {
                return Id;
            }
        }
        public override string LoginName {
            get {
                return ClientName;
            }
        }

        #endregion Properties

        #region MemberVariables
        #endregion MemberVariables

        #region Constractors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Client() {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ds"></param>
        public Client(DataSet ds) {
            this.Id = ds.Tables[0].Rows[0][MESConsts.client_id].ToString();
            this.IsPc = StringUtil.ParseBool(ds.Tables[0].Rows[0][MESConsts.is_pc].ToString());
            this.IsMobile = StringUtil.ParseBool(ds.Tables[0].Rows[0][MESConsts.is_mobile].ToString());
            this.Password = ds.Tables[0].Rows[0][MESConsts.password].ToString();
            this.AuthorityCd = ds.Tables[0].Rows[0][MESConsts.authority_cd].ToString();
            this.ClientName = ds.Tables[0].Rows[0][MESConsts.client_name].ToString();
            this.Note1 = ds.Tables[0].Rows[0][MESConsts.client_note1].ToString();
            this.Note2 = ds.Tables[0].Rows[0][MESConsts.client_note2].ToString();
            this.Note3 = ds.Tables[0].Rows[0][MESConsts.client_note3].ToString();
            this.Note4 = ds.Tables[0].Rows[0][MESConsts.client_note4].ToString();
            this.Note5 = ds.Tables[0].Rows[0][MESConsts.client_note5].ToString();
            this.LastLoginDate = DateTime.Parse(ds.Tables[0].Rows[0][MESConsts.last_login_date].ToString());
            this.PasswordUpdateId = ds.Tables[0].Rows[0][MESConsts.pwd_up_id].ToString();
            this.PasswordUpdateDate = DateTimeUtil.ParseEx(ds.Tables[0].Rows[0][MESConsts.pwd_up_date].ToString());
            this.LastAccessDate = DateTime.Parse(ds.Tables[0].Rows[0][MESConsts.last_access_date].ToString());
            this.MemberOf = ds.Tables[0].Rows[0][MESConsts.member_of].ToString();
            this.SessionId = ds.Tables[0].Rows[0][CommonConsts.session_id].ToString();
            this.AdminMenuLock = StringUtil.ParseBool(ds.Tables[0].Rows[0][MESConsts.admin_menu_lock].ToString());
        }

        #endregion Constractors

        #region EventHandlers
        #endregion EventHandlers

        #region PublicMethods

        /// <summary>
        /// ステータスバーのログイン情報文字列
        /// </summary>
        /// <returns></returns>
        public string GetLoginInfoString(MultiLangUtil mlu) {
            string auth = "";
            if (AuthorityCd == MESConsts.SystemAuthorityAdmin) {
                auth = mlu.GetMsg(MESConsts.AUTH_TYPE_ADMIN);
            } else if (AuthorityCd == MESConsts.SystemAuthorityPlanner) {
                auth = mlu.GetMsg(MESConsts.AUTH_TYPE_PLANNER);
            } else if (AuthorityCd == MESConsts.SystemAuthorityWorker) {
                auth = mlu.GetMsg(MESConsts.AUTH_TYPE_WORKER);
            }

            return Id + "／" + ClientName + " （" + auth + "）";
        }

        /// <summary>
        /// FLEXSCHE利用権限があるか
        /// </summary>
        /// <returns></returns>
        public bool HasFlexscheUseAuthority() {

            bool ret = false;

            switch (AuthorityCd) {

                case MESConsts.SystemAuthorityAdmin:
                    // システム管理者
                    ret = true;
                    break;
                case  MESConsts.SystemAuthorityPlanner:
                    // 計画者
                    ret = true;
                    break;
                case MESConsts.SystemAuthorityWorker:
                    // 作業者
                    ret = false;
                    break;
                default:
                    ret = false;
                    break;
            }

            return ret;

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

            dt.Columns.Add(MESConsts.client_id, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.password, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.authority_cd, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.authority_name, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_name, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.is_pc, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.is_mobile, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_note1, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_note2, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_note3, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_note4, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.client_note5, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.last_login_date, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.pwd_up_id, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.pwd_up_date, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.last_access_date, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.member_of, Type.GetType("System.String"));
            dt.Columns.Add(CommonConsts.session_id, Type.GetType("System.String"));
            dt.Columns.Add(MESConsts.admin_menu_lock, Type.GetType("System.String"));

            return dt;
        }
        /// <summary>
        /// DataRowとして追加
        /// </summary>
        /// <param name="dt"></param>
        public void AppendDataRow(DataTable dt) {
            DataRow dr = dt.NewRow();
            dr[MESConsts.client_id] = this.Id;
            dr[MESConsts.password] = this.Password;
            dr[MESConsts.authority_cd] = this.AuthorityCd;
            dr[MESConsts.authority_name] = "";
            dr[MESConsts.client_name] = this.ClientName;
            dr[MESConsts.is_pc] = this.IsPc;
            dr[MESConsts.is_mobile] = this.IsMobile;
            dr[MESConsts.client_note1] = this.Note1;
            dr[MESConsts.client_note2] = this.Note2;
            dr[MESConsts.client_note3] = this.Note3;
            dr[MESConsts.client_note4] = this.Note4;
            dr[MESConsts.client_note5] = this.Note5;
            dr[MESConsts.last_login_date] = this.LastLoginDate;
            dr[MESConsts.pwd_up_id] = this.PasswordUpdateId;
            dr[MESConsts.pwd_up_date] = this.PasswordUpdateDate;
            dr[MESConsts.last_access_date] = this.LastAccessDate;
            dr[MESConsts.member_of] = this.MemberOf;
            dr[CommonConsts.session_id] = this.SessionId;
            dr[MESConsts.admin_menu_lock] = this.AdminMenuLock;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        #endregion PublicMethods

        #region ProtectedMethods
        #endregion ProtectedMethods
        #region PrivateMethods
        #endregion PrivateMethods
    }
}
