﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokudaSearch.SQLSrc {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class t_index_history {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal t_index_history() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PokudaSearch.SQLSrc.t_index_history", typeof(t_index_history).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   INSERT INTO [t_index_history] (
        /// [作成開始]
        ///,[作成完了]
        ///,[モード]
        ///,[パス]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///) VALUES (
        /// @作成開始
        ///,@作成完了
        ///,@モード
        ///,@パス
        ///,@作成時間(分)
        ///,@対象ファイル数
        ///,@インデックス済み
        ///,@インデックス対象外
        ///,@総バイト数
        ///,@テキスト抽出器
        ///); に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string INSERT {
            get {
                return ResourceManager.GetString("INSERT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   INSERT OR REPLACE INTO [t_index_history] (
        /// [予約No]
        ///,[作成開始]
        ///,[作成完了]
        ///,[モード]
        ///,[パス]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///) VALUES ( 
        /// @予約No
        ///,@作成開始
        ///,@作成完了
        ///,@モード
        ///,@パス
        ///,@作成時間(分)
        ///,@対象ファイル数
        ///,@インデックス済み
        ///,@インデックス対象外
        ///,@総バイト数
        ///,@テキスト抽出器
        ///) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string INSERT_OR_REPLACE {
            get {
                return ResourceManager.GetString("INSERT_OR_REPLACE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        /// [予約No]
        ///,[作成開始]
        ///,[作成完了]
        ///,[モード]
        ///,[パス]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///FROM [t_index_history]
        ///WHERE (1 = 1) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SELECT_ALL {
            get {
                return ResourceManager.GetString("SELECT_ALL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        /// [予約No]
        ///, [作成開始]
        ///, [作成完了]
        ///, [モード]
        ///, [パス]
        ///, [作成時間(分)]
        ///, [対象ファイル数]
        ///, [インデックス済み]
        ///, [インデックス対象外]
        ///, [総バイト数]
        ///, [テキスト抽出器]
        ///FROM [t_index_history]
        ///ORDER BY [予約No] DESC
        ///LIMIT 1 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SELECT_NEW_ONE {
            get {
                return ResourceManager.GetString("SELECT_NEW_ONE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE [t_index_history] SET
        /// [作成完了] = @作成完了
        ///,[作成時間(分)] = @作成時間
        ///,[対象ファイル数] = @対象ファイル数
        ///,[インデックス済み] = @インデックス済み
        ///,[インデックス対象外] = @インデックス対象外
        ///,[総バイト数] = @総バイト数
        ///WHERE [予約No] = @予約No に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string UPDATE {
            get {
                return ResourceManager.GetString("UPDATE", resourceCulture);
            }
        }
    }
}
