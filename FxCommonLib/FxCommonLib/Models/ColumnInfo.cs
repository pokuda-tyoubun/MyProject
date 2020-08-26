
namespace FxCommonLib.Models {
    /// <summary>
    /// 列定義情報（列単位）
    /// </summary>
    public class ColumnInfo {

        #region Properties
        /// <summary>DB列名</summary>
        public string DBName { get; set; }
        /// <summary>列幅</summary>
        public int Width { get; set; }
        /// <summary>呼称列名</summary>
        public string ColName { get; set; }
        /// <summary>列設定編集フラグ</summary>
        public bool ConfEditable { get; set; }
        /// <summary>表示フラグ</summary>
        public bool Visible { get; set; }
        /// <summary>編集可フラグ</summary>
        public bool Editable { get; set; }
        /// <summary>表示順</summary>
        public int DisplayOrder { get; set; }
        /// <summary>データ型</summary>
        public string DataType { get; set; }
        /// <summary>最大長</summary>
        public int MaxLength { get; set; }
        /// <summary>必須フラグ</summary>
        public bool Required { get; set; }
        /// <summary>パスワード文字</summary>
        public string PasswordChar { get; set; }
        /// <summary>プライマリキーフラグ</summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>備考(列の説明)</summary>
        public string Note { get; set; }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColumnInfo() {
        }
        #endregion Constractors
    }
}
