using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Taskbar;
using PokudaSearch.IndexUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;
<<<<<<< HEAD
using FxCommonLib.Controls;
using FxCommonLib.Log4NetAppender;
using log4net.Appender;
=======
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正

namespace PokudaSearch.Views {
    public partial class IndexBuildForm : Form {

        #region Constants
        public enum ActiveIndexColIdx : int {
            [EnumLabel("パス")]
            IndexedPath = 0,
            [EnumLabel("インデックスパス")]
            IndexStorePath,
            [EnumLabel("モード")]
            CreateMode,
            [EnumLabel("作成時間(分)")]
            CreateTime,
            [EnumLabel("対象ファイル数")]
            TargetCount,
            [EnumLabel("インデックス済み数")]
            IndexedCount,
            [EnumLabel("インデックス対象外数")]
            SkippedCount,
            [EnumLabel("総バイト数")]
            TotalBytes,
            [EnumLabel("テキスト抽出器")]
            TextExtractMode,
            [EnumLabel("作成日")]
            InsertDate, 
            [EnumLabel("更新日")]
            UpdateDate
        }
        private enum IndexHistoryColIdx : int {
            [EnumLabel("予約No")]
            ReservedNo = 0,
            [EnumLabel("モード")]
            CreateMode,
            [EnumLabel("パス")]
            IndexedPath,
            [EnumLabel("作成開始")]
            StartTime,
            [EnumLabel("作成完了")]
            EndTime,
            [EnumLabel("作成時間(分)")]
            CreateTime,
            [EnumLabel("対象ファイル数")]
            TargetCount,
            [EnumLabel("インデックス済み数")]
            IndexedCount,
            [EnumLabel("インデックス対象外数")]
            SkippedCount,
            [EnumLabel("総バイト数")]
            TotalBytes,
            [EnumLabel("テキスト抽出器")]
            TextExtractMode
        }
        /// <summary>行ヘッダー数</summary>
        private const int HeaderRowCount = 1;
        #endregion Constants

        #region Properties
        /// <summary>インデックス作成履歴</summary>
        private DataTable _history = null;
        public DataTable IndexedHistory {
            get { return _history; }
        }
        /// <summary>有効インデックス</summary>
        private static DataTable _activeIndex = null;
        public DataTable ActiveIndex {
            get { return _activeIndex; }
        }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IndexBuildForm() {
            InitializeComponent();
<<<<<<< HEAD
=======

            _history = ReadHistoryCSV();
            LoadHistory(_history);
            LoadActiveIndex();
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
        }

        #endregion Constractors

<<<<<<< HEAD
        /// <summary>
        /// 非アクティブになっているインデックスを削除
        /// </summary>
        /// <param name="row"></param>
        private void DeleteNonActiveIndex() {
            var dic = new Dictionary<string, string>();

            DataTable dt = SelectActiveIndex();
            foreach (DataRow dr in dt.Rows) {
                string storePath = StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.IndexStorePath)]);
                dic.Add(storePath, "");
            }
               
            //非アクティブインデックスを削除
            DirectoryInfo dir = new DirectoryInfo(AppObject.RootDirPath);
            List<DirectoryInfo> dirList = new List<DirectoryInfo>(dir.GetDirectories());
            foreach (DirectoryInfo di in dirList) {
                if (!dic.ContainsKey(di.FullName)) {
                    //削除
                    FileUtil.DeleteDirectory(di.FullName);
                }
            }

        }
        /// <summary>
        /// 有効Indexを更新
        /// </summary>
        /// <param name="row"></param>

        private void UpdateActiveIndex(DataRow row, string storePath = "") {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@パス", row[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedPath)]));
                if (StringUtil.NullToBlank(storePath) == "") {
                    param.Add(new SQLiteParameter("@インデックスパス", AppObject.RootDirPath + @"\Index" + LuceneIndexBuilder.ReservedNo));
                } else {
                    param.Add(new SQLiteParameter("@インデックスパス", storePath));
                }
                param.Add(new SQLiteParameter("@モード", row[EnumUtil.GetLabel(IndexHistoryColIdx.CreateMode)]));
                param.Add(new SQLiteParameter("@作成時間(分)", row[EnumUtil.GetLabel(IndexHistoryColIdx.CreateTime)]));
                param.Add(new SQLiteParameter("@対象ファイル数", row[EnumUtil.GetLabel(IndexHistoryColIdx.TargetCount)]));
                param.Add(new SQLiteParameter("@インデックス済み", row[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedCount)]));
                param.Add(new SQLiteParameter("@インデックス対象外", row[EnumUtil.GetLabel(IndexHistoryColIdx.SkippedCount)]));
                param.Add(new SQLiteParameter("@総バイト数", row[EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes)]));
                param.Add(new SQLiteParameter("@テキスト抽出器", row[EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode)]));
                param.Add(new SQLiteParameter("@作成完了", row[EnumUtil.GetLabel(IndexHistoryColIdx.EndTime)]));
