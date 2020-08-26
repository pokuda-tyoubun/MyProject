using C1.Win.C1FlexGrid;
using FxCommonLib;
using FxCommonLib.Consts.MES;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FxCommonLib.Models {
    public class FilterHelper {

        #region MemberVariables
        /// <summary>グリッド検索フィルタ</summary>
        private ConditionFilter _searchFilter = new ConditionFilter();
        #endregion MemberVariables

        #region PublicMethods
        /// <summary>
        /// フィルタ設定
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        public void SetGridFilter(Cursor cur, FlexGridEx grid, string condition, string msg) {
            SetGridFilter(cur, (C1FlexGrid)grid, condition, msg);
        }
        /// <summary>
        /// フィルタ設定
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="grid"></param>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        //HACK 引数msgは使用していない。
        public void SetGridFilter(Cursor cur, C1FlexGrid grid, string condition, string msg) {
            // フィルタを構成します。
            _searchFilter.Condition1.Operator = ConditionOperator.Contains;
            _searchFilter.Condition1.Parameter = condition;

            int count = 0;
            try {
                // フィルタを設定します。
                grid.BeginUpdate();
                for (int r = grid.Rows.Fixed; r < grid.Rows.Count; r++) {
                    bool visible = false;
                    for (int c = grid.Cols.Fixed; c < grid.Cols.Count; c++) {
                        Column col = grid.Cols[c];
                        Object val = null;
                        //表示列のみフィルタ対象とする。
                        if (col.Visible == true) {
                            if (col.DataMap != null) {
                                //プルダウンの場合、名称でフィルタリング
                                Dictionary<string, string> dic = (Dictionary<string, string>)col.DataMap;
                                if (dic.ContainsKey(StringUtil.NullToBlank(grid[r, c]))) {
                                    val = dic[grid[r, c].ToString()].ToString();
                                }
                            } else {
                                val = grid[r, c];
                            }
                        }
                        if (_searchFilter.Apply(val)) {
                            visible = true;
                            count++;
                            break;
                        }
                    }
                    grid.Rows[r].Visible = visible;
                    Application.DoEvents();
                }
            } finally { 
                grid.EndUpdate();
            }
        }
        #endregion PublicMethods
    }
}
