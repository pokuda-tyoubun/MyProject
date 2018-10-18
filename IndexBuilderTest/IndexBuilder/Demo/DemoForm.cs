using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IndexBuilder.Demo {
    public partial class DemoForm : Form {

        private string _rootDir = @"";
        private string _indexDir = @"\Index";
        private string _buildDir = @"\Build";


        public DemoForm() {
            InitializeComponent();

            _rootDir = Directory.GetParent(Application.ExecutablePath).FullName;
            _rootDir += @"\DemoIndex";
        }

        /// <summary>
        /// 参照ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, EventArgs e) {
            this.FolderBrowserDialog.SelectedPath = this.PathText.Text;
            if (this.FolderBrowserDialog.ShowDialog() == DialogResult.OK) {
                this.PathText.Text = this.FolderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// インデックス作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateIndexButton_Click(object sender, EventArgs e) {
            LuceneIndexBuilder lib = new LuceneIndexBuilder();

            Directory.CreateDirectory(_rootDir);
            Directory.CreateDirectory(_rootDir + _indexDir);
            Directory.CreateDirectory(_rootDir + _buildDir);

            Cursor.Current = Cursors.WaitCursor;
            try {
                lib.CreateIndex(_rootDir, _indexDir, _buildDir, this.PathText.Text);
                status("インデックス作成完了");
            } finally {
                this.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e) {
            Cursor.Current = Cursors.WaitCursor;
            try {
                Search();
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

		private Query parseQuery(string searchQuery, QueryParser parser) {
			Query query;
			try {
				query = parser.Parse(searchQuery.Trim());
			}
			catch (ParseException) {
				query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
			}
			return query;
		}
        /// <summary>
        /// 検索
        /// </summary>
        private void Search() {
            DateTime start = DateTime.Now;
            IndexSearcher searcher = null;
            Lucene.Net.Store.Directory indexDirectory = null;
            try {
                //ファイルシステムインデックスの場所を取得
                indexDirectory = FSDirectory.Open(new DirectoryInfo(_rootDir + _indexDir));
                searcher = new IndexSearcher(indexDirectory);
            } catch (IOException ex) {
                MessageBox.Show("インデックスファイルが存在しないか、破損しております。インデックスを再作成してください。\n詳細：\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.ResultsList.Items.Clear();

            string keyword = this.QueryText.Text.Trim();
            if (keyword == String.Empty) {
                return;
            }

			var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "text", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
			var query = parseQuery(keyword, parser);

            // Search
			var hits_limit = 1000;
			var hits = searcher.Search(query, hits_limit).ScoreDocs;

            foreach (var scoreDoc in hits) {
                var result = new SearchResults();
                var doc = searcher.Doc(scoreDoc.Doc);
                result.FileName = doc.GetField("title").StringValue;
                result.Path = doc.GetField("path").StringValue;
                result.Folder = Path.GetDirectoryName(result.Path);

                ListViewItem item = new ListViewItem(new string[] {null, filename, di.FullName, hits.Score(i).ToString()});
                item.Tag = path;
                this.ResultsList.Items.Add(item);
                Application.DoEvents();
            }

            foreach (var hit in hits) {
                Document doc = hit.Doc;
            }

            for (int i = 0; i < hits.Length; i++) {
                Document doc = hits..Doc[i];
                searcher.Doc[]

                // 検索結果をセット
                string filename = doc.Get("title");
                string path = doc.Get("path");
                string folder = Path.GetDirectoryName(path);
                DirectoryInfo di = new DirectoryInfo(folder);

                ListViewItem item = new ListViewItem(new string[] {null, filename, di.FullName, hits.Score(i).ToString()});
                item.Tag = path;
                this.ResultsList.Items.Add(item);
                Application.DoEvents();
            } 
            searcher.Close();

            string searchReport = String.Format("検索時間：{0} 該当ファイル数：{1}", (DateTime.Now - start), hits.Length());
            status(searchReport);
        }
        private void status(string msg) {
            status(msg, false);
        }

        private void status(string msg, bool error) {
            this.StatusLabel.Text = msg;

            if (error) {
                this.StatusLabel.ForeColor = Color.Red;
            } else {
                this.StatusLabel.ForeColor = DefaultForeColor;
            }

            Application.DoEvents();
        }

        private void ResultsList_DoubleClick(object sender, EventArgs e) {

            ListViewItem lvi = this.ResultsList.SelectedItems[0];
            Process.Start(lvi.SubItems[2].Text + @"\" + lvi.SubItems[1].Text);
        }

        private void OpenParentFolderMenu_Click(object sender, EventArgs e) {
            ListViewItem lvi = this.ResultsList.SelectedItems[0];
            Process.Start(lvi.SubItems[2].Text);
        }

        private void OpenFileMenu_Click(object sender, EventArgs e) {
            ListViewItem lvi = this.ResultsList.SelectedItems[0];
            Process.Start(lvi.SubItems[2].Text + @"\" + lvi.SubItems[1].Text);
        }
    }
}
