using C1.Win.C1Input;
using FxCommonLib;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Controls;
using FxCommonLib.Models;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FxCommonLib.BaseForm {
    /// <summary>
    /// 列設定ダイアログ
    /// </summary>
    public partial class BaseColConfDialog : Form {

        #region Constants
        protected const int DispOrderOffset = 5;
        protected const int VisibleOffset = 60;
        protected const int ColNameOffset = 95;
        protected const int WidthOffset = 330;
        protected const int NoteOffset = 385;
        protected const int DBNameOffset = 735;
        /// <summary>行間のオフセット</summary>
        protected const int RowOffset = 25;
        #endregion Constants

        #region Properties
        /// <summary>ColumnConfigUtil</summary>
        public ColumnConfigUtil ColConfUtil { get; set; }
        /// <summary>FlexGridEx</summary>
        public FlexGridEx Grid { get; set; }
        /// <summary>適用したかどうか</summary>
        public bool IsApply { get; set; }
        /// <summary>多言語化ユーティリティ</summary>
        private MultiLangUtil _mlu = null;
        public MultiLangUtil MultiLangUtil {
            set { _mlu = value; }
            get { return _mlu; }
        }
        public C1NumericEdit HeightNumCtl {
            get { return this.HeightNum; }
        }
        public C1NumericEdit FixedNumCtl {
            get { return this.FixedNum; }
        }
        #endregion Properties

        #region MemberVariables
        private static Size _dispOrderSize = new Size(40, 20);
        private static Size _timeUnitSize = new Size(80, 20);
        private static Size _visibleSize = new Size(30, 18);
        private static Size _colNameSize = new Size(220, 20);
        private static Size _noteSize = new Size(340, 20);
        private static Size _dbNameSize = new Size(160, 20);

        private ValueInterval _viZeroToIntMax = new ValueInterval(0, Int32.MaxValue, true, true);
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseColConfDialog() {
            InitializeComponent();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ccu"></param>
        /// <param name="grid"></param>
        public BaseColConfDialog(MultiLangUtil mlu, ColumnConfigUtil ccu, FlexGridEx grid) {
            MultiLangUtil = mlu;
            ColConfUtil = ccu;
            Grid = grid;

            _viZeroToIntMax.UseMinValue = true;
            _viZeroToIntMax.UseMaxValue = true;

            InitializeComponent();
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseColConfDialog_Load(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    this.ConfigPanel.SuspendLayout();
                    try {
                        IsApply = false;
                        LoadControl();
                    } finally {
                        this.ConfigPanel.ResumeLayout(false);
                    }
                }
            } finally {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 全選択ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllButton_Click(object sender, EventArgs e) {
            foreach (Control c in this.ConfigPanel.Controls) {
                if (Regex.IsMatch(c.Name, "Visible$")) {
                    if (c.Enabled) {
                        ((CheckBox)c).Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// 全解除ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseAllButton_Click(object sender, EventArgs e) {
            foreach (Control c in this.ConfigPanel.Controls) {
                if (Regex.IsMatch(c.Name, "Visible$")) {
                    if (c.Enabled) {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }
        }
        /// <summary>
        /// キャンセルボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            IsApply = false;
            this.Close();
        }
        /// <summary>
        /// 適用ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e) {
            Apply();
        }

        #endregion EventHandlers


        #region ProtectedMethods
        /// <summary>
        /// 新たに採番した表示順を設定
        /// </summary>
        protected void SetNewDispOrderToCCU() {
            DataTable sortTable = new DataTable();
            //列設定
            sortTable.Columns.Add(CommonConsts.db_name);
            sortTable.Columns.Add(CommonConsts.disp_order, Type.GetType("System.Double"));
            sortTable.Columns.Add("disp_order2", Type.GetType("System.Double"));

            int preOrder = 1;
            var query = from ci in ColConfUtil.ColConf.ColList
                        orderby ci.DisplayOrder
                        select ci;
            foreach (ColumnInfo ci in query) {
                DataRow dr = sortTable.NewRow();

                dr[CommonConsts.db_name] = ci.DBName;
                //順序
                Control ctl = this.ConfigPanel.Controls[ci.DBName + "DispOrder"];
                if (ctl != null) {
                    string val = ((C1NumericEdit)ctl).Value.ToString();
                    if (!ci.ConfEditable) {
                        dr[CommonConsts.disp_order] = double.MinValue;
                    } else  if (val == "") {
                        dr[CommonConsts.disp_order] = double.MaxValue;
                    } else {
                        dr[CommonConsts.disp_order] = double.Parse(val);
                    }
                }
                dr["disp_order2"] = preOrder;

                sortTable.Rows.Add(dr);
                preOrder++;
            }

            //ソートして採番
            DataRow[] rows = (
                from row in sortTable.AsEnumerable()
                let dispOrder = row.Field<double>(CommonConsts.disp_order)
                let dispOrder2 = row.Field<double>("disp_order2")
                orderby dispOrder, dispOrder2
                select row).ToArray();
            int i = 1;
            foreach (DataRow dr in rows) {
                ColumnInfo ci = ColConfUtil.DictionaryByDBName[dr[CommonConsts.db_name].ToString()];
                ci.DisplayOrder = i;
                i++;
            }
        }
        /// <summary>
        /// 更新パラメータ取得
        /// </summary>
        /// <returns></returns>
        protected DataSet GetParam() {
            DataSet ds = new DataSet(CommonConsts.ParamDataSet);
            ds.Tables.Add(ColConfUtil.GetDataTable());
            return ds;
        }

        /// <summary>
        /// 画面の設定値をColConfに反映
        /// </summary>
        protected void ApplyConfToCCU(int height, int fixedCount) {
            //高さ
            ColConfUtil.ColConf.Height = height;
            //列固定
            ColConfUtil.ColConf.LeftFixedCount = fixedCount;

            var query = from ci in ColConfUtil.ColConf.ColList
                        orderby ci.DisplayOrder
                        select ci;
            foreach (ColumnInfo ci in query) {
                //列幅
                Control ctl = this.ConfigPanel.Controls[ci.DBName + "Width"];
                if (ctl != null) {
                    ci.Width = int.Parse(StringUtil.NullBlankToZero(((C1NumericEdit)ctl).Value));
                }
                //表示
                ctl = this.ConfigPanel.Controls[ci.DBName + "Visible"];
                if (ctl != null) {
                    ci.Visible = ((CheckBox)ctl).Checked;
                }
            }
        }
        protected virtual void Apply() {
            //子クラスで実装
        }
        #endregion ProtectedMethods

        #region PrivateMethods
        /// <summary>
        /// コントロール配置
        /// </summary>
        private void LoadControl() {
            try {
                //高さ
                this.HeightNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                this.HeightNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                this.HeightNum.Value = ColConfUtil.ColConf.Height;
                this.HeightNum.PostValidation.Intervals.Add(_viZeroToIntMax);
                this.HeightNum.MaxLength = CommonConsts.C1NumericEditMaxLength;
                //列固定
                this.FixedNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                this.FixedNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                this.FixedNum.Value = ColConfUtil.ColConf.LeftFixedCount;
                this.FixedNum.PostValidation.Intervals.Add(_viZeroToIntMax);
                this.FixedNum.MaxLength = CommonConsts.C1NumericEditMaxLength;

                //パネルにコントロールを設定
                var query = from ci in ColConfUtil.ColConf.ColList
                            orderby ci.DisplayOrder
                            select ci;
                int itemY = 5;
                foreach (ColumnInfo ci in query) {
                    //順序
                    C1NumericEdit nedo = new C1NumericEdit();
                    this.ConfigPanel.Controls.Add(nedo);
                    nedo.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                    nedo.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                    nedo.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                    nedo.Location = new Point(DispOrderOffset, itemY);
                    nedo.Name = ci.DBName + "DispOrder";
                    nedo.Size = _dispOrderSize;
                    nedo.VisibleButtons = DropDownControlButtonFlags.None;
                    nedo.TextAlign = HorizontalAlignment.Right;
                    nedo.ShowFocusRectangle = true;
                    //HACK ※これを有効にすると、何故か編集時にExceptionが発生する。
                    //nedo.PostValidation.Intervals.Add(AppObject.Int32ValueInterval);
                    nedo.MaxLength = CommonConsts.C1NumericEditMaxLength;
                    if (ci.ConfEditable) {
                        nedo.Value = ci.DisplayOrder;
                    }

                    //表示
                    CheckBox visibleCheck = new CheckBox();
                    this.ConfigPanel.Controls.Add(visibleCheck);
                    visibleCheck.Location = new Point(VisibleOffset, itemY);
                    visibleCheck.Name = ci.DBName + "Visible";
                    visibleCheck.Size = _visibleSize;
                    visibleCheck.Checked = ci.Visible;
                    //列名
                    SetLabel(ColNameOffset, itemY, _colNameSize, ci.DBName + "Col", ci.ColName);
                    //列幅
                    C1NumericEdit widthNum = new C1NumericEdit();
                    this.ConfigPanel.Controls.Add(widthNum);
                    widthNum.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                    widthNum.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                    widthNum.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                    widthNum.Location = new Point(WidthOffset, itemY);
                    widthNum.Name = ci.DBName + "Width";
                    widthNum.Size = _dispOrderSize;
                    widthNum.VisibleButtons = DropDownControlButtonFlags.None;
                    widthNum.TextAlign = HorizontalAlignment.Right;
                    widthNum.FormatType = FormatTypeEnum.Integer;
                    widthNum.DataType = typeof(int);
                    widthNum.ShowFocusRectangle = true;
                    widthNum.Value = ci.Width;
                    widthNum.PostValidation.Intervals.Add(_viZeroToIntMax);
                    widthNum.CharCategory = CharCategory.Number;
                    widthNum.MaxLength = CommonConsts.C1NumericEditMaxLength;
                    //備考
                    SetLabel(NoteOffset, itemY, _noteSize, ci.DBName + "Note", ci.Note);
                    //物理名
                    SetLabel(DBNameOffset, itemY, _dbNameSize, ci.DBName + "DBName", ci.DBName);

                    if (!ci.ConfEditable) {
                        nedo.ReadOnly = true;
                        visibleCheck.Enabled = false;
                    }

                    itemY += RowOffset;
                }
            } finally {
            }
        }

        /// <summary>
        /// テキストラベルを配置
        /// </summary>
        /// <param name="itemX"></param>
        /// <param name="itemY"></param>
        /// <param name="s"></param>
        /// <param name="ctlName"></param>
        /// <param name="text"></param>
        private void SetLabel(int itemX, int itemY, Size s, string ctlName, string text) {
            TextBox tb = new TextBox();
            this.ConfigPanel.Controls.Add(tb);

            tb.Location = new Point(itemX, itemY);
            tb.Name = ctlName;
            tb.Size = s;
            tb.Text = text;
            tb.ReadOnly = true;
        }
        #endregion PrivateMethods
    }
}
