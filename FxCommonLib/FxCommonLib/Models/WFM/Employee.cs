using FxCommonLib.Consts;
using FxCommonLib.Consts.WFM;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Models.WFM {
    public class Employee : BaseUser {
        /// <summary>従業員ID</summary>
        public string EmployeeId { get; set; }
        /// <summary>従業員名</summary>
        public string EmployeeName { get; set; }
        /// <summary>カナ</summary>
        public string EmployeeKana { get; set; }
        /// <summary>権限コード</summary>
        public string AuthorityCode { get; set; }
        /// <summary>所属部署</summary>
        public string DepCode { get; set; }
        /// <summary>メイングループID</summary>
        public string MainGroupId { get; set; }
        /// <summary>雇用形態</summary>
        public string EmploymentType { get; set; }
        /// <summary>扶養希望</summary>
        public bool Supported { get; set; }
        /// <summary>会社コード</summary>
        public string ComCode { get; set; }
        /// <summary>時給</summary>
        public int HourlyWage { get; set; }
        /// <summary>交通費</summary>
        public int TransportationCost { get; set; }
        /// <summary>通勤時間（分）</summary>
        public int CommutingMinutes { get; set; }
        /// <summary>備考</summary>
        public string Note { get; set; }
        /// <summary>ログインID</summary>
        public override string LoginId {
            get {
                return EmployeeId;
            }
        }
        /// <summary>ログイン名</summary>
        public override string LoginName {
            get {
                return EmployeeName;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Employee() {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ds"></param>
        public Employee(DataSet ds) {
            DataRow dr = ds.Tables[0].Rows[0];
            DataColumnCollection columns = ds.Tables[0].Columns;
            
            this.EmployeeId = dr[WFMConsts.employee_id].ToString();
            this.EmployeeName = dr[WFMConsts.employee_name].ToString();
            this.EmployeeKana = dr[WFMConsts.employee_kana].ToString();
            this.AuthorityCode = dr[WFMConsts.authority_code].ToString();
            this.DepCode = dr[WFMConsts.dep_code].ToString();
            this.MainGroupId = dr[WFMConsts.main_group_id].ToString();
            this.EmploymentType = dr[WFMConsts.employment_type].ToString();
            this.Supported = StringUtil.ParseBool(dr[WFMConsts.supported].ToString());
            this.ComCode = dr[WFMConsts.com_code].ToString();
            this.HourlyWage = int.Parse(dr[WFMConsts.hourly_wage].ToString());
            this.TransportationCost = int.Parse(dr[WFMConsts.transportation_cost].ToString());
            this.CommutingMinutes = int.Parse(dr[WFMConsts.commuting_minutes].ToString());
            this.Note = dr[CommonConsts.note].ToString();
            this.Password = dr[WFMConsts.password].ToString();
            this.PasswordUpdateId = dr[WFMConsts.pwd_up_id].ToString();
            this.PasswordUpdateDate = DateTimeUtil.ParseEx(dr[WFMConsts.pwd_up_date].ToString());
            this.LastLoginDate = DateTimeUtil.ParseEx(dr[WFMConsts.last_login_date].ToString());
            if (columns.Contains(CommonConsts.session_id)) {
                this.SessionId = dr[CommonConsts.session_id].ToString();
            } else {
                this.SessionId = "";
            }
            if (columns.Contains(WFMConsts.admin_menu_lock)) {
                this.AdminMenuLock = StringUtil.ParseBool(dr[WFMConsts.admin_menu_lock].ToString());
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

            dt.Columns.Add(WFMConsts.employee_id, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.employee_name, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.employee_kana, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.authority_code, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.authority_name, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.dep_code, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.dep_name, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.main_group_id, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.main_group_name, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.employment_type, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.supported, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.com_code, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.com_name, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.hourly_wage, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.transportation_cost, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.commuting_minutes, Type.GetType("System.String"));
            dt.Columns.Add(CommonConsts.note, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.password, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.pwd_up_id, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.pwd_up_date, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.last_login_date, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.last_access_date, Type.GetType("System.String"));
            dt.Columns.Add(CommonConsts.session_id, Type.GetType("System.String"));
            dt.Columns.Add(WFMConsts.admin_menu_lock, Type.GetType("System.String"));

            return dt;
        }
        /// <summary>
        /// DataRowとして追加
        /// </summary>
        /// <param name="dt"></param>
        public void AppendDataRow(DataTable dt) {
            DataRow dr = dt.NewRow();
            dr[WFMConsts.employee_id] = this.EmployeeId;
            dr[WFMConsts.employee_name] = this.EmployeeName;
            dr[WFMConsts.employee_kana] = this.EmployeeKana;
            dr[WFMConsts.authority_code] = this.AuthorityCode;
            dr[WFMConsts.authority_name] = "";
            dr[WFMConsts.dep_code] = this.DepCode;
            dr[WFMConsts.dep_name] = "";
            dr[WFMConsts.main_group_id] = this.MainGroupId;
            dr[WFMConsts.main_group_name] = "";
            dr[WFMConsts.employment_type] = this.EmploymentType;
            dr[WFMConsts.supported] = this.Supported;
            dr[WFMConsts.com_code] = this.ComCode;
            dr[WFMConsts.com_name] = "";
            dr[WFMConsts.hourly_wage] = this.HourlyWage;
            dr[WFMConsts.transportation_cost] = this.TransportationCost;
            dr[WFMConsts.commuting_minutes] = this.CommutingMinutes;
            dr[CommonConsts.note] = this.Note;
            dr[WFMConsts.password] = this.Password;
            dr[WFMConsts.pwd_up_id] = this.PasswordUpdateId;
            dr[WFMConsts.pwd_up_date] = this.PasswordUpdateDate;
            dr[WFMConsts.last_login_date] = this.LastLoginDate;
            dr[WFMConsts.last_access_date] = this.LastAccessDate;
            dr[CommonConsts.session_id] = this.SessionId;
            dr[WFMConsts.admin_menu_lock] = this.AdminMenuLock;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        /// <summary>
        /// ステータスバーのログイン情報文字列
        /// </summary>
        /// <returns></returns>
        public string GetLoginInfoString(MultiLangUtil mlu) {
            string auth = "";
            if (AuthorityCode == WFMConsts.SystemAuthorityAdmin) {
                auth = mlu.GetMsg(WFMConsts.AUTH_TYPE_ADMIN);
            } else if (AuthorityCode == WFMConsts.SystemAuthorityPlanner) {
                auth = mlu.GetMsg(WFMConsts.AUTH_TYPE_PLANNER);
            } else if (AuthorityCode == WFMConsts.SystemAuthorityWorker) {
                auth = mlu.GetMsg(WFMConsts.AUTH_TYPE_WORKER);
            }

            return EmployeeId + "／" + EmployeeName + " （" + auth + "）";
        }
    }
}
