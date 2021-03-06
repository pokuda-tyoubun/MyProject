﻿using FlexLucene.Analysis;
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
using PokudaSearch.IPC;
using System.Runtime.Remoting;
using System.Windows.Threading;
using FxCommonLib.Win32API;

namespace PokudaSearch {
    static class Program {

        private static IpcServerChannel _serverChannel = null;
        private static IPCShareInfo _shareInfo = null;

        /// <summary>新プロセスでファイラを起動</summary>
        private const string FilerOption = "/f";
        /// <summary>同一プロセスでファイラをActivate(MainExplorerに表示、MainのパスはSubへ)</summary>
        private const string SingleFilerOption = "/sf";
        /// <summary>同一プロセスでファイラをActivate(MainExplorerに表示)</summary>
        private const string SingleFilerMainOption = "/sfm";
        /// <summary>同一プロセスでファイラをActivate(SubExplorerに表示)</summary>
        private const string SingleFilerSubOption = "/sfs";

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
                if (option == FilerOption) {
                    if (Directory.Exists(tmpPath)) {
                        //引数のパスでファイラを起動
                        defaultPath = tmpPath;
                    }
                } else if (option == SingleFilerOption || 
                           option == SingleFilerMainOption ||
                           option == SingleFilerSubOption) {
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
                        shareInfo.SendInfo(option, tmpPath);

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
            if (Properties.Settings.Default.RunIPC) {
                RunIPCServer();
            }

            Microsoft.Win32.SystemEvents.PowerModeChanged += 
                      new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

            AppObject.DefaultPath = defaultPath;
            AppObject.BootMode = AppObject.BootModes.Filer;

            AppObject.Frame = new MainFrameForm();
            Application.Run(AppObject.Frame);
        }

        private static void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e) {
            switch (e.Mode) {
                case Microsoft.Win32.PowerModes.Suspend:
                    //IPC停止
                    if (_serverChannel != null) {
                        AppObject.Logger.Warn("IPC停止");
                        _serverChannel.StopListening(null);
                        _serverChannel = null;
                        System.GC.Collect();
                    }
                    break;
                case Microsoft.Win32.PowerModes.Resume:
                    //IPC再開
                    if (_serverChannel == null) {
                        AppObject.Logger.Warn("IPC再開");
                        RunIPCServer();
                    }
                    break;
            }
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
            //if (MainFrameForm.SearchForm != null) {
            //    MainFrameForm.SearchForm.WindowState = FormWindowState.Normal;
            //}
            //対象パスをエクスプローラで表示
            string option = e.Option;
            string path = e.Path;
            AppObject.Frame.FileExplorerFormButtonPerformClick();
            if (option == SingleFilerOption) {
                MainFrameForm.FileExplorerForm.LoadMainToSubExplorer(path);
            } else if (option == SingleFilerMainOption) {
                MainFrameForm.FileExplorerForm.LoadMainExplorer(path);
            } else if (option == SingleFilerSubOption) {
                MainFrameForm.FileExplorerForm.LoadSubExplorer(path);
            }
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
