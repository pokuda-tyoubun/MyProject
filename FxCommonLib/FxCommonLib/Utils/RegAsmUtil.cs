using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Utils {
    /// <summary>
    /// RegAsm.exeを呼び出して、引数のアセンブリを登録します。
    /// </summary>
    public class RegAsmUtil {

        public static string StdOutputs = "";

        public static void RegistAssembly(string[] args) {
            string path = System.IO.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "RegAsm.exe");

            Process p = new Process();
            try {
                p.StartInfo.FileName = path;
                // 渡されたコマンドライン引数をそのまま渡す
                StringBuilder buff = new StringBuilder(128);
                foreach(string arg in args) {
                    buff.Append(arg + " ");
                }
                p.StartInfo.Arguments = buff.ToString();
                // 出力を取得できるようにする
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                // ウィンドウを表示しない
                p.StartInfo.CreateNoWindow = true;

                // 起動
                p.Start();

                // 出力を取得
                string outputs = p.StandardOutput.ReadToEnd();
                string errors = p.StandardError.ReadToEnd();
                // プロセス終了まで待機する
                p.WaitForExit();
                // 出力された結果を表示
                Debug.WriteLine(outputs);
                Debug.WriteLine(errors);
                StdOutputs += outputs;
                StdOutputs += errors;
            } finally {
                p.Close();
            }
        }

        /// <summary>
        /// 指定したProgIDが既に登録されているかどうか判定
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static bool IsAlreadyRegisted(string progId) {
            bool ret = false;
            COMUtil comUtil = new COMUtil();

            try {
                object tmp = comUtil.CreateObject(progId);
                ret = true;
                comUtil.MReleaseComObject(tmp);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            return ret;
        }
    }
}
