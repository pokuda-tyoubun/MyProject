using FlexLucene.Analysis;
using FlexLucene.Analysis.Standard;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Search;
using FlexLucene.Store;
using FlexLucene.Util;
using java.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.SandBox {
    //参考サイト
    //https://co2.ddns.net/work-blog/flexlucene_sort_sample/
    public class IndexTest {

        public static Analyzer analyzer = new StandardAnalyzer();
        public static IndexWriterConfig config = new IndexWriterConfig(analyzer);
        public static RAMDirectory ramDirectory = new RAMDirectory();
        public static IndexWriter indexWriter;

        public void Run() {
            CreateIndex();
            SearchSingleTerm("title", "lucene");
            ramDirectory.Close();
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
                indexWriter = new IndexWriter(ramDirectory, config);
                createDoc("Sam", "Lucece index option analyzed vs not analyzed", "2016-12-12 20:19:57");
                createDoc("Sam", "Lucene field boost and query time boost example", "2016-03-16 16:57:44");
                createDoc("Jack", "How to do Lucene search highlight example", "2016-03-16 17:47:38");
                createDoc("Smith", "Lucene BooleanQuery is depreacted as of 5.3.0", "2015-04-30 11:44:25");
                createDoc("Smith", "What is term vector in Lucene", "2015-04-10 20:33:53");

                indexWriter.Close();
            } catch (IOException  ex) {
                System.Console.WriteLine("Exception : " + ex.getLocalizedMessage());
            }
        }

        public static void SearchIndexNoSortAndDisplayResults(Query query) {
            try {
                IndexReader idxReader = DirectoryReader.Open(ramDirectory);
                IndexSearcher idxSearcher = new IndexSearcher(idxReader);

                TopDocs docs = idxSearcher.Search(query, 10);
                System.Console.WriteLine("length of top docs: " + docs.ScoreDocs.Length);
                foreach (ScoreDoc doc in docs.ScoreDocs) {
                    Document thisDoc = idxSearcher.Doc(doc.Doc);
                    System.Console.WriteLine(doc.Doc + "\t" + thisDoc.Get("author")
                            + "\t" + thisDoc.Get("title"));
                }
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
            }
        }

        public static void SearchIndexAndDisplayResults(Query query) {
            try {
                IndexReader idxReader = DirectoryReader.Open(ramDirectory);
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
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
            }
        }
        public static void SearchSingleTerm(String field, String termText) {
            Term term = new Term(field, termText);
            TermQuery termQuery = new TermQuery(term);

            SearchIndexAndDisplayResults(termQuery);
            SearchIndexNoSortAndDisplayResults(termQuery);
        }
    }
}
