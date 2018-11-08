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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SimpleSearchForm() {
            InitializeComponent();

            CreateHeader();
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

            var bqb = new BooleanQueryBuilder();
            //QueryParser titleQp = new QueryParser("title", AppObject.AppAnalyzer);
            //Query titleQuery = titleQp.Parse("*" + this.KeywordText.Text + "*"); //最初の文字にWildCardは適用されないようだ。
            Query titleUpperQuery = new WildcardQuery(new Term("title", "*" + this.KeywordText.Text.ToUpper() + "*"));
            bqb.Add(titleUpperQuery, BooleanClauseOccur.SHOULD);
            Query titleLowerQuery = new WildcardQuery(new Term("title", "*" + this.KeywordText.Text.ToLower() + "*"));
            bqb.Add(titleLowerQuery, BooleanClauseOccur.SHOULD);
            QueryParser contentQp = new QueryParser("content", AppObject.AppAnalyzer);
            Query contentQuery = contentQp.Parse(this.KeywordText.Text);
            bqb.Add(contentQuery, BooleanClauseOccur.SHOULD);

            //HACK 上位1000件の旨を表示
            TopDocs docs = idxSearcher.Search(bqb.Build(), 1000);

            //HACK DataTableに格納してLinqで絞り込む？

            Highlighter hi = CreateHilighter(bqb.Build());

            AppObject.Logger.Info("length of top docs: " + docs.ScoreDocs.Length);
            this.ResultGrid.Rows.Count = RowHeaderCount + docs.ScoreDocs.Length;
            int row = RowHeaderCount;
            BitmapUtil bu = new BitmapUtil();
            foreach (ScoreDoc doc in docs.ScoreDocs) {
                Document thisDoc = idxSearcher.Doc(doc.Doc);
                string fullPath = thisDoc.Get("path");

                Bitmap bmp = Properties.Resources.File16;
                if (File.Exists(fullPath)) {
                    ShellFile shellFile = ShellFile.FromFilePath(fullPath);
                    bmp = shellFile.Thumbnail.Bitmap;
                }
                bmp.MakeTransparent();
                this.ResultGrid.SetCellImage(row, (int)ColIndex.FileIcon, bu.Resize(bmp, 16, 16));
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
            if (row > this.ResultGrid.Rows.Fixed) {
                string val = StringUtil.NullToBlank(this.ResultGrid.Rows[row][(int)ColIndex.Hilight]);
                this.PreviewLabel.Text = val;
            }
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
