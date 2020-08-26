using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    public class FxGanttView : C1FlexGrid {

        #region Constants
        /// <summary>列インデックス</summary>
        private enum ColIndex : int {
            WorkDiv = 1,
            Operation,
            ResourcesName,
            P0H
        }
        /// <summary>固定行インデックス</summary>
        private const int FixedRowIdx = 2;
        /// <summary>固定列インデックス</summary>
        private const int FrozenColumnIdx = (int)ColIndex.ResourcesName;
        /// <summary>時間列幅</summary>
        private const int HourColumnWidth = 40;
        /// <summary>分列幅</summary>
        private const double MinWidth = (double)HourColumnWidth / 60d;
        #endregion Constants

        #region Properties
        /// <summary>表示日</summary>
        public DateTime DisplayDate;
        #endregion Properties

        #region MemberVariables
        /// <summary>時間領域クリック時のスクロールポジション</summary>
        private Point? _currentPoint = null;
        /// <summary>時間の区切り線のペン</summary>
        private Pen _stripePen = new Pen(Color.Gainsboro, 1);
        /// <summary>バー選択時のペン</summary>
        private Pen _selectPen = new Pen(Color.Red, 3);
        /// <summary>バー文字のフォント</summary>
        private Font _barFont = new Font("MS UI Gothic", 8);
        /// <summary>ヘッダスタイル</summary>
        private CellStyle _headerCellStyle = null;
        /// <summary>データディクショナリ</summary>
        private Dictionary<string, string> _dataDic = null;
        /// <summary>多国言語ユーティリティ</summary>
        private MultiLangUtil _mlu = null;
        /// <summary>ガントバーリスト</summary>
        private List<FxGanttRow> _barList = null;
        /// <summary>作業情報ツールチップ</summary>
        private C1SuperTooltip GanttBarToolTip;
        private System.ComponentModel.IContainer components;
        private C1SuperTooltip ResourcesToolTip;
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FxGanttView() {
            InitializeComponent();

            //オーナードローイベントを発生させる
            this.DrawMode = DrawModeEnum.OwnerDraw;
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// セルのオーナードロー
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOwnerDrawCell(OwnerDrawCellEventArgs e) {
            base.OnOwnerDrawCell(e);

            if (_barList == null) {
                return;
            }

            if (FixedRowIdx <= e.Row && e.Row < this.Rows.Count && e.Col == FrozenColumnIdx + 1) {
                e.DrawCell(C1.Win.C1FlexGrid.DrawCellFlags.Background | C1.Win.C1FlexGrid.DrawCellFlags.Border);
                //ストライプ描画
                DrawHourStripe(this.ScrollPosition.X, e.Bounds, e.Graphics);
                //バー描画
                DrawBar(this.ScrollPosition.X, e.Bounds, e.Graphics, _barList[e.Row - FixedRowIdx].BarList);
            }
        }

        /// <summary>
        /// スクロール前
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeScroll(RangeEventArgs e) {
            if (_currentPoint != null) {
                this.ScrollPosition = (Point)_currentPoint;
                _currentPoint = null;
            }
            base.OnBeforeScroll(e);
        }
        /// <summary>
        /// マウスクリック
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e) {
            int colIdx = this.HitTest(e.X, e.Y).Column;
            if (colIdx >= (int)ColIndex.P0H) {
                //時間領域をクリックされた場合、P0Hに水平スクロールされるのを防ぐ
                _currentPoint = this.ScrollPosition;
            } else {
                _currentPoint = null;
            }

            base.OnMouseClick(e);
        }
        /// <summary>
        /// マウスムーブ
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (_barList == null) {
                return;
            }

            //ツールチップ表示処理
            int r = this.MouseRow;
            int c = this.MouseCol;
            if (r > 1 && c >= FrozenColumnIdx + 1) {
                foreach (FxGanttBar fgb in _barList[r - FixedRowIdx].BarList) {
                    int canvasSX = TimeToCanvasX(fgb.DetailStartTime, DisplayDate);
                    int canvasEX = TimeToCanvasX(fgb.DetailEndTime, DisplayDate);
                    int canvasX = ClientToCanvasX(this.Cols[FrozenColumnIdx + 1].Left, this.ScrollPosition.X, e.X);
                    if (canvasSX <= canvasX && canvasX <= canvasEX) {
                        //Debug.Print("canvasSX:" + canvasSX + " canvasX:" + canvasX + " canvasEX:" + canvasEX);
                        //Debug.Print("Left:" + this.Cols[FROZEN_COLUMN_IDX + 1].Left);
                        if (fgb.PartType != FxGanttBar.PartTypes.Other) {
                            //直接作業
                            GanttBarToolTip.BackgroundGradient = BackgroundGradient.Silver;
                        } else {
                            //間接作業
                            GanttBarToolTip.BackgroundGradient = BackgroundGradient.Blue;
                        }
                        GanttBarToolTip.SetToolTip(this, fgb.Tooltip);

                        return;
                    }
                }
            }
            GanttBarToolTip.Hide();
        }
        /// <summary>
        /// MouseEnterCellイベント
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnMouseEnterCell(RowColEventArgs e) {
        //    base.OnMouseEnterCell(e);

        //    if (e.Col == (int)ColIndex.Resources) {
        //        //利用資源の場合ツールチップを表示
        //        ResourcesToolTip.SetToolTip(this, this[e.Row, e.Col].ToString());
        //    } else {
        //        ResourcesToolTip.Hide();
        //    }
        //}
        #endregion EventHandlers

        #region PublicMethods
        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="mlu"></param>
        public void InitDayGantt(
            MultiLangUtil mlu, 
            Dictionary<string, string> dataDic, 
            List<FxGanttRow> barList, 
            DateTime displayDate) {

            _mlu = mlu;
            _dataDic = dataDic;
            _barList = barList;
            DisplayDate = displayDate;

			_headerCellStyle = this.Styles.Add("Header");
            _headerCellStyle.TextAlign = TextAlignEnum.CenterCenter;

            this.Rows.Fixed = FixedRowIdx;
            this.Cols.Frozen = FrozenColumnIdx;

            _stripePen.DashStyle = DashStyle.Dash;

            //列ヘッダー定義
            this.Cols.Count = (int)ColIndex.P0H + 24;
            this[0, (int)ColIndex.WorkDiv] = dataDic[MESConsts.work_div_name];
            this[0, (int)ColIndex.ResourcesName] = dataDic[MESConsts.result_resources_name];
            this[0, (int)ColIndex.Operation] = dataDic[MESConsts.operation];
            this[0, (int)ColIndex.P0H] = "0";
            this[0, (int)ColIndex.P0H + 1] = "1";
            this[0, (int)ColIndex.P0H + 2] = "2";
            this[0, (int)ColIndex.P0H + 3] = "3";
            this[0, (int)ColIndex.P0H + 4] = "4";
            this[0, (int)ColIndex.P0H + 5] = "5";
            this[0, (int)ColIndex.P0H + 6] = "6";
            this[0, (int)ColIndex.P0H + 7] = "7";
            this[0, (int)ColIndex.P0H + 8] = "8";
            this[0, (int)ColIndex.P0H + 9] = "9";
            this[0, (int)ColIndex.P0H + 10] = "10";
            this[0, (int)ColIndex.P0H + 11] = "11";
            this[0, (int)ColIndex.P0H + 12] = "12";
            this[0, (int)ColIndex.P0H + 13] = "13";
            this[0, (int)ColIndex.P0H + 14] = "14";
            this[0, (int)ColIndex.P0H + 15] = "15";
            this[0, (int)ColIndex.P0H + 16] = "16";
            this[0, (int)ColIndex.P0H + 17] = "17";
            this[0, (int)ColIndex.P0H + 18] = "18";
            this[0, (int)ColIndex.P0H + 19] = "19";
            this[0, (int)ColIndex.P0H + 20] = "20";
            this[0, (int)ColIndex.P0H + 21] = "21";
            this[0, (int)ColIndex.P0H + 22] = "22";
            this[0, (int)ColIndex.P0H + 23] = "23";

            //ヘッダマージ
            CellRange cr;
            this.AllowMergingFixed = AllowMergingEnum.Custom;
            //作業区分
             cr = this.GetCellRange(0, (int)ColIndex.WorkDiv, 1, (int)ColIndex.WorkDiv);
            this.MergedRanges.Add(cr);
            //利用資源
            cr = this.GetCellRange(0, (int)ColIndex.ResourcesName, 1, (int)ColIndex.ResourcesName);
            this.MergedRanges.Add(cr);
            //作業
             cr = this.GetCellRange(0, (int)ColIndex.Operation, 1, (int)ColIndex.Operation);
            this.MergedRanges.Add(cr);
            //24時間
            cr = this.GetCellRange(0, FrozenColumnIdx + 1, 0, FrozenColumnIdx + 24);
            this.MergedRanges.Add(cr);

            //ヘッダースタイルの適用
            cr = this.GetCellRange(0, 1, 1, FrozenColumnIdx + 24);
            cr.Style = _headerCellStyle;

            //時刻
            this[1, FrozenColumnIdx + 1] = "00:00";
            this[1, FrozenColumnIdx + 2] = "01:00";
            this[1, FrozenColumnIdx + 3] = "02:00";
            this[1, FrozenColumnIdx + 4] = "03:00";
            this[1, FrozenColumnIdx + 5] = "04:00";
            this[1, FrozenColumnIdx + 6] = "05:00";
            this[1, FrozenColumnIdx + 7] = "06:00";
            this[1, FrozenColumnIdx + 8] = "07:00";
            this[1, FrozenColumnIdx + 9] = "08:00";
            this[1, FrozenColumnIdx + 10] = "09:00";
            this[1, FrozenColumnIdx + 11] = "10:00";
            this[1, FrozenColumnIdx + 12] = "11:00";
            this[1, FrozenColumnIdx + 13] = "12:00";
            this[1, FrozenColumnIdx + 14] = "13:00";
            this[1, FrozenColumnIdx + 15] = "14:00";
            this[1, FrozenColumnIdx + 16] = "15:00";
            this[1, FrozenColumnIdx + 17] = "16:00";
            this[1, FrozenColumnIdx + 18] = "17:00";
            this[1, FrozenColumnIdx + 19] = "18:00";
            this[1, FrozenColumnIdx + 20] = "19:00";
            this[1, FrozenColumnIdx + 21] = "20:00";
            this[1, FrozenColumnIdx + 22] = "21:00";
            this[1, FrozenColumnIdx + 23] = "22:00";
            this[1, FrozenColumnIdx + 24] = "23:00";

            this.Cols[(int)ColIndex.WorkDiv].Width = 70;
            this.Cols[(int)ColIndex.ResourcesName].Width = 120;
            //簡易実績入力モードでは、利用資源が入力されないので、全面的に非表示とする。
            this.Cols[(int)ColIndex.ResourcesName].Visible = false;  
            this.Cols[(int)ColIndex.Operation].Width = 200;
            this.Cols[FrozenColumnIdx + 1].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 2].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 3].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 4].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 5].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 6].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 7].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 8].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 9].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 10].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 11].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 12].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 13].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 14].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 15].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 16].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 17].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 18].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 19].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 20].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 21].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 22].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 23].Width = HourColumnWidth;
            this.Cols[FrozenColumnIdx + 24].Width = HourColumnWidth;

            //日付(曜日)
            this[0, FrozenColumnIdx + 1] = DisplayDate.ToString(_mlu.GetMsg(CommonConsts.DATE_FORMAT_YYYYMMDD)) +
                " (" + DisplayDate.ToString("ddd") + ")";

            //カレンダ部マージ
            this.AllowMerging = AllowMergingEnum.Custom;

            //読取専用
            this.AllowEditing = false;
            //サイズ変更不可
            this.AllowResizing = AllowResizingEnum.None;
            //ソート不可
            this.AllowSorting = AllowSortingEnum.None;

            //行数設定
            if (_barList != null) {
                this.Rows.Count = _barList.Count + FixedRowIdx;
                for (int i = FixedRowIdx; i < this.Rows.Count; i++ ) {
                    //時間セルマージ
                    CellRange crTmp = this.GetCellRange(i, FrozenColumnIdx + 1, i, FrozenColumnIdx + 24);
                    this.MergedRanges.Add(crTmp);

                    //作業区分名
                    this[i, (int)ColIndex.WorkDiv] = _barList[i - FixedRowIdx].WorkDivName.ToString();
                    //利用資源名
                    this[i, (int)ColIndex.ResourcesName] = _barList[i - FixedRowIdx].ResultResourcesName.ToString();
                    //作業
                    this[i, (int)ColIndex.Operation] = _barList[i - FixedRowIdx].Operation.ToString();
                }
            }
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// GanttBar描画
        /// </summary>
        /// <param name="scrollX"></param>
        /// <param name="bounds"></param>
        /// <param name="g"></param>
        /// <param name="bars"></param>
        private void DrawBar(int scrollX, Rectangle bounds, Graphics g, List<FxGanttBar> bars) {
            int barHeight = bounds.Height - 2;
            foreach (FxGanttBar fgb in bars) {
                int canvasSX = 0;
                int clientSX = 0;
                int canvasEX = 0;
                int clientEX = 0;
                if (fgb.IsPlan) {
                    //計画
                    canvasSX = TimeToCanvasX(fgb.PlanStartTime, DisplayDate);
                    canvasEX = TimeToCanvasX(fgb.PlanEndTime, DisplayDate);
                } else {
                    //実績
                    canvasSX = TimeToCanvasX(fgb.DetailStartTime, DisplayDate);
                    canvasEX = TimeToCanvasX(fgb.DetailEndTime, DisplayDate);
                }
                clientSX = CanvasToClientX(bounds.X, scrollX, canvasSX);
                clientEX = CanvasToClientX(bounds.X, scrollX, canvasEX);
                //if (((bounds.X <= clientSX && clientSX <= bounds.X + bounds.Width) || //開始位置、または、終了位置が領域内か？
                //    (bounds.X <= clientEX && clientEX <= bounds.X + bounds.Width)) &&
                //    (clientSX < clientEX)) { //開始位置より終了位置が大きい
                if (clientSX < clientEX) { 
                    //描画エリア内であれば描画
                    Rectangle rect = new Rectangle(clientSX, bounds.Y + 1, clientEX - clientSX, barHeight);
                    if (fgb.PartType == FxGanttBar.PartTypes.Setup || fgb.PartType == FxGanttBar.PartTypes.Teardown) {
                        rect.Height = rect.Height - 4;
                        rect.Y = rect.Y + 2;
                    }
                    g.IntersectClip(rect);
                    g.FillRectangle(fgb.BarBrush, rect);
                    //境界を3Dスタイルに変更
                    Rectangle borderRect = new Rectangle(rect.X - 1, rect.Y - 1, rect.Width + 1, rect.Height + 1);
                    ControlPaint.DrawBorder(g, borderRect, Color.AliceBlue, ButtonBorderStyle.Outset);
                    g.DrawString(fgb.Text, _barFont, Brushes.Black, rect.X, rect.Y);
                    if (fgb.IsSelect) {
                        rect.Height = rect.Height - 1;
                        //rect.Width = rect.Width + 2;
                        //rect.X = rect.X - 1;
                        g.DrawRectangle(_selectPen, rect);
                    }
                    g.ResetClip();
                }
            }
        }
        /// <summary>
        /// 分を幅(pixel)に変換
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private int MinutesToWidth(int m) {
            return (int)Math.Ceiling(m * MinWidth);
        }
        /// <summary>
        /// 時分をキャンバス(時間帯部分の結合セル)上の水平位置に変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int TimeToCanvasX(DateTime? dt, DateTime displayDate) {
            if (!dt.HasValue) {
                return -1;
            }
            if (dt.Value.ToString("yyyyMMdd") == displayDate.ToString("yyyyMMdd")) {
                //当日
                return (dt.Value.Hour * HourColumnWidth) + (int)Math.Ceiling(dt.Value.Minute * MinWidth);
            } else if (int.Parse(dt.Value.ToString("yyyyMMdd")) < int.Parse(displayDate.ToString("yyyyMMdd"))) {
                //前日
                return 0;
            } else {
                //後日
                return 24 * HourColumnWidth;
            }
        }
        /// <summary>
        /// 時間の区切り線を描画
        /// </summary>
        /// <param name="scrollX"></param>
        /// <param name="bounds"></param>
        /// <param name="g"></param>
        private void DrawHourStripe(int scrollX, Rectangle bounds, Graphics g) {
            for (int i = 1; i <= 23; i++ ) {
                int canvasX = i * HourColumnWidth;
                int clientX = CanvasToClientX(bounds.X, scrollX, canvasX);
                if (bounds.X <= clientX && clientX <= bounds.X + bounds.Width) {
                    //描画エリア内であれば描画
                    g.DrawLine(_stripePen, clientX, bounds.Y, clientX, bounds.Y + bounds.Height);
                }
            }
        }
        /// <summary>
        /// クライアント上のX座標をキャンバス上のX座標に変換
        /// </summary>
        /// <param name="orgX"></param>
        /// <param name="scrollX"></param>
        /// <param name="clientX"></param>
        /// <returns></returns>
        private int ClientToCanvasX(int orgX, int scrollX, int clientX) {
            return clientX - scrollX - orgX;
        }
        /// <summary>
        /// キャンバス上のX座標をグリッドコントロール上のX座標に変換
        /// </summary>
        /// <param name="orgX"></param>
        /// <param name="scrollX"></param>
        /// <param name="canvasX"></param>
        /// <returns></returns>
        private int CanvasToClientX(int orgX, int scrollX, int canvasX) {
            return orgX + canvasX + scrollX;
        }
        /// <summary>
        /// コンポーネント初期化
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.GanttBarToolTip = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.ResourcesToolTip = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GanttBarToolTip
            // 
            this.GanttBarToolTip.AutoPopDelay = 50000;
            this.GanttBarToolTip.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GanttBarToolTip.InitialDelay = 100;
            this.GanttBarToolTip.ReshowDelay = 0;
            this.GanttBarToolTip.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            // 
            // ResourcesToolTip
            // 
            this.ResourcesToolTip.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ResourcesToolTip.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            // 
            // FxGanttView
            // 
            this.Rows.DefaultSize = 18;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion PrivateMethods
    }
}
