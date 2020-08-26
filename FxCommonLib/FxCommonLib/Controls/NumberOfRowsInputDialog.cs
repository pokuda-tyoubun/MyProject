using C1.Win.C1Input;
using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using System;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    public partial class NumberOfRowsInputDialog : Form {

        #region Properties
        /// <summary>新規追加行数</summary>
        public int InputNum {get; private set; }
        /// <summary>最大追加行数</summary>
        public int MaxCount {get; private set; }
        /// <summary>多言語化ユーティリティ</summary>
        private static MultiLangUtil _mlu = null;
        public static MultiLangUtil MultiLangUtil {
            set { _mlu = value; }
            get { return _mlu; }
        }
        #endregion Properties

        #region Constractors
        public NumberOfRowsInputDialog(MultiLangUtil mlu, int maxCount) {
            InitializeComponent();

            MaxCount = maxCount;
            _mlu = mlu;
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// パネルのロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOfRowsInputDialog_Load(object sender, EventArgs e) {
            InputNum = 0;
            //int max = int.Parse(AppObject.CommonConfDic[MESConsts.MaxAppendRowCount].ToString());
            ValueInterval vi = new ValueInterval(1, MaxCount, true, true);
            vi.UseMinValue = true;
            vi.UseMaxValue = true;
            this.RowNumericEdit.PostValidation.Intervals.Add(vi);
            this.RowNumericEdit.MaxLength = CommonConsts.C1NumericEditMaxLength;
            this.RowNumericEdit.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.RowNumericEdit.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
        }

        /// <summary>
        /// OKボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e) {
            if (RowNumericEdit.Value != DBNull.Value) {
                InputNum = Convert.ToInt32(RowNumericEdit.Value);
            }
            this.Close();
        }

        /// <summary>
        /// キャンセルボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion EventHandlers
    }
}
