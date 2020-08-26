using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// テキストボックス拡張
    /// ・グリッドの最大入力桁数の制御に利用。
    /// ・正規表現で禁則文字を入力不可に設定。
    /// ・Ctrl+Aで全選択可能
    /// </summary>
    public partial class TextBoxEx : TextBox {

        #region Properties
        /// <summary>入力制限するための正規表現</summary>
        private string _ngWordRegex = "";
        public string NGWordRegex { 
            set {
                _ngWordRegex = value; 
                _ngWordReg = new Regex(_ngWordRegex); 
            }
            get { return _ngWordRegex; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>正規表現クラス</summary>
        private Regex _ngWordReg;
        #endregion MemberVariables

        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="max"></param>
        public TextBoxEx(int max) {
            InitializeComponent();
            if (max >= 0) {
                this.MaxLength = max;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextBoxEx() {
            InitializeComponent();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public TextBoxEx(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }
        #endregion Constractor

        #region EventHandlers
        /// <summary>
        /// キープレス(入力文字があった時)
        ///   ・禁則文字指定があれば、無効化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e) {
            if (_ngWordRegex != "") {
                if (_ngWordReg.IsMatch(e.KeyChar.ToString())) {
                    //入力不可
                    e.Handled = true;
                    return;
                }
            }
            base.OnKeyPress(e);
        }

        /// <summary>
        /// キーダウン(キーが押された時)
        ///   ・Ctrl+Aをサポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.A) {
                this.SelectAll();
            }
            base.OnKeyDown(e);
        }
        #endregion EventHandlers
    }
}
