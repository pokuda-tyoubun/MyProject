using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// FlexGridExのDataTypeDM,DO用の日付設定ダイアログ
    /// </summary>
    public partial class DateEditDialog : Form {

        #region Properties
        /// <summary>選択日付</summary>
        public string SelectedDate { get; set; }
        /// <summary>日付フォーマット</summary>
        public string DateFormat { get; set; }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="format"></param>
        public DateEditDialog(string format) {
            InitializeComponent();

            SelectedDate = "";
            DateFormat = format;
            this.InputDate.CustomFormat = format;
            this.InputDate.Value = DateTime.Parse(DateTime.Now.ToString(format));
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            SelectedDate = "";
            this.Close();
        }

        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e) {
            SelectedDate = DateTime.Parse(this.InputDate.Text).ToString(DateFormat);
            this.Close();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateEditDialog_Load(object sender, EventArgs e) {

        }
        #endregion EventHandlers
    }
}