=======
        private void SaveHistoryCSV() {
            CSVUtil csvUtil = new CSVUtil();

            string path = Environment.CurrentDirectory + @"\" + Properties.Settings.Default.IndexHistoryCSV;
            csvUtil.WriteCsv(_history, path, true);
        }
        private void UpdateActiveIndex(DataRow row) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@パス", row[(int)IndexHistoryColIdx.IndexedPath]));
                param.Add(new SQLiteParameter("@インデックスパス", AppObject.RootDirPath + @"\Index" + LuceneIndexBuilder.ReservedNo));
                param.Add(new SQLiteParameter("@モード", row[(int)IndexHistoryColIdx.CreateMode]));
                param.Add(new SQLiteParameter("@作成時間(分)", row[(int)IndexHistoryColIdx.CreateTime]));
                param.Add(new SQLiteParameter("@対象ファイル数", row[(int)IndexHistoryColIdx.TargetCount]));
                param.Add(new SQLiteParameter("@インデックス済み", row[(int)IndexHistoryColIdx.IndexedCount]));
                param.Add(new SQLiteParameter("@インデックス対象外", row[(int)IndexHistoryColIdx.SkippedCount]));
                param.Add(new SQLiteParameter("@総バイト数", row[(int)IndexHistoryColIdx.TotalBytes]));
                param.Add(new SQLiteParameter("@テキスト抽出器", row[(int)IndexHistoryColIdx.TextExtractMode]));
                param.Add(new SQLiteParameter("@作成完了", DateTime.Parse(row[(int)IndexHistoryColIdx.EndTime].ToString())));
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_active_index.INSERT_OR_REPLACE, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }
        }
        /// <summary>
        /// 有効Indexを削除
        /// </summary>
        /// <param name="path"></param>
        private void DeleteActiveIndex(string path) {

            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@パス", path));
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_active_index.DELETE, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }
        }
<<<<<<< HEAD
        /// <summary>
        /// Index作成履歴を更新
        /// </summary>
        /// <param name="param"></param>
        private void UpdateHistory(List<SQLiteParameter> param) {
=======

        private void UpdateHistory2DB(DataRow row, int reservedNo) {
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                //HACK パラメタ処理の共通化
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@予約No", reservedNo));
                param.Add(new SQLiteParameter("@作成開始", DateTime.Parse(row[(int)IndexHistoryColIdx.StartTime].ToString())));
                param.Add(new SQLiteParameter("@作成完了", DateTime.Parse(row[(int)IndexHistoryColIdx.EndTime].ToString())));
                param.Add(new SQLiteParameter("@モード", row[(int)IndexHistoryColIdx.CreateMode]));
                param.Add(new SQLiteParameter("@パス", row[(int)IndexHistoryColIdx.IndexedPath]));
                param.Add(new SQLiteParameter("@作成時間(分)", row[(int)IndexHistoryColIdx.CreateTime]));
                param.Add(new SQLiteParameter("@対象ファイル数", row[(int)IndexHistoryColIdx.TargetCount]));
                param.Add(new SQLiteParameter("@インデックス済み", row[(int)IndexHistoryColIdx.IndexedCount]));
                param.Add(new SQLiteParameter("@インデックス対象外", row[(int)IndexHistoryColIdx.SkippedCount]));
                param.Add(new SQLiteParameter("@総バイト数", row[(int)IndexHistoryColIdx.TotalBytes]));
                param.Add(new SQLiteParameter("@テキスト抽出器", row[(int)IndexHistoryColIdx.TextExtractMode]));
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_index_history.INSERT_OR_REPLACE, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }

        }
