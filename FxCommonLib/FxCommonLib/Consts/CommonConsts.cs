using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Consts {
    public static class CommonConsts {

        #region Enum
        /// <summary>更新区分</summary>
        public enum UpdateType : int {
            RequestInfo = 0,
            Add = 1,
            Update = 2,
            Delete = 3, 
            AddOrUpdate = 4, 
            Error =  9
        }
        #endregion

        /// <summary>メッセージID</summary>
        #region MessageId
        /// <summary>設定保存中 ...</summary>
        public const string ACT_CONF_SAVE = "ACT_CONF_SAVE";
        /// <summary>削除中...</summary>
        public const string ACT_DELETE = "ACT_DELETE";
        /// <summary>ダウンロード中 ...</summary>
        public const string ACT_DOWNLOAD = "ACT_DOWNLOAD";
        /// <summary></summary>
        public const string ACT_END = "ACT_END";
        /// <summary>抽出中...</summary>
        public const string ACT_EXTRACT = "ACT_EXTRACT";
        /// <summary>絞込中 ...</summary>
        public const string ACT_FILTER = "ACT_FILTER";
        /// <summary>読込み中...</summary>
        public const string ACT_LOAD = "ACT_LOAD";
        /// <summary>登録中 ...</summary>
        public const string ACT_REGIST = "ACT_REGIST";
        /// <summary>絞込を解除中 ...</summary>
        public const string ACT_RESET_FILTER = "ACT_RESET_FILTER";
        /// <summary>検索中 ...</summary>
        public const string ACT_SEARCH = "ACT_SEARCH";
        /// <summary>取込中 ...</summary>
        public const string ACT_IMPORT = "ACT_IMPORT";
        /// <summary>書出し中 ...</summary>
        public const string ACT_EXPORT = "ACT_EXPORT";
        /// <summary>処理中 ...</summary>
        public const string ACT_PROCESSING = "ACT_PROCESSING";
        /// <summary>yyyy/MM/dd</summary>
        public const string DATE_FORMAT_YYYYMMDD = "DATE_FORMAT_YYYYMMDD";
        /// <summary>yyyy/MM/dd HH:mm</summary>
        public const string DATE_FORMAT_Y_MIN = "DATE_FORMAT_Y_MIN";
        /// <summary>yyyy/MM/dd HH:mm:ss</summary>
        public const string DATE_FORMAT_Y_SEC = "DATE_FORMAT_Y_SEC";
        /// <summary>HH:mm</summary>
        public const string DATE_FORMAT_HHMM = "DATE_FORMAT_HHMM";
        /// <summary>00:00</summary>
        public const string DATE_MASK_HHMM = "DATE_MASK_HHMM";
        /// <summary>無効なセッションです。 ログインしなおしてください。</summary>
        public const string MSG_INVALID_SESSION = "MSG_INVALID_SESSION";
        /// <summary>セッションタイムアウト ログインしなおしてください。</summary>
        public const string MSG_SESSION_TIMEOUT = "MSG_SESSION_TIMEOUT";
        /// <summary>新規追加(&A)</summary>
        public const string CMD_ADD = "CMD_ADD";
        /// <summary>列の設定(&L)</summary>
        public const string CMD_CONF_COLUMN = "CMD_CONF_COLUMN";
        /// <summary>切り取り(&T) Ctrl+X</summary>
        public const string CMD_CUT = "CMD_CUT";
        /// <summary>コピー(&C) Ctrl+C</summary>
        public const string CMD_COPY = "CMD_COPY";
        /// <summary>削除(&D)</summary>
        public const string CMD_DELETE = "CMD_DELETE";
        /// <summary>列を非表示(&H)</summary>
        public const string CMD_HIDE_COLUMN = "CMD_HIDE_COLUMN";
        /// <summary>貼り付け(&P) Ctrl+V</summary>
        public const string CMD_PASTE = "CMD_PASTE";
        /// <summary>日</summary>
        public const string DC_D = "DC_D";
        /// <summary>より、</summary>
        public const string DC_FROM = "DC_FROM";
        /// <summary>、未来</summary>
        public const string DC_FUTURE = "DC_FUTURE";
        /// <summary>時間</summary>
        public const string DC_H = "DC_H";
        /// <summary>ヵ月</summary>
        public const string DC_M = "DC_M";
        /// <summary>分</summary>
        public const string DC_MI = "DC_MI";
        /// <summary>現在日時</summary>
        public const string DC_NOW = "DC_NOW";
        /// <summary>、過去</summary>
        public const string DC_PAST = "DC_PAST";
        /// <summary>年</summary>
        public const string DC_Y = "DC_Y";
        /// <summary>Excelのバージョンが2003以前なので、257列以降は切り捨てます。</summary>
        public const string MSG_EXCEL2003_DATA_TRUNCATE = "MSG_EXCEL2003_DATA_TRUNCATE";
        /// <summary>抽出対象データがありません。</summary>
        public const string MSG_EXTRACT_ZERO = "MSG_EXTRACT_ZERO";
        /// <summary>検索条件を指定した場合は、表示項目にする必要があります。</summary>
        public const string MSG_VISIBLE_FLG_ERROR = "MSG_VISIBLE_FLG_ERROR";
        /// <summary>追加</summary>
        public const string ROW_ADD = "ROW_ADD";
        /// <summary>削除</summary>
        public const string ROW_DELETE = "ROW_DELETE";
        /// <summary>更新</summary>
        public const string ROW_UPDATE = "ROW_UPDATE";
        /// <summary>エラー</summary>
        public const string TITLE_ERROR = "TITLE_ERROR";
        /// <summary>情報</summary>
        public const string TITLE_INFO = "TITLE_INFO";
        /// <summary>質問</summary>
        public const string TITLE_QUESTION = "TITLE_QUESTION";
        /// <summary>警告</summary>
        public const string TITLE_WARN = "TITLE_WARN";
        /// <summary>入力値は、範囲外です。</summary>
        public const string MSG_OUT_OF_BOUND = "MSG_OUT_OF_BOUND";
        /// <summary>{0}は半角英数で入力してください。</summary>
        public const string MSG_FORMAT_AL_NUM = "MSG_FORMAT_AL_NUM";
        /// <summary>無効な色設定です。</summary>
        public const string MSG_INVALID_COLOR_FORMAT = "MSG_INVALID_COLOR_FORMAT";
        /// <summary>背景色設定中…</summary>
        public const string MSG_COLORING_BACKCOLOR = "MSG_COLORING_BACKCOLOR";
        /// <summary>処理を続行できないため強制終了します。再ログインしてください。</summary>
        public const string MSG_CRITICAL_ERROR = "MSG_CRITICAL_ERROR";
        /// <summary>指定された{0}は、存在しません。</summary>
        public const string MSG_NOT_FOUND = "MSG_NOT_FOUND";
        /// <summary>必須項目です。</summary>
        public const string MSG_ANY_REQUIRED_ITEM = "MSG_ANY_REQUIRED_ITEM";
        /// <summary>入力値にエラーがあります。 エラー部分にマウスカーソルを当てると内容を確認できます。</summary>
        public const string MSG_REMAIN_ERROR = "MSG_REMAIN_ERROR";
        /// <summary>更新しました。</summary>
        public const string MSG_UPDATED = "MSG_UPDATED";
        /// <summary>データ形式が一致しないため、貼り付けできません。</summary>
        public const string MSG_PASTE_MISSMATCH_ERROR = "MSG_PASTE_MISSMATCH_ERROR";
        #endregion MessageId

        /// <summary>データディクショナリ</summary>
        #region DataDictionary
        /// <summary>インデントレベル</summary>
        public const string indent_level = "indent_level";
        /// <summary>update_key</summary>
        public const string update_key = "update_key";
        /// <summary>登録者ID</summary>
        public const string insert_id = "insert_id";
        /// <summary>登録者名</summary>
        public const string insert_name = "insert_name";
        /// <summary>登録日</summary>
        public const string insert_date = "insert_date";
        /// <summary>更新者ID</summary>
        public const string update_id = "update_id";
        /// <summary>更新者名</summary>
        public const string update_name = "update_name";
        /// <summary>更新日</summary>
        public const string update_date = "update_date";
        /// <summary>削除者ID</summary>
        public const string delete_id = "delete_id";
        /// <summary>削除者名</summary>
        public const string delete_name = "delete_name";
        /// <summary>削除日時</summary>
        public const string delete_date = "delete_date";
        /// <summary>削除フラグ</summary>
        public const string delete_flg = "delete_flg";
        /// <summary>セッションID</summary>
        public const string session_id = "session_id";
        /// <summary>グリッド名</summary>
        public const string grid_name = "grid_name";
        /// <summary>表示</summary>
        public const string visible = "visible";
        /// <summary>Window名</summary>
        public const string window_name = "window_name";
        /// <summary>物理名</summary>
        public const string db_name = "db_name";
        /// <summary>表示順</summary>
        public const string disp_order = "disp_order";
        /// <summary>列幅</summary>
        public const string width = "width";
        /// <summary>高さ</summary>
        public const string height = "height";
        /// <summary>設定編集</summary>
        public const string conf_editable = "conf_editable";
        /// <summary>編集</summary>
        public const string editable = "editable";
        /// <summary>列固定</summary>
        public const string col_fixed = "col_fixed";
        /// <summary>データタイプ</summary>
        public const string data_type = "data_type";
        /// <summary>最大桁数</summary>
        public const string max_length = "max_length";
        /// <summary>必須</summary>
        public const string required = "required";
        /// <summary>プライマリキー</summary>
        public const string primary_key = "primary_key";
        /// <summary>パスワード文字</summary>
        public const string password_char = "password_char";
        /// <summary>備考</summary>
        public const string note = "note";
        /// <summary>備考(文字数)</summary>
        public const string note_length = "note_length";
        /// <summary>値1</summary>
        public const string value1 = "value1";
        /// <summary>値2</summary>
        public const string value2 = "value2";
        /// <summary>列名</summary>
        public const string col_name = "col_name";
        /// <summary>コード</summary>
        public const string code = "code";
        /// <summary>コード2</summary>
        public const string code2 = "code2";
        /// <summary>コード3</summary>
        public const string code3 = "code3";
        /// <summary>名称</summary>
        public const string name = "name";
        /// <summary>説明</summary>
        public const string description = "description";
        /// <summary>コントロールタイプ</summary>
        public const string ctl_type = "ctl_type";
        /// <summary>メッセージID</summary>
        public const string message_id = "message_id";
        /// <summary>メッセージ</summary>
        public const string message = "message";
        /// <summary>設定値</summary>
        public const string conf_value = "conf_value";
        /// <summary>コントロール名</summary>
        public const string control_name = "control_name";
        /// <summary>カテゴリ</summary>
        public const string category = "category";
        /// <summary>カテゴリ名</summary>
        public const string category_name = "category_name";
        /// <summary>小計</summary>	
        public const string subtotal = "subtotal";

        #endregion DataDictionary

        /// <summary>グリッドデータタイプ</summary>
        #region GridDataType
        public const string GridDataTypeS = "S"; //文字型
        public const string GridDataTypeP = "P"; //パスワードタイプ
        public const string GridDataTypeI = "I"; //整数型
        public const string GridDataTypeD = "D"; //日付型(年月日時分秒)
        public const string GridDataTypeDM = "DM"; //日付型(年月日時分)
        public const string GridDataTypeDO = "DO"; //日付型(年月日)
        public const string GridDataTypeF = "F"; //浮動小数型
        public const string GridDataTypeFP = "FP"; //浮動小数型(正の値のみ)
        public const string GridDataTypeFM = "FM"; //浮動小数型(金額表示）
        public const string GridDataTypeFPM = "FPM"; //浮動小数型(正の値のみ金額表示)
        public const string GridDataTypeB = "B"; //論理型
        public const string GridDataTypeCB = "CB"; //セルボタン型
        public const string GridDataTypeL = "L"; //プルダウンリスト型
        public const string GridDataTypeCL = "CL"; //色指定型
        public const string GridDataTypeSCU = "SCU"; // 得意先コード型
        public const string GridDataTypeSSP = "SSP"; // 仕入先コード型
        public const string GridDataTypeSZS = "SZS"; // 材料仕入先コード型
        public const string GridDataTypeSSS = "SSS"; // 表面処理仕入先コード型
        public const string GridDataTypeSSR = "SSR"; // 表面処理コード型

        #endregion

        /// <summary>コントロールタイプ</summary>
        #region ControlType
        public const string CtlTypeString = "S"; //文字型(常に部分一致 '*'指定可)
        public const string CtlTypeWildCardSpecifiableString = "WS"; //文字型('*'指定可)
        public const string CtlTypePerfectMatchString = "PS"; //文字型(完全一致)
        public const string CtlTypeNumericRange = "NN"; //数値型
        public const string CtlTypeDate = "D"; //日付型(年月日時分秒)
        public const string CtlTypeDateRange = "DD"; //日付型範囲指定
        public const string CtlTypeTimeRange = "TT"; //時間型範囲指定
        public const string CtlTypeValidDate = "VD"; //適用日付型
        public const string CtlTypePulldownlist = "L"; //プルダウンリスト型
        public const string CtlTypeAutocompleteCombo = "AC"; //オートコンプリートコンボ型
        public const string CtlTypeCheckbox = "CH"; //チェックボックス型
        public const string CtlTypeThreshold = "TH"; //閾値型
        #endregion

        /// <summary>テーブル名</summary>
        #region TableName
        public const string TableNameMCommonConf = "m_common_conf";
        public const string TableNameMDataDictionary = "m_data_dictionary";
        #endregion

        /// <summary>パラメータDataTable名</summary>
        #region ParameterTableName
        public const string SearchItemTbl = "ItemTable";
        public const string ParamDataSet = "ParamDataSet";
        /// <summary>値:"ParamDataTbl"</summary>
        public const string ParamDataTbl = "ParamDataTbl";
        #endregion

        /// <summary>数値コントロールの最大桁数</summary>
        public const int C1NumericEditMaxLength = 10;

        /// <summary>初期表示チェックコントロール名</summary>
        public const string ShowInitDataCheckCtlName = "ShowInitDataCheck";

        /// <summary>全候補</summary>
        public const string AllCandidate = "";

        /// <summary>SQLServer金額型の最小値</summary>
        public const decimal MoneyMinValue = -922337203685477.5808M;
        /// <summary>SQLServer金額型の最大値</summary>
        public const decimal MoneyMaxValue = 922337203685477.5807M;

        /// <summary>
        /// 丸付き数字
        /// NOTE:0～50まで
        /// </summary>
        public static readonly string[] CircleNumberStringArray
            = { "⓪"
                  ,"①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩"
                  ,"⑪", "⑫", "⑬", "⑭", "⑮", "⑯", "⑰", "⑱", "⑲", "⑳"
                  , "㉑", "㉒", "㉓", "㉔", "㉕", "㉖", "㉗", "㉘", "㉙", "㉚"
                  , "㉛", "㉜", "㉝", "㉞", "㉟", "㊱", "㊲", "㊳", "㊴", "㊵"
                  , "㊶", "㊷", "㊸", "㊹", "㊺", "㊻", "㊼", "㊽", "㊾", "㊿" 
              };
    }
}
