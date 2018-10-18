using Lucene.Net.Index;
using Lucene.Net.Store;
using NMeCab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JapaneseAnalyzerTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void AnalyzeButton_Click(object sender, EventArgs e) {
            var param = new MeCabParam();
            param.DicDir = @"..\..\dic\ipadic";

            MeCabTagger t = MeCabTagger.Create(param);
            MeCabNode node = t.ParseToNode(this.TestText.Text);
            var result = new StringBuilder("");
            while (node != null) {
                if (node.CharType > 0) {
                    result.AppendLine(node.Surface + "\t" + node.Feature);
                }
                node = node.Next;
            }

            this.ResultText.Text = result.ToString();
        }

        private void CreateIndexButton_Click(object sender, EventArgs e) {
            string indexPath = @"..\..\index";

            var indexDirectory = FSDirectory.Open(new DirectoryInfo(indexPath));
            bool isExists = IndexReader.IndexExists(indexDirectory);
            if (isExists) {
                System.IO.Directory.Delete(indexPath, true);
                System.IO.Directory.CreateDirectory(indexPath);
            }
            var writer = new IndexWriter(indexDirectory, NMeCab.A);
        }
    }
}
