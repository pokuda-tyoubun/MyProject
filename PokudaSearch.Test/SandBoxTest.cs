using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexLucene.Queryparser.Surround.Parser;
using FlexLucene.Queryparser.Classic;
using FlexLucene.Analysis.Ja;
using System.IO;
using PokudaSearch.IndexUtil;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Search;
using java.nio.file;
using FlexLucene.Store;
using FlexLucene.Index;
using System.Diagnostics;
using FlexLucene.Document;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PokudaSearch.Test.TaskCompletionSource;
using FlexLucene.Analysis.Core;
using com.sun.istack.@internal;
using TikaOnDotNet.TextExtraction;
using jdk.nashorn.@internal.runtime.logging;
using com.rometools.rome.feed.atom;
using Hnx8.ReadJEnc;
using javax.xml.crypto;
using org.apache.fontbox.encoding;
using PokudaSearch.Win32API;
using org.quartz;
using FxCommonLib.Utils;
using PokudaSearch.WebDriver;

namespace PokudaSearch.Test {
    [TestClass]
    public class SandBoxTest {
        [TestMethod]
        public void WebApiTest() {
            var api = new DMMWebAPIUtil();

            var list = api.GetItemList("test");
        }
        [TestMethod]
        public void ExcelWasteNameTest() {
            string root = @"C:\Workspace\Repo\Git\ecoLLaboMES\doc";
            //Excelファイルを探す
            var fileList = FileUtil.GetAllFileInfo(root);
            foreach (FileInfo fi in fileList) {
                if (fi.Extension.ToLower() == ".xls" || fi.Extension.ToLower() == ".xlsx") {

                }
            }
        }
        [TestMethod]
        public void SHGetKnownFolderPathTest() {
            string path;

            //ダウンロードフォルダ
            //Guid id = new Guid("374DE290-123F-4565-9164-39C4925E467B");
            //ライブラリ
            Guid id = new Guid("031E4825-7B94-4DC3-B131-E946B44C8DD5");
            int result = Shell32.SHGetKnownFolderPath(id, 0, IntPtr.Zero, out path);
            Debug.Print(path);
        }
        [TestMethod]
        public void DetectingEncodeTest() {
            var path = @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\TestData\TestDocs\細則.txt";

            byte[] bytes = null;
            using (var fs = new FileStream(path, FileMode.Open)) {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
            }
            string str = null;
            var encode = ReadJEnc.JP.GetEncoding(bytes, bytes.Length, out str);

            Debug.WriteLine(encode.ToString());
        }

        private CharCode GetCharCode(string path) {

            byte[] bytes = null;
            using (var fs = new FileStream(path, FileMode.Open)) {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
            }
            string str = null;
            var encode = ReadJEnc.JP.GetEncoding(bytes, bytes.Length, out str);

            return encode;
        }

        //private string ConvertSJisToUtf8(string src) {
        //    byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(src);
        //    string ret = '%'+ BitConverter.ToString(bytesData).Replace('-','%');
        //    return ret;
        //}
        private string ConvertSJisToUtf8(string src) {
            var sjis = System.Text.Encoding.GetEncoding("Shift_JIS");
            var utf8Bytes = System.Text.Encoding.Convert(sjis, System.Text.Encoding.UTF8, sjis.GetBytes(src.ToCharArray()));
            var utf8Str = System.Text.Encoding.UTF8.GetString(utf8Bytes);

            return utf8Str;
        }

        public static string ConvertEncoding(string src, System.Text.Encoding destEnc) {
        	byte[] srcTemp = System.Text.Encoding.ASCII.GetBytes(src);
        	byte[] destTemp = System.Text.Encoding.Convert(System.Text.Encoding.ASCII, destEnc, srcTemp);
        	string ret = destEnc.GetString(destTemp);
        	return ret;
        }

        [TestMethod]
        public void TikaExtractorTest() {
            var txtExtractor = new TextExtractor();

            var path = @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\TestData\TestDocs\細則.txt";
            //var path = @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\TestData\TestDocs\201000161035技術検討ＷＧ会議の実施について.xls";
            //var path = @"C:\Workspace\Repo\Git\ecoLLaboMES\doc\※顧客別\003.MDIS\002.打合せ記録\20170612_13_製造指図,POP\議事メモ.txt";


            //Shift-JisはUtf-8に変換
            string src = "";
            string result = "";
            if (GetCharCode(path) == CharCode.SJIS) {
                using (var reader = new StreamReader(path, System.Text.Encoding.GetEncoding("Shift_JIS"))) {
                    src = reader.ReadToEnd();
                }
                //result = ConvertEncoding(src, System.Text.Encoding.GetEncoding("Shift_JIS"));
                result = ConvertSJisToUtf8(src);
            }

            var content = txtExtractor.Extract(path);

            Debug.WriteLine(content.Text);
            Debug.WriteLine(src);
            Debug.WriteLine(result);
        }