<<<<<<< HEAD
        /// <summary>
        /// Index作成履歴を挿入
        /// </summary>
        /// <param name="param"></param>
        private void InsertHistory(List<SQLiteParameter> param) {
=======
        private void InsertHistory2DB(DataRow row) {
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                //param.Add(new SQLiteParameter("@予約No", 1));
                param.Add(new SQLiteParameter("@作成開始", DateTime.Parse(row[(int)IndexHistoryColIdx.StartTime].ToString())));
                param.Add(new SQLiteParameter("@作成完了", DBNull.Value));
                param.Add(new SQLiteParameter("@モード", row[(int)IndexHistoryColIdx.CreateMode]));
                param.Add(new SQLiteParameter("@パス", row[(int)IndexHistoryColIdx.IndexedPath]));
                param.Add(new SQLiteParameter("@作成時間(分)", row[(int)IndexHistoryColIdx.CreateTime]));
                param.Add(new SQLiteParameter("@対象ファイル数", row[(int)IndexHistoryColIdx.TargetCount]));
                param.Add(new SQLiteParameter("@インデックス済み", row[(int)IndexHistoryColIdx.IndexedCount]));
                param.Add(new SQLiteParameter("@インデックス対象外", row[(int)IndexHistoryColIdx.SkippedCount]));
                param.Add(new SQLiteParameter("@総バイト数", row[(int)IndexHistoryColIdx.TotalBytes]));
                param.Add(new SQLiteParameter("@テキスト抽出器", row[(int)IndexHistoryColIdx.TextExtractMode]));
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_index_history.INSERT, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }

        }

        /// <summary>
        /// 予約No取得
        /// </summary>
        /// <returns></returns>
        private int GetReservedNo() {
            int ret = 0;

            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_index_history.SELECT_NEW_ONE, param.ToArray());
                if (ds.Tables[0].Rows.Count > 0) {
                    ret = int.Parse(ds.Tables[0].Rows[0]["予約No"].ToString());
                }
            } finally {
                AppObject.DbUtil.Close();
            }
            return ret;
        }

