using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;

namespace FxCommonLib.Controls {

    /// <summary>
    /// 数値テキストボックス拡張
    /// ・小数点以下の有効桁数について、桁数、丸め方法を指定可
    /// ・最小値、最大値をValueChangedイベントで範囲変換
    /// </summary>
    public partial class NumericEditEx : C1NumericEdit {

        #region Constants
        /// <summary>小数点以下丸め区分</summary>
        public enum RoundType : int {
            RoundDefault = 0, // デフォルト・・四捨五入銀行丸め(最近接偶数への丸め)
            RoundAwayFromZero = 1, // 四捨五入
            Floor = 2, // 切り下げ
            Truncate = 3, // 切り捨て
            Ceiling = 4 // 切り上げ
        }
        #endregion Constants

        #region Properties

        /// <summary>多言語対応ユーティリティ</summary>
        //private static MultiLangUtil _mlu = null;
        //public static MultiLangUtil MultiLangUtil {
        //    set { _mlu = value; }
        //    get { return _mlu; }
        //}

        /// <summary>入力最小数値</summary>
        private decimal _minNumValue = decimal.MinValue;

        [Description("入力可能な最小数値を設定します。この値より小さな値を入力した場合はValueChangedイベントにより最小数値に変換されます.")] 
        public decimal MinNumValue {
            set {
                _minNumValue = value;

            }
            get { return _minNumValue; }
        }
        /// <summary>入力最大数値</summary>
        private decimal _maxNumValue = decimal.MaxValue;

        [Description("入力可能な最大数値を設定します。この値より大きな値を入力した場合はValueChangedイベントにより最大数値に変換されます.")] 
        public decimal MaxNumValue {
            set {
                _maxNumValue = value;
                
            }
            get { return _maxNumValue; }
        }
        /// <summary>小数点以下有効桁数</summary>
        private int _digitsAfterTheDecimalPoint = 0;

        [Description("小数点以下の有効桁数を設定します。ValueChangedイベントにて有効桁数範囲外は丸められます.")] 
        public int DigitsAfterTheDecimalPoint {
            set {
                _digitsAfterTheDecimalPoint = value;

            }
            get { return _digitsAfterTheDecimalPoint; }
        }
        /// <summary>小数点以下の端数処理方法</summary>
        private RoundType _roundTypeAfterTheDecimalPoint = RoundType.Truncate;

        [Description("小数点以下の有効桁数を丸める方法を指定します.")] 
        public RoundType RoundTypeAfterTheDecimalPoint {
            set {
                _roundTypeAfterTheDecimalPoint = value;
            }
            get { return _roundTypeAfterTheDecimalPoint; }
        }

        #endregion Properties

        #region Constractors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumericEditEx(){
            InitializeComponent();
            this.MaxLength = CommonConsts.C1NumericEditMaxLength;

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public NumericEditEx(IContainer container) {


            container.Add(this);

            InitializeComponent();

            this.MaxLength = CommonConsts.C1NumericEditMaxLength;
        }

        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// ValueChangedイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValueChanged(EventArgs e) {

            // 入力が有効な場合のみ
            if (!this.ReadOnly || this.Enabled) {
                // 最大値/最小値の変換
                ConvertIntervalNumValue();

                // 小数点以下の端数処理
                Rounding();
            }

            base.OnValueChanged(e);

        }
        #endregion

        #region PrivateMethods

        /// <summary>
        /// 最大値、最小値の範囲に値を変換
        /// </summary>
        private void ConvertIntervalNumValue() {

            if (StringUtil.NullDBNullToBlank(this.Value) == "") {
                return;
            }

            decimal numvalue = 0;

            numvalue = Convert.ToDecimal(this.Value);

            if (this._minNumValue > numvalue) {
                numvalue = this._minNumValue;
            } else if (this._maxNumValue < numvalue) {
                numvalue = this._maxNumValue;
            }

            this.Value = numvalue;
        }

        /// <summary>
        /// 丸め処理
        /// </summary>
        private void Rounding() {

            if (StringUtil.NullDBNullToBlank(this.Value) == "") {
                return;
            }

            decimal newValue = Convert.ToDecimal(this.Value);


            // 小数点以下有効桁数計算用
            decimal multiplenum = 0;
            multiplenum = Convert.ToDecimal(Math.Pow(10,this._digitsAfterTheDecimalPoint));

            switch (_roundTypeAfterTheDecimalPoint) {

                case RoundType.RoundDefault:

                    newValue = Math.Round(newValue, this._digitsAfterTheDecimalPoint);

                    break;
                case RoundType.RoundAwayFromZero:

                    newValue = Math.Round(newValue, this._digitsAfterTheDecimalPoint, MidpointRounding.AwayFromZero);

                    break;
                case RoundType.Floor:

                    newValue = (Math.Floor(newValue * multiplenum)) / (multiplenum);

                    break;
                case RoundType.Truncate:

                    newValue = (Math.Truncate(newValue * multiplenum)) / (multiplenum);

                    break;
                case RoundType.Ceiling:

                    newValue = (Math.Ceiling(newValue * multiplenum)) / (multiplenum);

                    break;
                default:
                    break;
            }

            this.Value = newValue;

        }

        /// <summary>
        /// 数値入力項目入力の閾値とエラーメッセージをセットする
        /// </summary>
        //private void SetValueIntervalAndErrorMsg() {

        //    decimal minValue = _minNumValue;
        //    decimal maxValue = _maxNumValue;

        //    ValueInterval vi = new ValueInterval(minValue, maxValue, true, true);
        //    vi.UseMinValue = true;
        //    vi.UseMaxValue = true;

        //    this.PostValidation.Intervals.Add(vi);
        //    this.MaxLength = CommonConsts.C1NumericEditMaxLength;

        //    if (_mlu != null) {
        //        this.ErrorInfo.ErrorMessageCaption = _mlu.GetMsg(CommonConsts.TITLE_ERROR);
        //        this.ErrorInfo.ErrorMessage = _mlu.GetMsg(CommonConsts.MSG_OUT_OF_BOUND);
        //    }

        //}

        #endregion PrivateMethods

    }
}
