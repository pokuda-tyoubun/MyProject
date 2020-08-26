using System.Collections.Generic;
using System.Linq;

namespace FxCommonLib.Models {
    /// <summary>
    /// 列定義情報（Grid単位）
    /// </summary>
    public class ColumnConfig {

        #region Properties
        /// <summary>高さ</summary>
        public int Height { get; set; }
        /// <summary>列固定数</summary>
        public int LeftFixedCount { get; set; }

        /// <summary>呼称列名をキーにした列情報辞書</summary>
        private List<ColumnInfo> _colList = new List<ColumnInfo>();
        public List<ColumnInfo> ColList {
            set {_colList = value; }
            get { return _colList; }
        }
        #endregion Properties

        #region Constractors
        /// <summary>コンストラクタ</summary>
        public ColumnConfig() {
        }
        #endregion Constractors

        #region PublicMethods
        public List<ColumnInfo> SortedByDispOrder() {
            _colList.OrderBy(ci => ci.DisplayOrder);
            return _colList;
        }
        #endregion PublicMethods

    }
}