<<<<<<< HEAD
        /// <summary>
        /// インデックス作成履歴を表示
        /// </summary>
        /// <returns></returns>
        public DataTable LoadHistory() {
            DataTable dt = SelectIndexHistory();
=======

        private DataTable ReadHistoryCSV() {
            CSVUtil csvUtil = new CSVUtil();
            DataTable historyTbl = new DataTable("IndexHistory");
            string path = Environment.CurrentDirectory + @"\" + Properties.Settings.Default.IndexHistoryCSV;
            if (File.Exists(path)) {
                historyTbl = csvUtil.ReadCsv(path, "IndexHistory");
            } else {
                //ファイルが存在しない場合は、空テーブルを返す
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.ReservedNo), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.CreateMode), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.IndexedPath), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.StartTime), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.EndTime), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.CreateTime), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TargetCount), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.IndexedCount), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.SkippedCount), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TotalBytes), typeof(string));
                historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TextExtractMode), typeof(string));
            }

            return historyTbl;
        }
        public void LoadActiveIndex() {
            DataTable dt = SelectActiveIndex();
            this.ActiveIndexGrid.DataSource = dt;
        }

        public static DataTable SelectActiveIndex() {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_active_index.SELECT, null);
                return ds.Tables[0];
            } finally {
                AppObject.DbUtil.Close();
            }
        }
        /// <summary>
        /// インデックス作成履歴グリッドのヘッダーを設定
        /// </summary>
        public void LoadHistory(DataTable history) {

            //this.IndexHistoryGrid.DataSource = history;
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正

            this.IndexHistoryGrid.Cols.Count = EnumUtil.GetCount(typeof(IndexHistoryColIdx)) + 1;
            this.IndexHistoryGrid.Rows.Count = _history.Rows.Count + HeaderRowCount;

            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.ReservedNo + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.ReservedNo);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.ReservedNo + 1].Width = 60;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.CreateMode + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.CreateMode);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateMode + 1].Width = 60;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.IndexedPath + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.IndexedPath);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedPath + 1].Width = 200;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.StartTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.StartTime);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.StartTime + 1].Width = 120;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.EndTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.EndTime);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.EndTime + 1].Width = 120;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.CreateTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.CreateTime);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].Width = 80;
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].DataType = typeof(int);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].Format = "#,##0";
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TargetCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TargetCount);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].Width = 80;
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].DataType = typeof(int);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].Format = "#,##0";
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.IndexedCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.IndexedCount);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].Width = 80;
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].DataType = typeof(int);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].Format = "#,##0";
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.SkippedCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.SkippedCount);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].Width = 80;
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].DataType = typeof(int);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].Format = "#,##0";
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TotalBytes + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TotalBytes + 1].Width = 80;
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TotalBytes + 1].TextAlign = TextAlignEnum.RightCenter;
            this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TextExtractMode + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode);
            this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TextExtractMode + 1].Width = 80;

            int row = HeaderRowCount;
            foreach (DataRow dr in _history.Rows) {
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.ReservedNo + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.ReservedNo)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.CreateMode)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.StartTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.StartTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.EndTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.EndTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.CreateTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedPath + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.IndexedPath)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TargetCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TargetCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.IndexedCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.SkippedCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.SkippedCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TotalBytes + 1] = 
<<<<<<< HEAD
                    FileUtil.GetSizeString(StringUtil.NullToZero(dr[EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes)]));
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TextExtractMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode)]);

                row++;
            }

            return dt;
        }
        /// <summary>
        /// 有効Indexを表示
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="appendCheckBox"></param>
        public static void LoadActiveIndex(FlexGridEx grid, bool appendCheckBox = false) {
            DataTable dt = SelectActiveIndex();
            _activeIndex = dt;

            int offset = 0;
            if (appendCheckBox) {
                offset++;
            }

            grid.Cols.Count = EnumUtil.GetCount(typeof(ActiveIndexColIdx)) + 1 + offset;
            grid.Rows.Count = dt.Rows.Count + HeaderRowCount;

            if (appendCheckBox) {
                grid.Cols[0].Width = 2;
                grid[0, 1] = "";
                grid.Cols[1].Width = 30;
                grid.Cols[1].DataType = typeof(bool);
            }
            grid[0, (int)ActiveIndexColIdx.IndexedPath + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.IndexedPath);
            if (appendCheckBox) {
                grid.Cols[(int)ActiveIndexColIdx.IndexedPath + 1 + offset].Width = 500;
            } else {
                grid.Cols[(int)ActiveIndexColIdx.IndexedPath + 1 + offset].Width = 320;
            }
            grid[0, (int)ActiveIndexColIdx.IndexStorePath + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.IndexStorePath);
            grid.Cols[(int)ActiveIndexColIdx.IndexStorePath + 1 + offset].Width = 80;
            grid[0, (int)ActiveIndexColIdx.CreateMode + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.CreateMode);
            grid.Cols[(int)ActiveIndexColIdx.CreateMode + 1 + offset].Width = 60;
            grid[0, (int)ActiveIndexColIdx.CreateTime + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.CreateTime);
            grid.Cols[(int)ActiveIndexColIdx.CreateTime + 1 + offset].Width = 80;
            grid.Cols[(int)ActiveIndexColIdx.CreateTime + 1 + offset].DataType = typeof(int);
            grid.Cols[(int)ActiveIndexColIdx.CreateTime + 1 + offset].Format = "#,##0";
            grid[0, (int)ActiveIndexColIdx.TargetCount + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.TargetCount);
            grid.Cols[(int)ActiveIndexColIdx.TargetCount + 1 + offset].Width = 80;
            grid.Cols[(int)ActiveIndexColIdx.TargetCount + 1 + offset].DataType = typeof(int);
            grid.Cols[(int)ActiveIndexColIdx.TargetCount + 1 + offset].Format = "#,##0";
            grid[0, (int)ActiveIndexColIdx.IndexedCount + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.IndexedCount);
            grid.Cols[(int)ActiveIndexColIdx.IndexedCount + 1 + offset].Width = 80;
            grid.Cols[(int)ActiveIndexColIdx.IndexedCount + 1 + offset].DataType = typeof(int);
            grid.Cols[(int)ActiveIndexColIdx.IndexedCount + 1 + offset].Format = "#,##0";
            grid[0, (int)ActiveIndexColIdx.SkippedCount + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.SkippedCount);
            grid.Cols[(int)ActiveIndexColIdx.SkippedCount + 1 + offset].Width = 80;
            grid.Cols[(int)ActiveIndexColIdx.SkippedCount + 1 + offset].DataType = typeof(int);
            grid.Cols[(int)ActiveIndexColIdx.SkippedCount + 1 + offset].Format = "#,##0";
            grid[0, (int)ActiveIndexColIdx.TotalBytes + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.TotalBytes);
            grid.Cols[(int)ActiveIndexColIdx.TotalBytes + 1 + offset].Width = 80;
            grid.Cols[(int)ActiveIndexColIdx.TotalBytes + 1 + offset].TextAlign = TextAlignEnum.RightCenter;
            grid[0, (int)ActiveIndexColIdx.TextExtractMode + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.TextExtractMode);
            grid.Cols[(int)ActiveIndexColIdx.TextExtractMode + 1 + offset].Width = 80;
            grid[0, (int)ActiveIndexColIdx.InsertDate + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.InsertDate);
            grid.Cols[(int)ActiveIndexColIdx.InsertDate + 1 + offset].Width = 120;
            grid[0, (int)ActiveIndexColIdx.UpdateDate + 1 + offset] = EnumUtil.GetLabel(ActiveIndexColIdx.UpdateDate);
            grid.Cols[(int)ActiveIndexColIdx.UpdateDate + 1 + offset].Width = 120;

            int row = HeaderRowCount;
            foreach (DataRow dr in dt.Rows) {
                if (appendCheckBox) {
                    grid[row, 1] = true;
                }
                grid[row, (int)ActiveIndexColIdx.IndexedPath + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.IndexedPath)]);
                grid[row, (int)ActiveIndexColIdx.IndexStorePath + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.IndexStorePath)]);
                grid[row, (int)ActiveIndexColIdx.CreateMode + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.CreateMode)]);
                grid[row, (int)ActiveIndexColIdx.CreateTime + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.CreateTime)]);
                grid[row, (int)ActiveIndexColIdx.TargetCount + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.TargetCount)]);
                grid[row, (int)ActiveIndexColIdx.IndexedCount + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.IndexedCount)]);
                grid[row, (int)ActiveIndexColIdx.SkippedCount + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.SkippedCount)]);
                grid[row, (int)ActiveIndexColIdx.TotalBytes + 1 + offset] = 
                    FileUtil.GetSizeString(StringUtil.NullToZero(dr[EnumUtil.GetLabel(ActiveIndexColIdx.TotalBytes)]));
                grid[row, (int)ActiveIndexColIdx.TextExtractMode + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.TextExtractMode)]);
                grid[row, (int)ActiveIndexColIdx.InsertDate + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.InsertDate)]);
                grid[row, (int)ActiveIndexColIdx.UpdateDate + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.UpdateDate)]);

                row++;
            }

        }

        /// <summary>
        /// Index履歴を選択
        /// </summary>
        /// <returns></returns>
        private DataTable SelectIndexHistory() {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_index_history.SELECT_ALL + " ORDER BY [予約No] DESC", null);
                return ds.Tables[0];
            } finally {
                AppObject.DbUtil.Close();
            }
