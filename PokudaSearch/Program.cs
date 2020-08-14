using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using PokudaSearch.IndexUtil;
using PokudaSearch.SandBox;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using FxCommonLib.Utils;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using PokudaSearch.Win32API;
using PokudaSearch.IPC;
using System.Runtime.Remoting;

namespace PokudaSearch {
    static class Program {

        private static IpcServerChannel _serverChannel = null;
        private static IPCShareInfo _shareInfo = null;


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {

            //デスクトップをデフォルトとする。
            string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Length > 2) {
                string option = args[1].ToLower();
                string tmpPath = StringUtil.NullToBlank(args[2]);
                if (option == "/f") {
                    if (Directory.Exists(tmpPath)) {
                        //引数のパスでファイラを起動
                        defaultPath = tmpPath;
                    }
                } else if (option == "/sf") {
                    //既にプロセスが存在するか？
                    var processArray = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                    if (processArray.Length > 1) {
                        //すでに起動している場合は、パスを渡して終了。

                        //プロセス間通信
                        var clientChannel = new IpcClientChannel();
                        ChannelServices.RegisterChannel(clientChannel, true);

                        var url = "ipc://PokudaSearchIPC/path";
                        IPCShareInfo shareInfo = (IPCShareInfo)Activator.GetObject(typeof(IPCShareInfo), url);
                        User32.SetForegroundWindow(Process.GetProcessById(shareInfo.ProcessId).MainWindowHandle);
                        shareInfo.SendInfo(tmpPath);

                        return;
                    } else {
                        if (Directory.Exists(tmpPath)) {
                            //引数のパスでファイラを起動
                            defaultPath = tmpPath;
                        }
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //x86,x64 dllの切替処理
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            //起動時に初期処理
            InitializeAnalyzer();
            RunIPCServer();

            AppObject.DefaultPath = defaultPath;
            AppObject.BootMode = AppObject.BootModes.Filer;

            AppObject.Frame = new MainFrameForm();
            Application.Run(AppObject.Frame);
        }

        private static void RunIPCServer() {
            const string ChannelName = "PokudaSearchIPC";
            try {
                //チャンネルが存在しない場合は起動
                _serverChannel = new IpcServerChannel(ChannelName);
                ChannelServices.RegisterChannel(_serverChannel, true);
                AppObject.Logger.Info(_serverChannel.GetChannelUri());

                //イベントを登録
                _shareInfo = new IPCShareInfo();
                _shareInfo.ProcessId = Process.GetCurrentProcess().Id;
                _shareInfo.OnSend += new IPCShareInfo.CallEventHandler(ShareInfo_OnSend);
                RemotingServices.Marshal(_shareInfo, "path", typeof(IPCShareInfo));
            } catch {
                //スルー
            }
        }

        public static void ShareInfo_OnSend(IPCShareInfo.IPCShareInfoEventArg e) {
            string path = e.Path;
            //対象パスをエクスプローラで表示
            MainFrameForm.FileExplorerForm.LoadMainExplorer(path);
        }

        /// <summary>
        /// 日本語アナライザ初期化処理
        /// </summary>
        private static void InitializeAnalyzer() {

            string sqliteDataSource = Directory.GetParent(Application.ExecutablePath).FullName + 
                                            Properties.Settings.Default.SQLITE_DATA_SOURCE;
            AppObject.ConnectString = AppObject.GetConnectString(sqliteDataSource);

            //ユーザ辞書の設定
            AppObject.RootDirPath = Directory.GetParent(Application.ExecutablePath).FullName;
            AppObject.RootDirPath += LuceneIndexBuilder.StoreDirName;

            //Analyzer
            java.io.Reader treader = new java.io.FileReader(AppObject.RootDirPath + @".\..\UserDictionary.txt");
            UserDictionary userDic = null;
            try {
                //ユーザ辞書
                userDic = UserDictionary.Open(treader);
            } finally {
                treader.close();    
            }

            AppObject.AppAnalyzer = new JapaneseAnalyzer(userDic, //ユーザ定義辞書
                JapaneseTokenizerMode.SEARCH,
                JapaneseAnalyzer.GetDefaultStopSet(), 
                JapaneseAnalyzer.GetDefaultStopTags());
            //JapaneseTokenizerMode.EXTENDED;
            // ->拡張モードは、未知の単語のユニグラムを出力します。
            //JapaneseTokenizerMode.NORMAL;
            // ->通常のセグメンテーション：化合物の分解なし
            //JapaneseTokenizerMode.SEARCH;
            // ->検索を対象としたセグメンテーション：
            //   これには、長い名詞の複合化プロセスが含まれ、同義語としての完全な複合トークンも含まれます。

            //AppObject.AppAnalyzer = new JapaneseAnalyzer();
        }
        
        private static Assembly Resolver(object sender, ResolveEventArgs args) {
            if (args.Name.StartsWith("CefSharp")) {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string path = Directory.GetParent(Application.ExecutablePath).FullName;
                string archSpecificPath = Path.Combine(path,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);
                AppObject.Logger.Info("CefSharp path:" + archSpecificPath);
                //string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                //                                       Environment.Is64BitProcess ? "x64" : "x86",
                //                                       assemblyName);

                return File.Exists(archSpecificPath) ? Assembly.LoadFile(archSpecificPath) : null;
            }
            return null;
        }
    }
}
