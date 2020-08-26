using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
namespace FxCommonLib.Utils {
    /// <summary>
    /// DateTimeユーティリティ
    /// </summary>
    public class DateTimeUtil {
        /// <summary>
        /// TimeSpanの単位で日時の切り捨てを行う。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime Truncate(DateTime dt, TimeSpan ts) {
            if (ts == TimeSpan.Zero) {
                return dt;
            }
            return dt.AddTicks(-(dt.Ticks % ts.Ticks));
        }
        /// <summary>
        /// 正しい日付であるか判定
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsDate(string val) {
            try {
                //文字列をDateTime値に変換する
                DateTime dt1 = DateTime.Parse(val);
            } catch (Exception ex) {
                Debug.Write(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// フォーマットを指定して正しい日付であるか判定
        /// </summary>
        /// <param name="val"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsDate(string val, string format) {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeStyles dts = DateTimeStyles.None;
            DateTime tmp;
            return (DateTime.TryParseExact(val, format, ci, dts, out tmp));
        }

        /// <summary>
        /// 時間を分単位に変換
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int HourToMinute(decimal hour) {
            return HourToMinute(StringUtil.NullToZero(hour));
        }

        /// <summary>
        /// 時間を分単位に変換
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int HourToMinute(string hour) {
            int ret = 0;
            int integral = 0;
            double fraction = 0d;

            if (Strings.InStr(hour, ".") > 0) {
                //小数点がある場合
                //整数部
                integral = Convert.ToInt32(Strings.Left(hour, Strings.InStr(hour, ".") - 1));
                //小数部
                fraction = Convert.ToDouble("0." + Strings.Right(hour, Strings.Len(hour) - Strings.InStr(hour, ".")));
            } else {
                integral = Convert.ToInt32(hour);
            }

            //「最近接偶数への丸め」ではなく一般的な四捨五入を採用
            ret = (integral * 60) + Convert.ToInt32(Math.Round((60 * fraction), 0, MidpointRounding.AwayFromZero));
            return ret;
        }

        /// <summary>
        /// 引数が本日であるか判定
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsToday(DateTime dt) {
            if (DateAndTime.Now.ToString("yyyy/MM/dd") == dt.ToString("yyyy/MM/dd")) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 引数が過去日付であるか判定(日単位での比較)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsPast(DateTime dt) {
            if (string.Compare(DateAndTime.Now.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd")) > 0) {
                return true;
            }
            return false;
        }


        public static string AutoComplete(string dt) {
            if (dt == "____/__/__ __:__:__") {
                //ブランクはそのまま返す
                return dt;
            } else {
                //FIXME
                string ret = "";
                //年の補完
                //月の補完
                //日の補完
                //時の補完
                //分の補完
                return ret;
            }
        }

        /// <summary>
        /// DBNullをnullに変換(Null許容型として扱う)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime? DBNullToNull(object dt) {
            if (dt == DBNull.Value) {
                return null;
            } else {
                return (DateTime)dt;
            }
        }

        /// <summary>
        /// ISO8601形式の文字列をTimeSpanに変換します。
        /// NOTE:FLEXSCHE仕様に合わせて変換
        /// </summary>
        /// <param name="iso8601"></param>
        /// <returns></returns>
        public static TimeSpan Iso8601ToTimeSpan(string iso8601) {
            if (iso8601 == null || iso8601 == "") {
                //0秒で返す。
                return new TimeSpan(0, 0, 0);
            }
            TimeSpan? ret = null;
            int index = iso8601.IndexOf("D");
            if (index > 0) {
                //日付部("P1D")が存在する場合
                string strDays = iso8601.Substring(0, index);
                TimeSpan days = new TimeSpan(int.Parse(strDays.Replace("P", "")), 0, 0, 0);
                ret = days;

                //時間部が存在する場合
                index = iso8601.IndexOf("DT");
                if (index > 0) {
                    TimeSpan other = XmlConvert.ToTimeSpan("PT" + iso8601.Substring(index + 2));
                    ret = days.Add(other);
                }
            } else {
                ret =  XmlConvert.ToTimeSpan(iso8601.Replace("P", "PT"));
            }
            return new TimeSpan(ret.Value.Days, ret.Value.Hours, ret.Value.Minutes, 0);
        }
        /// <summary>
        /// TimeSpanの値をISO形式文字列に変換します。
        /// NOTE:FLEXSCHE仕様に合わせて以下を変換
        ///      ・プリフィクスの"PT"を"P"に変換
        ///      ・秒の小数部は切り捨て
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string TimeSpanToIso8601(TimeSpan ts) {
            //秒を丸める
            TimeSpan tmp = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);
            return XmlConvert.ToString(tmp).Replace("PT", "P");
        }

        /// <summary>
        /// "yyyy/MM/dd HH:mm"形式の文字列に変換
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        public static string FormatYMi(string val, MultiLangUtil mu) {
            string ret = val;
            if (val != "") {
                ret = DateTime.Parse(val).ToString(mu.GetMsg(CommonConsts.DATE_FORMAT_Y_MIN));
            }
            return ret;
        }
        /// <summary>
        /// "yyyy/MM/dd HH:mm:ss"形式の文字列に変換
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        public static string FormatYSec(string val, MultiLangUtil mu) {
            string ret = val;
            if (val != "") {
                ret = DateTime.Parse(val).ToString(mu.GetMsg(CommonConsts.DATE_FORMAT_Y_SEC));
            }
            return ret;
        }

        /// <summary>
        /// 日付型変換(ブランク or nullの場合は、nullを返す)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime? ParseEx(string val) {
            if (val == null || val == "") {
                return null;
            }
            return DateTime.Parse(val);
        }

        /// <summary>
        /// 土日以外の日を加算した日付をもとめます。
        /// 祝日は勘案なし
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime AddDaysWithoutSatSun(DateTime date, int days) {
            double sign = Convert.ToDouble(Math.Sign(days));
            int unsignedDays = Math.Sign(days) * days;
            for (int i = 0; i < unsignedDays; i++) {
                do {
                    date = date.AddDays(sign);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday);
            }
            return date;
        }

        /// <summary>
        /// 該当年月の日数を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static int DaysInMonth(DateTime dt) {
            return DateTime.DaysInMonth(dt.Year, dt.Month);
        }

        /// <summary>
        /// 月初日を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>Datetime</returns>
        public static DateTime BeginOfMonth(DateTime dt) {
            return dt.AddDays((dt.Day - 1) * -1);
        }

        /// <summary>
        /// 月末日を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfMonth(DateTime dt) {
            return new DateTime(dt.Year, dt.Month, DaysInMonth(dt));
        }

        /// <summary>
        /// 時刻を落として日付のみにする
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime StripTime(DateTime dt) {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        /// <summary>
        /// 日付を落として時刻のみにする
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="base_date">DateTime* : 基準日</param>
        /// <returns>DateTime</returns>
        public static DateTime StripDate(DateTime dt, DateTime? base_date = null) {
            base_date = base_date ?? DateTime.MinValue;
            return new DateTime(base_date.Value.Year, base_date.Value.Month, base_date.Value.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatEx(string val, string format) {
            if (val == null || val == "") {
                return null;
            }
            return DateTime.Parse(val).ToString(format);
        }
        public static Object ParseExDBNull(string val) {
            if (val == null || val == "") {
                return DBNull.Value;
            }
            return DateTime.Parse(val);
        }
        public static Object NullToDBNull(DateTime? val) {
            if (val == null) {
                return DBNull.Value;
            }
            return val;
        }

    }
}