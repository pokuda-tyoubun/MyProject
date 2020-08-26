using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    public class GoogleMapsApiUtil {

        public enum MovingMode : int {
            driving = 1, //車
            //transit, //電車 ←日本ではサポート外
            walking, //徒歩
            //bicycling //自転車 ←日本ではサポート外
        }

        /// <summary>
        /// ２点間の距離を取得
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static double GetDistance(string origin, string destination, string apiKey) {
            double ret = 0;
            WebResponse response = null;
            try {
                string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + 
                    origin + "&destinations=" + destination + "&key=" + apiKey;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sReader = new StreamReader(dataStream);
                string jsonString = sReader.ReadToEnd();
                Debug.Write(jsonString);

                var jo = JObject.Parse(jsonString);
                JToken jt = jo.SelectToken("$.rows..elements..distance.value");
                ret = double.Parse(StringUtil.NullBlankToZero(jt.ToString()));

                return ret;
            } finally {
                response.Close();
            }
        }

        /// <summary>
        /// 2点間の移動時間を取得
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static double GetDuration(string origin, string destination, MovingMode mode, string apiKey) {
            double ret = 0;
            WebResponse response = null;
            try {
                string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?origins=" +
                    origin + "&destinations=" + destination + "&mode=" + mode.ToString() + "&key=" + apiKey;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sReader = new StreamReader(dataStream);
                string jsonString = sReader.ReadToEnd();
                Debug.Write(jsonString);

                var jo = JObject.Parse(jsonString);
                JToken jt = jo.SelectToken("$.rows..elements..duration.value");
                ret = double.Parse(StringUtil.NullBlankToZero(jt.ToString()));

                return ret;
            } finally {
                response.Close();
            }
        }
    }
}
