using System.ComponentModel;
using System.Diagnostics;

namespace FxCommonLib.Utils {
    public sealed class DesignModeUtil {
        /// <summary>
        /// デザインモード判定
        /// コントロールが入れ子の場合でも対応
        /// </summary>
        /// <returns></returns>
        public static bool IsDesignMode() {
            bool ret = false;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) {
                ret = true;
            } else if (Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV")) {
                ret = true;
            }

            return ret;
        }
    }
}
