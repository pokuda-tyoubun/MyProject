using System;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// iFilterによるテキスト抽出
///関連の定数定義
/// </summary>
namespace IndexBuilder {
    //https://msdn.microsoft.com/en-us/library/ms691091(v=vs.85).aspx
    [Flags]
    public enum IFILTER_INIT : uint {
        NONE                   = 0,
        CANON_PARAGRAPHS       = 1, //
        HARD_LINE_BREAKS       = 2, //\r,\n,\r\nを強制的に改行とみなす
        CANON_HYPHENS          = 4, //ハイフン文字の標準化
                                    //オプションハイフン文字をNORMAL HYPHENS(0x2010)または、HYPHEN-MINUSES (0x002D)に変換するフラグ。
        CANON_SPACES           = 8, //スペース文字の標準化
        APPLY_INDEX_ATTRIBUTES = 16,//
        APPLY_CRAWL_ATTRIBUTES = 256,
        APPLY_OTHER_ATTRIBUTES = 32,
        INDEXING_ONLY          = 64,
        SEARCH_LINKS           = 128,       
        FILTER_OWNED_VALUE_OK  = 512
    }

    //https://msdn.microsoft.com/en-us/library/ms691123(v=vs.85).aspx
    public enum CHUNK_BREAKTYPE {
        CHUNK_NO_BREAK = 0, //区切らない
        CHUNK_EOW      = 1, //単語で区切る
        CHUNK_EOS      = 2, //文で区切る
        CHUNK_EOP      = 3, //段落で区切る
        CHUNK_EOC      = 4  //章で区切る
    }

    //https://msdn.microsoft.com/en-us/library/ms691020(v=vs.85).aspx
    [Flags]
    public enum CHUNKSTATE {
        CHUNK_TEXT               = 0x1, //テキスト
        CHUNK_VALUE              = 0x2, //バイナリ
        CHUNK_FILTER_OWNED_VALUE = 0x4  //未使用
    }