=======
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TotalBytes)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TextExtractMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TextExtractMode)]);

                row++;
            }
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
        }
        private DataTable SelectActiveIndex(string indexedPath) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@パス", indexedPath));
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_active_index.SELECT_ONE, param.ToArray());
                return ds.Tables[0];
            } finally {
                AppObject.DbUtil.Close();
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// 有効Indexを選択
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectActiveIndex() {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_active_index.SELECT, null);
                return ds.Tables[0];
            } finally {
                AppObject.DbUtil.Close();
            }
        }

=======
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
        /// <summary>
        /// インデックス作成の進捗報告
        /// </summary>
        /// <param name="report"></param>
        private void SetProgressValue(ProgressReport report) {
            this.ProgressBar.Style = ProgressBarStyle.Continuous;
            this.ProgressBar.Value = report.Percent;
            this.LogViewerText.Text = report.ProgressCount.ToString() + "/" + report.TargetCount.ToString();

            //HACK isAppendMode対応

            //タスクバーのプログレス
            TaskbarManager.Instance.SetProgressValue(report.Percent, 100);

            if (report.Status == ProgressReport.ProgressStatus.Start) {
                //開始時
<<<<<<< HEAD
                var param = new List<SQLiteParameter>();
                //param.Add(new SQLiteParameter("@予約No", 1));
                param.Add(new SQLiteParameter("@作成開始", LuceneIndexBuilder.StartTime));
                //param.Add(new SQLiteParameter("@作成開始", DBNull.Value));
                param.Add(new SQLiteParameter("@作成完了", DBNull.Value));
                param.Add(new SQLiteParameter("@モード", EnumUtil.GetLabel(LuceneIndexBuilder.CreateMode)));
                param.Add(new SQLiteParameter("@パス", LuceneIndexBuilder.IndexedPath));
                param.Add(new SQLiteParameter("@作成時間(分)", LuceneIndexBuilder.CreateTime.TotalMinutes));
                param.Add(new SQLiteParameter("@対象ファイル数", LuceneIndexBuilder.TargetCount));
                param.Add(new SQLiteParameter("@インデックス済み", LuceneIndexBuilder.IndexedCount));
                param.Add(new SQLiteParameter("@インデックス対象外", LuceneIndexBuilder.SkippedCount));
                param.Add(new SQLiteParameter("@総バイト数", LuceneIndexBuilder.TotalBytes));
                param.Add(new SQLiteParameter("@テキスト抽出器", EnumUtil.GetName(LuceneIndexBuilder.TextExtractMode)));

                InsertHistory(param);
                LoadHistory();
=======
                var newRow = _history.NewRow();
                newRow[(int)IndexHistoryColIdx.ReservedNo] = -1;
                newRow[(int)IndexHistoryColIdx.CreateMode] = "再作成";
                newRow[(int)IndexHistoryColIdx.StartTime] = LuceneIndexBuilder.StartTime.ToString("yyyy/MM/dd HH:mm:ss");
                newRow[(int)IndexHistoryColIdx.EndTime] = DBNull.Value;
                newRow[(int)IndexHistoryColIdx.CreateTime] = LuceneIndexBuilder.CreateTime.TotalMinutes;
                newRow[(int)IndexHistoryColIdx.IndexedPath] = LuceneIndexBuilder.IndexedPath;
                newRow[(int)IndexHistoryColIdx.TargetCount] = LuceneIndexBuilder.TargetCount;
                newRow[(int)IndexHistoryColIdx.IndexedCount] = LuceneIndexBuilder.IndexedCount;
                newRow[(int)IndexHistoryColIdx.SkippedCount] = LuceneIndexBuilder.SkippedCount;
                newRow[(int)IndexHistoryColIdx.TotalBytes] = FileUtil.GetSizeString(LuceneIndexBuilder.TotalBytes);
                newRow[(int)IndexHistoryColIdx.TextExtractMode] = EnumUtil.GetName(LuceneIndexBuilder.TextExtractMode);

                InsertHistory2DB(newRow);
                newRow[(int)IndexHistoryColIdx.ReservedNo] = LuceneIndexBuilder.ReservedNo;

                _history.Rows.InsertAt(newRow, 0);
                _history.AcceptChanges();
                LoadHistory(_history);
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正

            } else if (report.Status == ProgressReport.ProgressStatus.Finished) {
                //完了 or 中断時
                //NOTE:マルチスレッドも見据えてカウンタをstatic化したので、
                //　　 処理結果を戻り値workerから受け取らずにstaticプロパティから受け取る
                if (LuceneIndexBuilder.DoStop == false) {
                    //HACK パラメタ処理の共通化
                    var param = new List<SQLiteParameter>();
                    param.Add(new SQLiteParameter("@作成完了", LuceneIndexBuilder.EndTime));
                    param.Add(new SQLiteParameter("@作成時間", LuceneIndexBuilder.CreateTime.TotalMinutes));
                    param.Add(new SQLiteParameter("@対象ファイル数", LuceneIndexBuilder.TargetCount));
                    param.Add(new SQLiteParameter("@インデックス済み", LuceneIndexBuilder.IndexedCount));
                    param.Add(new SQLiteParameter("@インデックス対象外", LuceneIndexBuilder.SkippedCount));
                    param.Add(new SQLiteParameter("@総バイト数", LuceneIndexBuilder.TotalBytes));
                    param.Add(new SQLiteParameter("@予約No", LuceneIndexBuilder.ReservedNo.ToString()));
                    UpdateHistory(param);
                    var historyTbl = LoadHistory();

                    if (LuceneIndexBuilder.CreateMode == LuceneIndexBuilder.CreateModes.Create) {
                        UpdateActiveIndex(historyTbl.Rows[0]);
                    } else {
                        UpdateActiveIndex(historyTbl.Rows[0], LuceneIndexBuilder.IndexStorePath);
                    }
                    LoadActiveIndex(this.ActiveIndexGrid);
                    LoadActiveIndex(SearchForm.TargetIndexGridControl, true);
                }

<<<<<<< HEAD
                DeleteNonActiveIndex();
=======
                DataRow[] historyRows = (
                    from row in _history.AsEnumerable()
                    let qReservedNo = row.Field<string>(EnumUtil.GetName(IndexHistoryColIdx.ReservedNo))
                    where qReservedNo == LuceneIndexBuilder.ReservedNo.ToString()
                    select row).ToArray();
                
                if (historyRows.Length > 0) {
                    DataRow dr = historyRows[0];
                    dr[(int)IndexHistoryColIdx.EndTime] = LuceneIndexBuilder.EndTime.ToString("yyyy/MM/dd HH:mm:ss");
                    dr[(int)IndexHistoryColIdx.CreateTime] = LuceneIndexBuilder.CreateTime.TotalMinutes;
                    dr[(int)IndexHistoryColIdx.TargetCount] = LuceneIndexBuilder.TargetCount;
                    dr[(int)IndexHistoryColIdx.IndexedCount] = LuceneIndexBuilder.IndexedCount;
                    dr[(int)IndexHistoryColIdx.SkippedCount] = LuceneIndexBuilder.SkippedCount;
                    dr[(int)IndexHistoryColIdx.TotalBytes] = FileUtil.GetSizeString(LuceneIndexBuilder.TotalBytes);
                }
                _history.AcceptChanges();
                LoadHistory(_history);

                UpdateHistory2DB(historyRows[0], LuceneIndexBuilder.ReservedNo);

                UpdateActiveIndex(historyRows[0]);
                LoadActiveIndex();

                //HACK SQLiteに移行
                SaveHistoryCSV();
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正

                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

                this.CreateIndexButton.Enabled = true;
                this.UpdateIndexButton.Enabled = true;
                this.StopButton.Enabled = false;
            }
        }

        /// <summary>
        /// 参照ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("検索対象フォルダ選択", this.TargetDirText.Text);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.TargetDirText.Text = selectedPath;
            }
        }

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildForm_FormClosed(object sender, FormClosedEventArgs e) {

            Properties.Settings.Default.InitIndexPath = this.TargetDirText.Text;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildForm_Load(object sender, EventArgs e) {
            this.CreateIndexButton.Enabled = true;
            this.UpdateIndexButton.Enabled = true;
            this.StopButton.Enabled = false;

            LoadHistory();
            LoadActiveIndex(this.ActiveIndexGrid);
            this.TargetDirText.Text = Properties.Settings.Default.InitIndexPath;

            var logViewer = new TextBoxAppender();
            logViewer.TextBoxName = this.Log4NetTextBox.Name;
            logViewer.FormName = this.Name;
            logViewer.PrefixFilter = "IndexingThread1";
            logViewer.Threshold = log4net.Core.Level.All;
            var consoleAppender = new log4net.Appender.ConsoleAppender { 
                Layout = new log4net.Layout.SimpleLayout()
            };
            var appenderList = new AppenderSkeleton[] { 
                logViewer, 
                consoleAppender 
            };
            log4net.Config.BasicConfigurator.Configure(appenderList);
        }

        /// <summary>
        /// インデックスマージボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MergeIndexButton_Click(object sender, EventArgs e) {
            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_SEARCH), true, sw);
            try {
                LuceneIndexBuilder.MergeAndMove(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    this.TargetDirText.Text);
            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
        }

        /// <summary>
        /// インデックスを再構築
        /// （既存インデックスがある場合は、削除して作り直す）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateIndexButton_Click(object sender, EventArgs e) {
            string targetDir = (this.TargetDirText.Text);
            if (!new DirectoryInfo(targetDir).Exists) {
                MessageBox.Show("指定されたフォルダは存在しません。",
                    AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CreateIndex(targetDir);
        }

        private void CreateIndex(string targetDir, string orgIndexStorePath = "") {
            LuceneIndexBuilder.TextExtractModes mode = LuceneIndexBuilder.TextExtractModes.Tika;
            if (this.IFilterRadio.Checked) {
                mode = LuceneIndexBuilder.TextExtractModes.IFilter;
            }

            this.CreateIndexButton.Enabled = false;
            this.UpdateIndexButton.Enabled = false;
            this.StopButton.Enabled = true;

            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_PROCESSING), true, sw);
            try {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
                this.LogViewerText.Text = "対象ファイルカウント中...";
                var progress = new Progress<ProgressReport>(SetProgressValue);

                LuceneIndexBuilder.ReservedNo = GetReservedNo();
                LuceneIndexBuilder.CreateIndexBySingleThread(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    targetDir,
                    progress,
                    mode,
                    orgIndexStorePath);

            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
        }

        /// <summary>
        /// インデックス更新
        /// （既存インデックスがあれば、差分更新する）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIndexButton_Click(object sender, EventArgs e) {
            string targetDir = (this.TargetDirText.Text);
            if (!new DirectoryInfo(targetDir).Exists) {
                MessageBox.Show("指定されたフォルダは存在しません。",
                    AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orgIndexStorePath = "";
            DataTable dt = SelectActiveIndex(targetDir);
            if (dt.Rows.Count > 0) {
                //ActiveIndexに存在する場合
                orgIndexStorePath = StringUtil.NullToBlank(dt.Rows[0][EnumUtil.GetLabel(ActiveIndexColIdx.IndexStorePath)]);
            }
            CreateIndex(targetDir, orgIndexStorePath);
        }

        /// <summary>
        /// インデックスを削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteIndexMenu_Click(object sender, EventArgs e) {
            string path = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, (int)ActiveIndexColIdx.IndexedPath + 1]);
            string indexedPath = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, (int)ActiveIndexColIdx.IndexStorePath + 1]);
            //インデックスディレクトリを削除
            if (Directory.Exists(indexedPath)) {
                FileUtil.DeleteDirectory(new DirectoryInfo(indexedPath));
            }
            //有効インデックステーブルから削除
            DeleteActiveIndex(path);
