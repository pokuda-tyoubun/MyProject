using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Controls;
using FxCommonLib.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 列定義ユーティリティ
    /// </summary>
    public class ColumnConfigUtil {

        #region Properties
        /// <summary>Window名</summary>
        public string WindowName { get; set; }
        /// <summary>Grid名</summary>
        public string GridName { get; set; }
        /// <summary>列定義情報</summary>
        public ColumnConfig ColConf { get; set; }
        /// <summary>呼称列名をキーにした列情報辞書</summary>
        private Dictionary<string, ColumnInfo> _dicByName = new Dictionary<string, ColumnInfo>();
        public Dictionary<string, ColumnInfo> DictionaryByName {
            set {_dicByName = value; }
            get { return _dicByName; }
        }
        /// <summary>DB列名をキーにした列情報辞書</summary>
        private Dictionary<string, ColumnInfo> _dicByDBName = new Dictionary<string, ColumnInfo>();
        public Dictionary<string, ColumnInfo> DictionaryByDBName {
            set {_dicByDBName = value; }
            get { return _dicByDBName; }
        }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="gridName"></param>
        public ColumnConfigUtil(string windowName, string gridName) {
            WindowName = windowName;
            GridName = gridName;
            ColConf = new ColumnConfig();
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// 列状態を保存
        /// </summary>
        public void Save(FlexGridEx grid) {
            //グリッドの状態を設定
            foreach (Column c in grid.Cols) {
                ColumnInfo ci = _dicByName[c.Name];
                ci.Visible = c.Visible;
                ci.Width = c.WidthDisplay;
            }

            //XMLファイルに保存
            XmlSerializer serializer = new XmlSerializer(typeof(ColumnConfig));
            //書き込むファイルを開く（UTF-8 BOM無し）
            using (StreamWriter sw = new StreamWriter(@".\ColumnConfigXML\" + WindowName + "_" + GridName + ".xml", false, new UTF8Encoding(false))) {
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, ColConf);
            }
        }
        /// <summary>
        /// 列状態を読込み
        /// </summary>
        public void Load() {
            XmlSerializer serializer = new XmlSerializer(typeof(ColumnConfig));
            using (StreamReader sr = new StreamReader(@".\ColumnConfigXML\" + WindowName + "_" + GridName + ".xml", new UTF8Encoding(false))) {
                ColConf = (ColumnConfig)serializer.Deserialize(sr);
            }
            CreateDictionary();
        }
        /// <summary>
        /// 辞書作成
        /// </summary>
        public void CreateDictionary() {
            foreach (ColumnInfo ci in ColConf.ColList) {
                _dicByName.Add(ci.ColName, ci);
                _dicByDBName.Add(ci.DBName, ci);
            }
        }
        /// <summary>
        /// 列名をDB列名から呼称列名に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable CopyAsDisplayData(DataTable dt) {
            if (dt.Columns.Count == 0) {
                //検索結果が0件の場合は、列定義より列を生成する。
                foreach (ColumnInfo ci in this.ColConf.ColList) {
                    dt.Columns.Add(ci.DBName);
                }
            }

            ////表示用の列名に変更
            DataTable newTable = null;
            DeepCopyUtil dcu = new DeepCopyUtil();
            newTable = (DataTable)dcu.DeepCopy(dt);
            foreach (DataColumn dc in newTable.Columns) {
                if (_dicByDBName.ContainsKey(dc.ColumnName)) {
                    ColumnInfo ci = _dicByDBName[dc.ColumnName];
                    dc.ColumnName = ci.ColName;

                }
            }

            return newTable;
        }
        /// <summary>
        /// 呼称列名から列名をDB列名に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void ConvertNameToDBName(DataTable dt) {
            foreach (DataColumn dc in dt.Columns) {
                ColumnInfo ci = _dicByName[dc.ColumnName];
                dc.ColumnName = ci.DBName;
            }
        }
        /// <summary>
        /// 列定義テーブルのデータテーブル取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable() {
            DataTable dt = new DataTable();
            //列設定
            dt.Columns.Add(CommonConsts.window_name);
            dt.Columns.Add(CommonConsts.grid_name);
            dt.Columns.Add(CommonConsts.db_name);
            dt.Columns.Add(CommonConsts.width);
            dt.Columns.Add(CommonConsts.height);
            dt.Columns.Add(CommonConsts.conf_editable);
            dt.Columns.Add(CommonConsts.visible);
            dt.Columns.Add(CommonConsts.editable);
            dt.Columns.Add(CommonConsts.disp_order);
            dt.Columns.Add(CommonConsts.col_fixed);
            dt.Columns.Add(CommonConsts.data_type);
            dt.Columns.Add(CommonConsts.max_length);
            dt.Columns.Add(CommonConsts.required);
            dt.Columns.Add(CommonConsts.primary_key);
            dt.Columns.Add(CommonConsts.password_char);
            dt.Columns.Add(CommonConsts.note);

            int i = 1;
            var query = from ci in ColConf.ColList
                        orderby ci.DisplayOrder
                        select ci; 
            foreach (ColumnInfo ci in query) {
                DataRow dr = dt.NewRow();

                dr[CommonConsts.window_name] = WindowName;
                dr[CommonConsts.grid_name] = GridName;
                dr[CommonConsts.db_name] = ci.DBName;
                dr[CommonConsts.width] = ci.Width.ToString();
                dr[CommonConsts.height] = ColConf.Height.ToString();
                dr[CommonConsts.conf_editable] = ci.ConfEditable ? "1" : "0";
                dr[CommonConsts.visible] = ci.Visible ? "1" : "0";
                dr[CommonConsts.editable] = ci.Editable ? "1" : "0";
                dr[CommonConsts.disp_order] = ci.DisplayOrder.ToString();
                if (i <= ColConf.LeftFixedCount) {
                    dr[CommonConsts.col_fixed] = "1";
                } else {
                    dr[CommonConsts.col_fixed] = "0";
                }
                dr[CommonConsts.data_type] = ci.DataType;
                dr[CommonConsts.max_length] = ci.MaxLength.ToString();
                dr[CommonConsts.required] = ci.Required ? "1" : "0";
                dr[CommonConsts.primary_key] = ci.IsPrimaryKey ? "1" : "0";
                dr[CommonConsts.password_char] = ci.PasswordChar;
                dr[CommonConsts.note] = ci.Note;

                dt.Rows.Add(dr);
                i++;
            }
            dt.AcceptChanges();

            return dt;
        }

        public DataTable GetBlankDataTable() {
            var ret = new DataTable();
            foreach (ColumnInfo ci in ColConf.ColList) {
                ret.Columns.Add(ci.DBName, typeof(string));
            }
            return ret;
        }
        #endregion PublicMethods
    }
}
