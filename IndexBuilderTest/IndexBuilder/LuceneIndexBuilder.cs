using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IndexBuilder {
    public class LuceneIndexBuilder {

		private long _bytesTotal = 0;
		private int _countTotal = 0;
		private int _countSkipped = 0;

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="rootPath">インデックスファイル用のルートフォルダ</param>
        /// <param name="indexDir">完成したインデックスファイルの置き場所</param>
        /// <param name="buildDir">ビルド時の中間ファイルの置き場所</param>
        /// <param name="targetDir">検索対象のフォルダ</param>
        public void CreateIndex(string rootPath, string indexDir, string buildDir, string targetDir) {
    		IndexWriter indexWriter = null;
            try {
                try {
                    //日本語を扱う場合は、StandardAnalyzer
                    indexWriter = new IndexWriter(rootPath + buildDir, new StandardAnalyzer(), true);

                    DirectoryInfo di = new DirectoryInfo(targetDir);
                    AddFolder(di, indexWriter);

                    indexWriter.Optimize();
                } finally {
                    if (indexWriter != null) {
                        indexWriter.Close();
                    }
                }

                //作成したIndexファイルを上書き
                DirectoryInfo dir = new DirectoryInfo(rootPath + indexDir);
                foreach (FileInfo fi in dir.GetFiles()) {
                    fi.Delete();
                }
                dir = new DirectoryInfo(rootPath + buildDir);
                foreach (FileInfo fi in dir.GetFiles()) {
                    File.Copy(fi.FullName, rootPath + indexDir + "\\" + fi.Name, true);
                }

				Console.WriteLine("インデックス構築処理完了");
            } finally {
            }
        }
		/// <summary>
		/// 再帰的にフォルダを探索してインデックスに追加
		/// </summary>
		/// <param name="directory"></param>
		private void AddFolder(DirectoryInfo directory, IndexWriter indexWriter) {
			foreach (FileInfo fi in directory.GetFiles()) {
				//Officeのテンポラリファイルは無視。
                if (fi.Name.StartsWith("~")) {
					continue;
                }

				try {
					AddIFilterDocument(fi.FullName, indexWriter);

					//インデックス作成ファイル表示
					_countTotal++;
					_bytesTotal += fi.Length;
					Console.WriteLine(fi.FullName);
				} catch (Exception e) {
					//インデックスが作成できなかったファイルを表示
                    Console.WriteLine(e.Message);
					_countSkipped++;
					Console.WriteLine("Skipped: " + fi.FullName);
				}
			}

			//再帰的にサブフォルダも追加
			foreach (DirectoryInfo di in directory.GetDirectories()) {
				AddFolder(di, indexWriter);
			}
		}
		/// <summary>
		/// iFilterで文字抽出したものをインデックス化
		/// </summary>
		/// <param name="path"></param>
		private void AddIFilterDocument(string path, IndexWriter indexWriter) {
			Document doc = new Document();
			string filename = Path.GetFileName(path);
	
			doc.Add(Field.UnStored("text", IFilterParser.Parse(path)));
			doc.Add(Field.Keyword("path", path));
			doc.Add(Field.Text("title", filename));
			indexWriter.AddDocument(doc);
		}
    }
}