<<<<<<< HEAD
            LoadActiveIndex(this.ActiveIndexGrid);
            LoadActiveIndex(SearchForm.TargetIndexGridControl, true);
=======
            LoadActiveIndex();
>>>>>>> parent of 169e1d1... 検索インデックスを指定できるように修正
        }

        /// <summary>
        /// コピーメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMenu_Click(object sender, EventArgs e) {
            this.IndexHistoryGrid.CopyEx();
        }

        /// <summary>
        /// コピーメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveIndexCopyMenu_Click(object sender, EventArgs e) {
            this.ActiveIndexGrid.CopyEx();
        }

        /// <summary>
        /// インデックス更新メニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIndexMenu_Click(object sender, EventArgs e) {
            string targetDir = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, 
                (int)ActiveIndexColIdx.IndexedPath + 1]);
            string orgIndexStorePath = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, 
                (int)ActiveIndexColIdx.IndexStorePath + 1]);

            this.CreateIndex(targetDir, orgIndexStorePath);
        }

        /// <summary>
        /// インデックス作成メニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateIndexMenu_Click(object sender, EventArgs e) {
            string path = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, (int)ActiveIndexColIdx.IndexedPath + 1]);
            this.TargetDirText.Text = path;
            this.CreateIndexButton.PerformClick();
        }

        private void StopButton_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(AppObject.MLUtil.GetMsg(Consts.MSG_DO_STOP),
                AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                LuceneIndexBuilder.DoStop = true;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                this.Close();
            }
        }
    }
}
