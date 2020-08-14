using FlexLucene.Index;
using FlexLucene.Store;
using java.nio.file;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.IndexUtil {


    public struct DocInfo {
        public int Id;
        public string Path;
        public DateTime UpdateDate;
        public bool Exists;
    }

    public class LuceneIndexReaderUtil {


        /// <summary>
        /// Luceneインデックスに登録されているドキュメントの辞書を作成
        /// </summary>
        /// <param name="idxDir"></param>
        /// <returns></returns>
        public Dictionary<string, DocInfo> CreateDocumentDic(FSDirectory idxDir) {
            Dictionary<string, DocInfo> dic = new Dictionary<string, DocInfo>();
            IndexReader ir = DirectoryReader.Open(idxDir);

            try {
                int max = ir.MaxDoc();
                for (int i = 0; i < max; i++) {
                    var doc = ir.Document(i);

                    var docInfo = new DocInfo();
                    docInfo.Id = i;
                    docInfo.Path = doc.GetField(LuceneIndexBuilder.Path).StringValue();
                    docInfo.UpdateDate = DateTime.FromBinary(long.Parse(doc.GetField(LuceneIndexBuilder.UpdateDate).StringValue()));
                    //docInfo.UpdateDate = DateTime.FromBinary(long.Parse(doc.GetBinaryValue(LuceneIndexBuilder.UpdateDate).ToString()));
                    docInfo.Exists = false;
                    
                    if (!dic.ContainsKey(docInfo.Path)) {
                        dic.Add(docInfo.Path, docInfo);
                    }
                }
            } finally {
                ir.Close();
            }

            return dic;
        }

        public int GetDocumentCount(string idxDirPath) {
            FSDirectory fsIdxDirPath = null;
            IndexReader ir = null;
            try {
                fsIdxDirPath = FSDirectory.Open(FileSystems.getDefault().getPath(idxDirPath));
                ir = DirectoryReader.Open(fsIdxDirPath);
                return ir.MaxDoc();
            } finally {
                ir.Close();
                fsIdxDirPath.Close();
            }
        }
    }
}