    //https://msdn.microsoft.com/ja-jp/library/aa380070
    [StructLayout(LayoutKind.Sequential)]
    public struct PROPSPEC {
        public uint ulKind;
        public uint propid;
        public IntPtr lpwstr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FULLPROPSPEC {
        public Guid guidPropSet;
        public PROPSPEC psProperty;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STAT_CHUNK {
        public uint  idChunk;
        [MarshalAs(UnmanagedType.U4)]
        public CHUNK_BREAKTYPE breakType;
        [MarshalAs(UnmanagedType.U4)]
        public CHUNKSTATE flags;
        public uint locale;
        [MarshalAs(UnmanagedType.Struct)] public FULLPROPSPEC attribute;
        public uint idChunkSource;
        public uint cwcStartSource;
        public uint cwcLenSource;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FILTERREGION {
        public uint idChunk;
        public uint cwcStart;
        public uint cwcExtent;
    }

    [ComImport]
    [Guid("89BCB740-6119-101A-BCB7-00DD010655AF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilter {
        void Init([MarshalAs(UnmanagedType.U4)] IFILTER_INIT grfFlags, uint cAttributes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] FULLPROPSPEC[] aAttributes, ref uint pdwFlags);
        [PreserveSig] int GetChunk(out STAT_CHUNK pStat);
        [PreserveSig] int GetText(ref uint pcwcBuffer, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer);
        void GetValue(ref UIntPtr ppPropValue);
        void BindRegion([MarshalAs(UnmanagedType.Struct)]FILTERREGION origPos, ref Guid riid, ref UIntPtr ppunk);
    }

    //iFilterとの違いは？
    [ComImport]
    [Guid("f07f3920-7b8c-11cf-9be8-00aa004b9986")]
    public class CFilter {
    }

    public class Constants {
        public const uint PID_STG_DIRECTORY               =0x00000002;
        public const uint PID_STG_CLASSID                 =0x00000003;
        public const uint PID_STG_STORAGETYPE             =0x00000004;
        public const uint PID_STG_VOLUME_ID               =0x00000005;
        public const uint PID_STG_PARENT_WORKID           =0x00000006;
        public const uint PID_STG_SECONDARYSTORE          =0x00000007;
        public const uint PID_STG_FILEINDEX               =0x00000008;
        public const uint PID_STG_LASTCHANGEUSN           =0x00000009;
        public const uint PID_STG_NAME                    =0x0000000a;
        public const uint PID_STG_PATH                    =0x0000000b;
        public const uint PID_STG_SIZE                    =0x0000000c;
        public const uint PID_STG_ATTRIBUTES              =0x0000000d;
        public const uint PID_STG_WRITETIME               =0x0000000e;
        public const uint PID_STG_CREATETIME              =0x0000000f;
        public const uint PID_STG_ACCESSTIME              =0x00000010;
        public const uint PID_STG_CHANGETIME              =0x00000011;
        public const uint PID_STG_CONTENTS                =0x00000013;
        public const uint PID_STG_SHORTNAME               =0x00000014;
        public const int  FILTER_E_END_OF_CHUNKS          =(unchecked((int)0x80041700));
        public const int  FILTER_E_NO_MORE_TEXT           =(unchecked((int)0x80041701));
        public const int  FILTER_E_NO_MORE_VALUES         =(unchecked((int)0x80041702));
        public const int  FILTER_E_NO_TEXT                =(unchecked((int)0x80041705));
        public const int  FILTER_E_NO_VALUES              =(unchecked((int)0x80041706));
        public const int  FILTER_S_LAST_TEXT              =(unchecked((int)0x00041709));
    }

    /// <summary> 
    /// IFilterのリターンコード
    /// </summary> 
    public enum IFilterReturnCodes : uint { 
        /// <summary> 
        /// 成功
        /// </summary> 
        S_OK = 0, 
        /// <summary> 
        /// フィルタファイルへのアクセスが拒否された。
        /// </summary> 
        E_ACCESSDENIED = 0x80070005, 
        /// <summary> 
        /// メモリ不足などにより、無効なハンドルを検出した。
        /// </summary> 
        E_HANDLE = 0x80070006, 
        /// <summary> 
        /// 無効なパラメータ。
        /// </summary> 
        E_INVALIDARG = 0x80070057, 
        /// <summary> 
        /// メモリ不足
        /// </summary> 
        E_OUTOFMEMORY = 0x8007000E, 
        /// <summary> 
        /// 未実装
        /// </summary> 
        E_NOTIMPL = 0x80004001, 
        /// <summary> 
        /// 不明なエラー
        /// </summary> 
        E_FAIL = 0x80000008, 
        /// <summary> 
        /// パスワード保護のためフィルタリングされていないファイルです。
        /// </summary> 
        FILTER_E_PASSWORD = 0x8004170B, 
        /// <summary> 
        /// ドキュメントフォーマットが認識できません。
        /// </summary> 
        FILTER_E_UNKNOWNFORMAT = 0x8004170C, 
        /// <summary> 
        /// テキストなし。
        /// </summary> 
        FILTER_E_NO_TEXT = 0x80041705, 
        /// <summary> 
        /// 利用可能なチャンクがなくなった。
        /// </summary> 
        FILTER_E_END_OF_CHUNKS = 0x80041700, 
        /// <summary> 
        /// 利用可能なチャンクが存在しない。
        /// </summary> 
        FILTER_E_NO_MORE_TEXT = 0x80041701, 
        /// <summary> 
        /// 利用できるプロパティ値がない。
        /// </summary> 
        FILTER_E_NO_MORE_VALUES = 0x80041702, 
        /// <summary> 
        /// オブジェクトへのアクセスが許可されていません。
        /// </summary> 
        FILTER_E_ACCESS = 0x80041703, 
        /// <summary> 
        /// 別名は利用できません。
        /// </summary> 
        FILTER_W_MONIKER_CLIPPED = 0x00041704, 
        /// <summary> 
        /// 埋め込みオブジェクトに対してIFilterを適用できません。
        /// </summary> 
        FILTER_E_EMBEDDING_UNAVAILABLE = 0x80041707, 
        /// <summary> 
        /// リンクオブジェクトに対してIFilterを適用できません。 
        /// </summary> 
        FILTER_E_LINK_UNAVAILABLE = 0x80041708, 
        /// <summary> 
        /// 現在のチャンクの最後のテキストです。
        /// </summary> 
        FILTER_S_LAST_TEXT = 0x00041709, 
        /// <summary> 
        /// 現在のチャンクの最後のプロパティ値です。
        /// </summary> 
        FILTER_S_LAST_VALUES = 0x0004170A 
    } 
}
