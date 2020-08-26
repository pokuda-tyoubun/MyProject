using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    public class GoogleCalendarApiUtil : PublicWebApiUtil {

        #region Constants
        private const int MaxResults = 1000;
        private const string JapaneseHolidaysId = "ja.japanese%23holiday@group.v.calendar.google.com";
        private const string GoogleUrl = "https://www.googleapis.com/calendar/v3/calendars/";
        private const string MethodString = "events";
        #endregion Constants

        #region MemberVariables
        /// <summary>APIKey</summary>
        private string _apiKey = "";
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GoogleCalendarApiUtil() {
            _apiKey = Properties.Resources.GOOGLE_API_KEY1;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// 祝日辞書取得
        /// </summary>
        /// <param name="toYear"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public Dictionary<DateTime, string> GetNationalHolidayDic(int toYear, int duration) {
            Dictionary<DateTime, string> ret = new Dictionary<DateTime, string>();

            //クエリーを作成する
            string query = string.Format(
                "key={0}&" +
                "timeMin={1}-01-01T00:00:00Z&" +
                "timeMax={2}-01-01T00:00:00Z&" +
                "maxResults={3}",
                _apiKey, toYear, toYear + duration, MaxResults);
            string url = GoogleUrl + JapaneseHolidaysId + "/" + MethodString + "?" + query;

            string jsonString = GetResponseString(url); 
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            if (!dic.ContainsKey("items")) {
                //itemsがなかったら失敗したと判断する
                return ret;
            }
            var items = JArray.Parse(dic["items"].ToString());
            foreach (var item in items) {
                string title = (string)item["summary"];
                string startTime = (string)(item["start"])["date"];
                ret.Add(DateTime.Parse(startTime), title);
            }

            return ret;
        }
        #endregion PublicMethods

        #region PrivateMethods
        #endregion PrivateMethods
    }
}
