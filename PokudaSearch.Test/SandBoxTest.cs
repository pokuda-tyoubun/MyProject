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

namespace PokudaSearch.Test {
    [TestClass]
    public class SandBoxTest {
        [TestMethod]
        public void MultiReaderTest() {

            //ユーザ辞書の設定
            java.io.Reader treader = new java.io.FileReader(@"C:\Workspace\Repo\Git\MyProject\PokudaSearch\UserDictionary.txt");
            UserDictionary userDic = null;
            try {
                //ユーザ辞書
                userDic = UserDictionary.Open(treader);
            } finally {
                treader.close();    
            }
            //Analyzer
            var analyzer = new JapaneseAnalyzer(userDic, 
                JapaneseTokenizerMode.SEARCH,
                JapaneseAnalyzer.GetDefaultStopSet(), 
                JapaneseAnalyzer.GetDefaultStopTags());

            //テスト用IndexReaderを２つ準備する。
            java.nio.file.Path idxPath1 = FileSystems.getDefault().getPath(
                @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\IndexForTest\Index1");
            java.nio.file.Path idxPath2 = FileSystems.getDefault().getPath(
                @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\IndexForTest\Index2");
            var fsDir1 = FSDirectory.Open(idxPath1);
            var fsDir2 = FSDirectory.Open(idxPath2);

            IndexReader ir1 = DirectoryReader.Open(fsDir1);
            IndexReader ir2 = DirectoryReader.Open(fsDir2);

            IndexReader[] indexReaders = new IndexReader[2];
            indexReaders[0] = ir1;
            indexReaders[1] = ir2;

            var multiReader = new FlexLucene.Index.MultiReader(indexReaders);
            var idxSearcher = new IndexSearcher(multiReader);

            Query titleQuery = new WildcardQuery(new Term(LuceneIndexBuilder.IndexFieldTitle, "*mbom*"));

            //HACK 上位200件の旨を表示
            TopDocs docs = idxSearcher.Search(titleQuery, 200);

            Trace.WriteLine("length of top docs: " + docs.ScoreDocs.Length);
            foreach (ScoreDoc doc in docs.ScoreDocs) {
                Document thisDoc = idxSearcher.Doc(doc.Doc);
                string fullPath = thisDoc.Get(LuceneIndexBuilder.IndexFieldPath);
                Trace.WriteLine(fullPath);
            }
        }
    }
}
