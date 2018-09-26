using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Store;
using FxCommonLib.FTSIndexer;
using java.nio.file;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch.IndexBuilder {
    public class LuceneIndexBuilder {
        //HACK フィージビリティを確認できたらFxCommonLibにこのクラスを移行し、IndexBuilderも再構成すること
        //HACK アプリ終了後もインデックス作成プロセスが残っている。
        //HACK*FastVectorHilighterに対応させる。(以下のURLを参考に実装)
        //        参考：https://gist.github.com/mocobeta/57a8f61250468180607d


        public enum TextExtractMode : int {
            Tika = 0,
            IFilter
        }

        private TextExtractor _txtExtractor = new TextExtractor();

		private long _bytesTotal = 0;
		private int _countTotal = 0;
		private int _countSkipped = 0;

        private TextBox _logViewer = null;

        private TextExtractMode _txtExtractMode = TextExtractMode.Tika;

        private StringBuilder _extractLog = new StringBuilder(""); 

        public LuceneIndexBuilder(TextBox logViewer, TextExtractMode txtExtractMode) {
            _logViewer = logViewer;
            _txtExtractMode = txtExtractMode;
        }

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="rootPath">インデックスファイル用のルートフォルダ</param>
        /// <param name="indexDir">完成したインデックスファイルの置き場所</param>
        /// <param name="buildDir">ビルド時の中間ファイルの置き場所</param>
        /// <param name="targetDir">検索対象のフォルダ</param>
        public void CreateIndex(string rootPath, string indexDir, string buildDir, string targetDir) {
            Analyzer analyzer = new JapaneseAnalyzer();
            IndexWriterConfig config = new IndexWriterConfig(analyzer);
    		IndexWriter indexWriter = null;

            try {
                var indexBuildDir = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + buildDir));
                indexWriter = new IndexWriter(indexBuildDir, config);

                var di = new DirectoryInfo(targetDir);
                AddDirRecursively(di, indexWriter);
            } finally {
                if (indexWriter != null) {
                    //クローズ時にIndexファイルがフラッシュされる
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

            AppendLogViewer("インデックス構築処理完了");
        }

        /// <summary>
		/// 再帰的にフォルダを探索してインデックスに追加
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="indexWriter"></param>
        private void AddDirRecursively(DirectoryInfo directory, IndexWriter indexWriter) {

			foreach (FileInfo fi in directory.GetFiles()) {
				//Officeのテンポラリファイルは無視。
                if (fi.Name.StartsWith("~")) {
					continue;
                }

				try {
					AddTikaDocument(fi.FullName, indexWriter);

					//インデックス作成ファイル表示
					_countTotal++;
					_bytesTotal += fi.Length;
                    AppendLogViewer(fi.FullName);
				} catch (Exception e) {
					//インデックスが作成できなかったファイルを表示
                    AppObject.Logger.Error(e.Message);
					_countSkipped++;
                    AppendLogViewer("Skipped: " + fi.FullName);
				}

                if (((_countTotal + _countSkipped) % 10) == 0) {
                    _logViewer.Text = (_countTotal + _countSkipped).ToString() + "件 処理済み";
                    Application.DoEvents();
                }
			}
            _logViewer.Text = (_countTotal + _countSkipped).ToString() + "件 処理済み";

			//再帰的にサブフォルダも追加
			foreach (DirectoryInfo di in directory.GetDirectories()) {

				AddDirRecursively(di, indexWriter);
			}
        }

        /// <summary>
		/// Apache Tika で文字抽出したものをインデックス化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indexWriter"></param>
        private void AddTikaDocument(string path, IndexWriter indexWriter) {
			Document doc = new Document();
			string filename = System.IO.Path.GetFileName(path);
			string extention = System.IO.Path.GetExtension(path);

            FieldType hilightFieldType = new FieldType();
            if (_txtExtractMode == TextExtractMode.Tika) {
                var content = _txtExtractor.Extract(path);
    			//doc.Add(new TextField("content", content.Text, FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), hilightFieldType));
            } else {
    			//doc.Add(new TextField("content", IFilterParser.Parse(path), FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), hilightFieldType));
            }

			doc.Add(new StringField("path", path, FieldStore.YES));
			doc.Add(new StringField("title", filename, FieldStore.YES));
			indexWriter.AddDocument(doc);
        }

        /// <summary>
        /// ハイライト対象フィールド用のフィールド定義
        /// </summary>
        private void SetHighlightFieldContentType(FieldType fieldType) {
            // IndexOptions.DOCS : ドキュメントのみインデックスする
            // IndexOptions.DOCS_AND_FREQS : ドキュメントと語の出現頻度をインデックスする
            // IndexOptions.DOCS_AND_FREQS_AND_POSITIONS : ドキュメントと語の出現頻度と出現位置をインデックスする。
            //                                              full scoring, フレーズ検索等が可能になる。
            // IndexOptions.DOCS_AND_FREQS_AND_POSITIONS_AND_OFFSETS;ドキュメントと語の出現頻度と出現位置と文字オフセットをインデックスする。

            fieldType.SetIndexOptions(IndexOptions.DOCS_AND_FREQS_AND_POSITIONS_AND_OFFSETS);
            fieldType.SetStored(true);
            fieldType.SetTokenized(true);
            // FastvectorHighlighterを使うための設定
            fieldType.SetStoreTermVectors(true);
            fieldType.SetStoreTermVectorPositions(true);
            fieldType.SetStoreTermVectorOffsets(true);
        }

        private void AppendLogViewer(string msg) {
            AppObject.Logger.Info(msg);

            if (_logViewer != null) {
                _extractLog.Insert(0, msg + "\r\n");
            }
        }
    }
}