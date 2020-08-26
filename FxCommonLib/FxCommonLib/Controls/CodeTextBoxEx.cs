using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// テキストボックス拡張 - コード入力用
    /// ・コード値を入力し、チェック、名称取得をするためのコントロール
    /// </summary>
    public partial class CodeTextBoxEx : TextBoxEx,ICodeTextBoxEx {

        #region Properties
        /// <summary>判定用DataTable</summary>
        protected DataTable _codeTable = null;
        public DataTable CodeTable {
            set { _codeTable = value; }
            //get { return _codeTable; }
        }

        /// <summary>判定用コード名称</summary>
        protected string _keyCode = null;
        public string keyCode {
            set { _keyCode = value; }
            //get { return _codeTable; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>前回チェックしたテキスト値</summary>
        private string _previousText;
        #endregion MemberVariables

        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="max"></param>
        public CodeTextBoxEx(int max) : base(max) {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CodeTextBoxEx() : base() {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public CodeTextBoxEx(IContainer container) : base(container) {
        }
        #endregion Constractor

        #region EventHandlers

        /// <summary>
        /// コードチェックOK時のイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dt"></param>
        public delegate void OnCheckOKHandler(object sender, EventArgs e,DataTable dt);

        /// <summary>
        /// コードチェックNG時のイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dt"></param>
        public delegate void OnCheckNGHandler();

        /// <summary>
        /// コードチェックOK時イベント
        /// コントロール呼出元にて内容をデリゲートしてください
        /// </summary>
        public event OnCheckOKHandler OnCheckOK;

        /// <summary>
        /// コードチェックNG時イベント
        /// コントロール呼出元にて内容をデリゲートしてください
        /// </summary>
        public event OnCheckNGHandler OnCheckNG;

        /// <summary>
        /// Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeTextBoxEx_Validating(object sender, System.ComponentModel.CancelEventArgs e) {

            if (this.ReadOnly) {
                return;
            }

            if (String.IsNullOrEmpty(this.Text)) {

                _previousText = String.Empty;

                // NG処理
                OnCheckNG();
                return;
            }

            DataTable dt = new DataTable();

            if (CheckCode(out dt)) {

                // OK処理
                OnCheckOK(sender,e,dt);

                _previousText = this.Text;

            } else {

                // NG処理
                OnCheckNG();

                // NOTE:グリッドセルへのエディタとして利用した場合に文字列をクリアできないためここでクリア
                this.Text = String.Empty;

                _previousText = this.Text;

                e.Cancel = true;
            }

        }

        #endregion EventHandlers

        #region PublicMethods

        /// <summary>
        /// 初期処理
        /// </summary>
        public void LoadControl() {
            AddValidatingEventHandler();
        }

        /// <summary>
        /// コードチェック
        /// </summary>
        /// <returns></returns>
        public bool CheckCode(out DataTable dt) {

            // プロパティチェック
            if (_codeTable == null) {
                throw new ArgumentException();
            }
            if (String.IsNullOrEmpty(_keyCode)) {
                throw new ArgumentException();
            }

            // 
            dt = Search();

            // 1件のみの取得をOK（特定）とする
            return (dt.Rows.Count == 1);

        }

        #endregion PublicMethods

        #region PrivateMethods

        /// <summary>
        /// 検索
        /// </summary>
        private DataTable Search() {

            DataTable retDt = new DataTable();

            DataRow[] rows = (
                from row in _codeTable.AsEnumerable()
                let code = row.Field<String>(_keyCode)
                where code == this.Text
                select row).ToArray();

            if (rows.Count() > 0) {
                retDt = rows.CopyToDataTable();
            }

            return retDt;
        }

        /// <summary>
        /// Validatingイベントを登録
        /// </summary>
        private void AddValidatingEventHandler() {
            base.Validating += CodeTextBoxEx_Validating;
        }

        #endregion PrivateMethods

    }
}
