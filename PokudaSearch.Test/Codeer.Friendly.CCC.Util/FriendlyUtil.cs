using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Ong.Friendly.FormsStandardControls;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using OpenCvSharp;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Codeer.Friendly.CCC.Util {
    /// <summary>
    /// Codeer.Friendly関連のユーティリティ
    /// </summary>
    public static class FriendlyUtil {
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int BM_CLICK = 0x00F5;
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;

        #region Public Method
        /// <summary>
        /// メッセージボックスのメッセージを取得
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        public static string GetMsgBoxMessage(WindowControl parentForm, Async async) {
            string ret = "";
            try {
                //メッセージを取得
                WindowControl msgBox = parentForm.WaitForNextModal();
                WindowControl msg = msgBox.IdentifyFromZIndex(0);
                ret = msg.GetWindowText();

                //MessageBoxを閉じる
                WindowControl okButton = msgBox.IdentifyFromWindowText("OK");
                okButton.SendMessage(BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            } finally {
                async.WaitForCompletion();
            }
            return ret;
        }

        /// <summary>
        /// マウスカーソルを指定座標に移動
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveMouseCursor(int x, int y) {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// マウスクリック
        /// </summary>
        public static void MouseClick() {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// TemplateMatchingした位置をクリック
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public static void TemplateMatchingClick(string templatePath, int offsetX = 0, int offsetY = 0) {
            Bitmap deskTopBmp = GetDeskTopBmp();

            try {
                OpenCvSharp.Point minPoint = new OpenCvSharp.Point();
                OpenCvSharp.Point maxPoint = new OpenCvSharp.Point();
                double minVal = 0;
                double maxVal = 0;
                Mat result = GetTemplateMatchingMat(templatePath, deskTopBmp);
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minPoint, out maxPoint);

                Debug.Print("minPoint:" + minPoint.ToString());
                Debug.Print("maxPoint:" + maxPoint.ToString());

                SetCursorPos(maxPoint.X + offsetX, maxPoint.Y + offsetY);
                MouseClick();
            } catch (OpenCvSharp.OpenCVException ee) {
                System.Diagnostics.Debug.WriteLine(ee.ErrMsg);
            }
        }
        /// <summary>
        /// 画像が見つかるまでスリープ
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="maxWaitTime"></param>
        public static void SleepUntilTemplateMatching(string templatePath, int maxWaitTime = 60000) {

            try {
                int count = 0;
                while (true) {
                    Thread.Sleep(1000);
                    if (maxWaitTime <= 1000 * count) {
                        break;
                    }

                    Bitmap deskTopBmp = GetDeskTopBmp();

                    OpenCvSharp.Point minPoint = new OpenCvSharp.Point();
                    OpenCvSharp.Point maxPoint = new OpenCvSharp.Point();
                    double minVal = 0;
                    double maxVal = 0;
                    Mat result = GetTemplateMatchingMat(templatePath, deskTopBmp);
                    Cv2.MinMaxLoc(result, out minVal, out maxVal, out minPoint, out maxPoint);

                    if (maxVal >= 0.9) {
                        //終了画像を見つけた
                        Trace.WriteLine("終了画像を見つけた:" + maxVal.ToString()); 
                        SetCursorPos(maxPoint.X + 10, maxPoint.Y + 10);
                        break;
                    }
                    count++;
                }
            } catch (OpenCvSharp.OpenCVException ee) {
                System.Diagnostics.Debug.WriteLine(ee.ErrMsg);
            }
        }
        /// <summary>
        /// WindowTitleからプロセスを取得する
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <returns></returns>
        public static Process GetProcessByWindowTitle(string windowTitle) {
            foreach (Process p in Process.GetProcesses()) {
                if (Regex.IsMatch(p.MainWindowTitle, windowTitle)) {
                    return p;
                }
            }
            return null;
        }
        #endregion Public Method
        #region Private Method
        /// <summary>
        /// デスクトップのBitmapを取得
        /// </summary>
        /// <returns></returns>
        private static Bitmap GetDeskTopBmp() {
            Bitmap deskTopBmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(deskTopBmp);
            try {
                //画面全体をコピーする
                g.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), deskTopBmp.Size);
            } finally {
                //解放
                g.Dispose();
            }

            return deskTopBmp;
        }
        /// <summary>
        /// TemplateMatchingの結果を取得
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="deskTopBmp"></param>
        /// <returns></returns>
        private static Mat GetTemplateMatchingMat(string templatePath, Bitmap deskTopBmp) {
            Mat matTarget = OpenCvSharp.Extensions.BitmapConverter.ToMat(deskTopBmp);
            Mat matTemplate = Cv2.ImRead(templatePath);

            Debug.Print("matTarget.Depth=" + matTarget.Depth().ToString());
            Debug.Print("matTemplate.Depth=" + matTemplate.Depth().ToString());
            Debug.Print("matTarget.Dims=" + matTarget.Dims().ToString());
            Debug.Print("matTemplate.Dims=" + matTemplate.Dims().ToString());
            //TargetとTemplateのTypeを揃える必要がある。
            Debug.Print("matTarget.Type=" + matTarget.Type().ToString());
            Debug.Print("matTemplate.Type=" + matTemplate.Type().ToString());

            Mat result = new Mat(matTarget.Height - matTemplate.Height + 1, matTarget.Width - matTemplate.Width + 1, MatType.CV_8UC1);
            //NOTE: [TemplateMatchModes.CCoeff]だと2回目マッチング時にずれる。
            Cv2.MatchTemplate(matTarget, matTemplate, result, TemplateMatchModes.CCoeffNormed);

            return result;
        }
        #endregion Private Method
    }
}
