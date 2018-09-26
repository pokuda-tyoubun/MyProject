using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Search;
using FlexLucene.Store;
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
            Score
        }
        #endregion Constants

        public static Analyzer _analyzer = new JapaneseAnalyzer();
        public static IndexWriterConfig _config = new IndexWriterConfig(_analyzer);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SimpleSearchForm() {
            InitializeComponent();

            CreateHeader();
        }

        private void WriteExcelButton_Click(object sender, EventArgs e) {

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
        }

        private void Search() {

            java.nio.file.Path idxPath = FileSystems.getDefault().getPath(AppObject.RootDirPath + Consts.IndexDirName);
            var fsDir = FSDirectory.Open(idxPath);
            IndexReader idxReader = DirectoryReader.Open(fsDir);
            IndexSearcher idxSearcher = new IndexSearcher(idxReader);

            var term = new Term("content", this.KeywordText.Text);
            TermQuery termQuery = new TermQuery(term);
            //HACK 上位1000件の旨を表示
            TopDocs docs = idxSearcher.Search(termQuery, 1000);

            //HACK DataTableに格納してLinqで絞り込む？

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

                row++;
            }
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
            OpenFile();
        }

        private void OpenFile() {
            if (this.ResultGrid.Selection.TopRow < this.ResultGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.ResultGrid[this.ResultGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            Process.Start(path);
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
