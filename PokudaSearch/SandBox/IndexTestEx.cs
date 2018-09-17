using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Standard;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Search;
using FlexLucene.Store;
using FlexLucene.Util;
using java.io;
using java.nio.file;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokudaSearch.SandBox {
    //参考サイト
    //https://co2.ddns.net/work-blog/flexlucene_sort_sample/
    //※2018.09.14段階のFlexLucene.dllではJapaneseAnalyzer()を使うとAddDocument(doc)
    //実行時にExceptionが発生するので以下のURLのdllを利用
    //https://github.com/FlexSearch/FlexLucene/blob/master/Artifacts/FlexLucene.dll
    public class IndexTestEx {

        private const string IndexDir = @"c:\temp\PokudaIndex";

        public static Analyzer analyzer = new JapaneseAnalyzer();
        public static IndexWriterConfig config = new IndexWriterConfig(analyzer);
        private static FSDirectory _dir = null;
        public static IndexWriter indexWriter;

        public void Run() {
            CreateIndex();
            SearchSingleTerm("content", "更改");
            //ramDirectory.Close();
        }

        public static void createDoc(String author, String title, String date) {
            Document doc = new Document();
            doc.Add(new TextField("author", author, FieldStore.YES));
            doc.Add(new TextField("title", title, FieldStore.YES));
            //doc.add(new Field ("date", date, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new SortedDocValuesField("date", new BytesRef(date) ));
            doc.Add(new StoredField("date", date));
 
            indexWriter.AddDocument(doc);
        }

        public static void CreateIndex() {
            try {
                java.nio.file.Path idxPath = FileSystems.getDefault().getPath(IndexDir);
                _dir = FSDirectory.Open(idxPath);
                indexWriter = new IndexWriter(_dir, config);

                string[] files = System.IO.Directory.GetFiles(@"C:\Workspace\MyToolBox\Tool\FullTextSearchCCC\kitei", "*.htm*", System.IO.SearchOption.AllDirectories);

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
                        indexWriter.AddDocument(doc);
                    }
                } catch (System.IO.IOException ex) {
                    System.Console.WriteLine(ex.ToString());
                }
                indexWriter.Close();
            } catch (java.io.IOException  ex) {
                System.Console.WriteLine("Exception : " + ex.getLocalizedMessage());
            }
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
        public static void SearchIndexNoSortAndDisplayResults(Query query) {
            try {
                IndexReader idxReader = DirectoryReader.Open(_dir);
                IndexSearcher idxSearcher = new IndexSearcher(idxReader);

                TopDocs docs = idxSearcher.Search(query, 10);
                System.Console.WriteLine("length of top docs: " + docs.ScoreDocs.Length);
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    System.Console.WriteLine(doc.Doc + "\t" + thisDoc.Get("title"));
                }
            } catch (java.io.IOException e) {
                e.printStackTrace();
            } finally {
            }
        }

        public static void SearchIndexAndDisplayResults(Query query) {
            try {
                IndexReader idxReader = DirectoryReader.Open(_dir);
                IndexSearcher idxSearcher = new IndexSearcher(idxReader);

                Sort sort = new Sort(new SortField("date", SortFieldType.STRING, true), SortField.FIELD_SCORE);

                TopDocs docs = idxSearcher.Search(query, 10, sort, true, true);
                System.Console.WriteLine("length of top docs: " + docs.ScoreDocs.Length + " sort by: " + sort);
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    System.Console.WriteLine(doc.Doc + "\t" + thisDoc.Get("author")
                            + "\t" + thisDoc.Get("title")
                            + "\t" + thisDoc.Get("date"));
                }
            } catch (java.io.IOException e) {
                e.printStackTrace();
            } finally {
            }
        }
        public static void SearchSingleTerm(String field, String termText) {
            Term term = new Term(field, termText);
            TermQuery termQuery = new TermQuery(term);

            //SearchIndexAndDisplayResults(termQuery);
            SearchIndexNoSortAndDisplayResults(termQuery);
        }
    }
}
