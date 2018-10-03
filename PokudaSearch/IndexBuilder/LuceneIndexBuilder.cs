using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Store;
using FxCommonLib.FTSIndexer;
using FxCommonLib.Utils;
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
        //HACK*FastVectorHilighterに対応させる。(以下のURLを参考に実装)
        //        参考：https://gist.github.com/mocobeta/57a8f61250468180607d
        //HACK フィージビリティを確認できたらFxCommonLibにこのクラスを移行し、IndexBuilderも再構成すること
        //HACK アプリ終了後もインデックス作成プロセスが残っている。
        //HACK ユーザ(流行語)辞書登録機能を実装
        //        参考：https://ichigo.hopto.org/2017/11/29/userdictionary_flexlucene_lucene_net/

        //DONE インデックスが追記モードになっているっぽい
        //DONE C\Tempでインデックスを作成してもキーワードが引っ掛からないのは何故か
        //     →hilightFieldType指定が誤っているようだ

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

        private FieldType _hilightFieldType = new FieldType();

        public LuceneIndexBuilder(TextBox logViewer, TextExtractMode txtExtractMode) {
            _logViewer = logViewer;
            _txtExtractMode = txtExtractMode;

            SetHighlightFieldContentType(_hilightFieldType);
        }

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="rootPath">インデックスファイル用のルートフォルダ</param>
        /// <param name="indexDir">完成したインデックスファイルの置き場所</param>
        /// <param name="buildDir">ビルド時の中間ファイルの置き場所</param>
        /// <param name="targetDir">検索対象のフォルダ</param>
        public void CreateIndex(Analyzer analyzer, string rootPath, string indexDir, string buildDir, string targetDir) {

            //インデックスを削除
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + buildDir));

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
					AddDocument(fi.FullName, indexWriter);

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
		/// 指定したテキスト抽出器でテキスト化したものをインデックス化
		/// テキスト抽出器の種類は以下のとおり
		/// 　・Apache Tika
		/// 　・IFilter
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indexWriter"></param>
        private void AddDocument(string path, IndexWriter indexWriter) {
			Document doc = new Document();
			string filename = System.IO.Path.GetFileName(path);
			string extention = System.IO.Path.GetExtension(path);

            if (_txtExtractMode == TextExtractMode.Tika) {
                var content = _txtExtractor.Extract(path);
    			//doc.Add(new TextField("content", content.Text, FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), _hilightFieldType));
            } else {
    			//doc.Add(new TextField("content", IFilterParser.Parse(path), FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), _hilightFieldType));
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

            //PostingsHighlighter用
            //fieldType.SetIndexOptions(IndexOptions.DOCS_AND_FREQS_AND_POSITIONS_AND_OFFSETS);

            //FastVectorHighlighterの時は以下を指定
            fieldType.SetIndexOptions(IndexOptions.DOCS_AND_FREQS_AND_POSITIONS);
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