        [TestMethod]
        public void LongPointTest() {
            var analyzer = new WhitespaceAnalyzer();
            var iwc = new IndexWriterConfig(analyzer);

            iwc.SetOpenMode(IndexWriterConfigOpenMode.CREATE);

            //インデックス作成---------------------------------------------
            DateTime baseDate = DateTime.Parse("2020/07/16 08:00:00");
            var ram = new RAMDirectory();
            var writer = new IndexWriter(ram, iwc);
            try {
                for (int i = 0; i < 10; i++) {
                    var doc = new Document();
                    doc.Add(new TextField("text", "hoge foo", FieldStore.YES));
                    DateTime tmp = baseDate.AddDays(i);
                    long l = long.Parse(tmp.ToString("yyyyMMddHHmmss"));
                    doc.Add(new LongPoint("date", l));
                    doc.Add(new StoredField("date", l));

                    writer.AddDocument(doc);
                }
                
            } finally {
                writer.Close();
            }

            //検索------------------------------------------------------------
            TermQuery tq = new TermQuery(new Term("text", "foo"));
            Query rq = LongPoint.NewRangeQuery("date", 20200717000000, 20200719000000);

            BooleanQueryBuilder b = new BooleanQueryBuilder();
            b.Add(tq, BooleanClauseOccur.MUST); //AND条件
            b.Add(rq, BooleanClauseOccur.FILTER); //AND条件（スコアリングに関与しない）
            Query q = b.Build();

            DirectoryReader dr = DirectoryReader.Open(ram);
            IndexSearcher searcher = new IndexSearcher(dr);
            ScoreDoc[] hits = searcher.Search(q, 100).ScoreDocs;
            for (int i = 0; i < hits.Length; i++) {
                var doc = searcher.Doc(hits[i].Doc);
                Debug.WriteLine(DateTime.ParseExact(doc.Get("date"), "yyyyMMddHHmmss", null));
            }

            Assert.AreEqual(hits.Length, 2);
        }
        [TestMethod]
        public void DateTimeToBinaryTest() {
            var d = DateTime.Parse("2020/07/14 00:00:00");
            long l = d.ToBinary();
            var result = DateTime.FromBinary(l);
            Assert.AreEqual(result, d);
        }
        [TestMethod]
        public void FilePropertiesTest() {
            var file = ShellFile.FromFilePath(@"C:\Temp\test.jpg");
            //拡張プロパティ取得
            Console.WriteLine(file.Properties.System.Title.Value);
            Console.WriteLine(file.Properties.System.Author.Value);
            Console.WriteLine(file.Properties.System.Comment.Value);

            //拡張プロパティセット
            ShellPropertyWriter propertyWriter =  file.Properties.GetPropertyWriter();
            propertyWriter.WriteProperty(SystemProperties.System.Title, new string[] { "タイトル" });
            propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { "著者" });
            propertyWriter.WriteProperty(SystemProperties.System.Comment, new string[] { "コメント" });
            propertyWriter.Close();
        }
        [TestMethod]
        public void PathReplaceTest() {
            string path1 = @"S:\bin\DB\test.db";
            string path2 = @"C:\Temp\bin\IndexStore\Index1";

            string tmp1 = path1.Substring(0, path1.IndexOf(@"bin\DB"));
            string tmp2 = path2.Substring(path2.IndexOf(@"bin\IndexStore"));
            Assert.AreEqual(tmp1 + tmp2, @"S:\bin\IndexStore\Index1");
        }



        [TestMethod]
        public void TaskCompletionSourceTest() {

            var p = new Program();
            p.RunAsync();

            //ステータス変更
            //p.CurrentStatus = Program.Status.Successful;

            var task = p.CompletionSource.Task;
            try {
                task.Wait();
            } catch { }
            Console.WriteLine(task.Status);

        }
    }
}
