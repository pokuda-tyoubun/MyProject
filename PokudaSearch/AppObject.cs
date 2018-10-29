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
        
        //HACK*2フェーズコミット対応
        //HACK UI高度化----------------------------------------------
        //HACK  →速度的に問題がなければMESに適用(Sencha側の問題がある。)
        //HACK UI高度化----------------------------------------------
        //HACKインデックス作成高速化----------------------------------------------
        //HACK* マルチプロセス→実装は楽だが、進度の仕組みをどうするか？(共有メモリ?)
        //HACK* マルチスレッド処理をoutofmemoryにならないように 
        //HACK 大量ファイルでインデックスを作成した場合に、segmentsファイルが作成されない。
        //     →最後にCommit()を追記
        //         →効果なし
        //     →1000ファイル毎ににCommit()を追記
        //         →効果なし
        //     →本体に少しずつマージ
        //         →保留
        //     →元のシングルスレッドにする。
        //         →進行中
        //     →マルチプロセスが良いかも
        //HACK RAMDirectoryとマルチスレッドでインデックスを作成を高速化する。
        //     →マルチコアの本のPart2-10を参考にしてみる。
        //　　→C\Temp(920件)で実装して、計測してみた。
        //        ・FSDirectory 3:50秒
        //        ・RAMDirectory 3:50秒
        //        ※マルチスレッドで分担すれば早くなる？
        //        （どちらにしろ、バックグラウンドでインデックス作成させたいので、RAMDirectoryへ）
        //HACK c:\Workspaceで試みるとハング(0xc0000005 メモリアクセス違反)した
        //     →定期的にファイルに書き出す or FSDirectoryをマルチにして統合する
        //     →実装してみたが 3:54秒(Thread1に大きいファイルが固まっていた)
        //HACK →データをばらして試す必要あり。
        //HACK 　→シングルスレッド 1:53秒
        //HACK 　→2スレッド 1:08秒
        //HACK 　→3スレッド 1:13秒
        //HACKインデックス作成高速化----------------------------------------------

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
            _mlu.MessageDictionary.Add("MSG_EXCEL2003_DATA_TRUNCATE", "Excelのバージョンが2003以前なので、257列以降は切り捨てます。");
            _mlu.MessageDictionary.Add("MSG_COLORING_BACKCOLOR", "背景色設定中…");
        }
    }
}
