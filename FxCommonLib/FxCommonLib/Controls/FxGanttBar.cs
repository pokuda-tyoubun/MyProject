using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using System;
using System.Drawing;
using System.Text;

namespace FxCommonLib.Controls {
    public class FxGanttBar : IDisposable {

        #region Constants
        /// <summary>
        /// 列インデックス
        /// </summary>
        public enum PartTypes : int {
            Setup = 1,      //前段取り
            Manufacture,    //製造
            Teardown,       //後段取り
            UnplannedSetup, //前段取り（計画外作業）
            UnplannedManufacture, //製造（計画外作業）
            UnplannedTeardown, //後段取り（計画外作業）
            Other           //間接作業など
        }
        #endregion Constants

        #region Properties
        /// <summary>開始～終了までの時間（分）</summary>
        public int StartToEndMinutes;
        /// <summary>主資源フラグ</summary>
        private bool _isPrimaryResource = false;
        public bool IsPrimaryResource {
            set {_isPrimaryResource = value; }
            get { return _isPrimaryResource; }
        }
        /// <summary>パートタイプ</summary>
        private PartTypes _partType = 0;
        public PartTypes PartType {
            set {_partType = value; }
            get { return _partType; }
        }
        /// <summary>作業実績明細区分</summary>
        private String _resultDetailDiv = "";
        public String ResultDetailDiv {
            set {_resultDetailDiv = value; }
            get { return _resultDetailDiv; }
        }
        /// <summary>開始日時(計画)</summary>
        private DateTime? _planStartTime = new DateTime();
        public DateTime? PlanStartTime {
            set {_planStartTime = value; }
            get { return _planStartTime; }
        }
        /// <summary>終了日時(計画)</summary>
        private DateTime? _planEndTime = new DateTime();
        public DateTime? PlanEndTime {
            set {_planEndTime = value; }
            get { return _planEndTime; }
        }
        /// <summary>開始日時(実績)</summary>
        private DateTime? _resultStartTime = new DateTime();
        public DateTime? ResultStartTime {
            set {_resultStartTime = value; }
            get { return _resultStartTime; }
        }
        /// <summary>終了日時(実績)</summary>
        private DateTime? _resultEndTime = new DateTime();
        public DateTime? ResultEndTime {
            set {_resultEndTime = value; }
            get { return _resultEndTime; }
        }
        /// <summary>実績数量</summary>
        private double? _resultQty = 0d;
        public double? ResultQty {
            set {_resultQty = value; }
            get { return _resultQty; }
        }
        /// <summary>不良数量</summary>
        private double? _failureQty = 0d;
        public double? FailureQty {
            set {_failureQty = value; }
            get { return _failureQty; }
        }
        /// <summary>数量</summary>
        private double? _qty = 0d;
        public double? Qty {
            set {_qty = value; }
            get { return _qty; }
        }
        /// <summary>ラベル</summary>
        private String _text = "";
        public String Text {
            set {_text = value; }
            get { return _text; }
        }
        /// <summary>ツールチップ</summary>
        private String _tooltip = "";
        public String Tooltip {
            set {_tooltip = value; }
            get { return _tooltip; }
        }
        /// <summary>ブラシ</summary>
        private SolidBrush _barBrush = null;
        public SolidBrush BarBrush {
            set {_barBrush = value; }
            get { return _barBrush; }
        }
        /// <summary>選択</summary>
        private bool _isSelect = false;
        public bool IsSelect {
            set {_isSelect = value; }
            get { return _isSelect; }
        }
        /// <summary>計画フラグ</summary>
        private bool _isPlan = false;
        public bool IsPlan {
            set {_isPlan = value; }
            get { return _isPlan; }
        }
        /// <summary>作業時間(計画)</summary>
        private int _planWorkingMinutes = 0;
        public int PlanWorkingMinutes {
            set {_planWorkingMinutes = value; }
            get { return _planWorkingMinutes; }
        }
        /// <summary>作業時間(実績)</summary>
        private int _resultWorkingMinutes = 0;
        public int ResultWorkingMinutes {
            set {_resultWorkingMinutes = value; }
            get { return _resultWorkingMinutes; }
        }
        /// <summary>明細開始日時</summary>
        private DateTime? _detailStartTime = new DateTime();
        public DateTime? DetailStartTime {
            set {_detailStartTime = value; }
            get { return _detailStartTime; }
        }
        /// <summary>明細終了日時</summary>
        private DateTime? _detailEndTime = new DateTime();
        public DateTime? DetailEndTime {
            set {_detailEndTime = value; }
            get { return _detailEndTime; }
        }
        /// <summary>明細時間</summary>
        private int _detailMinutes = 0;
        public int DetailMinutes {
            set {_detailMinutes = value; }
            get { return _detailMinutes; }
        }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FxGanttBar() {
        }
        #endregion Constractors

