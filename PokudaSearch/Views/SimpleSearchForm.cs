using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Queryparser.Classic;
using FlexLucene.Search;
using FlexLucene.Search.Highlight;
using FlexLucene.Store;
using FxCommonLib.Consts;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using java.nio.file;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.IndexBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class SimpleSearchForm : Form {

        #region Constants
        /// <summary>検索結果一覧のヘッダ行数</summary>
        private const int RowHeaderCount = 1;

        /// <summary>列定義</summary>
        private enum ColIndex : int {
            [EnumLabel("種類")]
            FileIcon = 1,
            [EnumLabel("ファイル名")]
            FileName,
            [EnumLabel("パス")]
            FullPath,
            [EnumLabel("拡張子")]
            Extention,
            [EnumLabel("更新日")]
            UpdateDate,
            [EnumLabel("スコア")]
            Score,
            [EnumLabel("ハイライト")]
            Hilight
        }
        #endregion Constants

        private object _xlApp = null;
        private ExcelUtil _eu = new ExcelUtil();

        private object _ppApp = null;
        private PowerPointUtil _ppu = new PowerPointUtil();

        private NetOffice.WordApi.Application _wdApp = null;

        private COMUtil _com = new COMUtil();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SimpleSearchForm() {
            InitializeComponent();

            CreateHeader();

        }

        private void SimpleSearchForm_Load(object sender, EventArgs e) {
            //キャッシュしておく
            _xlApp = _com.CreateObject("Excel.Application");
            _eu.SetVisible(_xlApp, false);
            _ppApp = _com.CreateObject("Powerpoint.Application");
            //_ppu.SetVisible(_ppApp, PowerPointUtil.MsoTriState.msoTrue);

             _wdApp = new NetOffice.WordApi.Application();
        }

        private void WriteExcelButton_Click(object sender, EventArgs e) {
            ProgressDialog pd = new ProgressDialog(AppObject.MLUtil.GetMsg(CommonConsts.ACT_EXTRACT), 
                new DoWorkEventHandler(Extract2Excel_DoWork), 0);

            DialogResult result;
            this.Cursor = Cursors.WaitCursor;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    AppObject.Logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SearchButton_Click(object sender, EventArgs e) {
            Cursor.Current = Cursors.WaitCursor;
            try {
                Search();
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

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
            this.ResultGrid.Cols[(int)ColIndex.FullPath].Width = 500;
            this.ResultGrid[0, (int)ColIndex.Extention] = EnumUtil.GetLabel(ColIndex.Extention);
            this.ResultGrid.Cols[(int)ColIndex.Extention].Width = 60;
            this.ResultGrid[0, (int)ColIndex.UpdateDate] = EnumUtil.GetLabel(ColIndex.UpdateDate);
            this.ResultGrid.Cols[(int)ColIndex.UpdateDate].Width = 60;
            this.ResultGrid[0, (int)ColIndex.Score] = EnumUtil.GetLabel(ColIndex.Score);
            this.ResultGrid.Cols[(int)ColIndex.Score].Width = 60;
            this.ResultGrid[0, (int)ColIndex.Hilight] = EnumUtil.GetLabel(ColIndex.Hilight);
            this.ResultGrid.Cols[(int)ColIndex.Hilight].Width = 600;
        }

        private void Search() {

            if (this.KeywordText.Text == "") {
                //TODO キーワード入力を促す
                return;
            }

            java.nio.file.Path idxPath = FileSystems.getDefault().getPath(AppObject.RootDirPath + LuceneIndexBuilder.IndexDirName);
            var fsDir = FSDirectory.Open(idxPath);
            IndexReader idxReader = DirectoryReader.Open(fsDir);
            IndexSearcher idxSearcher = new IndexSearcher(idxReader);

            var allQuery = new BooleanQueryBuilder();
            var contentBqb = new BooleanQueryBuilder();
            //QueryParser titleQp = new QueryParser("title", AppObject.AppAnalyzer);
            //Query titleQuery = titleQp.Parse("*" + this.KeywordText.Text + "*"); //最初の文字にWildCardは適用されないようだ。
            Query titleQuery = new WildcardQuery(new Term("title", "*" + this.KeywordText.Text.ToLower() + "*"));
            contentBqb.Add(titleQuery, BooleanClauseOccur.SHOULD);
            QueryParser contentQp = new QueryParser("content", AppObject.AppAnalyzer);
            Query contentQuery = contentQp.Parse(this.KeywordText.Text);
            contentBqb.Add(contentQuery, BooleanClauseOccur.SHOULD);
            allQuery.Add(contentBqb.Build(), BooleanClauseOccur.MUST);
            if (this.ExtentionText.Text != "") {
                var extBqb = new BooleanQueryBuilder();
                Query extentionQuery = new WildcardQuery(new Term("extention", "*" + this.ExtentionText.Text.ToLower() + "*"));
                allQuery.Add(extentionQuery, BooleanClauseOccur.MUST);
            }

            //HACK 上位1000件の旨を表示
            TopDocs docs = idxSearcher.Search(allQuery.Build(), 1000);

            //HACK DataTableに格納してLinqで絞り込む？

            Highlighter hi = CreateHilighter(contentBqb.Build());

            try {
                this.ResultGrid.Redraw = false;

                AppObject.Logger.Info("length of top docs: " + docs.ScoreDocs.Length);
                this.ResultGrid.Rows.Count = RowHeaderCount + docs.ScoreDocs.Length;
                int row = RowHeaderCount;
                BitmapUtil bu = new BitmapUtil();
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    string fullPath = thisDoc.Get("path");

                    Bitmap bmp = Properties.Resources.File16;
                    if (File.Exists(fullPath)) {
                        try {
                            ShellFile shellFile = ShellFile.FromFilePath(fullPath);
                            bmp = shellFile.Thumbnail.LargeBitmap;
                        } catch {
                            //プレビューを取得できない場合は、デフォルトアイコンを表示
                        }
                    }
                    bmp.MakeTransparent();
                    //16,16
                    this.ResultGrid.SetCellImage(row, (int)ColIndex.FileIcon, bu.Resize(bmp, 256, 256));
                    this.ResultGrid[row, (int)ColIndex.FileName] = thisDoc.Get("title");
                    this.ResultGrid[row, (int)ColIndex.FullPath] = fullPath;

                    //HACK ループ内のnewを避けれないか?
                    var fi = new FileInfo(fullPath);
                    this.ResultGrid[row, (int)ColIndex.Extention] = fi.Extension;
                    this.ResultGrid[row, (int)ColIndex.UpdateDate] = fi.LastWriteTime;
                    this.ResultGrid[row, (int)ColIndex.Score] = doc.Score;

                    // Highlighterで検索キーワード周辺の文字列(フラグメント)を取得
                    // TokenStream が必要なので取得
                    TokenStream stream = TokenSources.GetAnyTokenStream(idxReader,
                            doc.Doc, "content", AppObject.AppAnalyzer);
                    string[] str = hi.GetBestFragments(stream, thisDoc.Get("content"), 5);
                    this.ResultGrid[row, (int)ColIndex.Hilight] = string.Join(",", str);

                    row++;
                }
                SetHtmlLabel();
            } finally {
                this.ResultGrid.Redraw = true;
            }

        }

        private Highlighter CreateHilighter(Query query) {

            // Highlighter 作成
            // Formatter と Scorer を与える
            Formatter formatter = new SimpleHTMLFormatter("<FONT color=\"red\"><B>", "</B></FONT>");
            QueryScorer scorer = new QueryScorer(query);
            Highlighter highlighter = new Highlighter(formatter, scorer);
            // Fragmenter には SimpleSpanFragmenter を指定
            // 固定長(デフォルト100バイト)でフィールドを分割する。
            // ただしフレーズクエリなどの場合に、クエリが複数のフラグメントに分断されないようにする
            Fragmenter fragmenter = new SimpleSpanFragmenter(scorer);
            highlighter.SetTextFragmenter(fragmenter);

            return highlighter;
        }

        private void OpenParentMenu_Click(object sender, EventArgs e) {
            if (this.ResultGrid.Selection.TopRow < this.ResultGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            Process.Start(System.IO.Directory.GetParent(path).FullName);
        }

        private void OpenFileMenu_Click(object sender, EventArgs e) {
            OpenFile();
        }

        private void ResultGrid_DoubleClick(object sender, EventArgs e) {
            MouseEventArgs me = (MouseEventArgs)e;
            if (this.ResultGrid.HitTest(me.X, me.Y).Row < this.ResultGrid.Rows.Fixed) {
                //ヘッダ部は処理しない。
                return;
            }
            OpenFile();
        }

        private void OpenFile() {
            if (this.ResultGrid.Selection.TopRow < this.ResultGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            if (File.Exists(path)) {
                Process.Start(path);
            } else {
                //HACK メッセージ
                MessageBox.Show("ファイルが存在しません。", AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResultGrid_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e) {
            if (e.Col == (int)ColIndex.Hilight && e.Row >= this.ResultGrid.Rows.Fixed) {  
                // draw background 
                e.DrawCell(DrawCellFlags.Background); 
                // use the C1SuperLabel to draw the html text  
                //if (StringUtil.NullToBlank(this.ResultGrid[e.Row, (int)ColIndex.Drawed]) != "1" &&
                //    e.Bounds.Width > 0 && e.Bounds.Height > 0) {
                string val = StringUtil.NullToBlank(this.ResultGrid[e.Row, e.Col]);
                if (e.Bounds.Width > 0 && 
                    e.Bounds.Height > 0 && 
                    val.IndexOf("<B>") >= 0 &&
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
        List<C1SuperLabel> _htmlLabelList = new List<C1SuperLabel>();
        private void SetHtmlLabel() {
            for (int row = this.ResultGrid.Rows.Fixed; row < this.ResultGrid.Rows.Count; row++) {
                string val = StringUtil.NullToBlank(this.ResultGrid[row, (int)ColIndex.Hilight]);
                var label = new C1SuperLabel();
                label.Text = val;
                label.BackColor = Color.Transparent;

                _htmlLabelList.Add(label);
            }
        }

        private void SimpleSearchForm_FormClosed(object sender, FormClosedEventArgs e) {
            _eu.Quit(_xlApp);
            _com.MReleaseComObject(_xlApp);

            _ppu.Quit(_ppApp);
            _com.MReleaseComObject(_ppApp);

            _wdApp.Quit();

            MainFrameForm.SimpleSearchForm = null;
        }

        private void ResultGrid_SelChange(object sender, EventArgs e) {
            //プレビュー更新
            UpdatePreviewLabel();
        }

        /// <summary>
        /// プレビューラベルを更新
        /// </summary>
        private void UpdatePreviewLabel() {
            int row = this.ResultGrid.Selection.TopRow;
            if (row >= this.ResultGrid.Rows.Fixed) {
                string val = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.Hilight]);
                this.PreviewLabel.Text = val;

                if (this.PreviewCheck.Checked) {
                    string extention = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.Extention]);
                    string fullPath = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.FullPath]);
                    if (extention.ToLower() == ".pdf") {
                        //そのままブラウザに表示
                    } else if (extention.ToLower() == ".xlsx" ||
                               extention.ToLower() == ".xls") {
                        string tmpPath = SaveXlsToMHtml(fullPath);
                        fullPath = tmpPath;
                    } else if (extention.ToLower() == ".pptx") {
                        string tmpPath = SavePptToXPS(fullPath, over2007:true);
                        fullPath = tmpPath;
                    } else if (extention.ToLower() == ".ppt") {
                        string tmpPath = SavePptToXPS(fullPath, over2007:false);
                        fullPath = tmpPath;
                    } else if (extention.ToLower() == ".docx") {
                        string tmpPath = SaveDocToMHtml(fullPath);
                        fullPath = tmpPath;
                    } else {
                        //TODO
                    }

                    if (fullPath == "") {
                        //TODO プレビュー不可を表示
                        this.WebBrowser.Navigate("");
                    } else {
                        this.WebBrowser.Navigate(fullPath);
                    }
                }
            }
        }

        private string SaveDocToMHtml(string fullPath) {
            NetOffice.WordApi.Documents docs = null;
            NetOffice.WordApi.Document doc = null;
            string tmpPath = "";

            try {
                docs = _wdApp.Documents;
                doc = docs.Open(fullPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + 
                            @"\" + Guid.NewGuid().ToString() + ".mhtml";
                //XPSとして保存
                doc.SaveAs(tmpPath, fileFormat: NetOffice.WordApi.Enums.WdSaveFormat.wdFormatWebArchive);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                doc.Close();
            }
            return tmpPath;
        }

        private string SavePptToXPS(string fullPath, bool over2007) {
            object presentations = null;
            object presentation = null;
            string tmpPath = "";

            try {
                presentations = _ppu.GetPresentations(_ppApp);
                if (over2007) {
                    presentation = _ppu.Open2007(presentations, fullPath, PowerPointUtil.MsoTriState.msoFalse);
                } else {
                    presentation = _ppu.Open(presentations, fullPath, PowerPointUtil.MsoTriState.msoFalse);
                }
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + 
                            @"\" + Guid.NewGuid().ToString() + ".xps";
                //XPSとして保存
                _ppu.SaveAs(presentation, tmpPath, PowerPointUtil.PpFileFormat.PpSaveAsXPS);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                _ppu.Close(presentation);
                _com.MReleaseComObject(presentation);
                _com.MReleaseComObject(presentations);
            }
            return tmpPath;
        }


        private string SaveXlsToMHtml(string fullPath) {

            object books = null;
            object book = null;
            string tmpPath = "";

            try {
                books = _eu.GetWorkbooks(_xlApp);
                book = _eu.Open(books, fullPath, "");
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + 
                            @"\" + Guid.NewGuid().ToString() + ".mhtml";
                //単一ファイルWebページとして保存
                _eu.SaveAs(book, tmpPath, ExcelUtil.XlFileFormat.XlWebArchive);

            } catch (Exception e) {
                AppObject.Logger.Warn(e.Message);
                //プレビュー不可
                tmpPath = "";
            } finally {
                _eu.Close(book);
                _com.MReleaseComObject(book);
                _com.MReleaseComObject(books);
            }
            return tmpPath;
        }

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
        /// フォーム表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleSearchForm_Shown(object sender, EventArgs e) {
            this.PreviewCheck.Checked = false;
        }

        /// <summary>
        /// WebBrowserのプログレスバー変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) {
            if (e.CurrentProgress >= 0) {
                this.BrowserProgress.Maximum = (int)e.MaximumProgress;
                this.BrowserProgress.Value = (int)e.CurrentProgress;
            } else {
                this.BrowserProgress.Maximum = 0;
                this.BrowserProgress.Value = 0;
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


        //private void SearchOld() {
        //    DateTime start = DateTime.Now;
        //    IndexSearcher searcher = null;

        //    try {
        //        searcher = new IndexSearcher(_rootDir + _indexDir);
        //    } catch (IOException ex) {
        //        MessageBox.Show("インデックスファイルが存在しないか、破損しております。インデックスを再作成してください。\n詳細：\n" + ex.Message,
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    this.ResultsList.Items.Clear();

        //    string keyword = this.QueryText.Text.Trim();
        //    if (keyword == String.Empty) {
        //        return;
        //    }

        //    Query query = QueryParser.Parse(keyword, "text", new StandardAnalyzer());

        //    // Search
        //    Hits hits = searcher.Search(query);

        //    for (int i = 0; i < hits.Length(); i++) {
        //        Document doc = hits.Doc(i);

        //        // 検索結果をセット
        //        string filename = doc.Get("title");
        //        string path = doc.Get("path");
        //        string folder = Path.GetDirectoryName(path);
        //        DirectoryInfo di = new DirectoryInfo(folder);

        //        ListViewItem item = new ListViewItem(new string[] {null, filename, di.FullName, hits.Score(i).ToString()});
        //        item.Tag = path;
        //        this.ResultsList.Items.Add(item);
        //        Application.DoEvents();
        //    } 
        //    searcher.Close();

        //    string searchReport = String.Format("検索時間：{0} 該当ファイル数：{1}", (DateTime.Now - start), hits.Length());
        //    status(searchReport);
        //}
    }
}
