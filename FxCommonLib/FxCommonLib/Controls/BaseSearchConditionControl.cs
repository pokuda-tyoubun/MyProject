using C1.Win.C1Input;
using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// 検索条件コントロール
    /// (※abstractクラスのためデザイナは利用できない)
    /// </summary>
    public abstract partial class BaseSearchConditionControl : UserControl {

        #region Constants
        /// <summary>高さ</summary>
        protected const int RowHeight = 25;
        /// <summary>ラベルの左位置</summary>
        protected const int LabelLeft = 5;
        /// <summary>入力エリアの左位置</summary>
        protected const int InputLeft = 170;
        /// <summary>～文字の左位置</summary>
        protected const int WaveLeft = 325;

        /// <summary>表示順入力コントロールの左位置</summary>
        protected const int DispOrderLeft = 5;
        /// <summary>表示チェックボックスの左位置</summary>
        protected const int VisibleCheckLeft = 60;
        /// <summary>コントロール名ラベルの左位置</summary>
        protected const int ControlNameLeft = 95;
        /// <summary>入力コントロール1の左位置</summary>
        protected const int Value1Left = 260;
        /// <summary>入力コントロール2の左位置</summary>
        protected const int Value2Left = 415;
        #endregion Constants

        #region Properties
        /// <summary>初期表示するかどうか</summary>
        public bool ShowInitData { get; set; }
        /// <summary>Window名</summary>
        public string WindowName { get; set; }
        /// <summary>Grid名</summary>
        public string GridName { get; set; }
        /// <summary>設定モード</summary>
        public bool ConfigMode { get; set; }
        public Panel GetMainPanel {
            get { return this.MainPanel; }
        }
        /// <summary>多言語化ユーティリティ</summary>
        private MultiLangUtil _mlu = null;
        protected MultiLangUtil MultiLangUtil {
//            set { _mlu = style; }
            get { return _mlu; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>初期設定値</summary>
        protected DataSet _initCondition = null;
        /// <summary>ラベルサイズ</summary>
        protected static Size _labelSize = new Size(160, 20);
        /// <summary>標準コントロールサイズ</summary>
        protected static Size _ctlSize = new Size(150, 20);
        /// <summary>ワイドコントロールサイズ</summary>
        protected static Size _wideCtlSize = new Size(210, 20);
        /// <summary>モアワイドコントロールサイズ</summary>
        protected static Size _moreWideCtlSize = new Size(330, 20);
        /// <summary>～文字サイズ</summary>
        protected static Size _waveSize = new Size(30, 20);
        /// <summary>表示順コントロールサイズ</summary>
        protected static Size _dispOrderSize = new Size(40, 20);
        /// <summary>表示チェックボックスサイズ</summary>
        protected static Size _visibleSize = new Size(30, 18);

        private DateTime _minDate = DateTime.Parse("1970/01/01 00:00:00");
        private DateTime _maxDate = DateTime.Parse("9999/12/31 23:59:59");
        private ValueInterval _int32ValueInterval = new ValueInterval(Int32.MinValue, Int32.MaxValue, true, true);
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="multilangutil"></param>
        public BaseSearchConditionControl(MultiLangUtil multilangutil) {

            if (multilangutil == null) {
                throw new System.ArgumentException("Parameter cannot be null", "multilangutil");
            }

            _mlu = multilangutil;

            InitializeComponent();
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// コントロールロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionControl_Load(object sender, EventArgs e) {
            //
        }
        #endregion EventHandlers

        #region PublicMethods
        /// <summary>
        /// コントロール配置
        /// </summary>
        public void LoadControl() {
            LoadControl(false);
        }

        /// <summary>
        /// 初期値を設定している場合、表示フラグをONにしているか検証する。
        /// </summary>
        /// <returns></returns>
        public bool ValidateVisibleFlg() {
            foreach (DataRow dr in _initCondition.Tables[CommonConsts.SearchItemTbl].Rows) {
                CheckBox visible = (CheckBox)this.MainPanel.Controls[dr[CommonConsts.db_name].ToString() + "Visible"];
                if (!visible.Checked) {
                    if (dr[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypePulldownlist ||
                        dr[CommonConsts.ctl_type].ToString() == MESConsts.CtlTypeGroupList ||
                        dr[CommonConsts.ctl_type].ToString() == MESConsts.CtlTypeItemGroupList) {
                        C1ComboBox ccb = (C1ComboBox)this.MainPanel.Controls[dr[CommonConsts.db_name].ToString()];
                        if (ccb.SelectedItem != null) {
                            if (ccb.SelectedItem.ToString() != "") {
                                visible.Select();
                                MessageBox.Show(
                                    _mlu.GetMsg(CommonConsts.MSG_VISIBLE_FLG_ERROR) + "(" + dr[CommonConsts.col_name] + ")",
                                    _mlu.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    } else if (dr[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypeDateRange && ConfigMode) {
                            C1DropDownControl c1 = (C1DropDownControl)this.MainPanel.Controls[dr[CommonConsts.db_name].ToString() + "_1"];
                            C1DropDownControl c2 = (C1DropDownControl)this.MainPanel.Controls[dr[CommonConsts.db_name].ToString() + "_2"];
                            if (c1.Value.ToString() != "" || c2.Value.ToString() != "") {
                                visible.Select();
                                MessageBox.Show(
                                    _mlu.GetMsg(CommonConsts.MSG_VISIBLE_FLG_ERROR) + "(" + dr[CommonConsts.col_name] + ")",
                                    _mlu.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                    } else {
                        Control c1 = null;
                        Control c2 = null;

                        c1 = this.MainPanel.Controls[dr[CommonConsts.db_name].ToString()];
                        if (c1 != null) {
                            if (c1.Text != "") {
                                visible.Select();
                                MessageBox.Show(
                                    _mlu.GetMsg(CommonConsts.MSG_VISIBLE_FLG_ERROR) + "(" + dr[CommonConsts.col_name] + ")",
                                    _mlu.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        } else {
                            c1 = this.MainPanel.Controls[dr[CommonConsts.db_name].ToString() + "_1"];
                            c2 = this.MainPanel.Controls[dr[CommonConsts.db_name].ToString() + "_2"];
                            if (c1.Text != "" || c2.Text != "") {
                                visible.Select();
                                MessageBox.Show(
                                    _mlu.GetMsg(CommonConsts.MSG_VISIBLE_FLG_ERROR) + "(" + dr[CommonConsts.col_name] + ")",
                                    _mlu.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 検索条件クリア(表示されている条件のみクリア)
        /// </summary>
        public void ClearCondition() {
            Control c1 = null;
            Control c2 = null;
            foreach (DataRow r in _initCondition.Tables[CommonConsts.SearchItemTbl].Rows) {
                if (!(bool)r[CommonConsts.visible]) {
                    continue;
                }
                if (r[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypePulldownlist) {
                    C1ComboBox ccb = (C1ComboBox)this.MainPanel.Controls[r[CommonConsts.db_name].ToString()];
                    ccb.SelectedIndex = 0;
                } else if (r[CommonConsts.ctl_type].ToString() == MESConsts.CtlTypeGroupList) {
                    C1ComboBox ccb = (C1ComboBox)this.MainPanel.Controls[r[CommonConsts.db_name].ToString()];
                    ccb.SelectedIndex = 0;
                } else if (r[CommonConsts.ctl_type].ToString() == MESConsts.CtlTypeItemGroupList) {
                    C1ComboBox ccb = (C1ComboBox)this.MainPanel.Controls[r[CommonConsts.db_name].ToString()];
                    ccb.SelectedIndex = 0;
                } else {
                    c1 = this.MainPanel.Controls[r[CommonConsts.db_name].ToString()];
                    if (c1 != null) {
                        c1.Text = "";
                    } else {
                        c1 = this.MainPanel.Controls[r[CommonConsts.db_name].ToString() + "_1"];
                        c2 = this.MainPanel.Controls[r[CommonConsts.db_name].ToString() + "_2"];
                        if (r[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypeDateRange) {
                            ((C1DateEdit)c1).Clear();
                            ((C1DateEdit)c2).Clear();
                        } else if(r[CommonConsts.ctl_type].ToString() == CommonConsts.CtlTypeNumericRange) {
                            ((C1NumericEdit)c1).Value = DBNull.Value;
                            ((C1NumericEdit)c2).Value = DBNull.Value;
                        } else {
                            c1.Text = "";
                            c2.Text = "";
                        }
                    }
                }
            }
        }
        #endregion PublicMethods

        #region ProtectedMethods
        /// <summary>
        /// ラベル設定
        /// </summary>
        /// <param name="itemX"></param>
        /// <param name="itemY"></param>
        /// <param name="s"></param>
        /// <param name="bs"></param>
        /// <param name="ctlName"></param>
        /// <param name="text"></param>
        protected void SetLabel(int itemX, int itemY, Size s, BorderStyle bs, string ctlName, string text) {
            Label lb = new Label();
            this.MainPanel.Controls.Add(lb);

            lb.BorderStyle = bs;
            lb.Location = new Point(itemX, itemY);
            lb.Name = ctlName;
            lb.Size = s;
            lb.Text = text;
            lb.TextAlign = ContentAlignment.MiddleLeft;
        }
        protected int SetConfCtlTypeDateRange(int lo, int vo, int wo, int itemY, DataRow r) {
            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());

            DateInitConfControl.MultiLangUtil = _mlu;

            //自
            C1DropDownControl ddc1 = new C1DropDownControl();
            this.MainPanel.Controls.Add(ddc1);
            ddc1.DropDownStyle = DropDownStyle.DropDownList;
            ddc1.DropDownFormClassName = "FxCommonLib.Controls.DateInitConfControl";
            ddc1.Location = new Point(vo, itemY);
            ddc1.Name = r[CommonConsts.db_name].ToString() + "_1";
            ddc1.Size = _moreWideCtlSize;
            ddc1.ShowFocusRectangle = true;
            ddc1.TextDetached = true;
            ddc1.VisibleButtons = DropDownControlButtonFlags.DropDown;
            ddc1.ImeMode = ImeMode.Disable;

            SetLabel(wo + 60, itemY, _waveSize, BorderStyle.None, r[CommonConsts.db_name].ToString() + "RLabel", "～");
            itemY += RowHeight;
            //至
            C1DropDownControl ddc2 = new C1DropDownControl();
            this.MainPanel.Controls.Add(ddc2);
            ddc2.DropDownStyle = DropDownStyle.DropDownList;
            ddc2.DropDownFormClassName = "FxCommonLib.Controls.DateInitConfControl";
            ddc2.Location = new Point(vo, itemY);
            ddc2.Name = r[CommonConsts.db_name].ToString() + "_2";
            ddc2.Size = _moreWideCtlSize;
            ddc2.ShowFocusRectangle = true;
            ddc2.TextDetached = true;
            ddc2.VisibleButtons = DropDownControlButtonFlags.DropDown;
            ddc2.ImeMode = ImeMode.Disable;
            //値設定
            string val1 = r[CommonConsts.value1].ToString();
            string val2 = r[CommonConsts.value2].ToString();
            ddc1.Value = val1;
            ddc1.Text = DateInitConfControl.GetDisplayValue(val1);
            ddc2.Value = val2;
            ddc2.Text = DateInitConfControl.GetDisplayValue(val2);

            return itemY;
        }
        /// <summary>
        /// 時間(自至)コントロール設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="wo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        protected int SetConfCtlTypeTimeRange(int lo, int vo, int wo, int itemY, DataRow r) {
            //HACK 作成中
            return itemY;
        }
        /// <summary>
        /// プルダウンを設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <param name="ds"></param>
        protected void SetCtlTypePulldownlist(int lo, int vo, int itemY, DataRow r, DataSet ds) {
            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());
            C1ComboBox cb = new C1ComboBox();
            this.MainPanel.Controls.Add(cb);
            cb.DropDownStyle = DropDownStyle.DropDownList;
            cb.Location = new Point(vo, itemY);
            cb.Name = r[CommonConsts.db_name].ToString();
            cb.Size = _ctlSize;
            cb.ShowFocusRectangle = true;
            BindingSource bs = new BindingSource();
            DataTable candidate = ds.Tables[r[CommonConsts.db_name].ToString()];
            bs.DataSource = candidate;
            cb.ItemsDataSource = bs;
            cb.TextDetached = true; //選択後、ValueMemberで表示されるのを防ぐ
            cb.ItemsDisplayMember = CommonConsts.name;
            cb.ItemsValueMember = CommonConsts.code;
            //値設定
            if (r[CommonConsts.value2].ToString() != "") {
                DataRow[] dra = candidate.Select("convert(code, 'System.String') = '" + r[CommonConsts.value2].ToString() + "'");
                cb.SelectedIndex = candidate.Rows.IndexOf(dra[0]);
            } else {
                cb.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 日付を設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        protected void SetCtlTypeDate(int lo, int itemY, DataRow r) {
            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());
        }
        /// <summary>
        /// 日付範囲指定を設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="wo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        protected int SetCtlTypeDateRange(int lo, int vo, int wo, int itemY, DataRow r) {
            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());
            //自
            C1DateEdit de1 = new C1DateEdit();
            this.MainPanel.Controls.Add(de1);
            de1.Location = new Point(vo, itemY);
            de1.Name = r[CommonConsts.db_name].ToString() + "_1";
            de1.Size = _ctlSize;
            SetCommonProperty(de1, _mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_SEC));

            SetLabel(wo, itemY, _waveSize, BorderStyle.None, r[CommonConsts.db_name].ToString() + "RLabel", "～");
            itemY += RowHeight;
            //至
            C1DateEdit de2 = new C1DateEdit();
            this.MainPanel.Controls.Add(de2);
            de2.Location = new Point(vo, itemY);
            de2.Name = r[CommonConsts.db_name].ToString() + "_2";
            de2.Size = _ctlSize;
            SetCommonProperty(de2, _mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_SEC));
            //値設定
            de1.Value = GetDateFromSaveValue(r[CommonConsts.value1].ToString());
            de2.Value = GetDateFromSaveValue(r[CommonConsts.value2].ToString());

            return itemY;
        }
        /// <summary>
        /// 時間範囲指定を設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="wo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        protected int SetCtlTypeTimeRange(int lo, int vo, int wo, int itemY, DataRow r) {
            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());
            //自
            C1DateEdit de1 = new C1DateEdit();
            this.MainPanel.Controls.Add(de1);
            de1.Location = new Point(vo, itemY);
            de1.Name = r[CommonConsts.db_name].ToString() + "_1";
            de1.Size = _ctlSize;
            de1.ShowDropDownButton = false;
            de1.ShowUpDownButtons = false;
            de1.EditMask = _mlu.GetMsg(CommonConsts.DATE_MASK_HHMM);
            SetCommonProperty(de1, _mlu.GetMsg(CommonConsts.DATE_FORMAT_HHMM));

            SetLabel(wo, itemY, _waveSize, BorderStyle.None, r[CommonConsts.db_name].ToString() + "RLabel", "～");
            itemY += RowHeight;
            //至
            C1DateEdit de2 = new C1DateEdit();
            this.MainPanel.Controls.Add(de2);
            de2.Location = new Point(vo, itemY);
            de2.Name = r[CommonConsts.db_name].ToString() + "_2";
            de2.Size = _ctlSize;
            de2.ShowDropDownButton = false;
            de2.ShowUpDownButtons = false;
            de2.EditMask = _mlu.GetMsg(CommonConsts.DATE_MASK_HHMM);
            SetCommonProperty(de2, _mlu.GetMsg(CommonConsts.DATE_FORMAT_HHMM));
            //値設定
            de1.Value = GetDateFromSaveValue(r[CommonConsts.value1].ToString());
            de2.Value = GetDateFromSaveValue(r[CommonConsts.value2].ToString());

            return itemY;
        }
        /// <summary>
        /// テキストボックスを設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        protected void SetCtlTypeString(int lo, int vo, int itemY, DataRow r) {
            //文字列
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());

            C1TextBox tb = new C1TextBox();
            this.MainPanel.Controls.Add(tb);
            tb.Location = new Point(vo, itemY);
            tb.Name = r[CommonConsts.db_name].ToString();
            tb.Size = _ctlSize;
            tb.ShowFocusRectangle = true;
            //値設定
            tb.Value = r[CommonConsts.value1].ToString();
        }
        /// <summary>
        /// 数値範囲指定を設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="wo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        protected int SetCtlTypeNumericRange(int lo, int vo, int wo, int itemY, DataRow r) {

            //ラベル
            SetLabel(lo, itemY, _labelSize, BorderStyle.FixedSingle, 
                r[CommonConsts.db_name].ToString() + "Label", r[CommonConsts.col_name].ToString());
            //自
            C1NumericEdit ne1 = new C1NumericEdit();
            this.MainPanel.Controls.Add(ne1);
            ne1.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
            ne1.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            ne1.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            ne1.Location = new Point(vo, itemY);
            ne1.Name = r[CommonConsts.db_name].ToString() + "_1";
            ne1.Size = _ctlSize;
            ne1.EmptyAsNull = true;
            ne1.ShowFocusRectangle = true;
            ne1.TextAlign = HorizontalAlignment.Right;
            ne1.PostValidation.Intervals.Add(_int32ValueInterval);
            ne1.MaxLength = CommonConsts.C1NumericEditMaxLength;
            ne1.ImeMode = ImeMode.Disable;

            SetLabel(wo, itemY, _waveSize, BorderStyle.None, r[CommonConsts.db_name].ToString() + "RLabel", "～");

            itemY += RowHeight;
            //至
            C1NumericEdit ne2 = new C1NumericEdit();
            this.MainPanel.Controls.Add(ne2);
            ne2.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
            ne2.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
            ne2.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            ne2.Location = new Point(vo, itemY);
            ne2.Name = r[CommonConsts.db_name].ToString() + "_2";
            ne2.Size = _ctlSize;
            ne2.EmptyAsNull = true;
            ne2.ShowFocusRectangle = true;
            ne2.TextAlign = HorizontalAlignment.Right;
            ne2.PostValidation.Intervals.Add(_int32ValueInterval);
            ne2.MaxLength = CommonConsts.C1NumericEditMaxLength;
            ne2.ImeMode = ImeMode.Disable;
            //値設定
            ne1.Value = r[CommonConsts.value1].ToString();
            ne2.Value = r[CommonConsts.value2].ToString();

            return itemY;
        }
        #endregion ProtectedMethods

        #region PrivateMethods
        /// <summary>
        /// ヘッダーラベル設定
        /// </summary>
        /// <param name="itemX"></param>
        /// <param name="itemY"></param>
        /// <param name="s"></param>
        /// <param name="ctlName"></param>
        /// <param name="text"></param>
        private void SetConfigHeaderLabel(int itemX, int itemY, Size s, string ctlName, string text) {
            Label lb = new Label();
            this.MainPanel.Controls.Add(lb);

            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.Location = new Point(itemX, itemY);
            lb.Name = ctlName;
            lb.Size = s;
            lb.Text = text;
            lb.TextAlign = ContentAlignment.MiddleLeft;
        }
        /// <summary>
        /// 日付(自至)コントロール設定
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="vo"></param>
        /// <param name="wo"></param>
        /// <param name="itemY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <summary>
        /// 共通プロパティ設定
        /// </summary>
        /// <param name="de"></param>
        /// <param name="format"></param>
        private void SetCommonProperty(C1DateEdit de, string format) {
            de.EmptyAsNull = true;
            de.ShowFocusRectangle = true;
            de.CustomFormat = format;
            de.FormatType = FormatTypeEnum.CustomFormat;
            de.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// 保存値から日付を取得
        /// </summary>
        /// <param name="internalValue"></param>
        /// <returns></returns>
        private string GetDateFromSaveValue(string internalValue) {
            DateTime dt = DateTime.Now;

            if (internalValue == "") {
                //未設定の場合
                return "";
            }
            
            string[] valArray = internalValue.Split(';');
            if (string.IsNullOrEmpty(valArray[0]) || valArray[0] == "Now") {
                // 起点日時入力なし、現在日時

                //現在日時
                dt = DateTime.Now;
            } else {
                //指定日時
                dt = DateTime.Parse(valArray[0]);
            }
            if (valArray.Length > 1) {
                //時間の方向
                if (valArray[6] == "-") {
                    //過去
                    try {
                        dt = dt.AddYears(int.Parse("-" + StringUtil.NullBlankToZero(valArray[1])));
                        dt = dt.AddMonths(int.Parse("-" + StringUtil.NullBlankToZero(valArray[2])));
                        dt = dt.AddDays(int.Parse("-" + StringUtil.NullBlankToZero(valArray[3])));
                        dt = dt.AddHours(int.Parse("-" + StringUtil.NullBlankToZero(valArray[4])));
                        dt = dt.AddMinutes(int.Parse("-" + StringUtil.NullBlankToZero(valArray[5])));

                        if (dt < _minDate) {
                            dt = _minDate;
                        }
                    } catch (ArgumentOutOfRangeException) {
                        dt = _minDate;
                    }
                } else {
                    //未来
                    try {
                        dt = dt.AddYears(int.Parse(StringUtil.NullBlankToZero(valArray[1])));
                        dt = dt.AddMonths(int.Parse(StringUtil.NullBlankToZero(valArray[2])));
                        dt = dt.AddDays(int.Parse(StringUtil.NullBlankToZero(valArray[3])));
                        dt = dt.AddHours(int.Parse(StringUtil.NullBlankToZero(valArray[4])));
                        dt = dt.AddMinutes(int.Parse(StringUtil.NullBlankToZero(valArray[5])));

                        if (dt > _maxDate) {
                            dt = _maxDate;
                        }
                    } catch (ArgumentOutOfRangeException) {
                        dt = _maxDate;
                    }
                }
            }

            return dt.ToString();
        }
        #endregion PrivateMethods

        #region abstract
        /// <summary>
        /// コントロール配置
        /// </summary>
        /// <returns></returns>
        public abstract void LoadControl(bool useShowInitDataMode);

        /// <summary>
        /// コントロール配置
        /// </summary>
        /// <param name="ds"></param>
        public abstract void SetControl();
        /// <summary>
        /// 入力された検索条件をDataSetとして取得
        /// </summary>
        /// <returns></returns>
        public abstract DataSet GetSearchParam();
        /// <summary>
        /// 設定変更用コントロール配置
        /// </summary>
        /// <param name="ds"></param>
        protected abstract void SetConfigControl();
        /// <summary>
        /// 検索条件初期設定情報を取得
        /// </summary>
        /// <returns></returns>
        protected abstract DataSet GetSearchCondition();
        #endregion
    }
}
