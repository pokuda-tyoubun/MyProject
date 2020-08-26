using System.Net.Mail;

namespace FxCommonLib.Utils {
    /// <summary>
    /// メール操作ユーティリティ
    /// </summary>
    public class MailUtil {

        #region Properties
        /// <summary>Smtpアドレス</summary>
        public string SmtpAddress { get; set; }
        /// <summary>ポート番号</summary>
        public int SmtpPort  { get; set; }
        #endregion Properties

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="smtpAddress"></param>
        /// <param name="smtpPort"></param>
        public MailUtil( string smtpAddress, int smtpPort) {
            this.SmtpAddress = smtpAddress;
            this.SmtpPort = smtpPort;
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="fromAddress">送信元アドレス</param>
        /// <param name="toAddress">送信先アドレス</param>
        /// <param name="subject">件名</param>
        /// <param name="body">メール本文</param>
        public void SendMail(string fromAddress, string toAddress, string subject, string body) {
            MailMessage msg = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try {
                msg.From = new MailAddress(fromAddress, "ecoLLabo MES");
                msg.To.Add(new MailAddress(toAddress, ""));
                //件名
                msg.Subject = subject;
                //本文
                msg.Body = body;

                //SMTPサーバーなどを設定する
                sc.Host = this.SmtpAddress;
                sc.Port = this.SmtpPort;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                //メッセージを送信する
                sc.Send(msg);
            } finally {
                msg.Dispose();
                sc.Dispose();
            }
        }
        #endregion PublicMethods
    }
}
