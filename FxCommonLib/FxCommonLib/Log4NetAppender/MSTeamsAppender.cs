using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;

using log4net.Config;
using log4net.DateFormatter;
using log4net.Filter;
using log4net.Layout;
using log4net.ObjectRenderer;
using log4net.Plugin;
using log4net.Repository;
using log4net.Util;
using FxCommonLib.Utils;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace FxCommonLib.Log4NetAppender {
    /// <summary>
    /// Microsoft Teamsにエラーログを出力するLog4Net Custom Appender
    /// </summary>
    public class MSTeamsAppender : AppenderSkeleton {

        /// <summary>Microsoft TeamsのコネクタURL</summary>
        public string IncomingWebhookURL { get; set; }  

        /// <summary>
        /// Microsoft Teamsにログ追記
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent) {
            var mstu = new MSTeamsUtil();
            string msg = loggingEvent.MessageObject.ToString();
            if (msg.IndexOf("FxProcessableException") < 0) {
                //継続可能なException以外
                if (!Regex.IsMatch(loggingEvent.UserName, "^NT_KEIRI.*",RegexOptions.IgnoreCase) &&
                    !Regex.IsMatch(loggingEvent.UserName, "^HON0.*",RegexOptions.IgnoreCase)) {
                    //CCCユーザ以外であればTeamsに投稿
                    mstu.PostPlainMessage(IncomingWebhookURL, RenderLoggingEvent(loggingEvent));
                }
            }
        }
    }
}
