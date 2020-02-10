using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Taskbar;
using PokudaSearch.IndexBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using FxCommonLib.Controls;

namespace PokudaSearch.Views {
    public partial class IndexBuildForm : Form {

        private static DataTable _activeIndexTbl = null;
        public DataTable ActiveIndexTbl {
            get { return _activeIndexTbl; }
        }

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
            [EnumLabel("インデックス済み")]
            IndexedCount,
            [EnumLabel("インデックス対象外")]
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
            [EnumLabel("インデックス済み")]
            IndexedCount,
            [EnumLabel("インデックス対象外")]
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
        /// <summary>有効インデックス</summary>
        private DataTable _activeIndex = null;
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

            LoadHistory();
            LoadActiveIndex(this.ActiveIndexGrid);
        }
        #endregion Constractors

        //private void SaveHistoryCSV() {
        //    CSVUtil csvUtil = new CSVUtil();

        //    string path = Environment.CurrentDirectory + @"\" + Properties.Settings.Default.IndexHistoryCSV;
        //    csvUtil.WriteCsv(_history, path, true);
        //}
        private void UpdateActiveIndex(DataRow row) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@パス", row[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedPath)]));
                param.Add(new SQLiteParameter("@インデックスパス", AppObject.RootDirPath + @"\Index" + LuceneIndexBuilder.ReservedNo));
                param.Add(new SQLiteParameter("@モード", row[EnumUtil.GetLabel(IndexHistoryColIdx.CreateMode)]));
                param.Add(new SQLiteParameter("@作成時間(分)", row[EnumUtil.GetLabel(IndexHistoryColIdx.CreateTime)]));
                param.Add(new SQLiteParameter("@対象ファイル数", row[EnumUtil.GetLabel(IndexHistoryColIdx.TargetCount)]));
                param.Add(new SQLiteParameter("@インデックス済み", row[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedCount)]));
                param.Add(new SQLiteParameter("@インデックス対象外", row[EnumUtil.GetLabel(IndexHistoryColIdx.SkippedCount)]));
                param.Add(new SQLiteParameter("@総バイト数", row[EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes)]));
                param.Add(new SQLiteParameter("@テキスト抽出器", row[EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode)]));
                param.Add(new SQLiteParameter("@作成完了", DateTime.Parse(row[EnumUtil.GetLabel(IndexHistoryColIdx.EndTime)].ToString())));
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_active_index.INSERT_OR_REPLACE, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }
        }
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

        private void UpdateHistory(List<SQLiteParameter> param) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_index_history.UPDATE, param.ToArray());
                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }

        }
        private void InsertHistory(List<SQLiteParameter> param) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.t_index_history.INSERT, param.ToArray());
                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }

        }

        private int GetReservedNo() {
            int ret = 0;

            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                var param = new List<SQLiteParameter>();
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_index_history.SELECT_NEW_ONE, param.ToArray());
                if (ds.Tables[0].Rows.Count > 0) {
                    ret = int.Parse(ds.Tables[0].Rows[0]["予約No"].ToString());
                    ret++;
                }
            } finally {
                AppObject.DbUtil.Close();
            }
            return ret;
        }


        //private DataTable ReadHistoryCSV() {
        //    CSVUtil csvUtil = new CSVUtil();
        //    DataTable historyTbl = new DataTable("IndexHistory");
        //    string path = Environment.CurrentDirectory + @"\" + Properties.Settings.Default.IndexHistoryCSV;
        //    if (File.Exists(path)) {
        //        historyTbl = csvUtil.ReadCsv(path, "IndexHistory");
        //    } else {
        //        //ファイルが存在しない場合は、空テーブルを返す
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.ReservedNo), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.CreateMode), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.IndexedPath), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.StartTime), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.EndTime), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.CreateTime), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TargetCount), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.IndexedCount), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.SkippedCount), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TotalBytes), typeof(string));
        //        historyTbl.Columns.Add(EnumUtil.GetName(IndexHistoryColIdx.TextExtractMode), typeof(string));
        //    }

        //    return historyTbl;
        //}
        public DataTable LoadHistory() {
            DataTable dt = SelectIndexHistory();

            this.IndexHistoryGrid.Cols.Count = EnumUtil.GetCount(typeof(IndexHistoryColIdx)) + 1;
            this.IndexHistoryGrid.Rows.Count = dt.Rows.Count + HeaderRowCount;

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
            foreach (DataRow dr in dt.Rows) {
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.ReservedNo + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.ReservedNo)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.CreateMode)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.StartTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.StartTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.EndTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.EndTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateTime + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.CreateTime)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedPath + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedPath)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TargetCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.TargetCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.IndexedCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.SkippedCount + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.SkippedCount)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TotalBytes + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TextExtractMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode)]);

                row++;
            }

            return dt;
        }
        public static void LoadActiveIndex(FlexGridEx grid, bool appendCheckBox = false) {
            DataTable dt = SelectActiveIndex();
            _activeIndexTbl = dt;

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
                    FileUtil.GetSizeString(StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.TotalBytes)]));
                grid[row, (int)ActiveIndexColIdx.TextExtractMode + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.TextExtractMode)]);
                grid[row, (int)ActiveIndexColIdx.InsertDate + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.InsertDate)]);
                grid[row, (int)ActiveIndexColIdx.UpdateDate + 1 + offset] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetLabel(ActiveIndexColIdx.UpdateDate)]);

                row++;
            }

        }

        private DataTable SelectIndexHistory() {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.t_index_history.SELECT_ALL + " ORDER BY [予約No] DESC", null);
                return ds.Tables[0];
            } finally {
                AppObject.DbUtil.Close();
            }
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
        //public void LoadHistory(DataTable history) {

        //    this.IndexHistoryGrid.Cols.Count = EnumUtil.GetCount(typeof(IndexHistoryColIdx)) + 1;
        //    this.IndexHistoryGrid.Rows.Count = _history.Rows.Count + HeaderRowCount;

        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.ReservedNo + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.ReservedNo);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.ReservedNo + 1].Width = 60;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.CreateMode + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.CreateMode);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateMode + 1].Width = 60;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.IndexedPath + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.IndexedPath);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedPath + 1].Width = 200;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.StartTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.StartTime);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.StartTime + 1].Width = 120;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.EndTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.EndTime);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.EndTime + 1].Width = 120;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.CreateTime + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.CreateTime);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].Width = 80;
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].DataType = typeof(int);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.CreateTime + 1].Format = "#,##0";
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TargetCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TargetCount);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].Width = 80;
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].DataType = typeof(int);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TargetCount + 1].Format = "#,##0";
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.IndexedCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.IndexedCount);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].Width = 80;
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].DataType = typeof(int);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.IndexedCount + 1].Format = "#,##0";
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.SkippedCount + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.SkippedCount);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].Width = 80;
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].DataType = typeof(int);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.SkippedCount + 1].Format = "#,##0";
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TotalBytes + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TotalBytes);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TotalBytes + 1].Width = 80;
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TotalBytes + 1].TextAlign = TextAlignEnum.RightCenter;
        //    this.IndexHistoryGrid[0, (int)IndexHistoryColIdx.TextExtractMode + 1] = EnumUtil.GetLabel(IndexHistoryColIdx.TextExtractMode);
        //    this.IndexHistoryGrid.Cols[(int)IndexHistoryColIdx.TextExtractMode + 1].Width = 80;

        //    int row = HeaderRowCount;
        //    foreach (DataRow dr in _history.Rows) {
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.ReservedNo + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.ReservedNo)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateMode + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.CreateMode)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.StartTime + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.StartTime)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.EndTime + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.EndTime)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.CreateTime + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.CreateTime)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedPath + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.IndexedPath)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TargetCount + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TargetCount)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.IndexedCount + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.IndexedCount)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.SkippedCount + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.SkippedCount)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TotalBytes + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TotalBytes)]);
        //        this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TextExtractMode + 1] = 
        //            StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TextExtractMode)]);

        //        row++;
        //    }
        //}

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
                var param = new List<SQLiteParameter>();
                //param.Add(new SQLiteParameter("@予約No", 1));
                param.Add(new SQLiteParameter("@作成開始", LuceneIndexBuilder.StartTime));
                //param.Add(new SQLiteParameter("@作成開始", DBNull.Value));
                param.Add(new SQLiteParameter("@作成完了", DBNull.Value));
                param.Add(new SQLiteParameter("@モード", "再作成"));
                param.Add(new SQLiteParameter("@パス", LuceneIndexBuilder.IndexedPath));
                param.Add(new SQLiteParameter("@作成時間(分)", LuceneIndexBuilder.CreateTime.TotalMinutes));
                param.Add(new SQLiteParameter("@対象ファイル数", LuceneIndexBuilder.TargetCount));
                param.Add(new SQLiteParameter("@インデックス済み", LuceneIndexBuilder.IndexedCount));
                param.Add(new SQLiteParameter("@インデックス対象外", LuceneIndexBuilder.SkippedCount));
                param.Add(new SQLiteParameter("@総バイト数", FileUtil.GetSizeString(LuceneIndexBuilder.TotalBytes)));
                param.Add(new SQLiteParameter("@テキスト抽出器", EnumUtil.GetName(LuceneIndexBuilder.TextExtractMode)));

                InsertHistory(param);
                LoadHistory();

            } else if (report.Status == ProgressReport.ProgressStatus.Finished) {
                //完了時
                //NOTE:マルチスレッドも見据えてカウンタをstatic化したので、
                //　　 処理結果を戻り値workerから受け取らずにstaticプロパティから受け取る

                //HACK パラメタ処理の共通化
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@予約No", LuceneIndexBuilder.ReservedNo.ToString()));
                param.Add(new SQLiteParameter("@作成完了", LuceneIndexBuilder.EndTime));
                param.Add(new SQLiteParameter("@作成時間(分)", LuceneIndexBuilder.CreateTime.TotalMinutes));
                param.Add(new SQLiteParameter("@対象ファイル数", LuceneIndexBuilder.TargetCount));
                param.Add(new SQLiteParameter("@インデックス済み", LuceneIndexBuilder.IndexedCount));
                param.Add(new SQLiteParameter("@インデックス対象外", LuceneIndexBuilder.SkippedCount));
                param.Add(new SQLiteParameter("@総バイト数", FileUtil.GetSizeString(LuceneIndexBuilder.TotalBytes)));
                UpdateHistory(param);
                var historyTbl = LoadHistory();

                UpdateActiveIndex(historyTbl.Rows[0]);
                LoadActiveIndex(this.ActiveIndexGrid);

                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                this.CreateIndexButton.Enabled = true;
            }
        }

        /// <summary>
        /// インデックスを再構築
        /// （既存インデックスがある場合は、削除して作り直す）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateIndexButton_Click(object sender, EventArgs e) {
            LuceneIndexBuilder.TextExtractModes mode = LuceneIndexBuilder.TextExtractModes.Tika;
            if (this.IFilterRadio.Checked) {
                mode = LuceneIndexBuilder.TextExtractModes.IFilter;
            }

            this.CreateIndexButton.Enabled = false;
            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_PROCESSING), true, sw);
            try {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
                this.LogViewerText.Text = "対象ファイルカウント中...";

                LuceneIndexBuilder.ReservedNo = GetReservedNo();
                var progress = new Progress<ProgressReport>(SetProgressValue);
                LuceneIndexBuilder.CreateIndexBySingleThread(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    this.TargetDirText.Text,
                    progress,
                    mode,
                    false);
                //Multri RAMDirectoryで構築
                //LuceneIndexWorker.CreateIndexByMultiRAM(
                //    AppObject.AppAnalyzer, 
                //    AppObject.RootDirPath, 
                //    this.TargetDirText.Text,
                //    progress,
                //    mode);

            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
        }

        /// <summary>
        /// 参照ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("検索対象フォルダ選択", Properties.Settings.Default.InitIndexPath);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.TargetDirText.Text = selectedPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainFrameForm.IndexBuildForm = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildForm_Load(object sender, EventArgs e) {
            this.TargetDirText.Text = Properties.Settings.Default.InitIndexPath;
        }

        /// <summary>
        /// 
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
        /// インデックス更新
        /// （既存インデックスがあれば、差分更新する）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIndexButton_Click(object sender, EventArgs e) {
            LuceneIndexBuilder.TextExtractModes mode = LuceneIndexBuilder.TextExtractModes.Tika;
            if (this.IFilterRadio.Checked) {
                mode = LuceneIndexBuilder.TextExtractModes.IFilter;
            }

            this.CreateIndexButton.Enabled = false;
            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_PROCESSING), true, sw);
            try {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
                this.LogViewerText.Text = "対象ファイルカウント中...";
                var progress = new Progress<ProgressReport>(SetProgressValue);
                LuceneIndexBuilder.CreateIndexBySingleThread(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    this.TargetDirText.Text,
                    progress,
                    mode,
                    true);

            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
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
            LoadActiveIndex(this.ActiveIndexGrid);
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

        private void UpdateIndexMenu_Click(object sender, EventArgs e) {

        }

        private void CreateIndexMenu_Click(object sender, EventArgs e) {

        }
    }
}
