using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    public class MSTeamsUtil {

        public MSTeamsUtil() {
        }
        /// <summary>
        /// HttpClientを使ってメッセージを投稿
        /// </summary>
        //[Obsolete]
        //public async void PostMessageByHttpClient(string uri, string message) {
        //    var param = new Dictionary<string, string>();
        //    // とりあえずTextパラメータは必須
        //    param["summary"] = "C# Post";
        //    param["title"] = message;
        //    var json = JsonConvert.SerializeObject(param);

        //    var content = new StringContent(json, Encoding.UTF8);
        //    //FIXME ここで失敗する。PowerShell投稿は成功している。
        //    HttpResponseMessage response = await _client.PostAsync(uri, content);
        //    if (!response.IsSuccessStatusCode) {
        //        Debug.Print("error");
        //    }
        //}


        /// <summary>
        /// Microsoft Temasに平文メッセージを投稿
        /// </summary>
        /// <param name="webhookURL">Incoming WebhooksのURL</param>
        /// <param name="message">平文メッセージ</param>
        public void PostPlainMessage(string webhookURL, string message) {
            using (var client = new WebClient()) {
                var param = new Dictionary<string, string>();
                // Textパラメータは必須
                param["Text"] = message;
                var json = JsonConvert.SerializeObject(param);

                client.Headers.Add(HttpRequestHeader.ContentType, "application/json;charset=UTF-8");
                client.Encoding = Encoding.UTF8;
                client.UploadString(webhookURL, json);
            }
        }
    }
}
