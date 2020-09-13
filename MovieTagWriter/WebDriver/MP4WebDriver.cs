using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieTagWriter.WebDriver {
    public class MP4WebDriver {

        public MP4WebDriver() {
        }


        public void ReportProgress(BackgroundWorker bw, int molecule, int denominator, string msg) {
            //プログレスバー更新
            int p = (int)(((double)molecule / (double)denominator) * 100);
            bw.ReportProgress(p, molecule.ToString() + "/" + denominator.ToString() + " " + msg);

            TaskbarManager.Instance.SetProgressValue(molecule, denominator);
        }

        public List<KeyValuePair<FileInfo, TagInfo>> GetTagInfo(string targetDir, BackgroundWorker bw) {
            int targetCount = 0;
            int finishedCount = 0;
            int skippedCount = 0;
            var resultList = new List<KeyValuePair<FileInfo, TagInfo>>();

            try {

                var fileList = FileUtil.GetAllFileInfo(targetDir);
                var targetList = new List<FileInfo>();
                foreach (var fi in fileList) {
                    if (fi.Extension.ToLower() == ".mp4") {
                        targetList.Add(fi);
                    }
                }
                targetCount = targetList.Count;
                //プログレスバー更新
                ReportProgress(bw, finishedCount + skippedCount, targetCount, "タグ情報取得中...");

                MP4WebDriver mp4wd = new MP4WebDriver();
                foreach (var fi in targetList) {
                    TagInfo ti = mp4wd.GetTagInfoFromDMM(fi);
                    resultList.Add(new KeyValuePair<FileInfo, TagInfo>(fi, ti));

                    if (StringUtil.NullToBlank(ti.Title) == "") {
                        skippedCount++;
                    } else {
                        finishedCount++;
                    }

                    //進捗度更新を呼び出し。
                    ReportProgress(bw, finishedCount + skippedCount, targetCount, "タグ情報取得中...");

                    if (bw.CancellationPending) {
                        return resultList;
                    }
                }
                //進捗度更新を呼び出し。
                ReportProgress(bw, finishedCount + skippedCount, targetCount, "完了");
            } catch (WebException) {
                MessageBox.Show("Webから情報を取得できませんでした。ネットワーク環境を確認して下さい。", 
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultList;
        }

        public TagInfo GetTagInfoFromDMM(FileInfo file) {
            var ret = new TagInfo();
            string fileName = file.Name.Replace(file.Extension, "");

            if (fileName == "") {
                return ret;
            }

            //FANZA--------------------------------
            var util = new DMMWebAPIUtil();
            var list = util.GetItemList("FANZA", fileName);
            if (list.Count > 0) {
                ret = list[0];
                //コメントはWebから取得
                //HACK 年齢認証の問題を解消する必要がある。
                //await AppObject.CefSharpPanel.LoadPageAsync(ret.pageUrl);
                //var comment = await AppObject.CefSharpPanel.GetTextContentByXPath("//*[@id='mu']/div/table/tbody/tr/td[1]/div[4]/p");
                //ret.Comment = comment;

                //ret.Comment = "監督:" + ret.Director + Environment.NewLine + "メーカー:" + ret.Maker;
                ret.Comment = "";

                return ret;
            }

            //名前を最適化して再検索
            var match = Regex.Match(fileName, "^[a-z].*[0-9]");
            if (match.Value == "") {
                return ret;
            }
            list = util.GetItemList("FANZA", match.Value);
            if (list.Count > 0) {
                ret = list[0];
                //ret.Comment = "監督:" + ret.Director + Environment.NewLine + "メーカー:" + ret.Maker;
                ret.Comment = "";

                return ret;
            }
            //DMM.com(一般)--------------------------------
            list = util.GetItemList("DMM.com", match.Value);
            if (list.Count > 0) {
                ret = list[0];
                //ret.Comment = "監督:" + ret.Director + Environment.NewLine + "メーカー:" + ret.Maker;
                ret.Comment = "";

                return ret;
            }

            return ret;
        }
    }
}
