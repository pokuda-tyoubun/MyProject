using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSReportUtility.Utils {
    public class RegexUtil {
        /// <summary>
        /// MS形式のワイルドカードを正規表現形式に変換
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string EscapeWildCard(string val) {
            val = Regex.Escape(val);
            val = val.Replace(@"\*", ".*");
            val = val.Replace(@"\?", ".");
            return val;
        }
    }
}