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
    internal class t_active_index {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal t_active_index() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PokudaSearch.SQLSrc.t_active_index", typeof(t_active_index).Assembly);
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
        ///   DELETE FROM [t_active_index]
        ///WHERE [パス] = @パス に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DELETE {
            get {
                return ResourceManager.GetString("DELETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   INSERT OR REPLACE INTO [t_active_index] (
        /// [パス]
        ///,[インデックスパス]
        ///,[モード]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///,[リモートパス]
        ///,[ローカルパス]
        ///,[作成日]
        ///,[更新日]
        ///) VALUES ( 
        /// @パス
        ///,@インデックスパス
        ///,@モード
        ///,@作成時間(分)
        ///,@対象ファイル数
        ///,@インデックス済み
        ///,@インデックス対象外
        ///,@総バイト数
        ///,@テキスト抽出器
        ///,@リモートパス
        ///,@ローカルパス
        ///,CASE WHEN EXISTS (SELECT * FROM [t_active_index] WHERE [パス] = @パス)
        /// THEN (SELECT [作成日] FROM [t_active_index] WHERE [パス] = @パス)
        /// ELSE @作成完了 END
        ///,@作成完了
        ///) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string INSERT_OR_REPLACE {
            get {
                return ResourceManager.GetString("INSERT_OR_REPLACE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        /// [パス]
        ///,[インデックスパス]
        ///,[モード]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///,[リモートパス]
        ///,[ローカルパス]
        ///,[作成日]
        ///,[更新日]
        ///FROM [t_active_index]
        ///ORDER BY [更新日] DESC に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SELECT {
            get {
                return ResourceManager.GetString("SELECT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        /// [パス]
        ///,[インデックスパス]
        ///,[モード]
        ///,[作成時間(分)]
        ///,[対象ファイル数]
        ///,[インデックス済み]
        ///,[インデックス対象外]
        ///,[総バイト数]
        ///,[テキスト抽出器]
        ///,[リモートパス]
        ///,[ローカルパス]
        ///,[作成日]
        ///,[更新日]
        ///FROM [t_active_index]
        ///WHERE [パス] = @パス に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SELECT_ONE {
            get {
                return ResourceManager.GetString("SELECT_ONE", resourceCulture);
            }
        }
    }
}
