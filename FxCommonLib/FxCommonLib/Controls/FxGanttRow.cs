using System.Collections.Generic;

namespace FxCommonLib.Controls {
    public class FxGanttRow {

        #region プロパティ
        /// <summary>タスクキー</summary>
        public string TaskKey { get; set; }
        /// <summary>作業</summary>
        public string Operation { get; set; }
        /// <summary>作業区分</summary>
        public string WorkDiv { get; set; }
        /// <summary>区分</summary>
        public string Div { get; set; }
        /// <summary>資源コード</summary>
        public string Resource { get; set; }
        /// <summary>資源名称</summary>
        public string ResourceExactName { get; set; }
        /// <summary>利用資源</summary>
        public string ResultResources { get; set; }
        /// <summary>利用資源名</summary>
        public string ResultResourcesName { get; set; }
        /// <summary>作業区分名称</summary>
        public string WorkDivName { get; set; }

        /// <summary>Ganttバーリスト</summary>
        private List<FxGanttBar> _barList = new List<FxGanttBar>();
        public List<FxGanttBar> BarList {
            set { _barList = value; }
            get { return _barList; }
        }
        #endregion

        #region Publicメソッド
        /// <summary>
        /// キー文字列取得
        /// </summary>
        /// <returns></returns>
        //public string ToKeyString() {
        //    return TaskKey + ";" + Operation + ";" + WorkDiv + ";" + Div;
        //}
        #endregion
    }
}
