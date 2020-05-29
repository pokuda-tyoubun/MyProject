
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Tile;
using CefSharp;
using CefSharp.WinForms;
using com.drew.metadata;
using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Document;
using FlexLucene.Facet;
using FlexLucene.Facet.Taxonomy.Directory;
using FlexLucene.Index;
using FlexLucene.Queries.Mlt;
using FlexLucene.Queryparser.Classic;
using FlexLucene.Search;
using FlexLucene.Search.Highlight;
using FlexLucene.Store;
using FxCommonLib.Consts;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using java.nio.file;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.IndexUtil;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class SearchForm : Form {

        #region Constants
        /// <summary>検索結果一覧のヘッダ行数</summary>
        private const int RowHeaderCount = 1;
        /// <summary>検索対象チェック列</summary>
        private const int TargetCheckCol = 1;

        /// <summary>列定義</summary>
        private enum ColIndex : int {
            [EnumLabel("種類")]
            FileIcon = 1,
            [EnumLabel("ファイル名")]
            FileName,
            [EnumLabel("パス")]
            FullPath,
            [EnumLabel("拡張子")]
            Extension,
            [EnumLabel("更新日")]
            UpdateDate,
            [EnumLabel("スコア")]
            Score,
            [EnumLabel("DocId")]
            DocId,
            [EnumLabel("ハイライト")]
            Hilight
        }
        #endregion Constants

        #region Properties
        /// <summary>最大検索結果数</summary>
        public int MaxSeachResultNum = 0;

        /// <summary>ナレッジ管理画面</summary>
        private static FlexGridEx _targetIndexGrid = null;
        public static FlexGridEx TargetIndexGridControl {
            set { _targetIndexGrid = value; }
            get { return _targetIndexGrid; }
        }

        #endregion Properties

        #region MemberVariables
        private bool _returnFocus = false;
        /// <summary>ビットマップユーティリティ</summary>
        private BitmapUtil _bu = new BitmapUtil();
        /// <summary>C1SuperLabelのリスト(高速化のためキャッシュする)</summary>
        private List<C1SuperLabel> _htmlLabelList = new List<C1SuperLabel>();
        /// <summary>プレビューリクエスト待ちスタック</summary>
        private Stack<int> _selectedStack = new Stack<int>();
        /// <summary>選択インデックスを一時的に保持</summary>
        private List<KeyValuePair<string, string>> _selectedIndexList = null;
        /// <summary>プレビュー用ブラウザ</summary>
        private ChromiumWebBrowser _chromeBrowser;
        /// <summary>外部パスの辞書</summary>
        private Dictionary<string, string> _outerPathDic;
        /// <summary>セルのツールチップ</summary>
		private ToolTip _ttip;
        /// <summary>直近のマウス直下のセル行</summary>
        private int _lastRow = 0;
        /// <summary>直近のマウス直下のセル列</summary>
        private int _lastCol = 0;
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchForm() {
            InitializeComponent();
            InitializeChromium();

            CreateHeader();

            TargetIndexGridControl = this.TargetIndexGrid;
            IndexBuildForm.LoadActiveIndex(AppObject.ConnectString, this.TargetIndexGrid, appendCheckBox:true);

            //チェックボックスとモードを固定
            this.TargetIndexGrid.Cols.Frozen = (int)IndexBuildForm.ActiveIndexColIdx.CreateMode + 2;
#if DEBUG
#else
            this.TileViewButton.Visible = false;
            this.ListViewButton.Visible = false;
            this.ResultTile.Visible = false;
#endif
        }
        #endregion Constractors



        #region EventHandlers
        /// <summary>
        /// Webブラウザーのプログレス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromeBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e) {
            delegate_UpdateProgressBar callback = new delegate_UpdateProgressBar(UpdateProgressBar);
            this.BrowserProgress.Invoke(callback, e.IsLoading);
        }
        /// <summary>
        /// Webブラウザーのロードエラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromeBrowser_LoadError(object sender, CefSharp.LoadErrorEventArgs e) {
            AppObject.Logger.Warn("ChromeBrowser LoadError occured. : " + e.FailedUrl);
            _chromeBrowser.Load("about:blank");
        }

        /// <summary>
        /// フォームロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Load(object sender, EventArgs e) {
            MaxSeachResultNum = Properties.Settings.Default.MaxSeachResultNum;
            PreviewCheckToolTip.SetToolTip(PreviewCheck, "プレビュー表示(Ctrl + P)");
            ExpandPreviewCheckToolTip.SetToolTip(ExpandPreviewCheck, "プレビュー拡大(Ctrl + Shift + P)");

            //差分ツールメニューの活性設定
            if (StringUtil.NullToBlank(Properties.Settings.Default.DiffExe) == "") {
                this.DiffMenu.Enabled = false;
            } else {
                this.DiffMenu.Enabled = true;
            }

            //デフォルトインデックスターゲットの設定
            CheckedLocalIndex(Properties.Settings.Default.LocalIndexChecked);
            CheckedOuterIndex(Properties.Settings.Default.OuterIndexChecked);
        }
        /// <summary>
        /// Excel出力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteExcelButton_Click(object sender, EventArgs e) {
            ProgressDialog pd = new ProgressDialog(AppObject.GetMsg(AppObject.Msg.ACT_EXTRACT),
                new DoWorkEventHandler(Extract2Excel_DoWork), 0);

            DialogResult result;
            this.Cursor = Cursors.WaitCursor;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    AppObject.Logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Excel抽出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extract2Excel_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            ExcelUtil eu = new ExcelUtil();
            eu.ExtractToExcel(this.ResultGrid, bw, AppObject.MLUtil);
        }
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e) {
            Stopwatch sw = new Stopwatch();
            Cursor.Current = Cursors.WaitCursor;
            AppObject.Frame.SetStatusMsg(AppObject.GetMsg(AppObject.Msg.ACT_SEARCH), true, sw);
            try {
                Search();
            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.GetMsg(AppObject.Msg.ACT_END), false, sw);
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// 親フォルダを開メニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenParentMenu_Click(object sender, EventArgs e) {
            if (this.ResultGrid.Selection.TopRow < this.ResultGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            Process.Start(System.IO.Directory.GetParent(path).FullName);
        }
        /// <summary>
        /// ファイルを開くメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileMenu_Click(object sender, EventArgs e) {
            OpenFile();
        }

        /// <summary>
        /// 検索結果グリッドダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultGrid_DoubleClick(object sender, EventArgs e) {
            MouseEventArgs me = (MouseEventArgs)e;
            if (this.ResultGrid.HitTest(me.X, me.Y).Row < this.ResultGrid.Rows.Fixed) {
                //ヘッダ部は処理しない。
                return;
            }
            OpenFile();
        }
        /// <summary>
        /// 検索結果グリッドのOwnerDraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultGrid_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e) {
            if (e.Col == (int)ColIndex.Hilight && 
                e.Row >= this.ResultGrid.Rows.Fixed && 
                _htmlLabelList.Count > e.Row - this.ResultGrid.Rows.Fixed) {
                // draw background 
                e.DrawCell(DrawCellFlags.Background);
                // use the C1SuperLabel to draw the html text  
                //if (StringUtil.NullToBlank(this.ResultGrid[e.Row, (int)ColIndex.Drawed]) != "1" &&
                //    e.Bounds.Width > 0 && e.Bounds.Height > 0) {
                string val = StringUtil.NullToBlank(this.ResultGrid[e.Row, e.Col]);
                if (e.Bounds.Width > 0 &&
                    e.Bounds.Height > 0 &&
                    val.IndexOf("<b>") >= 0 &&
                    _htmlLabelList.Count > 0) {
                    var htmlLabel = _htmlLabelList[e.Row - this.ResultGrid.Rows.Fixed];
                    htmlLabel.DrawToGraphics(e.Graphics, e.Bounds);
                }
                // and draw border last  
                e.DrawCell(DrawCellFlags.Border);
                // we're done with this cell  
                e.Handled = true;
            }
        }
        /// <summary>
        /// 検索結果グリッドの選択を変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultGrid_SelChange(object sender, EventArgs e) {
            int selectedRow = this.ResultGrid.Selection.TopRow;

            //NOTE Loading時に再表示するとCOMエラーになるのを回避
            if (_chromeBrowser.IsLoading) {
                _selectedStack.Push(selectedRow);
            } else {
                if (_selectedStack.Count > 0) {
                    selectedRow = _selectedStack.Pop();
                    _selectedStack.Clear();
                }
                //プレビュー更新
                UpdatePreviewLabel(selectedRow);
            }
        }
        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e) {
            Cef.Shutdown();
            MainFrameForm.SearchForm = null;
        }
        /// <summary>
        /// グリッドフィルタのKeyPress時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeywordText_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                this.SearchButton.PerformClick();
                e.Handled = true;
            }
        }
        /// <summary>
        /// 類似文書を検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreLikeThisMenu_Click(object sender, EventArgs e) {
            string unlinkedIndexPath = "";
            MultiReader multiReader = GetMultiReader(_selectedIndexList, out unlinkedIndexPath);
            if (unlinkedIndexPath != "") {
                //検索中断
                MessageBox.Show(string.Format(AppObject.GetMsg(AppObject.Msg.ERR_UNLINKED_INDEX), unlinkedIndexPath),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IndexSearcher idxSearcher = new IndexSearcher(multiReader);
            MoreLikeThis mlt = new MoreLikeThis(multiReader);
            mlt.SetFieldNames(new string[] { LuceneIndexBuilder.Title, LuceneIndexBuilder.Content });
            mlt.SetAnalyzer(AppObject.AppAnalyzer);

            int docId = (int)this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.DocId];
            Query q = mlt.Like(docId);

            try {
                this.ResultGrid.Rows.Count = RowHeaderCount;
                _htmlLabelList.Clear();

                this.ResultGrid.Redraw = false;

                TopDocs docs = idxSearcher.Search(q, 10);
                Highlighter hi = CreateHilighter(q);

                this.ResultGrid.Rows.Count = RowHeaderCount + docs.ScoreDocs.Length;
                this.DenominatorLabel.Text = docs.ScoreDocs.Length.ToString() + "件";
                int row = RowHeaderCount;
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    string fullPath = thisDoc.Get(LuceneIndexBuilder.Path);

                    Debug.WriteLine(fullPath);

                    Bitmap bmp = Properties.Resources.File16;
                    if (File.Exists(fullPath)) {
                        try {

                            bmp = Icon.ExtractAssociatedIcon(fullPath).ToBitmap();
                        } catch {
                            //プレビューを取得できない場合は、デフォルトアイコンを表示
                        }
                    }
                    bmp.MakeTransparent();
                    //16,16
                    this.ResultGrid.SetCellImage(row, (int)ColIndex.FileIcon, _bu.Resize(bmp, 16, 16));
                    this.ResultGrid[row, (int)ColIndex.FileName] = thisDoc.Get(LuceneIndexBuilder.Title);
                    this.ResultGrid[row, (int)ColIndex.FullPath] = fullPath;

                    //HACK ループ内のnewを避けれないか?
                    var fi = new FileInfo(fullPath);
                    this.ResultGrid[row, (int)ColIndex.Extension] = fi.Extension;
                    this.ResultGrid[row, (int)ColIndex.UpdateDate] = fi.LastWriteTime;
                    this.ResultGrid[row, (int)ColIndex.Score] = doc.Score;
                    this.ResultGrid[row, (int)ColIndex.DocId] = doc.Doc;

                    // Highlighterで検索キーワード周辺の文字列(フラグメント)を取得
                    // TokenStream が必要なので取得
                    TokenStream stream = TokenSources.GetAnyTokenStream(multiReader,
                            doc.Doc, LuceneIndexBuilder.Content, AppObject.AppAnalyzer);
                    string[] str = hi.GetBestFragments(stream, thisDoc.Get(LuceneIndexBuilder.Content), 5);
                    this.ResultGrid[row, (int)ColIndex.Hilight] = string.Join(",", str);

                    row++;
                }
                SetHtmlLabel();
            } finally {
                multiReader.Close();
                this.ResultGrid.Redraw = true;
            }
        }

        /// <summary>
        /// フィルタグリッドボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void FilterGridButton_Click(object sender, EventArgs e) {
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.ResultGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_FILTER));
        }

        /// <summary>
        /// クリアフィルタボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearFilterButton_Click(object sender, EventArgs e) {
            this.SearchGridText.Text = "";
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.ResultGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_RESET_FILTER));
        }

        /// <summary>
        /// プレビュー表示ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPreviewButton_Click(object sender, EventArgs e) {

            this.Cursor = Cursors.WaitCursor;

            try {
                int row = this.ResultGrid.Selection.TopRow;
                string extension = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.Extension]);
                string fullPath = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.FullPath]);

                if (extension.ToLower() == ".pdf") {
                    //そのままブラウザに表示
                } else if (extension.ToLower() == ".xlsx" ||
                           extension.ToLower() == ".xls") {
                    string tmpPath = SaveXlsToHTML(fullPath);
                    fullPath = tmpPath;
                } else if (extension.ToLower() == ".pptx") {
                    string tmpPath = SavePptToPDF(fullPath, over2007: true);
                    fullPath = tmpPath;
                } else if (extension.ToLower() == ".ppt") {
                    string tmpPath = SavePptToPDF(fullPath, over2007: false);
                    fullPath = tmpPath;
                } else if (extension.ToLower() == ".docx" ||
                           extension.ToLower() == ".doc") {
                    string tmpPath = SaveDocToPDF(fullPath);
                    fullPath = tmpPath;
                }
                _chromeBrowser.Load(fullPath);

            } finally {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 差分メニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void DiffMenu_Click(object sender, EventArgs e) {
            string diffExe = Properties.Settings.Default.DiffExe;
            if (!File.Exists(diffExe)) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_INVALID_PATH),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int cnt = 1;
            string src1 = "";
            string src2 = "";
            foreach (Row r in this.ResultGrid.Rows.Selected) {
                if (cnt == 1) {
                    src1 = StringUtil.NullToBlank(r[(int)ColIndex.FullPath]);
                } else if (cnt == 2) {
                    src2 = StringUtil.NullToBlank(r[(int)ColIndex.FullPath]);
                    break;
                }
                cnt++;
            }
            Process.Start(Properties.Settings.Default.DiffExe, "\"" + src1 + "\" \"" + src2 + "\"");
        }
        /// <summary>
        /// フォーム表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Shown(object sender, EventArgs e) {
            this.PreviewCheck.Checked = true;
            this.ExpandPreviewCheck.Checked = false;
            this.KeywordText.Focus();
            this.KeywordText.Select(this.KeywordText.Text.Length, 0);
        }
        /// <summary>
        /// プレビュー機能のOnOff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewCheck_CheckedChanged(object sender, EventArgs e) {
            if (this.PreviewCheck.Checked) {
                if (this.PreviewSplitter.IsCollapsed) {
                    this.PreviewSplitter.PerformClick();
                }
            } else {
                if (!this.PreviewSplitter.IsCollapsed) {
                    this.PreviewSplitter.PerformClick();
                }
            }
        }

        /// <summary>
        /// コピーメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMenu_Click(object sender, EventArgs e) {
            this.ResultGrid.CopyEx();
        }

        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e) {
            this.KeywordText.Text = "";
            this.ExtensionText.Text = "";
            this.UpdateDate1.Value = "";
            this.UpdateDate2.Value = "";

            this.KeywordText.Focus();
        }
        /// <summary>
        /// ショートカット設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F && e.Control == true) {
                this.KeywordText.Focus();
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.F && e.Control == true) {
                this.SearchGridText.Focus();
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.P && e.Control == true && e.Shift == true) {
                this.ExpandPreviewCheck.Checked = !this.ExpandPreviewCheck.Checked;
                return;
            }
            if (e.KeyCode == Keys.P && e.Control == true) {
                this.PreviewCheck.Checked = !this.PreviewCheck.Checked;
                return;
            }
        }


        /// <summary>
        /// グリッドフィルタテキストKeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGridText_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                this.FilterGridButton.PerformClick();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllMenu_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index > 0) {
                    r[TargetCheckCol] = true;
                }
            }
        }

        /// <summary>
        /// 全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseAllMenu_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index > 0) {
                    r[TargetCheckCol] = false;
                }
            }
        }

        /// <summary>
        /// フォルダを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenu_Click(object sender, EventArgs e) {
            if (this.TargetIndexGrid.Selection.TopRow < this.TargetIndexGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.TargetIndexGrid[this.TargetIndexGrid.Selection.TopRow,
                (int)IndexBuildForm.ActiveIndexColIdx.IndexedPath + 2]);
            Process.Start(path);
        }

        /// <summary>
        /// コピーメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetCopyMenu_Click(object sender, EventArgs e) {
            this.TargetIndexGrid.CopyEx();
        }
        private void ListViewButton_Click(object sender, EventArgs e) {

        }

        private void TileViewButton_Click(object sender, EventArgs e) {

            //タイル初期化
            this.ResultTile.Groups[0].Tiles.Clear(true);

            //FIXME まずはグリッドと同じ情報を表示してみる
            //for(int i = 1; i <= 12; i++) {
            //    var tile = new Tile();

            //    string fileName = "WS000003.JPG_" + i.ToString();

            //    tile.HorizontalSize = 1;
            //    tile.VerticalSize = 1;
            //    tile.Template = tempDoc;
            //    tile.Text = fileName;
            //    tile.Text1 = "2020/04/23 17:43";
            //    tile.Tag = "Tag";

            //    string tmpUri = SaveToThumbnailBitmap(@"C:\Temp\WS000003.JPG");
            //    tile.Image = Image.FromFile(tmpUri.Replace("file://", ""));
            //    //tile.Click += Tile_Click;

            //    //if (!string.IsNullOrEmpty(photo.ThumbnailUri))
            //    //    _downloadQueue.Enqueue(new DownloadItem(photo.ThumbnailUri, tile, false));
            //    //if (!string.IsNullOrEmpty(photo.AuthorBuddyIconUri))
            //    //    _downloadQueue.Enqueue(new DownloadItem(photo.AuthorBuddyIconUri, tile, true));


            //    this.ResultTile.Groups[0].Tiles.Add(tile);
            //}

            int i = 0;
            foreach(Row r in this.ResultGrid.Rows) {
                if (i < RowHeaderCount) {
                    i++;
                    continue;
                }

                string fileName = StringUtil.NullToBlank(r[(int)ColIndex.FileName]);
                string updateDate = StringUtil.NullToBlank(r[(int)ColIndex.UpdateDate]);
                string fullPath = StringUtil.NullToBlank(r[(int)ColIndex.FullPath]);
                var tile = new Tile();

                tile.HorizontalSize = 1;
                tile.VerticalSize = 1;
                tile.Template = tempDoc;
                tile.Text = fileName;
                tile.Text1 = updateDate;
                tile.Tag = "Tag";

                ShellFile shellFile = ShellFile.FromFilePath(fullPath);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;
                tile.Image = bmp;

                this.ResultTile.Groups[0].Tiles.Add(tile);

                i++;
            }
        }

        private void ExpandPreviewCheck_CheckedChanged(object sender, EventArgs e) {
            if (this.ExpandPreviewCheck.Checked) {
                this.PreviewPanel.Width = (int)(this.Width * 0.9);
                this.BrowserPreviewPanel.Height = (int)(this.Height * 0.8);
            } else {
                this.PreviewPanel.Width = 420;
                this.BrowserPreviewPanel.Height = (int)(this.Height * 0.8);
            }
        }

        private void SelectLocalIndexButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                string createMode = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.CreateMode + 2]);
                if (createMode == EnumUtil.GetLabel(LuceneIndexBuilder.CreateModes.OuterReference)) {
                    r[TargetCheckCol] = false;
                } else {
                    r[TargetCheckCol] = true;
                }
            }
        }

        private void SelectOuterIndexButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                string createMode = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.CreateMode + 2]);
                if (createMode == EnumUtil.GetLabel(LuceneIndexBuilder.CreateModes.OuterReference)) {
                    r[TargetCheckCol] = true;
                } else {
                    r[TargetCheckCol] = false;
                }
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                r[TargetCheckCol] = true;
            }
        }

        private void ReleaseAllButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                r[TargetCheckCol] = false;
            }
        }
        private void TargetIndexGrid_MouseMove(object sender, MouseEventArgs e) {
			ShowTooltip(sender, e);
        }
        #endregion EventHandlers

        #region PrivateMethods
        /// <summary>
        /// グリッドのヘッダを作成
        /// </summary>
        private void CreateHeader() {
            //データクリア
            this.ResultGrid.Rows.Count = RowHeaderCount;
            this.ResultGrid.Cols.Count = Enum.GetValues(typeof(ColIndex)).Length + 1;

            this.ResultGrid[0, (int)ColIndex.FileIcon] = EnumUtil.GetLabel(ColIndex.FileIcon);
            this.ResultGrid.Cols[(int)ColIndex.FileIcon].Width = 20;
            this.ResultGrid.Cols[(int)ColIndex.FileIcon].ImageAlign = ImageAlignEnum.CenterCenter;
            this.ResultGrid[0, (int)ColIndex.FileName] = EnumUtil.GetLabel(ColIndex.FileName);
            this.ResultGrid.Cols[(int)ColIndex.FileName].Width = 200;
            this.ResultGrid[0, (int)ColIndex.FullPath] = EnumUtil.GetLabel(ColIndex.FullPath);
            this.ResultGrid.Cols[(int)ColIndex.FullPath].Width = 400;
            this.ResultGrid[0, (int)ColIndex.Extension] = EnumUtil.GetLabel(ColIndex.Extension);
            this.ResultGrid.Cols[(int)ColIndex.Extension].Width = 40;
            this.ResultGrid[0, (int)ColIndex.UpdateDate] = EnumUtil.GetLabel(ColIndex.UpdateDate);
            this.ResultGrid.Cols[(int)ColIndex.UpdateDate].Width = 100;
            this.ResultGrid[0, (int)ColIndex.Score] = EnumUtil.GetLabel(ColIndex.Score);
            this.ResultGrid.Cols[(int)ColIndex.Score].Width = 80;
            this.ResultGrid[0, (int)ColIndex.DocId] = EnumUtil.GetLabel(ColIndex.DocId);
            this.ResultGrid.Cols[(int)ColIndex.DocId].Width = 40;
            this.ResultGrid[0, (int)ColIndex.Hilight] = EnumUtil.GetLabel(ColIndex.Hilight);
            this.ResultGrid.Cols[(int)ColIndex.Hilight].Width = 600;
        }

        /// <summary>
        /// LuceneのMultiReaderを取得
        /// </summary>
        /// <param name="targetIndexList"></param>
        /// <returns></returns>
        private MultiReader GetMultiReader(List<KeyValuePair<string, string>> targetIndexList, 
            out string unlinkedIndexPath) {

            unlinkedIndexPath = "";
            IndexReader[] idxList = new IndexReader[targetIndexList.Count];
            int cnt = 0;
            foreach (var kvp in targetIndexList) {
                string indexedPath = kvp.Key;
                string storePath = kvp.Value;
                try {
                    java.nio.file.Path idxPath = FileSystems.getDefault().getPath(storePath);
                    var fsDir = FSDirectory.Open(idxPath);
                    var idxReader = DirectoryReader.Open(fsDir);
                    idxList[cnt] = idxReader;
                } catch (java.io.IOException) {
                    //var ex = new UnlinkedIndexException();
                    //ex.TargetIndex = indexedPath;
                    //throw ex;
                    unlinkedIndexPath = indexedPath;
                    return null;
                }

                cnt++;
            }

            return new MultiReader(idxList);
        }

        /// <summary>
        /// 選択された検索対象インデックスを取得
        /// </summary>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> GetSelectedIndex() {
            var ret = new List<KeyValuePair<string, string>>();

            for (int i = 1; i < this.TargetIndexGrid.Rows.Count; i++) {
                Row r = this.TargetIndexGrid.Rows[i];
                if (bool.Parse(StringUtil.NullToBlank(r[TargetCheckCol]))) {
                    string indexedPath = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.IndexedPath + 2]);
                    string storePath = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.IndexStorePath + 2]);
                    ret.Add(new KeyValuePair<string, string>(indexedPath, storePath));
                }
            }
            return ret;
        }

        /// <summary>
        /// 選択されている外部パスの辞書を作成
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CreateSelectedOuterPathDic() {
            var ret = new Dictionary<string, string>();

            for (int i = 1; i < this.TargetIndexGrid.Rows.Count; i++) {
                Row r = this.TargetIndexGrid.Rows[i];
                if (bool.Parse(StringUtil.NullToBlank(r[TargetCheckCol]))) {
                    string outerPath = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.OuterPath + 2]);
                    string localPath = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.LocalPath + 2]);
                    if (outerPath != "" && !ret.ContainsKey(outerPath)) {
                        ret.Add(outerPath, localPath);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 検索
        /// </summary>
        private void Search() {

            if (this.KeywordText.Text == "") {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.MSG_INPUT_KEYWORD),
                    AppObject.GetMsg(AppObject.Msg.TITLE_INFO), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _selectedIndexList = GetSelectedIndex();
            _outerPathDic = CreateSelectedOuterPathDic();
            if (_selectedIndexList.Count == 0) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.MSG_CHECKON_TARGET_INDEX),
                    AppObject.GetMsg(AppObject.Msg.TITLE_INFO), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string unlinkedIndexPath = "";
            MultiReader multiReader = GetMultiReader(_selectedIndexList, out unlinkedIndexPath);
            if (unlinkedIndexPath != "") {
                //検索中断
                MessageBox.Show(string.Format(AppObject.GetMsg(AppObject.Msg.ERR_UNLINKED_INDEX), unlinkedIndexPath),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IndexSearcher idxSearcher = new IndexSearcher(multiReader);

            var allQuery = new BooleanQueryBuilder();
            var contentBqb = new BooleanQueryBuilder();
            //QueryParser titleQp = new QueryParser("Title", AppObject.AppAnalyzer);
            //Query titleQuery = titleQp.Parse("*" + this.KeywordText.Text + "*"); //最初の文字にWildCardは適用されないようだ。
            Query titleQuery = new WildcardQuery(new Term(LuceneIndexBuilder.Title, "*" + QueryParser.Escape(this.KeywordText.Text.ToLower()) + "*"));
            contentBqb.Add(titleQuery, BooleanClauseOccur.SHOULD);
            QueryParser contentQp = new QueryParser(LuceneIndexBuilder.Content, AppObject.AppAnalyzer);
            Query contentQuery = contentQp.Parse(QueryParser.Escape(this.KeywordText.Text));
            contentBqb.Add(contentQuery, BooleanClauseOccur.SHOULD);
            allQuery.Add(contentBqb.Build(), BooleanClauseOccur.MUST);
            if (this.ExtensionText.Text != "") {
                var extBqb = new BooleanQueryBuilder();
                Query extensionQuery = new WildcardQuery(new Term(LuceneIndexBuilder.Extension,
                    "*" + this.ExtensionText.Text.ToLower() + "*"));
                allQuery.Add(extensionQuery, BooleanClauseOccur.MUST);
            }

            TopDocs docs = idxSearcher.Search(allQuery.Build(), MaxSeachResultNum);

            //HACK DataTableに格納してLinqで絞り込む？
            
            Highlighter hi = CreateHilighter(contentBqb.Build());

            try {
                this.ResultGrid.Rows.Count = RowHeaderCount;
                _htmlLabelList.Clear();
                this.ResultGrid.Redraw = false;

                AppObject.Logger.Info("length of top docs: " + docs.ScoreDocs.Length);
                this.ResultGrid.Rows.Count = RowHeaderCount + docs.ScoreDocs.Length;
                this.DenominatorLabel.Text = docs.ScoreDocs.Length.ToString() + "件";
                int row = RowHeaderCount;
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    string fullPath = thisDoc.Get(LuceneIndexBuilder.Path);
                    fullPath = ConvertOuterPath(fullPath);

                    Bitmap bmp = Properties.Resources.File16;
                    if (File.Exists(fullPath)) {
                        try {

                            bmp = Icon.ExtractAssociatedIcon(fullPath).ToBitmap();
                        } catch {
                            //プレビューを取得できない場合は、デフォルトアイコンを表示
                        }
                    }
                    bmp.MakeTransparent();
                    //16,16
                    this.ResultGrid.SetCellImage(row, (int)ColIndex.FileIcon, _bu.Resize(bmp, 16, 16));
                    this.ResultGrid[row, (int)ColIndex.FileName] = thisDoc.Get(LuceneIndexBuilder.Title);
                    this.ResultGrid[row, (int)ColIndex.FullPath] = fullPath;

                    //HACK ループ内のnewを避けれないか?
                    var fi = new FileInfo(fullPath);
                    this.ResultGrid[row, (int)ColIndex.Extension] = fi.Extension;
                    this.ResultGrid[row, (int)ColIndex.UpdateDate] = fi.LastWriteTime;
                    this.ResultGrid[row, (int)ColIndex.Score] = doc.Score;
                    this.ResultGrid[row, (int)ColIndex.DocId] = doc.Doc;

                    // Highlighterで検索キーワード周辺の文字列(フラグメント)を取得
                    // TokenStream が必要なので取得
                    TokenStream stream = TokenSources.GetAnyTokenStream(multiReader,
                            doc.Doc, LuceneIndexBuilder.Content, AppObject.AppAnalyzer);
                    string[] str = hi.GetBestFragments(stream, thisDoc.Get(LuceneIndexBuilder.Content), 5);
                    this.ResultGrid[row, (int)ColIndex.Hilight] = string.Join(",", str);

                    row++;
                }
                SetHtmlLabel();
            } finally {
                multiReader.Close();
                this.ResultGrid.Redraw = true;
            }
        }

        /// <summary>
        /// 外部パスをローカルパスに変換
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string ConvertOuterPath(string fullPath) {
            string ret = fullPath;
            if (_outerPathDic.Count > 0) {
                foreach (var kvp in _outerPathDic) {
                    string outerPath = kvp.Key;
                    string localPath = kvp.Value;
                    if (fullPath.Contains(outerPath)) {
                        ret = ret.Replace(outerPath, localPath);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Highlighter作成
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private Highlighter CreateHilighter(Query query) {

            // Highlighter 作成
            // Formatter と Scorer を与える
            Formatter formatter = new SimpleHTMLFormatter("<font color=\"red\"><b>", "</b></font>");
            QueryScorer scorer = new QueryScorer(query);
            Highlighter highlighter = new Highlighter(formatter, scorer);
            // Fragmenter には SimpleSpanFragmenter を指定
            // 固定長(デフォルト100バイト)でフィールドを分割する。
            // ただしフレーズクエリなどの場合に、クエリが複数のフラグメントに分断されないようにする
            Fragmenter fragmenter = new SimpleSpanFragmenter(scorer);
            highlighter.SetTextFragmenter(fragmenter);

            return highlighter;
        }

        /// <summary>
        /// ファイルを開く
        /// </summary>
        private void OpenFile() {
            if (this.ResultGrid.Selection.TopRow < this.ResultGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            if (File.Exists(path)) {
                Process.Start(path);
            } else {
                //HACK メッセージ
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_FILE_NOT_FOUND),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 検索結果グリッドのハイライト列にHTMLラベルを一括でセット
        /// </summary>
        private void SetHtmlLabel() {
            for (int row = this.ResultGrid.Rows.Fixed; row < this.ResultGrid.Rows.Count; row++) {
                string val = StringUtil.NullToBlank(this.ResultGrid[row, (int)ColIndex.Hilight]);
                var label = new C1SuperLabel();
                label.Text = val;
                label.BackColor = Color.Transparent;

                _htmlLabelList.Add(label);
            }
        }

        /// <summary>
        /// プレビューラベルを更新
        /// </summary>
        private void UpdatePreviewLabel(int selectedRow) {
            this.PreviewWarnLabel.Visible = false;
            this.ShowPreviewButton.Visible = false;

            if (this.ResultGrid.Rows.Fixed <= selectedRow && selectedRow < this.ResultGrid.Rows.Count) {
                string val = StringUtil.NullToBlank(this.ResultGrid.Rows[selectedRow][(int)ColIndex.Hilight]);
                this.PreviewLabel.Text = val;

                if (this.PreviewCheck.Checked) {
                    _chromeBrowser.Visible = true;
                    string extension = StringUtil.NullToBlank(this.ResultGrid.Rows[selectedRow][(int)ColIndex.Extension]);
                    string fullPath = StringUtil.NullToBlank(this.ResultGrid.Rows[selectedRow][(int)ColIndex.FullPath]);
                    if (!File.Exists(fullPath)) {
                        fullPath = "";
                    } else {
                        if (extension.ToLower() == ".pdf") {
                            //そのままブラウザに表示
                        } else if (extension.ToLower() == ".xlsx" ||
                                   extension.ToLower() == ".xls") {

                            this.PreviewWarnLabel.Visible = true;
                            this.ShowPreviewButton.Visible = true;
                            string tmpPath = SaveToThumbnailBitmap(fullPath);
                            fullPath = tmpPath;
                        } else if (extension.ToLower() == ".csv") {
                            this.RichTextBox.Text = LuceneIndexBuilder.ReadToString(fullPath);
                            _chromeBrowser.Visible = false;
                            return;
                        } else if (extension.ToLower() == ".pptx") {

                            this.PreviewWarnLabel.Visible = true;
                            this.ShowPreviewButton.Visible = true;
                            string tmpPath = SaveToThumbnailBitmap(fullPath);
                            fullPath = tmpPath;
                            //_chromeBrowser.Load("about:blank");
                        } else if (extension.ToLower() == ".ppt") {
                            this.PreviewWarnLabel.Visible = true;
                            this.ShowPreviewButton.Visible = true;
                            string tmpPath = SaveToThumbnailBitmap(fullPath);
                            fullPath = tmpPath;
                        } else if (extension.ToLower() == ".docx" ||
                                   extension.ToLower() == ".doc") {

                            this.PreviewWarnLabel.Visible = true;
                            this.ShowPreviewButton.Visible = true;
                            string tmpPath = SaveToThumbnailBitmap(fullPath);
                            fullPath = tmpPath;
                        } else {
                        }
                    }
                    if (fullPath == "") {
                        //TODO プレビュー不可を表示
                        _chromeBrowser.Load("about:blank");
                    } else {
                        _chromeBrowser.Load(fullPath);
                        //NOTE : Load後、何故かResultGridからFocusが外れてPageDown／Upが効かなくなるので
                        //       強制的にResultGridにフォーカスを戻す。
                        _returnFocus = true;
                    }
                }
            }
        }

        /// <summary>
        /// WordファイルをMHTMLとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string SaveDocToMHtml(string fullPath) {
            NetOffice.WordApi.Application wdApp = new NetOffice.WordApi.Application();
            NetOffice.WordApi.Documents docs = null;
            NetOffice.WordApi.Document doc = null;
            string tmpPath = "";

            try {
                docs = wdApp.Documents;
                doc = docs.Open(fullPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".mhtml";
                //MHTMLとして保存
                doc.SaveAs(tmpPath, fileFormat: NetOffice.WordApi.Enums.WdSaveFormat.wdFormatWebArchive);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                doc.Close();
                wdApp.Dispose();
            }
            return tmpPath;
        }
        /// <summary>
        /// WordのファイルをPDFとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string SaveDocToPDF(string fullPath) {
            NetOffice.WordApi.Application wdApp = null;
            NetOffice.WordApi.Documents docs = null;
            NetOffice.WordApi.Document doc = null;
            string tmpPath = "";

            try {
                wdApp = new NetOffice.WordApi.Application();
            } catch (Exception) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_UNINSTALLED_MS_WORD),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return tmpPath;
            }
            try {
                docs = wdApp.Documents;
                doc = docs.Open(fullPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".pdf";
                //PDFとして保存
                doc.SaveAs(tmpPath, fileFormat: NetOffice.WordApi.Enums.WdSaveFormat.wdFormatPDF);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                doc.Close();
                wdApp.Dispose();
            }
            return tmpPath;
        }

        /// <summary>
        /// PowerPointのファイルをXPSとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="over2007"></param>
        /// <returns></returns>
        private string SavePptToXPS(string fullPath, bool over2007) {
            NetOffice.PowerPointApi.Application pptApp = new NetOffice.PowerPointApi.Application();
            NetOffice.PowerPointApi.Presentations presentations = null;
            NetOffice.PowerPointApi.Presentation presentation = null;
            string tmpPath = "";

            try {
                presentations = pptApp.Presentations;
                if (over2007) {
                    presentation = presentations.Open2007(fileName: fullPath,
                                                          readOnly: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                          untitled: Type.Missing,
                                                          withWindow: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                          openAndRepair: Type.Missing);
                } else {
                    presentation = presentations.Open(fileName: fullPath,
                                                      readOnly: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                      untitled: Type.Missing,
                                                      withWindow: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse);
                }
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".xps";
                //XPSとして保存
                presentation.SaveAs(tmpPath, fileFormat: NetOffice.PowerPointApi.Enums.PpSaveAsFileType.ppSaveAsXPS);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                presentation.Close();
                pptApp.Dispose();
            }
            return tmpPath;
        }
        /// <summary>
        /// ExcelをHTMLとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string SaveXlsToHTML(string fullPath) {
            NetOffice.ExcelApi.Application xlsApp = null;
            NetOffice.ExcelApi.Workbooks books = null;
            NetOffice.ExcelApi.Workbook book = null;
            string tmpPath = "";

            try {
                xlsApp = new NetOffice.ExcelApi.Application();
            } catch (Exception) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_UNINSTALLED_MS_XLS),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return tmpPath;
            }
            try {
                books = xlsApp.Workbooks;
                book = books.Open(fullPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".html";
                //MHTMLとして保存
                book.SaveAs(tmpPath, fileFormat: NetOffice.ExcelApi.Enums.XlFileFormat.xlHtml);
            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                book.Close();
                xlsApp.Dispose();
            }
            return tmpPath;
        }
        /// <summary>
        /// ExcelをMHtmlとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string SaveXlsToMHtml(string fullPath) {
            NetOffice.ExcelApi.Application xlsApp = new NetOffice.ExcelApi.Application();
            NetOffice.ExcelApi.Workbooks books = null;
            NetOffice.ExcelApi.Workbook book = null;
            string tmpPath = "";

            try {
                books = xlsApp.Workbooks;
                book = books.Open(fullPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".mhtml";
                //MHTMLとして保存
                book.SaveAs(tmpPath, fileFormat: NetOffice.ExcelApi.Enums.XlFileFormat.xlWebArchive);
            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                book.Close();
                xlsApp.Dispose();
            }
            return tmpPath;
        }
        /// <summary>
        /// パワーポイントのファイルをPDFとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="over2007"></param>
        /// <returns></returns>
        private string SavePptToPDF(string fullPath, bool over2007) {
            NetOffice.PowerPointApi.Application pptApp = null;
            NetOffice.PowerPointApi.Presentations presentations = null;
            NetOffice.PowerPointApi.Presentation presentation = null;
            string tmpPath = "";

            try {
                pptApp = new NetOffice.PowerPointApi.Application();
            } catch (Exception) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_UNINSTALLED_MS_PPT),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return tmpPath;
            }
            try {
                presentations = pptApp.Presentations;
                if (over2007) {
                    presentation = presentations.Open2007(fileName: fullPath,
                                                          readOnly: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                          untitled: Type.Missing,
                                                          withWindow: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                          openAndRepair: Type.Missing);
                } else {
                    presentation = presentations.Open(fileName: fullPath,
                                                      readOnly: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse,
                                                      untitled: Type.Missing,
                                                      withWindow: NetOffice.OfficeApi.Enums.MsoTriState.msoFalse);
                }
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                            @"\" + Guid.NewGuid().ToString() + ".pdf";
                //PDFとして保存
                presentation.SaveAs(tmpPath, fileFormat: NetOffice.PowerPointApi.Enums.PpSaveAsFileType.ppSaveAsPDF);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                presentation.Close();
                pptApp.Dispose();
            }
            return tmpPath;
        }

        /// <summary>
        /// サムネイルBitmapとして保存
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string SaveToThumbnailBitmap(string fullPath) {
            string tmpPath = "";

            try {
                ShellFile shellFile = ShellFile.FromFilePath(fullPath);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;

                //JPGで保存
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) +
                        @"\" + Guid.NewGuid().ToString() + ".jpg";
                _bu.SaveAsJpeg(bmp, tmpPath, 100);
            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
            }
            return "file://" + tmpPath;
        }

        /// <summary>
        /// Chromiumの初期化処理
        /// </summary>
        private void InitializeChromium() {

            CefSettings settings = new CefSettings();
            settings.Locale = "ja";
            settings.LogSeverity = LogSeverity.Disable;
            settings.BrowserSubprocessPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            _chromeBrowser = new ChromiumWebBrowser("about:blank");
            this.BrowserPreviewPanel.Controls.Add(_chromeBrowser);
            _chromeBrowser.Dock = DockStyle.None;
            _chromeBrowser.Size = new Size(412, 470);
            _chromeBrowser.Location = new Point(3, 3);
            _chromeBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _chromeBrowser.BringToFront();

            _chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
            _chromeBrowser.LoadError += ChromeBrowser_LoadError;
        }

        /// <summary>プログレスバー更新イベント</summary>
        /// <param name="isLoading"></param>
        delegate void delegate_UpdateProgressBar(bool isLoading);
        /// <summary>
        /// プレビューブラウザのプログレスバー更新
        /// </summary>
        /// <param name="isLoading"></param>
        private void UpdateProgressBar(bool isLoading) {
            if (isLoading) {
                this.BrowserProgress.Style = ProgressBarStyle.Marquee;
                this.BrowserProgress.MarqueeAnimationSpeed = 100;
            } else {
                this.BrowserProgress.Style = ProgressBarStyle.Blocks;
                this.BrowserProgress.Value = 0;
                this.MainPanel.Focus();
            }
        }

        /// <summary>
        /// セルのツールチップを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTooltip(object sender, System.Windows.Forms.MouseEventArgs e) {
            string text = null;
            if (e.Button == MouseButtons.None) {
                // マウス座標を取得します。
                int row = this.TargetIndexGrid.MouseRow;
                int col = this.TargetIndexGrid.MouseCol;

                // セル上でのみ
                if (row == _lastRow && col == _lastCol) {
                    return;
                }

                _lastRow = row;
                _lastCol = col;

				// ツールチップに表示するテキストを取得します。
				if (row > -1 && col > -1) {
					// 書式設定されている値を取得します。
					text = this.TargetIndexGrid.GetDataDisplay(row, col);

					Rectangle rc = this.TargetIndexGrid.GetCellRect(row, col, false);
					rc.Intersect(this.TargetIndexGrid.ClientRectangle);

					// テキストのサイズを取得します。
					using(Graphics g = this.TargetIndexGrid.CreateGraphics()) {
						CellStyle s = this.TargetIndexGrid.GetCellStyleDisplay(row, col);
						float wid = g.MeasureString(text, s.Font).Width;
						wid += s.Margins.Left + s.Margins.Right + s.Border.Width;
						if(wid < rc.Width) {
							text = null;
						}
					}
				}
            }
			// ツールチップを生成します。
			if (text != null && _ttip == null) {
				_ttip = new ToolTip();
			}

			// ツールチップに表示するテキストを設定します。
			if(_ttip != null && _ttip.GetToolTip(this.TargetIndexGrid) != text) {
				_ttip.SetToolTip(this.TargetIndexGrid, text);
			}
        }

        /// <summary>
        /// プレビューパネル最大化
        /// </summary>
        private void MaximizedPreview() {
            this.PreviewCheck.Checked = true;
            this.PreviewPanel.Width = 1000;
        }
        #endregion PrivateMethods

        private void ResultGrid_Leave(object sender, EventArgs e) {
            if (_returnFocus) {
                //NOTE : Load後、何故かResultGridからFocusが外れてPageDown／Upが効かなくなるので
                //       強制的にResultGridにフォーカスを戻す。
                this.ResultGrid.Focus();
                _returnFocus = false;
            }
        }

        public void CheckedLocalIndex(bool isChecked) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                string createMode = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.CreateMode + 2]);
                if (createMode != EnumUtil.GetLabel(LuceneIndexBuilder.CreateModes.OuterReference)) {
                    r[TargetCheckCol] = isChecked;
                }
            }
        }
        public void CheckedOuterIndex(bool isChecked) {
            foreach (Row r in this.TargetIndexGrid.Rows) {
                if (r.Index < RowHeaderCount) {
                    continue;
                }
                string createMode = StringUtil.NullToBlank(r[(int)IndexBuildForm.ActiveIndexColIdx.CreateMode + 2]);
                if (createMode == EnumUtil.GetLabel(LuceneIndexBuilder.CreateModes.OuterReference)) {
                    r[TargetCheckCol] = isChecked;
                }
            }
        }

        private void FacetTest() {
            string unlinkedIndexPath = "";
            MultiReader multiReader = GetMultiReader(_selectedIndexList, out unlinkedIndexPath);
            IndexSearcher idxSearcher = new IndexSearcher(multiReader);

            FacetsCollector fc = new FacetsCollector();
            //Top10カテゴリを表示
            FacetsCollector.Search(idxSearcher, new MatchAllDocsQuery(), 10, fc);

            //予めカテゴリを登録する必要があるようなので、一旦保留
        }
    }
}
