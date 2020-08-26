using FxCommonLib.Consts;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 金額に関するユーティリティクラス
    /// </summary>
    public class MoneyUtil {

        /// <summary>
        /// SQLServerの金額型範囲内か
        /// </summary>
        /// <param name="val"></param>
        /// <param name="allowsNull">true: Nullを範囲内とみなす false:Null,DBNullを範囲内とみなさない</param>
        /// <returns></returns>
        public static bool InRange(decimal val, bool allowsNull) {

            if (allowsNull) {
                val = decimal.Parse(StringUtil.NullDBNullToZero(val));
            } else {
                return false;
            }
            return (CommonConsts.MoneyMinValue <= val &&
                CommonConsts.MoneyMaxValue >= val); 
        }

        /// <summary>
        /// SQLServerの金額型範囲内か
        /// (Null,DBNullは範囲内とみなします）
        /// </summary>
        /// <param name="val"></param>
        /// <returns>true:範囲内 false:範囲外</returns>
        public static bool InRange(decimal val) {
            return InRange(val, true);
        }



    }
}
