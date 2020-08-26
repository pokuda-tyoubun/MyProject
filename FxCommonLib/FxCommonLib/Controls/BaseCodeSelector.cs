using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FxCommonLib.Consts.MES;
using FxCommonLib.Controls;
using FxCommonLib.Utils;

namespace FxCommonLib.Controls {
    public abstract partial class BaseCodeSelector : UserControl {

        #region Properties

        /// <summary>コード値</summary>
        public string Value {
            get { return this.CodeText.Text; }
            set { this.CodeText.Text = value; }
        }
        /// <summary>コード値</summary>
        public override string Text {
            get { return this.CodeText.Text; }
        }

        /// <summary>名称値</summary>
        public string NameValue {
            get { return this.NameText.Text; }
            set { this.NameText.Value = value; }
        }
        /// <summary>コード値の背景色</summary>
        public Color CodeTextBackColor {
            set { this.CodeText.BackColor = value; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>コード値のdbカラム名</summary>
        protected string _codeColString;
        /// <summary>名称値のdbカラム名</summary>
        protected string _nameColString;
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseCodeSelector() {
            InitializeComponent();

            this.NameText.ShowFocusRectangle = true;

            // 名称補完用dbカラム名登録
            SetKeyNameColName();

            // 名称補完用データの登録
            SetCodeAssistData();
            
            // 名称補完イベントの登録
            AddCodeCheckHandler();

            this.CodeText.BackColor = Color.White;

        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param customerName="sender"></param>
        /// <param customerName="e"></param>
        protected abstract void SelectButton_Click(object sender, EventArgs e);
        /// <summary>
        /// マウスエンター
        /// </summary>
        /// <param customerName="sender"></param>
        /// <param customerName="e"></param>
        protected void NameText_MouseEnter(object sender, EventArgs e) {
            this.Tooltip.SetToolTip(this.NameText, this.NameText.Text);
        }
        /// <summary>
        /// コードチェック後 - 成功時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CodeText_CodeCheckOk(object sender, EventArgs e, DataTable dt) {
            DataRow dr = dt.Rows[0];
            this.NameText.Value = StringUtil.NullToBlank(dr[_nameColString]);
        }
        /// <summary>
        /// コードチェック後 - 成功時 - 失敗時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CodeText_CodeCheckNg() {
            this.CodeText.Text = String.Empty;
            this.NameText.Value = String.Empty;
        }
        #endregion EventHandlers

        #region ProtectedMethods
        /// <summary>
        /// 名称補完用dbカラム名登録
        /// </summary>
        protected abstract void SetKeyNameColName();
        /// <summary>
        /// 名称補完用データの登録
        /// </summary>
        protected abstract void SetCodeAssistData();
        #endregion ProtectedMethods

        #region PrivateMethods
        /// <summary>
        /// 名称補完イベントの追加
        /// </summary>
        private void AddCodeCheckHandler() {
            this.CodeText.OnCheckOK += CodeText_CodeCheckOk;// 成功時
            this.CodeText.OnCheckNG += CodeText_CodeCheckNg;// 失敗時
        }
        #endregion PrivateMethods

    }
}
