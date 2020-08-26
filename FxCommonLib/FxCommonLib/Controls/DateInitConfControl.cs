using C1.Win.C1Input;
using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Models;
using FxCommonLib.Utils;
using System;
using System.Drawing;
using System.Text;

namespace FxCommonLib.Controls {
    /// <summary>
    /// 日付(自至)コントロール設定用
    /// (【注意】MultiLangUtilは利用前にsetしてください)
    /// </summary>
    public class DateInitConfControl : C1.Win.C1Input.DropDownForm {

        #region Properties
        /// <summary>選択値</summary>
        private ValuePair _selectedValue = new ValuePair("", "");
        public ValuePair SelectedValue {
            set { _selectedValue = value; }
            get { return _selectedValue; }
        }
        /// <summary>多言語対応ユーティリティ</summary>
        private static MultiLangUtil _mlu = null;
        public static MultiLangUtil MultiLangUtil {
            set { _mlu = value; }
            get { return _mlu; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>年範囲設定</summary>
        private ValueInterval _yearValueInterval = new ValueInterval(0, 999, true, true);
        /// <summary>月範囲設定</summary>
        private ValueInterval _monthValueInterval = new ValueInterval(0, 9999, true, true);
        /// <summary>日範囲設定</summary>
        private ValueInterval _dayValueInterval = new ValueInterval(0, 9999, true, true);
        /// <summary>時間範囲設定</summary>
        private ValueInterval _hourValueInterval = new ValueInterval(0, 9999, true, true);
        /// <summary>分範囲設定</summary>
        private ValueInterval _minutesValueInterval = new ValueInterval(0, 9999, true, true);
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DateInitConfControl() : base() {
            InitializeComponent();

            _yearValueInterval.UseMinValue = true;
            _yearValueInterval.UseMaxValue = true;
            _monthValueInterval.UseMinValue = true;
            _monthValueInterval.UseMaxValue = true;
            _dayValueInterval.UseMinValue = true;
            _dayValueInterval.UseMaxValue = true;
            _hourValueInterval.UseMinValue = true;
            _hourValueInterval.UseMaxValue = true;
            _minutesValueInterval.UseMinValue = true;
            _minutesValueInterval.UseMaxValue = true;
        }
        #endregion Constractors

        #region InitializeComponent
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.Label label5;
        protected C1.Win.C1Input.C1NumericEdit MonthNum;
        protected C1.Win.C1Input.C1NumericEdit DayNum;
        protected C1.Win.C1Input.C1NumericEdit HourNum;
        protected C1.Win.C1Input.C1NumericEdit MinutesNum;
        protected C1.Win.C1Input.C1DateEdit SpecifiedDate;
        protected System.Windows.Forms.RadioButton NotSpecifiedRadio;
        protected System.Windows.Forms.RadioButton SpecifiedRadio;
        protected System.Windows.Forms.RadioButton NowDateRadio;
        protected System.Windows.Forms.RadioButton PastRadio;
        protected System.Windows.Forms.RadioButton FutureRadio;
        protected System.Windows.Forms.GroupBox DiffTimeGroup;
        protected System.Windows.Forms.RadioButton NoneRadio;
        internal System.Windows.Forms.Button CancelButton1;
        internal System.Windows.Forms.Button OKButton;
        protected C1.Win.C1Input.C1NumericEdit YearNum;
        protected void InitializeComponent() {
            this.YearNum = new C1.Win.C1Input.C1NumericEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MonthNum = new C1.Win.C1Input.C1NumericEdit();
            this.DayNum = new C1.Win.C1Input.C1NumericEdit();
            this.HourNum = new C1.Win.C1Input.C1NumericEdit();
            this.MinutesNum = new C1.Win.C1Input.C1NumericEdit();
            this.SpecifiedDate = new C1.Win.C1Input.C1DateEdit();
            this.NotSpecifiedRadio = new System.Windows.Forms.RadioButton();
            this.SpecifiedRadio = new System.Windows.Forms.RadioButton();
            this.NowDateRadio = new System.Windows.Forms.RadioButton();
            this.PastRadio = new System.Windows.Forms.RadioButton();
            this.FutureRadio = new System.Windows.Forms.RadioButton();
            this.DiffTimeGroup = new System.Windows.Forms.GroupBox();
            this.NoneRadio = new System.Windows.Forms.RadioButton();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.YearNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HourNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecifiedDate)).BeginInit();
            this.DiffTimeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // YearNum
            // 
            this.YearNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.YearNum.CustomFormat = "##0";
            this.YearNum.DataType = typeof(int);
            this.YearNum.EmptyAsNull = true;
            this.YearNum.ErrorInfo.ShowErrorMessage = false;
            this.YearNum.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.YearNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.YearNum.Location = new System.Drawing.Point(6, 39);
            this.YearNum.Name = "YearNum";
            this.YearNum.ShowFocusRectangle = true;
            this.YearNum.Size = new System.Drawing.Size(60, 17);
            this.YearNum.TabIndex = 3;
            this.YearNum.Tag = null;
            this.YearNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YearNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(67, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "年";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(149, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ヵ月";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(236, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "日";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(318, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "時間";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(411, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "分";
            // 
            // MonthNum
            // 
            this.MonthNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.MonthNum.DataType = typeof(int);
            this.MonthNum.EmptyAsNull = true;
            this.MonthNum.ErrorInfo.ShowErrorMessage = false;
            this.MonthNum.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.MonthNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MonthNum.Location = new System.Drawing.Point(88, 39);
            this.MonthNum.Name = "MonthNum";
            this.MonthNum.ShowFocusRectangle = true;
            this.MonthNum.Size = new System.Drawing.Size(60, 17);
            this.MonthNum.TabIndex = 5;
            this.MonthNum.Tag = null;
            this.MonthNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MonthNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // DayNum
            // 
            this.DayNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.DayNum.DataType = typeof(int);
            this.DayNum.EmptyAsNull = true;
            this.DayNum.ErrorInfo.ShowErrorMessage = false;
            this.DayNum.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.DayNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.DayNum.Location = new System.Drawing.Point(175, 39);
            this.DayNum.Name = "DayNum";
            this.DayNum.ShowFocusRectangle = true;
            this.DayNum.Size = new System.Drawing.Size(60, 17);
            this.DayNum.TabIndex = 7;
            this.DayNum.Tag = null;
            this.DayNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DayNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // HourNum
            // 
            this.HourNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.HourNum.DataType = typeof(int);
            this.HourNum.EmptyAsNull = true;
            this.HourNum.ErrorInfo.ShowErrorMessage = false;
            this.HourNum.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.HourNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.HourNum.Location = new System.Drawing.Point(257, 39);
            this.HourNum.Name = "HourNum";
            this.HourNum.ShowFocusRectangle = true;
            this.HourNum.Size = new System.Drawing.Size(60, 17);
            this.HourNum.TabIndex = 9;
            this.HourNum.Tag = null;
            this.HourNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HourNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // MinutesNum
            // 
            this.MinutesNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.MinutesNum.DataType = typeof(int);
            this.MinutesNum.EmptyAsNull = true;
            this.MinutesNum.ErrorInfo.ShowErrorMessage = false;
            this.MinutesNum.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.MinutesNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MinutesNum.Location = new System.Drawing.Point(350, 39);
            this.MinutesNum.Name = "MinutesNum";
            this.MinutesNum.ShowFocusRectangle = true;
            this.MinutesNum.Size = new System.Drawing.Size(60, 17);
            this.MinutesNum.TabIndex = 11;
            this.MinutesNum.Tag = null;
            this.MinutesNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MinutesNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // SpecifiedDate
            // 
            this.SpecifiedDate.AllowSpinLoop = false;
            // 
            // 
            // 
            this.SpecifiedDate.Calendar.DayNameLength = 1;
            this.SpecifiedDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.SpecifiedDate.CustomFormat = "yyyy/MM/dd HH:mm";
            this.SpecifiedDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.SpecifiedDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.SpecifiedDate.Location = new System.Drawing.Point(86, 49);
            this.SpecifiedDate.Name = "SpecifiedDate";
            this.SpecifiedDate.ShowFocusRectangle = true;
            this.SpecifiedDate.Size = new System.Drawing.Size(154, 17);
            this.SpecifiedDate.TabIndex = 3;
            this.SpecifiedDate.Tag = null;
            // 
            // NotSpecifiedRadio
            // 
            this.NotSpecifiedRadio.AutoSize = true;
            this.NotSpecifiedRadio.Location = new System.Drawing.Point(14, 7);
            this.NotSpecifiedRadio.Name = "NotSpecifiedRadio";
            this.NotSpecifiedRadio.Size = new System.Drawing.Size(59, 16);
            this.NotSpecifiedRadio.TabIndex = 0;
            this.NotSpecifiedRadio.Text = "未設定";
            this.NotSpecifiedRadio.UseVisualStyleBackColor = true;
            this.NotSpecifiedRadio.CheckedChanged += new System.EventHandler(this.NotSpecifiedRadio_CheckedChanged);
            // 
            // SpecifiedRadio
            // 
            this.SpecifiedRadio.AutoSize = true;
            this.SpecifiedRadio.Location = new System.Drawing.Point(14, 49);
            this.SpecifiedRadio.Name = "SpecifiedRadio";
            this.SpecifiedRadio.Size = new System.Drawing.Size(71, 16);
            this.SpecifiedRadio.TabIndex = 2;
            this.SpecifiedRadio.Text = "指定日時";
            this.SpecifiedRadio.UseVisualStyleBackColor = true;
            // 
            // NowDateRadio
            // 
            this.NowDateRadio.AutoSize = true;
            this.NowDateRadio.Location = new System.Drawing.Point(14, 27);
            this.NowDateRadio.Name = "NowDateRadio";
            this.NowDateRadio.Size = new System.Drawing.Size(71, 16);
            this.NowDateRadio.TabIndex = 1;
            this.NowDateRadio.Text = "現在日時";
            this.NowDateRadio.UseVisualStyleBackColor = true;
            // 
            // PastRadio
            // 
            this.PastRadio.AutoSize = true;
            this.PastRadio.Location = new System.Drawing.Point(83, 18);
            this.PastRadio.Name = "PastRadio";
            this.PastRadio.Size = new System.Drawing.Size(47, 16);
            this.PastRadio.TabIndex = 1;
            this.PastRadio.Text = "過去";
            this.PastRadio.UseVisualStyleBackColor = true;
            // 
            // FutureRadio
            // 
            this.FutureRadio.AutoSize = true;
            this.FutureRadio.Location = new System.Drawing.Point(129, 18);
            this.FutureRadio.Name = "FutureRadio";
            this.FutureRadio.Size = new System.Drawing.Size(47, 16);
            this.FutureRadio.TabIndex = 2;
            this.FutureRadio.Text = "未来";
            this.FutureRadio.UseVisualStyleBackColor = true;
            // 
            // DiffTimeGroup
            // 
            this.DiffTimeGroup.Controls.Add(this.NoneRadio);
            this.DiffTimeGroup.Controls.Add(this.FutureRadio);
            this.DiffTimeGroup.Controls.Add(this.PastRadio);
            this.DiffTimeGroup.Controls.Add(this.YearNum);
            this.DiffTimeGroup.Controls.Add(this.label1);
            this.DiffTimeGroup.Controls.Add(this.label2);
            this.DiffTimeGroup.Controls.Add(this.MinutesNum);
            this.DiffTimeGroup.Controls.Add(this.label3);
            this.DiffTimeGroup.Controls.Add(this.HourNum);
            this.DiffTimeGroup.Controls.Add(this.label4);
            this.DiffTimeGroup.Controls.Add(this.DayNum);
            this.DiffTimeGroup.Controls.Add(this.label5);
            this.DiffTimeGroup.Controls.Add(this.MonthNum);
            this.DiffTimeGroup.Location = new System.Drawing.Point(12, 81);
            this.DiffTimeGroup.Name = "DiffTimeGroup";
            this.DiffTimeGroup.Size = new System.Drawing.Size(433, 73);
            this.DiffTimeGroup.TabIndex = 4;
            this.DiffTimeGroup.TabStop = false;
            this.DiffTimeGroup.Text = "差分指定";
            // 
            // NoneRadio
            // 
            this.NoneRadio.AutoSize = true;
            this.NoneRadio.Location = new System.Drawing.Point(7, 18);
            this.NoneRadio.Name = "NoneRadio";
            this.NoneRadio.Size = new System.Drawing.Size(66, 16);
            this.NoneRadio.TabIndex = 0;
            this.NoneRadio.Text = "差分なし";
            this.NoneRadio.UseVisualStyleBackColor = true;
            this.NoneRadio.CheckedChanged += new System.EventHandler(this.NoneRadio_CheckedChanged);
            // 
            // CancelButton1
            // 
            this.CancelButton1.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(299, 43);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(140, 35);
            this.CancelButton1.TabIndex = 6;
            this.CancelButton1.Text = "キャンセル(&C)";
            this.CancelButton1.UseVisualStyleBackColor = false;
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.SystemColors.Control;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(299, 8);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(140, 35);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            // 
            // DateInitConfControl
            // 
            this.ClientSize = new System.Drawing.Size(452, 164);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DiffTimeGroup);
            this.Controls.Add(this.NowDateRadio);
            this.Controls.Add(this.SpecifiedRadio);
            this.Controls.Add(this.NotSpecifiedRadio);
            this.Controls.Add(this.SpecifiedDate);
            this.FocusControl = this.OKButton;
            this.Name = "DateInitConfControl";
            this.PostChanges += new System.EventHandler(this.DateInitConfControl_PostChanges);
            this.Open += new System.EventHandler(this.DateInitConfControl_Open);
            this.Load += new System.EventHandler(this.DateInitConfControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.YearNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HourNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecifiedDate)).EndInit();
            this.DiffTimeGroup.ResumeLayout(false);
            this.DiffTimeGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// 差分入力の活性・非活性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoneRadio_CheckedChanged(object sender, EventArgs e) {
            if (this.NoneRadio.Checked) {
                this.YearNum.Enabled = false;
                this.MonthNum.Enabled = false;
                this.DayNum.Enabled = false;
                this.HourNum.Enabled = false;
                this.MinutesNum.Enabled = false;
                this.YearNum.Value = DBNull.Value;
                this.MonthNum.Value = DBNull.Value;
                this.DayNum.Value = DBNull.Value;
                this.HourNum.Value = DBNull.Value;
                this.MinutesNum.Value = DBNull.Value;
            } else {
                this.YearNum.Enabled = true;
                this.MonthNum.Enabled = true;
                this.DayNum.Enabled = true;
                this.HourNum.Enabled = true;
                this.MinutesNum.Enabled = true;
            }
        }
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DateInitConfControl_Load(object sender, EventArgs e) {
            this.YearNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            this.YearNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.YearNum.PostValidation.Intervals.Add(_yearValueInterval);
            this.YearNum.MaxLength = 3;
            this.MonthNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            this.MonthNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.MonthNum.PostValidation.Intervals.Add(_monthValueInterval);
            this.MonthNum.MaxLength = 4;
            this.DayNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            this.DayNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.DayNum.PostValidation.Intervals.Add(_dayValueInterval);
            this.DayNum.MaxLength = 4;
            this.HourNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            this.HourNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.HourNum.PostValidation.Intervals.Add(_hourValueInterval);
            this.HourNum.MaxLength = 4;
            this.MinutesNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            this.MinutesNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            this.MinutesNum.PostValidation.Intervals.Add(_minutesValueInterval);
            this.MinutesNum.MaxLength = 4;
        }
        /// <summary>
        /// 未設定ラジオの選択変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotSpecifiedRadio_CheckedChanged(object sender, EventArgs e) {
            if (this.NotSpecifiedRadio.Checked) {
                this.DiffTimeGroup.Enabled = false;
            } else {
                this.DiffTimeGroup.Enabled = true;
            }
        }

