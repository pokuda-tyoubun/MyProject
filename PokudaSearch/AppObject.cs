using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FxCommonLib.Models;
using FxCommonLib.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch {
    public static class AppObject {
        
        //着手候補の優先度------------------
        //HACK*インデックスをディレクトリ毎に予約化し、マルチスレッドで作成する。（結果をマージしない。）
            //t_index_historyに予約-作成状況を記録
            //複数のパスをIndex化できる。
            //選択したIndexを削除できる。
            //WebページもIndex化できる。

        //HACK*差分Updateモードを実装する。

        //HACK*.chmをダウンロードしないように

        //HACK*FileExplorerのパスコンボを画面の大きさに応じてサイズ変更するようにする。
        //HACK*7zip、TortoiseGitのコンテキストメニューを表示するようにする。
        //HACK*通常のエクスプローラーを表示できるようにする。
        //HACK*Alt↑を有効にする。

        //HACK グリッドフィルタリングにMigemoを組み込む
        //     参考 http://thinkingskeever.hatenablog.com/entry/2018/03/11/003000

        //HACK Officeなどプレビューに時間がかかるファイルは、プレビュー表示ボタンで初めて表示するのでも良い。

        //HACK 拡張子"txt"のファイルはReadToEndでテキストを受け取るようにする。

        //HACK 検索対象フォルダを絞り込めるか？

        //HACK 文書のカテゴライズ
        //HACK ファセット機能の実装

        //HACK 重複ファイル検知
            //サイズ && Tikaの抽出内容
            //ユーグリッド法(類似)

        //HACK 類似画像検索
            //apatch alike

        //HACK インデックス有効拡張子マスタメンテ機能を設ける。

        //HACK売り方--------------------------------------------------------------
        //基本機能は無償にする。
        //ヘルプサイトに広告を付ける。
        //上位機能を有償版にする?
        //寄与方式にする？(簡単に寄与できる仕組み,クラファン？1Click課金みたいな)
        //HACK売り方--------------------------------------------------------------

        //HACK AI機能(上位オプション)----------------------------------------------
        //・画像ファイルの自動命名
        //  →Microsoft Computer Vision API
        //・画像検索(画像とテキストの相互検索)
        //  →A3RT Image Search API
        //・文章の要約
        //  →TextSummarization
        //・ランキング学習
        //・サジェスチョン
        //HACK AI機能--------------------------------------------------------------

        //HACK*Officeのプレビュー(mhtml or pdfに変換してWebBrowserで表示)

        //HACK 検索スコアにBM25Simiralityも選択できるようにする。


        //HACKインデックス作成高速化----------------------------------------------
        //HACK    →GroupDocsで実現できそうだが有料
        //HACK*インデックスをディレクトリ毎に予約化し、マルチスレッドで作成する。（結果をマージしない。）
        //HACK*差分Updateモードを実装する。
        //HACK*親画面を閉じた時に、インデックス作成を中断する。
        //HACK*シングルかマルチか上位で分けるように実装
        //HACK ディレクトリが異なればindexを分けてMultiReader(Lucene3ではMultiSearcher)を使うようにする。
            //PokudaSearchTest.MultiReaderTestを参照
        //HACK Yahoo Googleの検索オプションのUIを真似る(P222も参考に)
        //HACK 特定のサイトもインデックス対象とする
        //HACK   →html、xmlはtikaを使う必要があるかも
        //HACK サーバーサイドでは、DelayClosedIndexSercherを使う。(Session毎にスレッドが違うので問題ない？)
        //HACKインデックス作成高速化----------------------------------------------

        //HACK UI高度化----------------------------------------------
        //HACK*ファイルエクスプローラの実装
            //HACK Viキーバインド化--------------------------------------------------
            //HACK "/yakyu"で"野球"にフォーカスを当てれるようにする。+"n"で次の野球にできる?
            //HACK or "/"でCtrl+Fにする
        //HACK 検索キーワードのサジェスチャー機能の作成（過去の入力値で学習？、Solrに機能がある？）
        //HACK コンテキストメニューにGitが表示されないが参考になるかも
        //HACK https://www.codeproject.com/Articles/14707/Dual-Pane-File-Manager
        //HACK CodeProjectでは、以下が参考になりそう（コンテキストメニューのGitが表示されていないが）
        //HACK 上側のURLの方をまずは解析してみる。
        //HACK    https://www.xlsoft.com/jp/products/groupdocs/index.html
        //HACK    https://www.codeproject.com/Articles/15059/C-File-Browser
        //HACK UI高度化----------------------------------------------

        //HACK*FastVectorHilighterに対応させる。(以下のURLを参考に実装)
        //        参考：https://gist.github.com/mocobeta/57a8f61250468180607d
        //HACK フィージビリティを確認できたらFxCommonLibにこのクラスを移行し、IndexBuilderも再構成すること
        //HACK アプリ終了後もインデックス作成プロセスが残っている。
        //HACK ユーザ(流行語)辞書登録機能を実装
        //        参考：https://ichigo.hopto.org/2017/11/29/userdictionary_flexlucene_lucene_net/

        //HACK ログの出力先をexeの側に変更(ファイルの設定は、デフォルトに？)
        //HACK 最大フィールド長1000らしいフィールド長を確認してみる
        //HACK SaveFSIndexFromRAMIndexに集約
        //HACK CopyIndexDirに集約
        //HACK 全ての検索条件の実装
        //HACK title部分の検索の仕方に工夫の余地あり。
        //HACK ディレクトリを絞って検索する機能が欲しい
        //      KWIC Finder風のＵＩにする
        //HACK 転置インデックスではなく、その場で抽出して検索する機能も欲しい
        //HACK インデックス以下をクロールしてサムネイルを作成する。

        //DONE List--------------------------------------------------------------------------
        //DONE SQLiteを活用する。
        //DONE 類似ファイル検索
        //DONE Apache Tikaだと大量ファイル時にインデックス作成が完了しない。（最後で止まる）
        //     →インデックス作成から6時間20分後にIndex Writer is Closedのエラーが発生している。
        //          →特定のファイルでcloseになったのか？
        //              →エラーが起きたフォルダだけを対象にしてみたが問題なかった。
        //          →Luceneのタイムアウトか?
        //              →タイムアウト設定方法が判らないので、定期的にcommitしてみることに。
        //          →Taskのタイムアウトか？
        //          →メモリ不足が原因→512MBバッファで解決
        //            https://doc.support-dreamarts.com/Luxor/V20/Luxor_Ver.2.0_%E9%81%8B%E7%94%A8%E3%82%AC%E3%82%A4%E3%83%89/index_migrationtool/index.html
        //DONE IndexWriter is closedをキャッチして中断できないか
        //DONE CodePackを使って、縮小表示を有効化してもらう。
        //     →"2JPEG"ライブラリもあるが商用$149->結局は、Office Interopのラッパーだった。
        //DONE 半角スラッシュを検索するとExceptionが発生
        //DONE タスクバー上のプログレス表示対応
        //DONE デフォルトを×ファイルにしてファイルが存在しないことを判るようにする。
        //DONE インデックス作成時のカウントの所でもマーキーのプログレスを表示する。
        //DONE PDFのプレビュー機能(WebBrowserコントロールで実現)
        //却下 インデックス作成高速化----------------------------------------------
        //却下  マルチプロセス→実装は楽だが、進度の仕組みをどうするか？(共有メモリ?)
        //却下  マルチスレッド処理をoutofmemoryにならないように 
        //却下  大量ファイルでインデックスを作成した場合に、segmentsファイルが作成されない。
        //却下  →最後にCommit()を追記
        //却下      →効果なし
        //却下  →1000ファイル毎ににCommit()を追記
        //却下      →効果なし
        //却下  →本体に少しずつマージ
        //却下      →保留
        //却下  →元のシングルスレッドにする。
        //却下      →進行中
        //却下  →マルチプロセスが良いかも
        //却下  RAMDirectoryとマルチスレッドでインデックスを作成を高速化する。
        //却下  →マルチコアの本のPart2-10を参考にしてみる。
        //却下 \Temp(920件)で実装して、計測してみた。
        //却下     ・FSDirectory 3:50秒
        //却下     ・RAMDirectory 3:50秒
        //却下     ※マルチスレッドで分担すれば早くなる？
        //却下     （どちらにしろ、バックグラウンドでインデックス作成させたいので、RAMDirectoryへ）
        //却下  c:\Workspaceで試みるとハング(0xc0000005 メモリアクセス違反)した
        //却下  →定期的にファイルに書き出す or FSDirectoryをマルチにして統合する
        //却下  →実装してみたが 3:54秒(Thread1に大きいファイルが固まっていた)
        //却下  →データをばらして試す必要あり。
        //却下  　→シングルスレッド 1:53秒
        //却下  　→2スレッド 1:08秒
        //却下  　→3スレッド 1:13秒
        //却下 インデックス作成高速化----------------------------------------------
        //DONE*自前トランザクションを止める
        //DONE*Delete－Insertモードを設ける。
        //DONE*Grid上にhtmlを表示できたが、かなり重い
        // 検索後、htmlLabelListを作成して、ドローするだけにする。
        //DONE*サムネイルを検索グリッドに表示
        //NOTE:Could not initialize class FlexLucene.Analysis.Ja.Dict.TokenInfoDictionarySingletonHolder
        //　上記のエラーが出力される。
        //【原因】
        //Nugetパッケージマネージャーコンソールを利用するとFlexLucene.dllが勝手に置き換えられるため。
        //パッケージのフレームワークを4.5.2に変更しても同様の状況が起きる
        //【対応方法】
        //FlexLucene.dllをNuGet管理外（Libフォルダ）に移動させた。
        //DONE titleの検索をCaseInsensitiveにする。
        //DONE Queryにも同じAnalyzerを適用する必要がある。
        //DONE マルチスレッドで最終どれだけ速くなったか記録を残しておく
        // 1スレッド(40万ファイル) : 3h10m
        // 2スレッド(40万ファイル) : 2h02m(但しマージ失敗)
        //DONE インデックスが追記モードになっているっぽい
        //DONE C\Tempでインデックスを作成してもキーワードが引っ掛からないのは何故か
        //     →hilightFieldType指定が誤っているようだ

        /// <summary>ロガー</summary>
        private static ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ILog Logger {
            get { return _logger; }
        }

        /// <summary>PokudaSearch.dbへの接続文字列</summary>
        public static string ConnectString { get; set; }

        /// <summary>SQLiteユーティリティ</summary>
        private static SQLiteDBUtil _db = new SQLiteDBUtil(AppObject.Logger);
        public static SQLiteDBUtil DbUtil {
            get { return _db; }
        }

        /// <summary>メインフレーム画面のインスタンス</summary>
        public static MainFrameForm Frame { get; set; }

        /// <summary>Luceneインデックス群配置パス</summary>
        public static string RootDirPath = @"";

        public static Analyzer AppAnalyzer = null;

        /// <summary>フィルターヘルパ</summary>
        private static FilterHelper _filterHelper = new FilterHelper();
        public static FilterHelper FilterHelper {
            get { return _filterHelper; }
        }

        /// <summary>メッセージリソース</summary>
        private static MultiLangUtil _mlu = new MultiLangUtil();
        public static MultiLangUtil MLUtil {
            get { return _mlu; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static AppObject() {
            //HACK Settingsファイルに移行
            _mlu.MessageDictionary.Add("ACT_EXTRACT", "抽出中...");
            _mlu.MessageDictionary.Add("ACT_SEARCH", "検索中…");
            _mlu.MessageDictionary.Add("ACT_PROCESSING", "処理中 ...");
            _mlu.MessageDictionary.Add("ACT_FILTER", "絞込中 ...");
            _mlu.MessageDictionary.Add("ACT_RESET_FILTER", "絞込を解除中 ...");
            _mlu.MessageDictionary.Add("ACT_END", "");
            _mlu.MessageDictionary.Add("MSG_EXTRACT_ZERO", "抽出対象データがありません。");
            _mlu.MessageDictionary.Add("TITLE_WARN", "警告");
            _mlu.MessageDictionary.Add("TITLE_ERROR", "エラー");
            _mlu.MessageDictionary.Add("MSG_EXCEL2003_DATA_TRUNCATE", "Excelのバージョンが2003以前なので、257列以降は切り捨てます。");
            _mlu.MessageDictionary.Add("MSG_COLORING_BACKCOLOR", "背景色設定中…");
        }

        //TODO 配置場所を要検討
        /// <summary>
        /// ファイルの中身をテキスト抽出して内容が同じか判定する
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool IsSameContents(string filePath1, string filePath2) {
            bool ret = false;

            //拡張子が同じか確認
            string ext1 = Path.GetExtension(filePath1);
            string ext2 = Path.GetExtension(filePath2);
            if (ext1 != ext2) {
                return false;
            }

            string txt1 = "";
            string txt2 = "";
            if (ext1.ToLower() == ".txt") {
                using (var reader = new StreamReader(filePath1)) {
                    txt1 = reader.ReadToEnd();
                }
                using (var reader = new StreamReader(filePath2)) {
                    txt2 = reader.ReadToEnd();
                }
                
            } else {
                var txtExtractor = new TextExtractor();

                var content1 = txtExtractor.Extract(filePath1);
                txt1 = content1.Text;
                var content2 = txtExtractor.Extract(filePath2);
                txt2 = content2.Text;
            }

            if (txt1 == txt2) {
                ret = true;
            }

            return ret;
        }
        //[TestMethod]
        //public void IsSameContentsTest() {
        //    bool ret = false;
        //    var txtExtractor = new TextExtractor();

        //    //テキストファイルでの比較
        //    //NOTE テキストファイルの中身がに日本語が入ると抽出できていない
        //    // ⇒ReadToEndで文字列を取得するようにした
        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.txt", @".\TestData\SampleCopy.txt");
        //    Assert.AreEqual(ret, true);

        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.txt", @".\TestData\SampleDiff.txt");
        //    Assert.AreEqual(ret, false);

        //    // エクセルでの比較
        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.xlsx", @".\TestData\SampleCopy.xlsx");
        //    Assert.AreEqual(ret, true);

        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.xlsx", @".\TestData\SampleDiff.xlsx");
        //    Assert.AreEqual(ret, false);

        //    //TODO テキスト抽出できない場合

        //}
    }
}
