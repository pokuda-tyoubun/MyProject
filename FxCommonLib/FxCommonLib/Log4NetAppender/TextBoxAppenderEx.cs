using FxCommonLib.Utils;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Log4NetAppender {
    /// <summary>
    /// log4net にテキストボックスへのアペンダを提供します。
    /// 開いているフォームの中から、
    /// 設定したフォーム名・テキストボックス名が一致するテキストボックスに書き込みを行います。
    /// </summary>
    public class TextBoxAppenderEx : AppenderSkeleton {
        /// <summary>
        /// ログを追記するメソッドのデリゲートです。
        /// </summary>
        /// <param name="loggingEvent">ロギングイベント。</param>
        protected delegate void AppendLogHandler(LoggingEvent loggingEvent);

        /// <summary>
        /// フォーム名を取得または設定します。
        /// </summary>
        /// <value>フォーム名。</value>
        public string FormName { get; set; }
        
        /// <summary>
        /// テキストボックス名を取得または設定します。
        /// </summary>
        /// <value>テキストボックス名。</value>
        /// <remarks>
        /// テキストボックスがタブやグループの中に存在する場合は、
        /// テキストボックス名の前にタブやグループの名前を"."区切りで設定してください。
        /// 例：tabControl1.tabPage1.textBox1
        /// </remarks>
        public string TextBoxName { get; set; }

        /// <summary>
        /// テキストボックスに出力する最大行数を取得または設定します。
        /// </summary>
        /// <value>テキストボックスに出力する最大行数。</value>
        /// <remarks>
        /// 最大行数を超えた場合、最初に出力されたログから順番に削除されます。
        /// 0以下を設定した場合、最大行数の設定は無効となります。
        /// </remarks>
        public int MaxLines { get; set; }

        public string PrefixFilter { get; set; }

        /// <summary>
        /// ログを出力する対象のフォームです。
        /// </summary>
        /// <value>ログを出力する対象のテキストボックス。</value>
        protected Form Form { get; set; }

        /// <summary>
        /// ログを出力する対象のテキストボックスです。
        /// </summary>
        /// <value>ログを出力する対象のテキストボックス。</value>
        protected TextBox TextBox { get; set; }

        /// <summary>
        /// ログをテキストボックスに追記します。
        /// 追記するテキストボックスが見つからない場合は何も行いません。
        /// </summary>
        /// <param name="loggingEvent">ロギングイベント</param>
        protected override void Append(LoggingEvent loggingEvent) {
            // 書き込み先のテキストボックスを特定します。
            if (TextBox == null || Form == null) {
                if (string.IsNullOrEmpty(FormName) || string.IsNullOrEmpty(TextBoxName)) {
                    return;
                }

                Form = Application.OpenForms[FormName];
                if (Form == null) {
                    return;
                }

                TextBox = FindControlRecursive(Form, TextBoxName) as TextBox;
                if (TextBox == null) {
                    return;
                }

                Form.FormClosing += (s, e) => { 
                    Form = null;
                    if (TextBox != null) {
                        TextBox.Clear();
                    }
                    TextBox = null; 
                };
            }

            if (Form.InvokeRequired) {
                // フォームのスレッドに合わせてログを追記します。
                // 出力元のスレッドを阻害しないように別スレッドで追記します。
                new Thread(InvokeAppendLog(loggingEvent)).Start();
            } else {
                AppendLog(loggingEvent);
            }
        }

        /// <summary>
        /// コントロールを再帰的に探索
        /// </summary>
        /// <param name="root"></param>
        /// <param name="textBoxName"></param>
        /// <returns></returns>
        private Control FindControlRecursive(Control root, string textBoxName) {
            if (root.Name == textBoxName) {
                return root;
            }
            foreach (Control c in root.Controls) {
                Control t = FindControlRecursive(c, textBoxName);
                if (t != null) {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// フォームに同期してログをテキストボックスに追記するメソッドを取得します。
        /// </summary>
        /// <param name="loggingEvent">ロギングイベント。</param>
        /// <returns>フォームに同期してログをテキストボックスに追記するメソッド。</returns>
        protected virtual ThreadStart InvokeAppendLog(LoggingEvent loggingEvent) {
            return () => {
                try {
                    Form.Invoke(new AppendLogHandler(AppendLog), new object[] { loggingEvent });
                } catch (ObjectDisposedException) {
                    // スレッドのタイミングによっては、解放した後に呼び出されてしまいます。
                    // あまり長くない期間なので、判定せずに例外をキャッチして無視しています。
                }
            };
        }

        /// <summary>
        /// ログをテキストボックスに追記する実体のメソッドです。
        /// 必要であれば対象のフォームと同じスレッドで書き込めるように分割しています。
        /// </summary>
        /// <param name="loggingEvent">ロギングイベント。</param>
        protected virtual void AppendLog(LoggingEvent loggingEvent) {
            RemoveUnnecessaryLines();

            if (Layout == null) {
                string msg = StringUtil.NullToBlank(loggingEvent.RenderedMessage);
                if (msg.IndexOf(this.PrefixFilter) == 0) {
                    TextBox.AppendText(msg + Environment.NewLine);
                }
            } else {
                TextBox.AppendText(RenderLoggingEvent(loggingEvent));
            }
        }

        /// <summary>
        /// テキストボックスが<seealso cref="MaxLines"/>を超えていれば、
        /// 余計な行を先頭から削除します。
        /// </summary>
        /// <remarks>
        /// 最大行数を超えた場合、最初に出力されたログから順番に削除されます。
        /// <seealso cref="MaxLines"/>に0以下を設定した場合、最大行数の設定は無効となります。
        /// </remarks>
        protected virtual void RemoveUnnecessaryLines() {
            // 0以下は無効とみなします。
            if (MaxLines <= 0) {
                return;
            }

            var lines = TextBox.Lines;
            if (lines.Length <= MaxLines) {
                return;
            }

            var removeLines = lines.Length - MaxLines;
            TextBox.Text = string.Join(
                Environment.NewLine,
                lines,
                removeLines,
                lines.Length - removeLines);
        }
    }
}
