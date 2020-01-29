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

            _history = ReadHistoryCSV();
            LoadHistory(_history);
            LoadActiveIndex();
        }
        #endregion Constractors

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

        private void UpdateHistory2DB(DataRow row, int reservedNo) {
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
        private void InsertHistory2DB(DataRow row) {
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
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TotalBytes)]);
                this.IndexHistoryGrid[row, (int)IndexHistoryColIdx.TextExtractMode + 1] = 
                    StringUtil.NullToBlank(dr[EnumUtil.GetName(IndexHistoryColIdx.TextExtractMode)]);

                row++;
            }
        }

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

            } else if (report.Status == ProgressReport.ProgressStatus.Finished) {
                //完了時
                //NOTE:マルチスレッドも見据えてカウンタをstatic化したので、
                //　　 処理結果を戻り値workerから受け取らずにstaticプロパティから受け取る

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
            LoadActiveIndex();
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
