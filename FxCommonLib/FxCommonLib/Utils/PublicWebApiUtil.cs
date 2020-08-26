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
    public class PublicWebApiUtil {

        #region Constants
        #endregion Constants

        #region MemberVariables
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PublicWebApiUtil() {
        }
        #endregion Constractors

        #region PublicMethods
        #endregion PublicMethods

        #region ProtectedMethods
        /// <summary>
        /// WebAPI呼び出し
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string GetResponseString(string url) {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            StreamReader sr = null;
            string jsonString = "";
            try {
                //サーバーからデータを受信する
                req = (HttpWebRequest)WebRequest.Create(url);
                res = (HttpWebResponse)req.GetResponse();
                sr = new StreamReader(res.GetResponseStream());
                //すべてのデータを受信する
                jsonString = sr.ReadToEnd();
            } finally {
                sr.Close();
                res.Close();
            }
            return jsonString;
        }
        #endregion PrivateMethods
    }
}
