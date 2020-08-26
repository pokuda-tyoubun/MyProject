using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Extensions {
    public static class DateTimeExtension {
        /// <summary>
        /// 週の最初の曜日を指定して開始日を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayOfWeek">週の最初の曜日</param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(this DateTime date, DayOfWeek startDayOfWeek) {
            if (startDayOfWeek == DayOfWeek.Sunday) {
                DateTime ymd = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
                return new DateTime(ymd.Year, ymd.Month, ymd.Day);
            } else {
                var diff = startDayOfWeek - date.DayOfWeek;
                DateTime ymd = date.AddDays((diff > 0) ? diff - 7 : diff);
                return new DateTime(ymd.Year, ymd.Month, ymd.Day);
            }
        }

        /// <summary>
        /// 週の最初の曜日を指定して最終日を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayOfWeek">週の最初の曜日</param>
        /// <returns></returns>
        public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek startDayOfWeek) {
            if (startDayOfWeek == DayOfWeek.Sunday) {
                return date.AddDays(DayOfWeek.Saturday - date.DayOfWeek);
            } else {
                var diff = startDayOfWeek - date.DayOfWeek;
                return date.AddDays((diff == 1) ? 0 : 6 + diff);
            }
        }
    }
}
