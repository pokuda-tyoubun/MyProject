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
        //HACK 全ての検索条件の実装
        //HACK title部分の検索の仕方に工夫の余地あり。
        //HACK ディレクトリを絞って検索する機能が欲しい
        //      KWIC Finder風のＵＩにする
        //HACK 転置インデックスではなく、その場で抽出して検索する機能も欲しい

        //--------------------------------------------------------------
        //DONE titleの検索をCaseInsensitiveにする。
        //DONE Queryにも同じAnalyzerを適用する必要がある。

        #region Constants
        private const int RowHeaderCount = 1;

        /// <summary>列定義</summary>
        private enum ColIndex : int {
            [EnumLabel("ファイル名")]
            FileName = 1,
            [EnumLabel("パス")]
            FullPath,
            [EnumLabel("拡張子")]
            Extention,
            [EnumLabel("更新日")]
            UpdateDate,
            [EnumLabel("スコア")]
            Score,
            [EnumLabel("ハイライト")]
            Hilight,
            [EnumLabel("描画済み")]
            Drawed
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
            this.ResultGrid[0, (int)ColIndex.Drawed] = EnumUtil.GetLabel(ColIndex.Drawed);
            this.ResultGrid.Cols[(int)ColIndex.Drawed].Width = 60;
        }

        private void Search() {

            java.nio.file.Path idxPath = FileSystems.getDefault().getPath(AppObject.RootDirPath + Consts.IndexDirName);
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
            foreach (ScoreDoc doc in docs.ScoreDocs) {
                Document thisDoc = idxSearcher.Doc(doc.Doc);
                string fullPath = thisDoc.Get("path");

                this.ResultGrid[row, (int)ColIndex.FileName] = thisDoc.Get("title");
                this.ResultGrid[row, (int)ColIndex.FullPath] = fullPath;

                //HACK ループ内のnewを避けれないか?
                var fi = new FileInfo(fullPath);
                this.ResultGrid[row, (int)ColIndex.Extention] = fi.Extension;
                this.ResultGrid[row, (int)ColIndex.UpdateDate] = fi.LastWriteTime;
                this.ResultGrid[row, (int)ColIndex.Score] = doc.Score;

                // Highlighterで検索キーワード周辺の文字列(フラグメント)を取得
                // デフォルトのSimpleHTMLFormatterは <B> タグで検索キーワードを囲って返す
                // TokenStream が必要なので取得
                TokenStream stream = TokenSources.GetAnyTokenStream(idxReader,
                        doc.Doc, "content", AppObject.AppAnalyzer);
                string[] str = hi.GetBestFragments(stream, thisDoc.Get("content"), 5);
                this.ResultGrid[row, (int)ColIndex.Hilight] = string.Join(",", str);

                row++;
            }
        }

        private Highlighter CreateHilighter(Query query) {

            // Highlighter 作成
            // Formatter と Scorer を与える
            Formatter formatter = new SimpleHTMLFormatter();
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
            Process.Start(path);
        }

        //HACK*Grid上にhtmlを表示できたが、かなり重い
        // →プレーンテキスト表示にして、プレビューパネルで表示した方が良い
        C1SuperLabel _htmlLabel = new C1SuperLabel();
        private void ResultGrid_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e) {
            //if (e.Col == (int)ColIndex.Hilight && e.Row >= this.ResultGrid.Rows.Fixed) {  
            //    // draw background 
            //    e.DrawCell(DrawCellFlags.Background); 
            //    // use the C1SuperLabel to draw the html text  
            //    //if (StringUtil.NullToBlank(this.ResultGrid[e.Row, (int)ColIndex.Drawed]) != "1" &&
            //    //    e.Bounds.Width > 0 && e.Bounds.Height > 0) {
            //    if (e.Bounds.Width > 0 && e.Bounds.Height > 0) {
            //        _htmlLabel.Text = StringUtil.NullToBlank(this.ResultGrid[e.Row, e.Col]);
            //        _htmlLabel.BackColor = Color.Transparent;  
            //        _htmlLabel.DrawToGraphics(e.Graphics, e.Bounds);

            //        this.ResultGrid[e.Row, (int)ColIndex.Drawed] = "1";
            //    }  
            //    // and draw border last  
            //    e.DrawCell(DrawCellFlags.Border);  
            //    // we're done with this cell  
            //    e.Handled = true;  
            //} 
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
