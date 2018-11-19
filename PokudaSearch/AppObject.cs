using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FxCommonLib.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch {
    public static class AppObject {
        
        //HACKインデックス作成高速化----------------------------------------------
        //HACK*インデックスをディレクトリ毎に予約化し、マルチスレッドで作成する。（結果をマージしない。）
        //HACKインデックス作成高速化----------------------------------------------
        //HACK*差分Updateモードを実装する。
        //HACK*親画面を閉じた時に、インデックス作成を中断する。
        //HACK*シングルかマルチか上位で分けるように実装
        //HACK ディレクトリが異なればindexを分けてmultiSercherを使うようにする。ディレクトリが異なればindexを分けてmultiSercherを使うようにする。
        //HACK Yahoo Googleの検索オプションのUIを真似る(P222も参考に)
        //HACK 特定のサイトもインデックス対象とする
        //HACK   →html、xmlはtikaを使う必要があるかも
        //HACK サーバーサイドでは、DelayClosedIndexSercherを使う。(Session毎にスレッドが違うので問題ない？)
        //HACK UI高度化----------------------------------------------
        //HACK*ファイルエクスプローラの実装
        //HACK コンテキストメニューにGitが表示されないが参考になるかも
        //HACK https://www.codeproject.com/Articles/14707/Dual-Pane-File-Manager
        //HACK*PDFのサムネイル
        //HACK*Officeのサムネイル(OpenOfficeのライブラリが使える？)
        //HACK    →GroupDocsで実現できそうだが有料
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

        //DONE List--------------------------------------------------------------------------
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

        /// <summary>メインフレーム画面のインスタンス</summary>
        public static MainFrameForm Frame { get; set; }

        /// <summary>Luceneインデックス群配置パス</summary>
        public static string RootDirPath = @"";

        public static Analyzer AppAnalyzer = null;


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
            _mlu.MessageDictionary.Add("ACT_END", "");
            _mlu.MessageDictionary.Add("MSG_EXTRACT_ZERO", "抽出対象データがありません。");
            _mlu.MessageDictionary.Add("TITLE_WARN", "警告");
            _mlu.MessageDictionary.Add("TITLE_ERROR", "エラー");
            _mlu.MessageDictionary.Add("MSG_EXCEL2003_DATA_TRUNCATE", "Excelのバージョンが2003以前なので、257列以降は切り捨てます。");
            _mlu.MessageDictionary.Add("MSG_COLORING_BACKCOLOR", "背景色設定中…");
        }
    }
}