        #region Destractors
        /// <summary>
        /// デストラクタ
        /// </summary>
        ~FxGanttBar() {
            Dispose(false);
        }
        /// <summary>
        /// Dispose処理
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose処理
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // 管理（managed）リソースの破棄処理をここに記述します。
                _barBrush.Dispose();
            }
            // 非管理（unmanaged）リソースの破棄処理をここに記述します。
        }
        #endregion Destractors


        #region PublicMethods
        /// <summary>
        /// オブジェクト作成
        /// </summary>
        public void CreateObject(MultiLangUtil mlu) {
            CreateToolTipString(mlu);
            StartToEndMinutes = CalcStartToEndMinutes();

            if (_isPlan) {
                if (_partType == PartTypes.Setup ||_partType == PartTypes.Teardown) {
                    _barBrush = new SolidBrush(Color.PeachPuff);
                } else if (_partType == PartTypes.Manufacture) {
                    _barBrush = new SolidBrush(Color.SpringGreen);
                } else if (_partType == PartTypes.Other) {
                    _barBrush = new SolidBrush(Color.LightBlue);
                }
            } else {
                if (_partType == PartTypes.Setup ||_partType == PartTypes.Teardown) {
                    _barBrush = new SolidBrush(Color.RosyBrown);
                } else if (_partType == PartTypes.Manufacture) {
                    _barBrush = new SolidBrush(Color.LightSeaGreen);
                } else if (_partType == PartTypes.Other) {
                    _barBrush = new SolidBrush(Color.LightBlue);
                } else if (_partType == PartTypes.UnplannedSetup || _partType == PartTypes.UnplannedTeardown) {
                    _barBrush = new SolidBrush(Color.MistyRose);
                } else if (_partType == PartTypes.UnplannedManufacture) {
                    _barBrush = new SolidBrush(Color.Aquamarine);
                }
            }
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// ツールチップ文字列作成
        /// </summary>
        private void CreateToolTipString(MultiLangUtil mlu) {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table>");
            sb.Append("<tr> <td><b><parm>" + _text + "</parm></b></td> </tr>");
            sb.Append("</table>");
            sb.Append("<div style='margin:1 12'>");
            sb.Append("<table>");
            //作業パート
            sb.Append("<tr>");
            sb.Append(" <td>");
            sb.Append(mlu.GetMsg(MESConsts.CTL_PART));
            sb.Append(" </td>");
            sb.Append(" <td>:</td>");
            sb.Append(" <td>");
            switch (_partType) {
                case PartTypes.Setup:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_SETUP));
                    break;
                case PartTypes.Manufacture:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_MANU));
                    break;
                case PartTypes.Teardown:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_TEARDOWN));
                    break;
                case PartTypes.UnplannedSetup:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_UNPLANNED_SETUP));
                    break;
                case PartTypes.UnplannedManufacture:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_UNPLANNED_MANU));
                    break;
                case PartTypes.UnplannedTeardown:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_UNPLANNED_TEARDOWN));
                    break;
                case PartTypes.Other:
                    sb.Append(mlu.GetMsg(MESConsts.CTL_INDIRECT));
                    break;
            }
            if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                //仕掛中
                sb.Append(mlu.GetMsg(MESConsts.MSG_INPRC));
            }
            sb.Append(" </td>");
            sb.Append("</tr>");
            switch (_partType) {
                case PartTypes.Setup:
                case PartTypes.Manufacture:
                case PartTypes.Teardown:
                    //開始日時(計画)
                    sb.Append("<tr>");
                    sb.Append(" <td>");
                    sb.Append(mlu.GetMsg(MESConsts.CTL_PLAN_START));
                    sb.Append(" </td>");
                    sb.Append(" <td>:</td>");
                    sb.Append(" <td>");
                    sb.Append(DateTimeUtil.FormatEx(_planStartTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
                    sb.Append(" </td>");
                    sb.Append("</tr>");
                    //終了日時(計画)
                    sb.Append("<tr>");
                    sb.Append(" <td>");
                    sb.Append(mlu.GetMsg(MESConsts.CTL_PLAN_END));
                    sb.Append(" </td>");
                    sb.Append(" <td>:</td>");
                    sb.Append(" <td>");
                    sb.Append(DateTimeUtil.FormatEx(_planEndTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
                    sb.Append(" </td>");
                    sb.Append("</tr>");
                    break;
            }
            //開始日時(実績)
            sb.Append("<tr>");
            sb.Append(" <td>");
            sb.Append(mlu.GetMsg(MESConsts.CTL_RESULT_START));
            sb.Append(" </td>");
            sb.Append(" <td>:</td>");
            sb.Append(" <td>");
            sb.Append(DateTimeUtil.FormatEx(_resultStartTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
            sb.Append(" </td>");
            sb.Append("</tr>");
            //終了日時(実績)
            sb.Append("<tr>");
            sb.Append(" <td>");
            sb.Append(mlu.GetMsg(MESConsts.CTL_RESULT_END));
            sb.Append(" </td>");
            sb.Append(" <td>:</td>");
            sb.Append(" <td>");
            if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                sb.Append("-");
            } else {
                sb.Append(DateTimeUtil.FormatEx(_resultEndTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
            }
            sb.Append(" </td>");
            sb.Append("</tr>");
            switch (_partType) {
                case PartTypes.Setup:
                case PartTypes.Manufacture:
                case PartTypes.Teardown:
                    //計画時間
                    sb.Append("<tr>");
                    sb.Append(" <td>");
                    sb.Append(mlu.GetMsg(MESConsts.CTL_PLAN_MINUTES));
                    sb.Append(" </td>");
                    sb.Append(" <td>:</td>");
                    sb.Append(" <td>");
                    sb.Append(_planWorkingMinutes.ToString());
                    sb.Append(" </td>");
                    sb.Append("</tr>");
                    break;
            }
            //実績時間
            sb.Append("<tr>");
            sb.Append(" <td>");
            sb.Append(mlu.GetMsg(MESConsts.CTL_RESULT_MINUTES));
            sb.Append(" </td>");
            sb.Append(" <td>:</td>");
            sb.Append(" <td>");
            if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                sb.Append("-");
            } else {
                sb.Append(_resultWorkingMinutes.ToString());
            }
            sb.Append(" </td>");
            sb.Append("</tr>");
            switch (_partType) {
                case PartTypes.Setup:
                case PartTypes.Manufacture:
                case PartTypes.Teardown:
                    //予実差
                    sb.Append("<tr>");
                    sb.Append(" <td>");
                    sb.Append(mlu.GetMsg(MESConsts.CTL_DIFF_MINUTES));
                    sb.Append(" </td>");
                    sb.Append(" <td>:</td>");
                    sb.Append(" <td>");
                    if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                        sb.Append("-");
                    } else {
                        sb.Append((_resultWorkingMinutes - _planWorkingMinutes).ToString());
                    }
                    sb.Append(" </td>");
                    sb.Append("</tr>");
                    break;
            }
            if (_partType != PartTypes.Other) {
                //直接作業の場合は表示
                //明細開始日時
                sb.Append("<tr>");
                sb.Append(" <td>");
                sb.Append(mlu.GetMsg(MESConsts.CTL_DETAIL_START));
                sb.Append(" </td>");
                sb.Append(" <td>:</td>");
                sb.Append(" <td>");
                sb.Append(DateTimeUtil.FormatEx(_detailStartTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
                sb.Append(" </td>");
                sb.Append("</tr>");
                //明細終了日時
                sb.Append("<tr>");
                sb.Append(" <td>");
                sb.Append(mlu.GetMsg(MESConsts.CTL_DETAIL_END));
                sb.Append(" </td>");
                sb.Append(" <td>:</td>");
                sb.Append(" <td>");
                if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                    sb.Append("-");
                } else {
                    sb.Append(DateTimeUtil.FormatEx(_detailEndTime.ToString(), mlu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN)));
                }
                sb.Append(" </td>");
                sb.Append("</tr>");
                //明細時間
                sb.Append("<tr>");
                sb.Append(" <td>");
                sb.Append(mlu.GetMsg(MESConsts.CTL_DETAIL_MINUTES));
                sb.Append(" </td>");
                sb.Append(" <td>:</td>");
                sb.Append(" <td>");
                if (_resultDetailDiv == MESConsts.DetailDivInProc) {
                    sb.Append("-");
                } else {
                    sb.Append(_detailMinutes.ToString());
                }
                sb.Append(" </td>");
                sb.Append("</tr>");
                switch (_partType) {
                    case PartTypes.Setup:
                    case PartTypes.Manufacture:
                    case PartTypes.Teardown:
                        //数量
                        sb.Append("<tr>");
                        sb.Append(" <td>");
                        sb.Append(mlu.GetMsg(MESConsts.CTL_OUT_QTY));
                        sb.Append(" </td>");
                        sb.Append(" <td>:</td>");
                        sb.Append(" <td>");
                        sb.Append(_qty);
                        sb.Append(" </td>");
                        sb.Append("</tr>");
                        break;
                }
                //実績数量
                sb.Append("<tr>");
                sb.Append(" <td>");
                sb.Append(mlu.GetMsg(MESConsts.CTL_RESULT_QTY));
                sb.Append(" </td>");
                sb.Append(" <td>:</td>");
                sb.Append(" <td>");
                sb.Append(_resultQty);
                sb.Append(" </td>");
                sb.Append("</tr>");
                //不良数
                sb.Append("<tr>");
                sb.Append(" <td>");
                sb.Append(mlu.GetMsg(MESConsts.CTL_FAILURE_QTY));
                sb.Append(" </td>");
                sb.Append(" <td>:</td>");
                sb.Append(" <td>");
                sb.Append(_failureQty);
                sb.Append(" </td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</div>");

            _tooltip = sb.ToString();
        }

        /// <summary>
        /// 開始から終了までの時間（分）
        /// バーの長さを求めるため、実際の作業時間ではなく、開始、終了間の時間が必要
        /// </summary>
        /// <returns></returns>
        private int CalcStartToEndMinutes() {
            if (IsPlan) {
                //計画時
                if (_planStartTime != null && _planEndTime != null) {
                    return (int)Math.Ceiling((_planEndTime.Value - _planStartTime.Value).TotalMinutes);
                }
            } else {
                //実績時
                if (_detailStartTime != null && _detailEndTime != null) {
                    return (int)Math.Ceiling((_detailEndTime.Value - _detailStartTime.Value).TotalMinutes);
                }
            }
            return 0;
        }
        #endregion PrivateMethods
    }
}