        /// <summary>
        /// 日付初期設定コントロールを開いた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateInitConfControl_Open(object sender, EventArgs e) {
            SelectedValue.InternalValue = (string)OwnerControl.Value;
            SelectedValue.DisplayValue = OwnerControl.Text;
            SetControlValue(SelectedValue.InternalValue);
        }

        /// <summary>
        /// 呼び出し元プルダウンの値を変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateInitConfControl_PostChanges(object sender, EventArgs e) {
            OwnerControl.Text = GetDisplayValue();
            OwnerControl.Value = GetValueForSave();
        }
        #endregion EventHandlers

        #region PublicMethods
        /// <summary>
        /// 表示文字取得
        /// </summary>
        /// <param name="internalValue"></param>
        /// <returns></returns>
        public static string GetDisplayValue(string internalValue) {
            StringBuilder ret = new StringBuilder("");

            if (internalValue == "") {
                return ret.ToString();
            }
            
            string[] valArray = internalValue.Split(';');

            ret.Append(valArray[0].Replace("Now", _mlu.GetMsg(CommonConsts.DC_NOW)));
            if (valArray.Length == 1) {
                return ret.ToString();
            } else {
                ret.Append(_mlu.GetMsg(CommonConsts.DC_FROM));
                //時間の差分
                if (valArray[1] != "") {
                    ret.Append(valArray[1]);
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_Y));
                }
                if (valArray[2] != "") {
                    ret.Append(valArray[2]);
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_M));
                }
                if (valArray[3] != "") {
                    ret.Append(valArray[3]);
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_D));
                }
                if (valArray[4] != "") {
                    ret.Append(valArray[4]);
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_H));
                }
                if (valArray[5] != "") {
                    ret.Append(valArray[5]);
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_MI));
                }
                //時間の方向
                if (valArray[6] == "-") {
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_PAST));
                } else {
                    ret.Append(_mlu.GetMsg(CommonConsts.DC_FUTURE));
                }
            }

            return ret.ToString();
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// 各コントロールの値をセット
        /// </summary>
        /// <param name="internalValue"></param>
        private void SetControlValue(string internalValue) {

            this.NoneRadio.Checked = true;
            this.SpecifiedDate.Value = DateTime.Now;
            if (internalValue == "") {
                //未設定の場合
                this.NotSpecifiedRadio.Checked = true;

                return;
            }
            
            string[] valArray = internalValue.Split(';');
            if (valArray[0] == "Now") {
                //現在日時
                this.NowDateRadio.Checked = true;
            } else {
                //指定日時
                this.SpecifiedRadio.Checked = true;
                this.SpecifiedDate.Value = valArray[0];
            }
            if (valArray.Length > 1) {
                //時間の差分
                this.YearNum.Value = valArray[1];
                this.MonthNum.Value = valArray[2];
                this.DayNum.Value = valArray[3];
                this.HourNum.Value = valArray[4];
                this.MinutesNum.Value = valArray[5];
                //時間の方向
                if (valArray[6] == "-") {
                    this.PastRadio.Checked = true;
                } else {
                    this.FutureRadio.Checked = true;
                }
            }
        }
        /// <summary>
        /// 保存用文字列の取得
        /// </summary>
        /// <returns></returns>
        private string GetValueForSave() {
            StringBuilder ret = new StringBuilder("");

            if (this.NotSpecifiedRadio.Checked) {
                //ブランクのまま
                return ret.ToString();
            } else if (this.SpecifiedRadio.Checked) {
                ret.Append(DateTimeUtil.FormatYMi(this.SpecifiedDate.Value.ToString(), _mlu));
            } else {
                ret.Append("Now");
            }
            //差分あり
            if (!this.NoneRadio.Checked) {
                ret.Append(";");
                //時間の差分
                if (this.YearNum.Value.ToString() != "") {
                    ret.Append(Math.Abs((int)YearNum.Value).ToString());
                }
                ret.Append(";");
                if (this.MonthNum.Value.ToString() != "") {
                    ret.Append(Math.Abs((int)MonthNum.Value).ToString());
                }
                ret.Append(";");
                if (this.DayNum.Value.ToString() != "") {
                    ret.Append(Math.Abs((int)DayNum.Value).ToString());
                }
                ret.Append(";");
                if (this.HourNum.Value.ToString() != "") {
                    ret.Append(Math.Abs((int)HourNum.Value).ToString());
                }
                ret.Append(";");
                if (this.MinutesNum.Value.ToString() != "") {
                    ret.Append(Math.Abs((int)MinutesNum.Value).ToString());
                }
                ret.Append(";");
                //時間の方向
                if (this.PastRadio.Checked) {
                    ret.Append("-");
                }
                if (this.FutureRadio.Checked) {
                    ret.Append("+");
                }
            }
            return ret.ToString();
        }
        /// <summary>
        /// 表示用の値を取得
        /// </summary>
        /// <returns></returns>
        private string GetDisplayValue() {
            StringBuilder ret = new StringBuilder("");

            if (this.NotSpecifiedRadio.Checked) {
                //ブランクのまま
                return ret.ToString();
            } else if (this.SpecifiedRadio.Checked) {
                ret.Append(DateTimeUtil.FormatYMi(this.SpecifiedDate.Value.ToString(), _mlu));
            } else {
                ret.Append(_mlu.GetMsg(CommonConsts.DC_NOW));
            }

            //差分あり
            if (!this.NoneRadio.Checked) {
                ret.Append(_mlu.GetMsg(CommonConsts.DC_FROM));
            }

            //時間の差分
            if (this.YearNum.Value.ToString() != "") {
                ret.Append(Math.Abs((int)YearNum.Value).ToString());
                ret.Append(_mlu.GetMsg(CommonConsts.DC_Y));
            }
            if (this.MonthNum.Value.ToString() != "") {
                ret.Append(Math.Abs((int)MonthNum.Value).ToString());
                ret.Append(_mlu.GetMsg(CommonConsts.DC_M));
            }
            if (this.DayNum.Value.ToString() != "") {
                ret.Append(Math.Abs((int)DayNum.Value).ToString());
                ret.Append(_mlu.GetMsg(CommonConsts.DC_D));
            }
            if (this.HourNum.Value.ToString() != "") {
                ret.Append(Math.Abs((int)HourNum.Value).ToString());
                ret.Append(_mlu.GetMsg(CommonConsts.DC_H));
            }
            if (this.MinutesNum.Value.ToString() != "") {
                ret.Append(Math.Abs((int)MinutesNum.Value).ToString());
                ret.Append(_mlu.GetMsg(CommonConsts.DC_MI));
            }
            //時間の方向
            if (this.PastRadio.Checked) {
                ret.Append(_mlu.GetMsg(CommonConsts.DC_PAST));
            }
            if (this.FutureRadio.Checked) {
                ret.Append(_mlu.GetMsg(CommonConsts.DC_FUTURE));
            }
            return ret.ToString();
        }
        #endregion PrivateMethods
    }
}
