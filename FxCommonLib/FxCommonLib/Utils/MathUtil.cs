using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System;

namespace FxCommonLib.Utils {
    public class MathUtil {

        /// <summary>
        /// 引数のデータの信頼区間を取得します。
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public Tuple<double, double> GetConfidenceInterval(double[] samples, double interval) {
            double theta = (interval + 1.0) / 2;
            double T = StudentT.InvCDF(0,1,samples.Length-1,theta);

            double mean = samples.Mean();
            double sd = samples.StandardDeviation();
            double t = T * (sd / Math.Sqrt(samples.Length));
            return Tuple.Create(mean - t, mean + t);
        }
    }
}
