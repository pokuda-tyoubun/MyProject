using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Models;
using FxCommonLib.Models.MES;
using FxCommonLib.Utils;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// C1FlexGrid拡張
    /// </summary>
    public partial class FlexGridEx : C1FlexGrid {

        #region Constants
        private const string ColorStringRegex = @"^#[\dA-Z]{6}$";
        /// <summary>金額表示フォーマット</summary>
        private const string NumberFormatMoney = "C";
        /// <summary>CopyOrCutVisibleOnlyメソッドのモード</summary>
        protected enum CopyCutMode {
            Copy = 1,
            Cut
        }
        #endregion Constants

        #region Properties
        /// <summary>Window名</summary>
        public string WindowsName { get; set; }
        /// <summary>Grid名</summary>
        public string GridName { get; set; }
        /// <summary>Enter押下時に右列に移動する</summary>
        private bool _isEnterRight = false;
        public bool IsEnterRight {
            set { _isEnterRight = value; }
            get { return _isEnterRight; }
        }
        /// <summary>読取専用カラーを有効化</summary>
        private bool _enableReadOnlyColor = false;
        public bool EnableReadOnlyColor {
            set { _enableReadOnlyColor = value; }
            get { return _enableReadOnlyColor; }
        }
        /// <summary>セル編集時に更新済スタイルを適用</summary>
        private bool _enableUpdateCellStyle = true;
        public bool EnableUpdateCellStyle {
            set { _enableUpdateCellStyle = value; }
            get { return _enableUpdateCellStyle; }
        }
        /// <summary>処理前検証エラー情報</summary>
        protected Dictionary<string, GetErrorInfoEventArgs> _beforeErrorInfo = new Dictionary<string, GetErrorInfoEventArgs>();
        public Dictionary<string, GetErrorInfoEventArgs> BeforeErrorInfo {
            get { return _beforeErrorInfo; }
        }
        /// <summary>処理後検証エラー情報</summary>
        private Dictionary<string, string> _afterErrorInfo = new Dictionary<string, string>();
        public Dictionary<string, string> AfterErrorInfo {
            set { _afterErrorInfo = value; }
            get { return _afterErrorInfo; }
        }
        /// <summary>列１の編集を選択チェックとして利用するので、更新フラグを立てない</summary>
        private bool _isCol1SelectCheck = false;
        public bool IsCol1SelectCheck {
            set { _isCol1SelectCheck = value; }
            get { return _isCol1SelectCheck; }
        }
        /// <summary>プルダウンリスト候補辞書</summary>
        private Dictionary<string, Dictionary<string, string>> _pulldownDic = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> PulldownDic {
            set { _pulldownDic = value; }
            get { return _pulldownDic; }
        }
        /// <summary>セルボタン候補辞書</summary>
        private Dictionary<string, HashSet<string>> _cellButtonDic = new Dictionary<string, HashSet<string>>();
        /// <summary>セルボタン候補辞書</summary>
        public Dictionary<string, HashSet<string>> CellButtonDic {
            get { return _cellButtonDic; }
            set { _cellButtonDic = value; }
        }
        /// <summary>Ctrl+Cキーが押されたとき、選択された範囲をコピーするか</summary>
        private bool _allowCtrlC = true;
        [DefaultValue(true)]
        /// <summary>Ctrl+Cキーが押されたとき、選択された範囲をコピーするか</summary>
        public bool AllowCtrlC {
            get { return _allowCtrlC; }
            set { _allowCtrlC = value; }
        }
        /// <summary>Ctrl+Pキーが押されたとき、選択された範囲に貼り付けするか</summary>
        private bool _allowCtrlP = true;
        [DefaultValue(true)]
        /// <summary>Ctrl+Pキーが押されたとき、選択された範囲に貼り付けするか</summary>
        public bool AllowCtrlP {
            get { return _allowCtrlP; }
            set { _allowCtrlP = value; }
        }
        /// <summary>Ctrl+Pキーが押されたとき、選択された範囲に貼り付けするか</summary>
        private bool _allowCtrlX = true;
        [DefaultValue(true)]
        /// <summary>Ctrl+Xキーが押されたとき、選択された範囲を切り取りするか</summary>
        public bool AllowCtrlX {
            get { return _allowCtrlX; }
            set { _allowCtrlX = value; }
        }
        #endregion Properties

        #region MemberVariables
        /// <summary>カラーユーティリティ</summary>
        private ColorUtil _cu = new ColorUtil();
        /// <summary>追加時のスタイル</summary>
        private CellStyle _addCellStyle = null;
        /// <summary>更新時のスタイル</summary>
        protected CellStyle _updateCellStyle = null;
        /// <summary>削除時のスタイル</summary>
        private CellStyle _deleteCellStyle = null;
        public CellStyle DeleteCellStyle {
            get { return _deleteCellStyle; }
        }
        /// <summary>読取専用列のスタイル</summary>
        private CellStyle _readOnlyCellStyle = null;
        public CellStyle ReadOnlyCellStyle {
            get { return _readOnlyCellStyle; }
        }
        private Color _readOnlyCellColor = Color.Gainsboro;
        public Color ReadOnlyCellColor {
            get { return _readOnlyCellColor; }
        }
        /// <summary>PrimaryKey列のスタイル</summary>
        private CellStyle _pKeyCellStyle = null;
        /// <summary>PrimaryKey列ヘッダのスタイル</summary>
        private CellStyle _pKeyHeaderStyle = null;

        /// <summary>マイナス金額のセルスタイル</summary>
        private CellStyle _minusMoneyCellStyle = null;

        /// <summary>追加時に最左列に表示する文言</summary>
        private string _addDiv = "Add";
        public string AddDiv {
            get { return _addDiv; }
        }
        /// <summary>更新時に最左列に表示する文言</summary>
        protected string _updateDiv = "Up";
        public string UpdateDiv {
            get {return _updateDiv; }
        }
        /// <summary>削除時に最左列に表示する文言</summary>
        private string _deleteDiv = "Del";
        public string DeleteDiv {
            get { return _deleteDiv; }
        }
        /// <summary>必須項目に表示する文言</summary>
        private string _requiredItemMsg = "";
        /// <summary>パスワードフォーマットエラーで表示する文言</summary>
        private string _passFormatErrMsg = "";
        /// <summary>色文字列フォーマットエラーで表示する文言</summary>
        private string _colorFormatErrMsg = "";
        /// <summary>数値範囲エラーで表示する文言</summary>
        private string _outOfBoundErrMsg = "";

        /// <summary>グリッド内のコンテキストメニュー</summary>
        private ContextMenuStrip _cellContextMenu = new ContextMenuStrip();

        /// <summary>多国語対応</summary>
        private MultiLangUtil _mlu = null; 
        /// <summary>列定義</summary>
        protected ColumnConfigUtil _ccu = null; 
        /// <summary>日付の書式設定</summary>
        private string _dateFormat = ""; 
        /// <summary>日付の書式設定</summary>
        private string _dateFormatDM = ""; 
        /// <summary>日付の書式設定</summary>
        private string _dateFormatDO = ""; 
        /// <summary>DateTime列用コントロール</summary>
        private C1DateEdit _c1DateEdit = new C1DateEdit();
        /// <summary>0-double.MaxValueの範囲</summary>
        private ValueInterval _zeroToDoubleMax = new ValueInterval(0, double.MaxValue, true, true);
        /// <summary>コード名称補完用DataTable辞書</summary>
        private Dictionary<string, DataTable> _codeDataTableDic = new Dictionary<string, DataTable>();

        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FlexGridEx() {
            _zeroToDoubleMax.UseMinValue = true;
            _zeroToDoubleMax.UseMaxValue = true;

            InitializeComponent();

            if (!this.DesignMode) {
                InitSetting();
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FlexGridEx(IContainer container) {
            container.Add(this);
            InitializeComponent();

            if (!this.DesignMode) {
                InitSetting();
            }
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// ソート後
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSort(SortColEventArgs e) {
            //セルカラーを再設定
            SetCellColor();

            base.OnAfterSort(e);
        }
        /// <summary>
        /// キーダウンイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            //Ctrl+X　切り取り
            if ((e.Modifiers | Keys.Control) == Keys.Control && e.KeyCode == Keys.X) {
                e.Handled = true;
                if (AllowCtrlX) {
                    this.CutEx();
                }
            }
            //Ctrl+C　コピー
            if ((e.Modifiers | Keys.Control) == Keys.Control && e.KeyCode == Keys.C) {
                e.Handled = true;
                if (AllowCtrlC) {
                    this.CopyOrCutVisibleOnly(CopyCutMode.Copy);
                }
            }
            //Ctrl+V　貼り付け
            if ((e.Modifiers | Keys.Control) == Keys.Control && e.KeyCode == Keys.V) {
                e.Handled = true;
                if (AllowCtrlP) {
                    this.Paste();
                }
            }
            //DeleteKeyの実装
            if(e.KeyCode == Keys.Delete) {
                //非連結モードは元のDeleteの処理を実行する
                if(this.DataSource == null) {
                    base.OnKeyDown(e);
                    return;
                } else {
                    e.Handled = true;
                }

                //削除対象セルを探索する
                var delDic = new Dictionary<DataRowView, List<int>>();
                for (int r = this.Selection.TopRow; r <= this.Selection.BottomRow; r++) {
                    //行の編集可否をチェック
                    if(!this.Rows[r].AllowEditing) {
                        continue;
                    }
                    for (int c = this.Selection.LeftCol; c <= this.Selection.RightCol; c++) {
                        //列設定の編集可否をチェック
                        if (_ccu != null) {
                            ColumnInfo ci;
                            if(_ccu.DictionaryByName.TryGetValue(this.Cols[c].Name, out ci)) {
                                if(!ci.Editable) {
                                    continue;
                                }
                            }
                        }
                        //列の編集可否をチェック
                        if (!this.Cols[c].AllowEditing) {
                            continue;
                        }
                        //編集許可セルの値のみ削除
                        var drv = this.Rows[r].DataSource as DataRowView;
                        if (delDic.ContainsKey(drv)) {
                            delDic[drv].Add(c);
                        } else {
                            delDic.Add(drv, new List<int>() { c });
                        }
                    }
                }

                if(delDic.Count == 0) {
                    base.OnKeyDown(e);
                    return;
                }

                //DataViewのソートを無効化
                var dataView = delDic.Keys.First().DataView;
                var sort = dataView.Sort;
                dataView.Sort = string.Empty;

                //対象セルのデータを削除する
                var delRows = this.Rows
                                  .Cast<Row>()
                                  .Skip(this.Rows.Fixed)
                                  .Where(r => delDic.ContainsKey(r.DataSource as DataRowView));
                foreach(var row in delRows) {
                    var drv = row.DataSource as DataRowView;
                    foreach (var c in delDic[drv]) {
                        this.SetData(row.Index, c, DBNull.Value, false);
                    }
                }
                //DataViewのソートを元に戻す
                dataView.Sort = sort;
            }
            base.OnKeyDown(e);
        }
        /// <summary>
        /// 編集前処理イベント
        /// ・特定のCellRangeのみ編集可・不可を制御するために利用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeEdit(RowColEventArgs e) {
            //PrimaryKey項目であれば編集不可(新規追加の場合は許可)
            if (_ccu != null && this.Cols[e.Col].Name != "") {
                if (_ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
                    if (ci.IsPrimaryKey && StringUtil.NullToBlank(this[e.Row, 0]) != _addDiv) {
                        //編集不可
                        e.Cancel = true;
                        return;
                    }
                }
            }

            base.OnBeforeEdit(e);
        }
        /// <summary>
        /// セル値変更時のイベント
        /// ・「更新」レコードとしてスタイルを設定
        /// ・金額表示セルの文字色を設定
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellChanged(RowColEventArgs e) {
            base.OnCellChanged(e);

            if (this.AllowEditing == true && 
                this.Cols[e.Col].Name != CommonConsts.update_key &&
                this.EnableUpdateCellStyle == true) {
                if (e.Row > this.Rows.Fixed - 1 && e.Col > 0 && StringUtil.NullToBlank(this[e.Row, 0]) == "") {
                    if (_isCol1SelectCheck == true && e.Col == 1) {
                        //更新と見做さない
                    } else {
                        SetUpdateFlag(e.Row);
                    }
                }
            }

            //デザイン時のエラー回避のため、_colConfUtil != nullのチェックを行う。
            if (_ccu != null &&
                e.Col > 0 &&
                this.EnableUpdateCellStyle == true) {

                // 
                if (_ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
                    // 金額（正負）セルの文字色
                    if (ci.DataType == CommonConsts.GridDataTypeFM) {
                        SetCellForeColorForMoney(e.Row, e.Col);
                    }

                    // 金額（正、正負）セルの値
                    // NOTE:Deleteキーによって値が削除される場合があるため
                    if (ci.DataType == CommonConsts.GridDataTypeFM ||
                        ci.DataType == CommonConsts.GridDataTypeFPM) {
                        // 値がない場合は0に設定
                        this[e.Row, e.Col] = StringUtil.NullDBNullToZero(this[e.Row, e.Col]);


                    }

                }

            }

        }

        /// <summary>
        /// セルエラーの設定
        /// ・必須項目チェックに利用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGetCellErrorInfo(GetErrorInfoEventArgs e) {
            base.OnGetCellErrorInfo(e);

            //デザイン時のエラー回避のため、_colConfUtil != nullのチェックを行う。
            if (_ccu != null && this.AllowEditing == true && e.Col > 0) {
                if (_ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
                    //色文字列チェック
                    if (ci.DataType == CommonConsts.GridDataTypeCL) {
                        string key = e.Row.ToString() + ":" + e.Col.ToString();
                        if (StringUtil.NullToBlank(this[e.Row, e.Col]) != "" &&
                            Regex.IsMatch(StringUtil.NullToBlank(this[e.Row, e.Col]), ColorStringRegex) == false) {
                            e.ErrorText = _colorFormatErrMsg;
                            if (!this.BeforeErrorInfo.ContainsKey(key)) {
                                this.BeforeErrorInfo.Add(key, e);
                            }
                        } else {
                            //エラー情報から削除
                            if (_beforeErrorInfo.ContainsKey(key)) {
                                _beforeErrorInfo.Remove(key);
                            }
                        }
                        return;
                    }
                    //負数チェック
                    if (ci.DataType == CommonConsts.GridDataTypeFP
                        || ci.DataType == CommonConsts.GridDataTypeFPM
                        ) {
                        string key = e.Row.ToString() + ":" + e.Col.ToString();
                        double val = 0;
                        if (double.TryParse(StringUtil.NullToZero(this[e.Row, e.Col]), out val)) {
                            if (val < 0) {
                                e.ErrorText = _outOfBoundErrMsg;
                                if (!this.BeforeErrorInfo.ContainsKey(key)) {
                                    this.BeforeErrorInfo.Add(key, e);
                                }
                            } else {
                                //エラー情報から削除
                                if (_beforeErrorInfo.ContainsKey(key)) {
                                    _beforeErrorInfo.Remove(key);
                                }
                            }
                            return;
                        }
                    }
                    //金額値範囲チェック
                    if (ci.DataType == CommonConsts.GridDataTypeFM
                        || ci.DataType == CommonConsts.GridDataTypeFPM
                        ) {
                        string key = e.Row.ToString() + ":" + e.Col.ToString();
                        decimal val = 0;
                        if (decimal.TryParse(StringUtil.NullToZero(this[e.Row, e.Col]), out val)) {

                            //if (!MoneyUtil.InRange(val)) {
                            //    e.ErrorText = _outOfBoundErrMsg;
                            //    if (!this.BeforeErrorInfo.ContainsKey(key)) {
                            //        this.BeforeErrorInfo.Add(key, e);
                            //    }
                            //} else {
                            //    //エラー情報から削除
                            //    if (_beforeErrorInfo.ContainsKey(key)) {
                            //        _beforeErrorInfo.Remove(key);
                            //    }
                            //}
                            return;
                        }
                    }
                    //必須チェック
                    if (ci.Required && ci.Editable && StringUtil.NullToBlank(this[e.Row, 0]) != _deleteDiv) {
                        string key = e.Row.ToString() + ":" + e.Col.ToString();
                        if (StringUtil.NullToBlank(this[e.Row, e.Col]) == "") {
                            //必須エラーメッセージ表示
                            e.ErrorText = _requiredItemMsg;
                            if (!_beforeErrorInfo.ContainsKey(key)) {
                                _beforeErrorInfo.Add(key, e);
                            }
                        } else {
                            //エラー情報から削除
                            if (_beforeErrorInfo.ContainsKey(key)) {
                                _beforeErrorInfo.Remove(key);
                            }
                        }
                        return;
                    }
                    //パスワードチェック
                    if (ci.DataType == CommonConsts.GridDataTypeP) {
                        //半角英数チェック
                        string key = e.Row.ToString() + ":" + e.Col.ToString();
                        if (Regex.IsMatch(StringUtil.NullToBlank(this[e.Row, e.Col]), @"^[0-9a-zA-Z]*$") == false ||
                            StringUtil.NullToBlank(this[e.Row, e.Col]) == "") {
                            //パスワードフォーマットエラーメッセージ表示
                            e.ErrorText = _passFormatErrMsg;
                            if (!_beforeErrorInfo.ContainsKey(key)) {
                                _beforeErrorInfo.Add(key, e);
                            }
                        } else {
                            //エラー情報から削除
                            if (_beforeErrorInfo.ContainsKey(key)) {
                                _beforeErrorInfo.Remove(key);
                            }
                        }
                        return;
                    }

                }
            }
        }
        /// <summary>
        /// 行エラーの設定
        /// ・主キー重複エラーで利用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGetRowErrorInfo(GetErrorInfoEventArgs e) {
            base.OnGetRowErrorInfo(e);

            if (!this.DesignMode && _ccu != null && _afterErrorInfo != null) {
                if (_afterErrorInfo.Count > 0 && _ccu.DictionaryByDBName.ContainsKey(CommonConsts.update_key)) {
                    string key = this[e.Row, CommonConsts.update_key].ToString();
                    if (_afterErrorInfo.ContainsKey(key)) {
                        e.ErrorText = _afterErrorInfo[key];
                        //this.Select(this.GetCellRange(e.Row, e.Col), true);
                    }
                }
            }
        }
        /// <summary>
        /// KeyDownEditイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDownEdit(KeyEditEventArgs e) {
            if (_isEnterRight) {
                // Enterキーで次列へ移動
                if (e.KeyCode == Keys.Enter) {
                    if (e.Col > GetVisibleEndColIdx()) {
                        //何もしない
                    } else {
                        this.Select(this.GetCellRange(e.Row, e.Col + 1), true);
                    }
                }
            }
            base.OnKeyDownEdit(e);
        }

        //protected override void OnAfterEdit(RowColEventArgs e) {
        //    if (_ccu != null && this.Cols[e.Col].Name != "") {
        //        if (_ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
        //            ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
        //            //カレンダーで日付選択後、日付フォーマットがデフォルトに戻るので修正
        //            if (ci.DataType == CommonConsts.GridDataTypeDM) {
        //                string tmp = this[e.Row, e.Col].ToString();
        //                if (tmp != "") {
        //                    this[e.Row, e.Col] = DateTime.Parse(tmp).ToString(_mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN));
        //                }
        //            }
        //            if (ci.DataType == CommonConsts.GridDataTypeDO) {
        //                string tmp = this[e.Row, e.Col].ToString();
        //                if (tmp != "") {
        //                    this[e.Row, e.Col] = DateTime.Parse(tmp).ToString(_mlu.GetMsg(CommonConsts.DATE_FORMAT_YYYYMMDD));
        //                }
        //            }
        //        }
        //    }

        //    base.OnAfterEdit(e);
        //}

        /// <summary>
        /// ドラッグして列幅を変更した時のイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterResizeColumn(RowColEventArgs e) {
            if (_ccu != null && _ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
                ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
                ci.Width = this.Cols[e.Col].Width;
            }

            base.OnAfterResizeColumn(e);
        }
        /// <summary>
        /// ドラッグして列順を変更した時のイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeDragColumn(DragRowColEventArgs e) {
            ColumnInfo ci = null;

            //ドラッグ対象の列が列設定変更不可であれば無効とする。
            ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
            if (!ci.ConfEditable) {
                e.Cancel = true;
                return;
            }

            //列設定変更不可列より左の移動は無効とする。
            ci = _ccu.DictionaryByName[this.Cols[e.Position].Name];
            if (!ci.ConfEditable) {
                e.Cancel = true;
                return;
            }

            //列順再設定
            double newDispOrder = 0;
            if (e.Position > e.Col) {
                //右へ移動
                newDispOrder = ci.DisplayOrder + 0.5d;
            } else {
                //左へ移動
                newDispOrder = ci.DisplayOrder - 0.5d;
            }
            SetNewDispOrderToCCU(this.Cols[e.Col].Name, newDispOrder);

            base.OnBeforeDragColumn(e);
        }
        /// <summary>
        /// 行ヘッダリサイズ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterResizeRow(RowColEventArgs e) {
            if (_ccu != null && e.Row < this.Rows.Fixed) {
                _ccu.ColConf.Height = this.Rows[e.Row].Height;
            }
            base.OnAfterResizeRow(e);
        }
        /// <summary>
        /// マウスダウンイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                int rowIdx = this.HitTest(e.X, e.Y).Row;
                int colIdx = this.HitTest(e.X, e.Y).Column;
                if (!this.Selection.Contains(rowIdx, colIdx)) {
                    //選択セル範囲外であれば、選択処理を行う。
                    this.Select(rowIdx, colIdx);
                }
            }
            base.OnMouseDown(e);
        }
        /// <summary>
        /// セルボタンクリック
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellButtonClick(RowColEventArgs e) {
            if (_ccu != null && _ccu.DictionaryByName.ContainsKey(this.Cols[e.Col].Name)) {
                ColumnInfo ci = _ccu.DictionaryByName[this.Cols[e.Col].Name];
                if (ci.DataType == CommonConsts.GridDataTypeDM || ci.DataType == CommonConsts.GridDataTypeDO) {
                    Rectangle rc = this.GetCellRect(e.Row, e.Col);
                    DateEditDialog ded = null;
                    if (ci.DataType == CommonConsts.GridDataTypeDM) {
                        ded = new DateEditDialog(_dateFormatDM);
                    } else {
                        ded = new DateEditDialog(_dateFormatDO);
                    }

                    int orgHeight = ded.Height;
                    int orgWidth = ded.Width;
                    Point p = this.PointToScreen(new Point(rc.Location.X, rc.Location.Y - (orgHeight / 2)));
                    ded.Location = p;
                    //ウィンドウサイズが変わるので再調整
                    ded.Height = orgHeight + SystemInformation.CaptionHeight + (SystemInformation.FrameBorderSize.Height * 2);
                    ded.Width = orgWidth + (SystemInformation.FrameBorderSize.Width * 2);
                    ded.ShowDialog();
                    if (ded.SelectedDate != "") {
                        this[e.Row, e.Col] = ded.SelectedDate;
                    }
                }
                //色指定型
                if (ci.DataType == CommonConsts.GridDataTypeCL) {
                    ColorDialog colorDlg = new ColorDialog();
                    // ダイアログを初期化します。
                    string colorString = StringUtil.NullToBlank(this[e.Row, e.Col]).Trim();
                    if (colorString != "" && Regex.IsMatch(colorString, ColorStringRegex)) {
                        colorDlg.Color = _cu.GetColor(colorString);
                    }
                    // ダイアログから新しい色を取得し、それをセルに割り当てます。
                    if (colorDlg.ShowDialog() == DialogResult.OK) {
                        this[e.Row, e.Col] = _cu.GetColorString(colorDlg.Color);
                        SetCellColorBySelf(e.Row, e.Col);
                    }
                }
            }
            base.OnCellButtonClick(e);
        }
        [Browsable(true)]
        [Description("コード名称補完セル編集時コード存在OKイベント")]
        [Category("")]
        public event CodeTextBoxEx.OnCheckOKHandler CellValidateCheckOK;
        protected virtual void OnCellValidateCheckOK(object sender, EventArgs e, DataTable dt) {
            CodeTextBoxEx.OnCheckOKHandler eventHandler = CellValidateCheckOK;
            if (eventHandler != null) {
                eventHandler(sender, e, dt);
            }
        }
        [Browsable(true)]
        [Description("コード名称補完セル編集時コード存在NGイベント")]
        [Category("")]
        public event CodeTextBoxEx.OnCheckNGHandler CellValidateCheckNG;
        protected virtual void OnCellValidateCheckNG() {
            CodeTextBoxEx.OnCheckNGHandler eventHandler = CellValidateCheckNG;
            if (eventHandler != null) {
                eventHandler();
            }
        }
        #endregion EventHandlers

        #region PublicMethods
        /// <summary>
        /// 列インデックスを取得
        /// </summary>
        /// <param name="ccu"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public int GetColIndex(ColumnConfigUtil ccu, string dbName) {
            if (ccu.DictionaryByDBName.ContainsKey(dbName)) {
                return this.Cols[ccu.DictionaryByDBName[dbName].ColName].Index;
            } else {
                return this.Cols[dbName].Index;
            }
        }
        /// <summary>
        /// 追加行としてフラグを立てる。
        /// </summary>
        /// <param name="row"></param>
        public void SetAddFlag(int row) {
            this[row, 0] = _addDiv;
            Row r = this.Rows[row];
            r.Style = _addCellStyle;
            this.Invalidate();
        }
        /// <summary>
        /// 更新済み行としてフラグを立てる。
        /// </summary>
        /// <param name="row"></param>
        public void SetUpdateFlag(int row) {
            this[row, 0] = _updateDiv;
            Row r = this.Rows[row];
            r.Style = _updateCellStyle;
            this.Invalidate();
        }
        /// <summary>
        /// 自分の高さ・幅を行列の高さ・幅に合わせて自動調整する。
        /// </summary>
        public void AutoOuterSize() {
            AutoOuterSize(-1, -1);
        }
        /// <summary>
        /// 自分の高さ・幅を行列の高さ・幅に合わせて自動調整する。
        /// </summary>
        public void AutoOuterSize(int maxHeight, int maxWidth, int heightMargine, int widthMargine) {
            //高さ
            AutoOuterHeight(maxHeight, heightMargine);
            //幅
            AutoOuterWidth(maxWidth, widthMargine);
        }
        /// <summary>
        /// 自分の高さ・幅を行列の高さ・幅に合わせて自動調整する。
        /// </summary>
        public void AutoOuterSize(int maxHeight, int maxWidth) {
            //高さ
            AutoOuterHeight(maxHeight);
            //幅
            AutoOuterWidth(maxWidth);

        }
        /// <summary>
        /// 自分の高さを全行高さに合わせて自動調整する。
        /// </summary>
        /// <param name="maxWidth"></param>
        public void AutoOuterHeight(int maxHeight, int margine = 0) {
            int height = 0;
            foreach (Row r in this.Rows) {
                if (r.Visible == true) {
                    if (r.Height < 0) {
                        //デフォルトサイズ
                        height += this.Rows.DefaultSize;
                    } else {
                        height += r.Height;
                    }
                }
            }
            this.Height = height + 4 + margine;
            if (maxHeight >= 0 && this.Height > maxHeight) {
                this.Height = maxHeight + margine;
            }
        }
        /// <summary>
        /// 自分の幅を全列幅に合わせて自動調整する。
        /// </summary>
        /// <param name="maxWidth"></param>
        public void AutoOuterWidth(int maxWidth, int margine = 0) {
            //幅
            int width = 0;
            foreach (Column c in this.Cols) {
                if (c.Visible == true) {
                    if (c.Width < 0) {
                        //デフォルトサイズ
                        width += this.Cols.DefaultSize;
                    } else {
                        width += c.Width;
                    }
                }
            }
            this.Width = width + 4 + margine;
            if (maxWidth >= 0 && this.Width > maxWidth) {
                this.Width = maxWidth + margine;
            }
        }
        /// <summary>
        /// 日付セルの表示文字列を再フォーマット
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        public void ReformatDateCell(int row, string colName) {
            ReformatDateCell(row, this.Cols[colName].Index);
        }
        /// <summary>
        /// 日付セルの表示文字列を再フォーマット
        /// ※OnAfterEdit, OnTextChangeなどのイベントで修正しようとしたが、
        /// "grid[row, col] = value"のように直接設定するとイベントが発火しないので、
        /// 利用側でこのメソッドを呼んでもらうように暫定対応した。
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void ReformatDateCell(int row, int col) {
            if (_ccu != null && this.Cols[col].Name != "") {
                if (_ccu.DictionaryByName.ContainsKey(this.Cols[col].Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[this.Cols[col].Name];
                    //カレンダーで日付選択後、日付フォーマットがデフォルトに戻るので修正
                    if (ci.DataType == CommonConsts.GridDataTypeDM) {
                        string tmp = this[row, col].ToString();
                        if (tmp != "") {
                            this[row, col] = DateTime.Parse(tmp).ToString(_mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN));
                        }
                    }
                    if (ci.DataType == CommonConsts.GridDataTypeDO) {
                        string tmp = this[row, col].ToString();
                        if (tmp != "") {
                            this[row, col] = DateTime.Parse(tmp).ToString(_mlu.GetMsg(CommonConsts.DATE_FORMAT_YYYYMMDD));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 切り取り
        /// </summary>
        public void CutEx() {
            if (this.Selection.TopRow < this.Rows.Fixed) {
                //ヘッダー部は切り取らない。
                SystemSounds.Beep.Play();
                return;
            }
            if (this.Selection.LeftCol < this.Cols.Fixed) {
                //列ヘッダー部は切り取らない。
                SystemSounds.Beep.Play();
                return;
            }
            this.CopyOrCutVisibleOnly(CopyCutMode.Cut);
        }

        /// <summary>
        /// コピー
        /// </summary>
        public void CopyEx() {
            this.CopyOrCutVisibleOnly(CopyCutMode.Copy);
        }
        /// <summary>
        /// カーソルセルの値をコピー
        /// </summary>
        public void CopyCursorCellValue() {
            if (this.CursorCell.TopRow >= 0) {
                string val = StringUtil.NullToBlank(this[this.CursorCell.TopRow, this.CursorCell.LeftCol]);
                Clipboard.SetDataObject(val);
            }
        }
        /// <summary>
        /// ペースト
        /// </summary>
        public void PasteEx() {
            this.Paste();
        }

        /// <summary>
        /// 新規行を追加
        /// </summary>
        public void AddItem() {
            Row r = this.Rows.Add();
            //更新区分セット
            this[r.Index, 0] = _addDiv;
            r.Style = _addCellStyle;

            this.Invalidate();
        }
        /// <summary>
        /// 任意の数だけ新規行を追加
        /// </summary>
        /// <param name="count"></param>
        public void AddItem(int count) {
            for (int i = 0; i < count; i++ ) {
                AddItem();
            }
        }

        /// <summary>
        /// 削除マークを付ける
        /// </summary>
        public void DeleteItem() {
            int topRow = this.Selection.TopRow;
            int bottomRow = this.Selection.BottomRow;
            for (int i = topRow; i <= bottomRow; i++) {
                if (i > this.Rows.Fixed - 1) {
                    if (StringUtil.NullToBlank(this[i, 0]) == _addDiv) {
                        //新規追加行であれば、レコードごと削除
                        this.Rows.Remove(this.Rows[i]);
                        bottomRow--;
                        i--;
                    } else {
                        this[i, 0] = _deleteDiv;
                        Row r = this.Rows[i];
                        r.Style = _deleteCellStyle;
                    }
                    //削除行の事前エラー情報を削除
                    //List<string> keysList = new List<string>(_beforeErrorInfo.Keys);
                    //foreach (string key in keysList) {
                    //    if (key.Contains(i.ToString() + ":")) {
                    //        _beforeErrorInfo.Remove(key);
                    //    }
                    //}
                }
            }
            this.Invalidate();
        }


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="mlu"></param>
        /// <param name="ccu"></param>
        public void Init(MultiLangUtil mlu, ColumnConfigUtil ccu) {
            //多国後対応（メッセージを保持しておく）
            _mlu = mlu; 
            _addDiv = _mlu.GetMsg(CommonConsts.ROW_ADD);
            _updateDiv = _mlu.GetMsg(CommonConsts.ROW_UPDATE);
            _deleteDiv = _mlu.GetMsg(CommonConsts.ROW_DELETE);
            _requiredItemMsg = _mlu.GetMsg(CommonConsts.MSG_ANY_REQUIRED_ITEM);
            _passFormatErrMsg = string.Format(_mlu.GetMsg(CommonConsts.MSG_FORMAT_AL_NUM), MESConsts.password);
            _colorFormatErrMsg = _mlu.GetMsg(CommonConsts.MSG_INVALID_COLOR_FORMAT);
            _outOfBoundErrMsg = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
            _dateFormat = _mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_SEC);
            _dateFormatDM = _mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN);
            _dateFormatDO = _mlu.GetMsg(CommonConsts.DATE_FORMAT_YYYYMMDD);

            _ccu = ccu;
            this.Rows.Count = this.Rows.Fixed;
        }

        /// <summary>
        /// 列定義情報に従ってスタイルを設定
        /// </summary>
        /// <param name="ccu"></param>
        public void SetStyle() {
            if (this.Rows.Count == this.Rows.Fixed) {
                SetHeaderOnly();
            }
            //列設定
            int i = 1;
            foreach (Column c in this.Cols) {
                if (c.Name == "") {
                    continue;
                }
                if (_ccu.DictionaryByName.ContainsKey(c.Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[c.Name];
                    c.WidthDisplay = ci.Width;
                    c.Visible = ci.Visible;
                    c.AllowEditing = ci.Editable;

                    if (ci.IsPrimaryKey) {
                        c.Style = _pKeyCellStyle;
                        this.SetCellStyle(0, c.Index, _pKeyHeaderStyle);
                    }

                    //DataType---------------------------------
                    if (ci.DataType == CommonConsts.GridDataTypeD) {
                        //日付タイプ(年月日時分秒)
                        c.DataType = typeof(DateTime);
                        c.Editor = _c1DateEdit;
                        c.Format = _dateFormat;
                    } else if (ci.DataType == CommonConsts.GridDataTypeDM) {
                        //日付タイプ(年月日時分)
                        //※表示時にフォーマット変換するためtypeof(string)として扱う必要がある。
                        c.ComboList = "...";
                    } else if (ci.DataType == CommonConsts.GridDataTypeDO) {
                        //日付タイプ(年月日)
                        //※表示時にフォーマット変換するためtypeof(string)として扱う必要がある。
                        c.ComboList = "...";
                    } else if (ci.DataType == CommonConsts.GridDataTypeS) {
                        //文字列タイプ
                        c.DataType = typeof(string);
                        TextBoxEx tbe = new TextBoxEx(ci.MaxLength);
                        c.Editor = tbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeP) {
                        //パスワードタイプ
                        c.DataType = typeof(string);
                        TextBoxEx tbe = new TextBoxEx(ci.MaxLength);
                        if (StringUtil.NullToBlank(ci.PasswordChar) != "") {
                            tbe.PasswordChar = ci.PasswordChar[0];
                        }
                        tbe.ImeMode = ImeMode.Disable;
                        c.Editor = tbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeI) {
                        //整数タイプ
                        c.DataType = typeof(int);
                    } else if (ci.DataType == CommonConsts.GridDataTypeF) {
                        //実数タイプ
                        C1NumericEdit ne = new C1NumericEdit();
                        ne.DataType = typeof(double);
                        ne.MaxLength = CommonConsts.C1NumericEditMaxLength;
                        ne.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                        ne.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                        ne.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                        ne.VisibleButtons = DropDownControlButtonFlags.None;
                        c.DataType = typeof(double);
                        c.Editor = ne;
                    } else if (ci.DataType == CommonConsts.GridDataTypeFP) {
                        //正の実数タイプ
                        C1NumericEdit ne = new C1NumericEdit();
                        ne.DataType = typeof(double);
                        ne.MaxLength = CommonConsts.C1NumericEditMaxLength;
                        ne.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                        ne.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                        ne.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                        ne.VisibleButtons = DropDownControlButtonFlags.None;
                        //ne.PostValidation.Intervals.Add(_zeroToDoubleMax);
                        c.DataType = typeof(double);
                        c.Editor = ne;
                    } else if (ci.DataType == CommonConsts.GridDataTypeFM) {
                        //実数タイプ(通貨金額表示）
                        C1NumericEdit ne = new C1NumericEdit();
                        ne.DataType = typeof(double);
                        ne.MaxLength = CommonConsts.C1NumericEditMaxLength + 5; // -\,,,
                        ne.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                        ne.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                        ne.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                        ne.VisibleButtons = DropDownControlButtonFlags.None;
                        c.DataType = typeof(double);
                        c.Editor = ne;
                        c.Format = NumberFormatMoney;
                    } else if (ci.DataType == CommonConsts.GridDataTypeFPM) {
                        //正の実数タイプ（通貨金額表示）
                        C1NumericEdit ne = new C1NumericEdit();
                        ne.DataType = typeof(double);
                        ne.MaxLength = CommonConsts.C1NumericEditMaxLength + 5; // -\,,,
                        ne.ErrorInfo.ErrorAction = ErrorActionEnum.ResetValue;
                        ne.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
                        ne.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
                        ne.VisibleButtons = DropDownControlButtonFlags.None;
                        //ne.PostValidation.Intervals.Add(_zeroToDoubleMax);
                        c.DataType = typeof(double);
                        c.Editor = ne;
                        c.Format = NumberFormatMoney;
                    } else if (ci.DataType == CommonConsts.GridDataTypeB) {
                        //ビットタイプ(チェックボックス)
                        c.DataType = typeof(bool);
                    } else if (ci.DataType == CommonConsts.GridDataTypeCB) {
                        //セルボタン
                        c.ComboList = "...";
                    } else if (ci.DataType == CommonConsts.GridDataTypeCL) {
                        //色指定
                        c.ComboList = "...";
                    } else if (ci.DataType == CommonConsts.GridDataTypeL) {
                        //プルダウンリスト
                        if (ci.IsPrimaryKey) {
                            //プルダウンだと常に変更可になるので、セルボタンに強制する。
                            c.ComboList = "...";
                        } else {
                            c.Editor = null;
                            c.DataMap = _pulldownDic[ci.DBName];
                        }
                    } else if (ci.DataType == CommonConsts.GridDataTypeSCU) {
                        // 得意先コード

                        CodeTextBoxEx ctbe = new CodeTextBoxEx();
                        if (!this._codeDataTableDic.ContainsKey(MESConsts.customer_cd)) {
                            throw new ArgumentNullException();
                        }
                        ctbe.LoadControl();

                        ctbe.keyCode = MESConsts.customer_cd;
                        ctbe.CodeTable = this._codeDataTableDic[MESConsts.customer_cd];
                        ctbe.MaxLength = ci.MaxLength;
                        ctbe.OnCheckOK += OnCellValidateCheckOK;
                        ctbe.OnCheckNG += OnCellValidateCheckNG;
                        
                        c.DataType = typeof(string);
                        c.Editor = ctbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeSSP) {
                        // 仕入先コード

                        CodeTextBoxEx ctbe = new CodeTextBoxEx();
                        if (!this._codeDataTableDic.ContainsKey(MESConsts.supplier_cd)) {
                            throw new ArgumentNullException();
                        }

                        ctbe.LoadControl();

                        ctbe.keyCode = MESConsts.supplier_cd;
                        ctbe.CodeTable = this._codeDataTableDic[MESConsts.supplier_cd];
                        ctbe.MaxLength = ci.MaxLength;
                        ctbe.OnCheckOK += OnCellValidateCheckOK;
                        ctbe.OnCheckNG += OnCellValidateCheckNG;

                        c.DataType = typeof(string);
                        c.Editor = ctbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeSZS) {
                        // 材料仕入先コード

                        CodeTextBoxEx ctbe = new CodeTextBoxEx();
                        if (!this._codeDataTableDic.ContainsKey(MESConsts.SupplierRelationMATERIAL + MESConsts.supplier_cd)) {
                            throw new ArgumentNullException();
                        }

                        ctbe.LoadControl();

                        ctbe.keyCode = MESConsts.supplier_cd;
                        ctbe.CodeTable = this._codeDataTableDic[MESConsts.SupplierRelationMATERIAL + MESConsts.supplier_cd];
                        ctbe.MaxLength = ci.MaxLength;
                        ctbe.OnCheckOK += OnCellValidateCheckOK;
                        ctbe.OnCheckNG += OnCellValidateCheckNG;

                        c.DataType = typeof(string);
                        c.Editor = ctbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeSSS) {
                        // 表面処理仕入先コード

                        CodeTextBoxEx ctbe = new CodeTextBoxEx();
                        if (!this._codeDataTableDic.ContainsKey(MESConsts.SupplierRelationSURFACE + MESConsts.supplier_cd)) {
                            throw new ArgumentNullException();
                        }

                        ctbe.LoadControl();

                        ctbe.keyCode = MESConsts.supplier_cd;
                        ctbe.CodeTable = this._codeDataTableDic[MESConsts.SupplierRelationSURFACE + MESConsts.supplier_cd];
                        ctbe.MaxLength = ci.MaxLength;
                        ctbe.OnCheckOK += OnCellValidateCheckOK;
                        ctbe.OnCheckNG += OnCellValidateCheckNG;

                        c.DataType = typeof(string);
                        c.Editor = ctbe;
                    } else if (ci.DataType == CommonConsts.GridDataTypeSSR) {
                        // 表面処理コード

                        CodeTextBoxEx ctbe = new CodeTextBoxEx();
                        if (!this._codeDataTableDic.ContainsKey(MESConsts.surface_cd)) {
                            throw new ArgumentNullException();
                        }

                        ctbe.LoadControl();

                        ctbe.keyCode = MESConsts.surface_cd;
                        ctbe.CodeTable = this._codeDataTableDic[MESConsts.surface_cd];
                        ctbe.MaxLength = ci.MaxLength;
                        ctbe.OnCheckOK += OnCellValidateCheckOK;
                        ctbe.OnCheckNG += OnCellValidateCheckNG;

                        c.DataType = typeof(string);
                        c.Editor = ctbe;
                    } else {
                        //
                    }

                    //Style------------------------------------
                    if (!c.AllowEditing && _enableReadOnlyColor) {
                        if (ci.DataType == CommonConsts.GridDataTypeB) {
                            //typeof(bool)にセルスタイルを適用すると、チェックボックスではなく文字列表示になってしまう。
                            c.Style.BackColor = _readOnlyCellColor;
                        } else {
                            c.Style = _readOnlyCellStyle;
                        }
                    }
                } else {
                    //グリッド設定がない列は非表示かつ編集不可
                    c.Visible = false;
                    c.AllowEditing = false;
                }

                i++;
            }

            //列表示順を設定
            SetColPosition();

            //高さ
            this.Rows[0].Height = _ccu.ColConf.Height;
            this.Rows[0].StyleNew.WordWrap = true;
            //列固定数
            this.Cols.Frozen = _ccu.ColConf.LeftFixedCount;

            this.ShowButtons = ShowButtonsEnum.Always;
        }

        /// <summary>
        /// 追加・更新・削除対象レコードのテーブルを作成
        /// </summary>
        /// <param name="ut"></param>
        /// <returns></returns>
        public DataTable GetUpdateTable(CommonConsts.UpdateType ut) {
            DataTable ret = new DataTable();

            ret.TableName = ut.ToString();

            string div = "default";
            if (ut == CommonConsts.UpdateType.Add) {
                div = _mlu.GetMsg(CommonConsts.ROW_ADD);
            } else if (ut == CommonConsts.UpdateType.Update) {
                div = _mlu.GetMsg(CommonConsts.ROW_UPDATE);
            } else if (ut == CommonConsts.UpdateType.Delete) {
                div = _mlu.GetMsg(CommonConsts.ROW_DELETE);
            }
            //ヘッダーの設定(物理名に変換)
            foreach (Column c in this.Cols) {
                if (c.Index > 0 && _ccu.DictionaryByName.ContainsKey(c.Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[c.Name];
                    ret.Columns.Add(ci.DBName, Type.GetType("System.String"));
                }
            }
            //データの設定
            foreach (Row r in this.Rows) {
                if ((string)(this[r.Index, 0]) == div) {
                    DataRow dr = ret.NewRow();
                    foreach (Column c in this.Cols) {
                        if (c.Index > 0 && _ccu.DictionaryByName.ContainsKey(c.Name)) {
                            ColumnInfo ci = _ccu.DictionaryByName[c.Name];
                            dr[ci.DBName] = StringUtil.NullToBlank(this[r.Index, c.Index]);
                        }
                    }
                    ret.Rows.Add(dr);
                }
            }
            ret.AcceptChanges();

            return ret;
        }


        /// <summary>
        /// ノードレベルを設定する。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="treeCol"></param>
        public void SetNodeLevel(DataTable dt, int treeCol) {
            this.Tree.Column = treeCol;
            int i = this.Rows.Fixed;
            int adjust = 0;
            foreach (DataRow dr in dt.Rows) {
                if (i == this.Rows.Fixed) {
                    adjust = int.Parse(dr[CommonConsts.indent_level].ToString());
                }
                this.Rows[i].IsNode = true;
                this.Rows[i].Node.Level = int.Parse(dr[CommonConsts.indent_level].ToString()) - adjust;
                i++;
            }
        }

        /// <summary>
        /// ツリー情報を表示する。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="treeColIdx"></param>
        public void BindTreeData(DataTable dt, int treeColIdx) {
            ConvertDateFormatDMDO(dt);
            DataTable newTbl = _ccu.CopyAsDisplayData(dt);

            this.Tree.Column = treeColIdx;
            SetHeaderOnly();

            int rowIdx = this.Rows.Fixed;
            int minLevel = 0;
            this.Rows.Count = newTbl.Rows.Count + this.Rows.Fixed;
            foreach (DataRow dr in newTbl.Rows) {
                int colIdx = 1;
                if (rowIdx == this.Rows.Fixed) {
                    minLevel = int.Parse(dr[CommonConsts.indent_level].ToString());
                }
                foreach (ColumnInfo ci in _ccu.ColConf.ColList) {
                    this[rowIdx, colIdx] = dr[ci.ColName];
                    colIdx++;
                }
                this.Rows[rowIdx].IsNode = true;
                this.Rows[rowIdx].Node.Level = int.Parse(dr[CommonConsts.indent_level].ToString()) - minLevel;
                rowIdx++;
            }

            this.SetStyle();
        }
        /// <summary>
        /// 言語変換してデータグリッドにBind
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="bind"></param>
        /// <param name="navi"></param>
        public void BindData(DataTable dt, BindingSource bind, BindingNavigator navi) {
            //エラー情報のクリア
            this.BeforeErrorInfo.Clear();

            ConvertDateFormatDMDO(dt);

            DataTable newTbl = _ccu.CopyAsDisplayData(dt);
            if (bind == null) {
                this.DataSource = newTbl;
            } else {
                bind.DataSource = newTbl;
                navi.BindingSource = bind;
                this.DataSource = bind;
            }

            //セルの色を設定
            SetCellColor();
            this.SetStyle();
        }

        /// <summary>
        /// 色文字列に合わせてセルの背景色をセット
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetCellColorBySelf(int row, int col) {
            CellRange cr = this.GetCellRange(row, col);
            string colorString = StringUtil.NullToBlank(this[row, col]).Trim();
            if (colorString != "" && Regex.IsMatch(colorString, ColorStringRegex)) {
                cr.StyleNew.BackColor = _cu.GetColor(this[row, col].ToString());
                cr.Style.ForeColor = _cu.GetXorColor(cr.Style.BackColor);
            }
        }


        /// <summary>
        /// 言語変換してデータグリッドにBind
        /// </summary>
        /// <param name="dt"></param>
        public void BindData(DataTable dt) {
            BindData(dt, null, null);
        }

        /// <summary>
        /// セミコロン区切りの複数文字列を取得する。
        /// </summary>
        /// <param name="keyCol"></param>
        /// <param name="valCol"></param>
        /// <param name="exceptStr"></param>
        /// <returns></returns>
        public string GetMultiString(int keyCol, int valCol, string exceptStr = null) {
            StringBuilder sb = new StringBuilder();
            for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                if (i != this.Rows.Fixed) {
                    sb.Append(";");
                }
                string key = this[i, keyCol].ToString();
                string val = this[i, valCol].ToString();

                if (exceptStr != null && exceptStr == key) {
                    key = "";
                }
                if (key == "") {
                    sb.Append(val);
                } else {
                    sb.Append(val);
                    sb.Append("/");
                    sb.Append(key);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 削除色を設定する。
        /// </summary>
        public void SetDeleteColor() {
            if (_ccu != null && _ccu.DictionaryByDBName.ContainsKey(CommonConsts.delete_flg)) {
                ColumnInfo ci = _ccu.DictionaryByDBName[CommonConsts.delete_flg];
                for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                    string val = this[i, ci.ColName].ToString();
                    if (val == MESConsts.DeleteFlagDeleted) {
                        this.Rows[i].Style = _deleteCellStyle;
                    }
                }
            }
        }

        /// <summary>
        /// 全ノードを展開する。
        /// </summary>
        public void UnCollapseAllNode() {
            foreach (Row r in this.Rows.Cast<Row>().Where(c => c.IsNode == true)) {
                r.Node.Collapsed = false;
            }
        }
        /// <summary>
        /// 全ノードを折り畳む
        /// </summary>
        public void CollapseAllNode() {
            foreach (Row r in this.Rows.Cast<Row>().Where(c => c.IsNode == true)) {
                r.Node.Collapsed = true;
            }
        }


        /// <summary>
        /// 選択列を非表示にする。
        /// </summary>
        public void HideSelectCol() {
            string selectColName = this.Cols[this.Selection.LeftCol].Name;
            if (_ccu.DictionaryByName.ContainsKey(selectColName)) {
                ColumnInfo ci = _ccu.DictionaryByName[selectColName];
                if (ci.ConfEditable) {
                    ci.Visible = false;
                    this.SetStyle();
                }
            }
        }

        /// <summary>
        /// グリッドの背景色を設定
        /// </summary>
        public void SetOpeBackColor(Color nonProcColor, Color inProcColor, Color finishColor, Color todayColor) {
            DateTime now = DateTime.Now;

            for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                string workStatusCd = this[i, MESConsts.work_status_cd].ToString();
                if (workStatusCd == MESConsts.WorkStatusNPRC) {
                    //未仕掛
                    this.Rows[i].StyleNew.BackColor = nonProcColor;
                    //本日作業判定
                    DateTime fromDate;
                    DateTime toDate;
                    bool fromParse = DateTime.TryParse(this[i, _ccu.DictionaryByDBName[MESConsts.ope_start].ColName].ToString(), out fromDate);
                    bool toParse = DateTime.TryParse(this[i, _ccu.DictionaryByDBName[MESConsts.ope_end].ColName].ToString(), out toDate);
                    if (fromParse && toParse) {
                        fromDate = DateTime.Parse(fromDate.ToString("yyyy/MM/dd") + " 00:00:00.00");
                        toDate = DateTime.Parse(toDate.ToString("yyyy/MM/dd") + " 23:59:59.99");
                        if (fromDate <= now && now <= toDate) {
                            this.Rows[i].StyleNew.BackColor = todayColor;
                        }
                    }
                    continue;
                } else if (workStatusCd == MESConsts.WorkStatusINPRC) {
                    //仕掛中
                    this.Rows[i].StyleNew.BackColor = inProcColor;
                    continue;
                } else if (workStatusCd == MESConsts.WorkStatusFIN) {
                    //完了
                    this.Rows[i].StyleNew.BackColor = finishColor;
                    continue;
                }
            }
        }
        /// <summary>
        /// グリッドの背景色を設定(検索ハイライト用)
        /// </summary>
        public void SetFTSHighlightBackColor(string strColor) {
            String keywordhitColDBName = "";
            int keywordhitColIndex = 0;

                foreach (Column c in this.Cols) {

                    if (c.Name == "") { continue;}
                    if (_ccu.DictionaryByName.ContainsKey(c.Name) == false) { continue; }
                    if (c.Visible == false) { continue;  }

                    keywordhitColDBName = _ccu.DictionaryByName[c.Name].DBName + MESConsts.KeywordMatched;
                    if (Cols.Contains(keywordhitColDBName) == false) {
                        continue;
                    }
                    keywordhitColIndex = Cols[keywordhitColDBName].Index;

                    for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {

                        if (this.Rows[i].Visible == false) { continue; }

                        // hit
                        if (this[i, keywordhitColDBName].ToString() == "True") {
                            CellRange cr = this.GetCellRange(i, c.Index);
                            cr.StyleNew.BackColor = _cu.GetColor(strColor);
                            //    cr.Style.ForeColor = _cu.GetXorColor(cr.Style.BackColor);
                        }

                    }
                }

        }
        /// <summary>
        /// 作業指示グリッドの作業着手状況アイコンを設定
        /// </summary>
        public void SetProgressIcon(Image green, string statusPRE,
                                    Image blue,  string statusS,
                                    Image yellow, string statusSDLY,
                                    Image red, string statusFDLY,
                                    Image grey, string statusFIN) {
            int progressColIdx = this.Cols[_ccu.DictionaryByDBName[MESConsts.progress_status].ColName].Index;
            for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                string progressStatusName = this[i, progressColIdx].ToString();

                if (progressStatusName == statusPRE) {
                    //開始前
                    this.SetCellImage(i, progressColIdx, green);
                    continue;
                } else if (progressStatusName == statusS) {
                    //着手済
                    this.SetCellImage(i, progressColIdx, blue);
                    continue;
                } else if (progressStatusName == statusSDLY) {
                    //着手遅れ
                    this.SetCellImage(i, progressColIdx, yellow);
                    continue;
                } else if (progressStatusName == statusFDLY) {
                    //完了遅れ
                    this.SetCellImage(i, progressColIdx, red);
                    continue;
                } else if (progressStatusName == statusFIN) {
                    //完了済み
                    this.SetCellImage(i, progressColIdx, grey);
                    continue;
                }
            }
        }
        /// <summary>
        /// Pasteメソッドの独自実装
        /// ・非表示列、行に対して貼り付けを行わない
        /// ・入力値チェック、最大長チェックを行う
        /// </summary>
        public new void Paste() {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject == null) {
                return;
            }
            if (dataObject.GetDataPresent(DataFormats.Text)) {
                if (!this.AllowEditing) {
                    return;
                }
                if (this.Selection.TopRow < this.Rows.Fixed) {
                    //行ヘッダー部はペーストしない。
                    SystemSounds.Beep.Play();
                    return;
                }
                if (this.Selection.LeftCol < this.Cols.Fixed) {
                    //列ヘッダー部はペーストしない。
                    SystemSounds.Beep.Play();
                    return;
                }
                if (_ccu == null) {
                    base.Paste();
                    return;
                }

                string value = dataObject.GetData(DataFormats.Text) as string;
                //末尾の改行を削除
                value = Regex.Replace(value, @"[\r\n]+$", "");
                string[][] values = value.Split(new string[] { "\r\n" }, StringSplitOptions.None)
                                         .Select(r => r.Split(new char[] { '\t' }))
                                         .ToArray();

                int valuesRowCount = values.Length;
                int valuesColCount = values[0].Length;
                List<Row> targetRows = GetPastingRows(valuesRowCount, valuesColCount);
                List<Column> targetCols = GetPastingCols(valuesRowCount, valuesColCount);

                //1つの値を複数セルに貼り付けするために値配列を再生成する
                if (valuesRowCount == 1 && valuesColCount == 1) {
                    values = targetRows.Select(r => targetCols.Select(c => values[0][0]).ToArray())
                                       .ToArray();
                }

                DataTable convertTable;
                bool couldConvart = TryConvartPasteValues(values, targetRows, targetCols, out convertTable);
                if (!couldConvart) {
                    MessageBox.Show(_mlu.GetMsg(CommonConsts.MSG_PASTE_MISSMATCH_ERROR), 
                        _mlu.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int rIdx = 0; rIdx < targetRows.Count; rIdx++) {
                    Row row = targetRows[rIdx];
                    if (!row.AllowEditing) {
                        continue;
                    }
                    for (int cIdx = 0; cIdx < targetCols.Count; cIdx++) {
                        Column col = targetCols[cIdx];
                        if(!col.AllowEditing) {
                            continue;
                        }
                        row[col.Name] = convertTable.Rows[rIdx][cIdx];
                    }
                }
            } else {
                base.Paste();
            }
        }
        /// <summary>
        /// 現在の選択をクリップボードにコピーします。
        /// ※基本的にCopyExを使用すること！！！
        /// </summary>
        public new void Copy() {
            base.Copy();
        }
        /// <summary>
        /// 現在選択されているコンテンツをクリップボードにコピーします。
        /// ※基本的にCutExを使用すること！！！
        /// </summary>
        public new void Cut() {
            base.Cut();
        }
        /// <summary>
        /// コード名称補完用の得意先マスタを登録
        /// </summary>
        /// <param name="dt"></param>
        public void SetCustomerCdTable(DataTable dt) {
            SetCodeAssistTable(MESConsts.customer_cd, dt);
        }
        /// <summary>
        /// コード名称補完用の仕入先マスタを登録
        /// </summary>
        /// <param name="dt"></param>
        public void SetSupplierCdTable(DataTable dt) {
            SetCodeAssistTable(MESConsts.supplier_cd, dt);
        }
        /// <summary>
        /// コード名称補完用の(材料)仕入先マスタを登録
        /// </summary>
        /// <param name="dt"></param>
        public void SetMaterialSupplierCdTable(DataTable dt) {
            SetCodeAssistTable(MESConsts.SupplierRelationMATERIAL + MESConsts.supplier_cd, dt);
        }
        /// <summary>
        /// コード名称補完用の(表面処理依頼先)仕入先マスタを登録
        /// </summary>
        /// <param name="dt"></param>
        public void SetSurfaceSupplierCdTable(DataTable dt) {
            SetCodeAssistTable(MESConsts.SupplierRelationSURFACE + MESConsts.supplier_cd, dt);
        }
        /// <summary>
        /// コード名称補完用の表面処理マスタを登録
        /// </summary>
        /// <param name="dt"></param>
        public void SetSurfaceCdTable(DataTable dt) {
            SetCodeAssistTable(MESConsts.surface_cd, dt);
        }
        /// <summary>
        /// 指定した範囲の事前エラーチェックを行う
        /// </summary>
        /// <param name="cr"></param>
        public void CheckBeforeError(CellRange cr) {
            for (int row = cr.TopRow; row <= cr.BottomRow; row++) {
                for (int col = cr.LeftCol; col <= cr.RightCol; col++) {
                    //一旦、エラー情報から削除
                    string key = row.ToString() + ":" + col.ToString();
                    _beforeErrorInfo.Remove(key);

                    this.OnGetCellErrorInfo(new GetErrorInfoEventArgs(row, col));
                }
            }
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// 色文字列に合わせて全てのセルの文字、背景色をセット
        /// </summary>
        private void SetCellColor() {
            foreach (Column c in this.Cols) {
                if (c.Name == "") {
                    continue;
                }
                if (_ccu != null && _ccu.DictionaryByName.ContainsKey(c.Name)) {
                    ColumnInfo ci = _ccu.DictionaryByName[c.Name];
                    if (ci.DataType == CommonConsts.GridDataTypeCL) {
                        for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                            SetCellColorBySelf(i, c.Index);
                        }
                    } else if (ci.DataType == CommonConsts.GridDataTypeFM) {
                        for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                            SetCellForeColorForMoney(i, c.Index);
                        }
                    }
                }

            }

        }
        /// <summary>
        /// 金額表示セルの文字色をセット
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        private void SetCellForeColorForMoney(int rowIdx, int colIdx) {

            decimal val = 0;
            if (decimal.TryParse(StringUtil.NullToZero(this[rowIdx, colIdx]), out val)) {
                CellRange c = GetCellRange(rowIdx, colIdx);
                if (val < 0) {

                    c.StyleNew.ForeColor = _minusMoneyCellStyle.ForeColor;
                    
                } else {

                    c.StyleNew.ForeColor = Styles.Normal.ForeColor;
                }
            }
        }

        /// <summary>
        /// 行追加後は、グリッドの特定領域を無効にします。
        /// (このことにより、"Added"スタイルがすぐに表示されます)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceRowChanged(object sender, DataRowChangeEventArgs e) {
            if(e.Action == DataRowAction.Add) {
                this.Invalidate();
            }
        }

        /// <summary>
        /// 切り取り
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cut_ItemClicked(object sender, EventArgs e) {
            this.CutEx();
        }
        /// <summary>
        /// 初期化
        /// </summary>
        private void InitSetting() {
            // --------------------------------------------------------------
            //ユーザの追加は許可しない
            this.AllowAddNew = false;
            // --------------------------------------------------------------
            //クリップボード記録を有効にする。
            this.AutoClipboard = true;
            // --------------------------------------------------------------
            // 日付編集コントロールの設定
            _c1DateEdit.FormatType = FormatTypeEnum.CustomFormat;
            _c1DateEdit.CustomFormat = _dateFormat;
            _c1DateEdit.ShowFocusRectangle = true;
            _c1DateEdit.EmptyAsNull = true;
            // --------------------------------------------------------------
            // カスタムスタイルを作成。
            _addCellStyle = this.Styles.Add("Add");
            _addCellStyle.BackColor = Color.MistyRose;
            _addCellStyle.Font = new Font(this.Font, FontStyle.Bold);

            _deleteCellStyle = this.Styles.Add("Delete");
            _deleteCellStyle.BackColor = Color.Gray;

            _updateCellStyle = this.Styles.Add("Modify");
            _updateCellStyle.BackColor = Color.LightGoldenrodYellow;

            _readOnlyCellStyle = this.Styles.Add("ReadOnly");
            _readOnlyCellStyle.BackColor = _readOnlyCellColor;

            _pKeyCellStyle = this.Styles.Add("PrimaryKey");
            _pKeyCellStyle.ForeColor = Color.Black;

            _pKeyHeaderStyle = this.Styles.Add("PrimaryKeyHeader");
            _pKeyHeaderStyle.Font = new Font(this.Font, FontStyle.Bold);

            _minusMoneyCellStyle = this.Styles.Add("MinusMoney");
            _minusMoneyCellStyle.ForeColor = Color.Red;

            // --------------------------------------------------------------
            // OwnerDrawCellイベントを発生させます。
            this.DrawMode = DrawModeEnum.OwnerDraw;

        }
        /// <summary>
        /// 表示順を再設定
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="newDispOrder"></param>
        private void SetNewDispOrderToCCU(string colName, double newDispOrder) {
            DataTable sortTable = new DataTable();
            //列設定
            sortTable.Columns.Add(CommonConsts.col_name);
            sortTable.Columns.Add(CommonConsts.disp_order, Type.GetType("System.Double"));

            var query = from ci in _ccu.ColConf.ColList
                        orderby ci.DisplayOrder
                        select ci;
            foreach (ColumnInfo ci in query) {
                DataRow dr = sortTable.NewRow();

                dr[CommonConsts.col_name] = ci.ColName;
                if (ci.ColName == colName) {
                    dr[CommonConsts.disp_order] = newDispOrder;
                } else {
                    dr[CommonConsts.disp_order] = double.Parse(ci.DisplayOrder.ToString());
                }

                sortTable.Rows.Add(dr);
            }

            //ソートして採番
            DataRow[] rows = (
                from row in sortTable.AsEnumerable()
                let dispOrder = row.Field<double>(CommonConsts.disp_order)
                orderby dispOrder
                select row).ToArray();
            int i = 1;
            foreach (DataRow dr in rows) {
                ColumnInfo ci = _ccu.DictionaryByName[dr[CommonConsts.col_name].ToString()];
                ci.DisplayOrder = i;
                i++;
            }
        }
        /// <summary>
        /// GridDataTypeDM列の値を変換
        /// </summary>
        /// <param name="dt"></param>
        private void ConvertDateFormatDMDO(DataTable dt) {
            string formatDM = _mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN);
            string formatDO = _mlu.GetMsg(CommonConsts.DATE_FORMAT_YYYYMMDD);
            foreach (DataColumn dc in dt.Columns) {
                if (_ccu.DictionaryByDBName.ContainsKey(dc.ColumnName)) { 
                    ColumnInfo ci = _ccu.DictionaryByDBName[dc.ColumnName];
                    if (ci.DataType == CommonConsts.GridDataTypeDM) {
                        //日付フォーマットを変換
                        //※バインドモードでは、DataTable側のDataTypeをDateTimeに設定する必要がある。
                        //  ただ、DateTimeだとnull設定できない(DateTime?も設定できない）ので文字列として変換するように対応した。
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            string val = dt.Rows[i][dc.ColumnName].ToString();
                            if (val != "") {
                                dt.Rows[i][dc.ColumnName] = DateTime.Parse(val).ToString(formatDM);
                            }
                        }
                    } 
                    if (ci.DataType == CommonConsts.GridDataTypeDO) {
                        //日付フォーマットを変換
                        //※バインドモードでは、DataTable側のDataTypeをDateTimeに設定する必要がある。
                        //  ただ、DateTimeだとnull設定できない(DateTime?も設定できない）ので文字列として変換するように対応した。
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            string val = dt.Rows[i][dc.ColumnName].ToString();
                            if (val != "") {
                                dt.Rows[i][dc.ColumnName] = DateTime.Parse(val).ToString(formatDO);
                            }
                        }
                    } 
                }
            }
        }
        /// <summary>
        /// コピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Copy_ItemClicked(object sender, EventArgs e) {
            this.CopyOrCutVisibleOnly(CopyCutMode.Copy);
        }
        /// <summary>
        /// コピー(非表示列はコピーしない)
        /// </summary>
        private void CopyOrCutVisibleOnly(CopyCutMode copyCutMode) {
            if(this.DataSource == null) {
                if (copyCutMode == CopyCutMode.Copy) {
                    base.Copy();
                } else {
                    base.Cut();
                }
                return;
            }
            var delDic = new Dictionary<DataRowView, List<int>>();
            if (this.SelectionMode == SelectionModeEnum.ListBox) {
                StringBuilder sb = new StringBuilder("");
                foreach (Row r in this.Rows.Selected) {
                    if (r.Visible == true) {
                        bool isFirstCol = true;
                        foreach (Column c in this.Cols) {
                            if (c.Visible == true && c.Index >= this.Cols.Fixed) {
                                if(isFirstCol == false) {
                                    sb.Append("\t");
                                }
                                isFirstCol = false;
                                if (c.DataMap != null) {
                                    Dictionary<string, string> dic = (Dictionary<string, string>)c.DataMap;
                                    if (!Convert.IsDBNull(this[r.Index, c.Index])) {
                                        sb.Append(StringUtil.NullToBlank(dic[this[r.Index, c.Index].ToString()]));
                                    }
                                } else {
                                    sb.Append(StringUtil.NullToBlank(this[r.Index, c.Index]));
                                }
                                if(copyCutMode == CopyCutMode.Cut && c.AllowEditing) {
                                    var drv = r.DataSource as DataRowView;
                                    if (delDic.ContainsKey(drv)) {
                                        delDic[drv].Add(c.Index);
                                    } else {
                                        delDic.Add(drv, new List<int>() { c.Index });
                                    }
                                }
                            }
                        }
                        sb.Append("\r\n");
                    }
                    // クリップボードに設定
                    Clipboard.SetDataObject(sb.ToString());
                }
            } else {
                // 選択されているセル範囲のCellRangeオブジェクトを取得
                CellRange cr = this.Selection;
                if (cr.r1 >= this.Rows.Fixed && cr.c1 >= this.Cols.Fixed) {
                    // 選択されているセル範囲から
                    // VisibleプロパティがFalseになっている行や列の値を除いて
                    // タブ記号や改行コードを追加した文字列にします
                    StringBuilder sb = new StringBuilder("");
                    for (int i = cr.r1; i <= cr.r2; i++) {
                        if (this.Rows[i].Visible == true) {
                            for (int j = cr.c1; j <= cr.c2; j++) {
                                if (this.Cols[j].Visible == true) {
                                    Column col = this.Cols[j];
                                    if (col.DataMap != null) {
                                        Dictionary<string, string> dic = (Dictionary<string, string>)col.DataMap;
                                        if (!Convert.IsDBNull(this[i, j]) && this[i, j] != null) {
                                            sb.Append(StringUtil.NullToBlank(dic[this[i, j].ToString()]));
                                        }
                                    } else {
                                        sb.Append(StringUtil.NullToBlank(this[i, j]));
                                    }
                                    if(j != cr.c2) {
                                        sb.Append("\t");
                                    }
                                    if (copyCutMode == CopyCutMode.Cut && col.AllowEditing) {
                                        var drv = this.Rows[i].DataSource as DataRowView;
                                        if (delDic.ContainsKey(drv)) {
                                            delDic[drv].Add(col.Index);
                                        } else {
                                            delDic.Add(drv, new List<int>() { col.Index });
                                        }
                                    }
                                }
                            }
                            sb.Append("\r\n");
                        }
                    }
                    // クリップボードに設定
                    Clipboard.SetDataObject(sb.ToString());
                }
            }

            if (copyCutMode == CopyCutMode.Copy || delDic.Count == 0) {
                return;
            }

            //DataViewのソートを無効化
            var dataView = delDic.Keys.First().DataView;
            var sort = dataView.Sort;
            dataView.Sort = string.Empty;

            //対象セルのデータを削除する
            var delRows = this.Rows
                              .Cast<Row>()
                              .Skip(this.Rows.Fixed)
                              .Where(r => delDic.ContainsKey(r.DataSource as DataRowView));
            foreach (var row in delRows) {
                var drv = row.DataSource as DataRowView;
                foreach (var c in delDic[drv]) {
                    this.SetData(row.Index, c, DBNull.Value, false);
                }
            }
            //DataViewのソートを元に戻す
            dataView.Sort = sort;
        }
        /// <summary>
        /// ペースト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paste_ItemClicked(object sender, EventArgs e) {
            this.Paste();
        }
        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_ItemClicked(object sender, EventArgs e) {
            AddItem();
        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_ItemClicked(object sender, EventArgs e) {
            DeleteItem();
        }
        /// <summary>
        /// 表示設定されている列の最終Indexを取得
        /// </summary>
        /// <returns></returns>
        private int GetVisibleEndColIdx() {
            int ret = 0;
            foreach (Column c in this.Cols) {
                if (c.Visible) {
                    ret = c.Index;
                }
            }
            return ret;
        }
        /// <summary>
        /// 列表示順を設定
        /// </summary>
        /// <param name="ccu"></param>
        private void SetColPosition() {
            var query = from ci in _ccu.ColConf.ColList
                        orderby ci.DisplayOrder
                        select ci; 
            foreach (var ci in query) {
                this.Cols[ci.ColName].Move(ci.DisplayOrder);
            } 
        }
        /// <summary>
        /// 列定義情報に従ってヘッダーのみ表示
        /// </summary>
        private void SetHeaderOnly() {

            //コンテキストメニュー設定
            if (_cellContextMenu.Items.Count == 0) {
                _cellContextMenu.Items.Add(_mlu.GetMsg(CommonConsts.CMD_CUT), null, new System.EventHandler(Cut_ItemClicked));
                _cellContextMenu.Items.Add(_mlu.GetMsg(CommonConsts.CMD_COPY), null, new System.EventHandler(Copy_ItemClicked));
                _cellContextMenu.Items.Add(_mlu.GetMsg(CommonConsts.CMD_PASTE), null, new System.EventHandler(Paste_ItemClicked));
                _cellContextMenu.Items.Add("-");
                _cellContextMenu.Items.Add(_mlu.GetMsg(CommonConsts.CMD_ADD), null, new System.EventHandler(Add_ItemClicked));
                _cellContextMenu.Items.Add(_mlu.GetMsg(CommonConsts.CMD_DELETE), null, new System.EventHandler(Delete_ItemClicked));
            }

            this.Rows.Count = this.Rows.Fixed;
            this.Cols.Count = _ccu.ColConf.ColList.Count + 1;
            int i = 1;
            foreach (ColumnInfo ci in _ccu.ColConf.ColList) {
                this[0, i] = ci.ColName;
                this.Cols[i].Name = ci.ColName;
                i++;
            }

            if (this.ContextMenuStrip == null) {
                //デフォルトコンテキストメニューを表示
                this.ContextMenuStrip = _cellContextMenu;
            }
        }

        /// <summary>
        /// グループ版グリッド背景色設定
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="color3"></param>
        /// <param name="color4"></param>
        public void SetGreBackColor(Color nonProcColor, Color inProcColor, Color finishColor) {
            DateTime now = DateTime.Now;

            for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
                string workStatusCd = this[i, _ccu.DictionaryByDBName[MESConsts.group_status].ColName].ToString();
                
                if (workStatusCd == MESConsts.GroupWorkStatusNPRC) {
                    //未仕掛
                    this.Rows[i].StyleNew.BackColor = nonProcColor;
                    continue;
                } else if (workStatusCd == MESConsts.GroupWorkStatusINPRC) {
                    //仕掛中
                    this.Rows[i].StyleNew.BackColor = inProcColor;
                    continue;
                } else if (workStatusCd == MESConsts.GroupWorkStatusFIN) {
                    //完了
                    this.Rows[i].StyleNew.BackColor = finishColor;
                    continue;
                }
            }
        }
        /// <summary>
        /// 貼り付け対象列の取得
        /// </summary>
        /// <param name="valueRowCount"></param>
        /// <param name="valueColCount"></param>
        /// <returns></returns>
        private List<Column> GetPastingCols(int valueRowCount, int valueColCount) {
            int leftCol = this.Selection.LeftCol;
            int rightCol = this.Selection.RightCol;
            if (valueRowCount > 1 || valueColCount > 1) {
                return this.Cols
                           .Cast<Column>()
                           .Skip(leftCol)
                           .Where(c => c.IsVisible)
                           .Take(valueColCount)
                           .ToList();
            } else {
                int selectColCount = rightCol - leftCol + 1;
                return this.Cols
                           .Cast<Column>()
                           .Skip(leftCol)
                           .Take(selectColCount)
                           .Where(c => c.IsVisible)
                           .ToList();
            }
        }

        /// <summary>
        /// 貼り付け対象行の取得
        /// </summary>
        /// <param name="valueRowCount"></param>
        /// <param name="valueColCount"></param>
        /// <returns></returns>
        private List<Row> GetPastingRows(int valueRowCount, int valueColCount) {
            int topRow = this.Selection.TopRow;
            int bottomRow = this.Selection.BottomRow;
            if (valueRowCount > 1 || valueColCount > 1) {
                return this.Rows
                           .Cast<Row>()
                           .Skip(topRow)
                           .Where(r => r.IsVisible)
                           .Take(valueRowCount)
                           .ToList();
            } else {
                int selectRowCout = bottomRow - topRow + 1;
                return this.Rows
                           .Cast<Row>()
                           .Skip(topRow)
                           .Take(selectRowCout)
                           .Where(r => r.IsVisible)
                           .ToList();
            }
        }

        /// <summary>
        /// 貼り付け用DataTableに変換する
        /// </summary>
        /// <param name="values"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        private bool TryConvartPasteValues(string[][] values, List<Row> rows, List<Column> columns, out DataTable ret) {

            ret = new DataTable();
            foreach (var col in columns) {
                //カラムにDataTypeが設定されていない場合はObject型とする
                ret.Columns.Add(col.Name, col.DataType ?? typeof(object));
            }

            for (int rIdx = 0; rIdx < rows.Count; rIdx++) {
                Row row = rows[rIdx];
                DataRow newRow = ret.NewRow();
                ret.Rows.Add(newRow);
                if (!row.AllowEditing) {
                    continue;
                }
                for (int cIdx = 0; cIdx < columns.Count; cIdx++) {
                    Column col = columns[cIdx];

                    if (!col.AllowEditing) {
                        continue;
                    }

                    string valueStr = values[rIdx][cIdx];
                    //NOTE:クリップボードのブランク文字はDBNull.Valueとして扱う。
                    if (string.IsNullOrEmpty(valueStr)) {
                        newRow[cIdx] = DBNull.Value;
                        continue;
                    }

                    ColumnInfo ci = _ccu.DictionaryByName[col.Name];
                    //値の検証と変換を行う
                    switch (ci.DataType) {
                        case CommonConsts.GridDataTypeS: //文字型
                        case CommonConsts.GridDataTypeP: //パスワードタイプ
                            if (valueStr.Length > ci.MaxLength) {
                                valueStr = valueStr.Substring(0, ci.MaxLength);
                            }
                            newRow[cIdx] = valueStr;
                            break;
                        case CommonConsts.GridDataTypeI: //整数型
                            int intValue;
                            if (!int.TryParse(valueStr, out intValue)) {
                                return false;
                            }
                            newRow[cIdx] = intValue;
                            break;
                        case CommonConsts.GridDataTypeD: //日付型(年月日時分秒)
                        case CommonConsts.GridDataTypeDM: //日付型(年月日時分)
                        case CommonConsts.GridDataTypeDO: //日付型(年月日)
                            DateTime dateTime;
                            if (!DateTime.TryParse(valueStr, out dateTime)) {
                                return false;
                            }
                            if (ci.DataType == CommonConsts.GridDataTypeD) {
                                newRow[cIdx] = dateTime;
                            } else if (ci.DataType == CommonConsts.GridDataTypeDM) {
                                newRow[cIdx] = dateTime.ToString(_dateFormatDM);
                            } else if (ci.DataType == CommonConsts.GridDataTypeDO) {
                                newRow[cIdx] = dateTime.ToString(_dateFormatDO);
                            }
                            break;
                        case CommonConsts.GridDataTypeF: //浮動小数型
                        case CommonConsts.GridDataTypeFP: //浮動小数型(正の値のみ)
                        case CommonConsts.GridDataTypeFM: //浮動小数型(金額表示）
                        case CommonConsts.GridDataTypeFPM: //浮動小数型(正の値のみ金額表示)
                            if (valueStr.Length > CommonConsts.C1NumericEditMaxLength) {
                                valueStr = valueStr.Substring(0, CommonConsts.C1NumericEditMaxLength);
                            }
                            double doubleValue;
                            if (!double.TryParse(valueStr, out doubleValue)) {
                                return false;
                            }
                            newRow[cIdx] = doubleValue;
                            break;
                        case CommonConsts.GridDataTypeB: //論理型
                            bool boolValue;
                            if (!bool.TryParse(valueStr, out boolValue)) {
                                return false;
                            }
                            newRow[cIdx] = boolValue;
                            break;
                        case CommonConsts.GridDataTypeCB: //セルボタン型
                            HashSet<string> hashSet;
                            if (!CellButtonDic.TryGetValue(ci.DBName, out hashSet)) {
                                //見つからない場合は検証しない
                                newRow[cIdx] = valueStr;
                                break;
                            }
                            if (!hashSet.Contains(valueStr)) {
                                return false;
                            }
                            newRow[cIdx] = valueStr;
                            break;
                        case CommonConsts.GridDataTypeL: //プルダウンリスト型
                            var keyValuePair = PulldownDic[ci.DBName].FirstOrDefault(kvp => kvp.Value == valueStr);
                            if (keyValuePair.Equals(default(KeyValuePair<string, string>))) {
                                return false;
                            }
                            newRow[cIdx] = keyValuePair.Key;
                            break;
                        case CommonConsts.GridDataTypeCL: //色指定型
                            if (!Regex.IsMatch(valueStr, ColorStringRegex)) {
                                return false;
                            }
                            newRow[cIdx] = valueStr;
                            break;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// コード名称補完用のDataTableを登録
        /// </summary>
        /// <param name="keyString"></param>
        /// <param name="dt"></param>
        private void SetCodeAssistTable(string keyString, DataTable dt) {
            if (this._codeDataTableDic.ContainsKey(keyString)) {
                this._codeDataTableDic.Remove(keyString);
            }
            this._codeDataTableDic.Add(keyString, dt);
        }
        #endregion PrivateMethods

        //public void SetProgressIcon(Image blue, Image yellow, Image red, string statusNDLY, string statusDLY, string statusFDLY) {
        //    int progressColIdx = this.Cols[_ccu.DictionaryByDBName[Consts.progress_status].ColName].Index;
        //    for (int i = this.Rows.Fixed; i < this.Rows.Count; i++) {
        //        string progressStatusName = this[i, progressColIdx].ToString();

        //        if (progressStatusName == statusNDLY) {
        //            //遅れなし
        //            this.SetCellImage(i, progressColIdx, blue);
        //            continue;
        //        } else if (progressStatusName == statusDLY) {
        //            //進捗遅れ
        //            this.SetCellImage(i, progressColIdx, yellow);
        //            continue;
        //        } else if (progressStatusName == statusFDLY) {
        //            //完了遅れ
        //            this.SetCellImage(i, progressColIdx, red);
        //            continue;
        //        }
        //    }
        //}
    }
}
