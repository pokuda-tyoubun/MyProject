﻿using FlexLucene.Analysis.Ja;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Store;
using java.nio.file;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch.SandBox {
    public partial class TestForm : Form {

        private const string IndexDir = @"c:\temp\PokudaIndex";

        public TestForm() {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e) {
        }

        private static void HTMLParse(ref string title, ref string content, ref string fileName) {
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));
            string text = sr.ReadToEnd();
            //正規表現パターンとオプションを指定してRegexオブジェクトを作成 
            Regex rTitle = new Regex("<title[^>]*>(.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex rPre = new Regex("<body[^>]*>(.*?)</body>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //TextBox1.Text内で正規表現と一致する対象をすべて検索 
            MatchCollection mcTitle = rTitle.Matches(text);
            MatchCollection mcPre = rPre.Matches(text);

            foreach (Match m in mcTitle) {
                title = m.Groups[1].Value;
            }
            foreach (Match m in mcPre) {
                content = m.Groups[1].Value;
            }
        }

        private void CreateIndexButton_Click(object sender, EventArgs e) {
            java.nio.file.Path idxPath = FileSystems.getDefault().getPath(IndexDir);
            FSDirectory dir = FSDirectory.Open(idxPath);

            JapaneseAnalyzer analyzer = new JapaneseAnalyzer();
            IndexWriterConfig config = new IndexWriterConfig(analyzer);
            IndexWriter writer = new IndexWriter(dir, config);

            string[] files = System.IO.Directory.GetFiles(this.TargetDirText.Text, "*.htm*", System.IO.SearchOption.AllDirectories);

            try {

                foreach (string file in files) {
                    string title, content, f;
                    title = "";
                    content = "";
                    f = file;
                    HTMLParse(ref title, ref content, ref f);

                    Field fldTitle = new StringField("title", title, FieldStore.YES);
                    Field fldPlace = new StringField("place", f, FieldStore.YES);
                    Field fldContent = new TextField("content", content, FieldStore.YES);

                    Document doc = new Document();
                    doc.Add(fldTitle);
                    doc.Add(fldPlace);
                    doc.Add(fldContent);
                    writer.AddDocument(doc);
                }
            } catch (System.IO.IOException ex) {
                System.Console.WriteLine(ex.ToString());
            }
            writer.Close();
        }

        private void TestButton_Click(object sender, EventArgs e) {
            var it = new IndexTestEx();
            it.Run();
        }

        private void TargetDirText_TextChanged(object sender, EventArgs e) {

        }

        private void TikaTestButton_Click(object sender, EventArgs e) {
            TextExtractor txtExt = new TextExtractor();

            var wordDocContents = txtExt.Extract(@"C:\Workspace\Repo\Git\ecoLLaboMES\doc\01.標準化\Fx標準化.xlsx");
            System.Console.WriteLine(wordDocContents.Text);

            var webPageContents = txtExt.Extract(new Uri("https://ichigo.hopto.org"));
            System.Console.WriteLine(webPageContents.Text);
        }

        private void ThumbnailButton_Click(object sender, EventArgs e) {
            this.PictureBox.Image = CreateThumbnail(this.PathText.Text, 1);
        }

        private Bitmap CreateThumbnail(string path, int scale) {
            // ファイルが存在した場合
            FileInfo fi = new FileInfo(path);
            if (fi.Exists) {
                ShellFile shellFile = ShellFile.FromFilePath(path);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;
                int w = (int)(bmp.Width * scale);
                int h = (int)(bmp.Height * scale);
                return bmp;
            }
 
            //TODO ファイルが存在しない場合はデフォルト表示
            return null;
            
        }
    }
}
