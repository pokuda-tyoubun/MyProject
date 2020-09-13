using FxCommonLib.Consts;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.WebDriver {
    public class MP4WebDriver {

        public MP4WebDriver() {
        }
        public TagInfo GetTagInfoFromFanza(FileInfo file) {
            var ret = new TagInfo();
            //string fileName = file.Name.Replace(file.Extension, "");

            //if (fileName == "") {
            //    return ret;
            //}

            //var util = new DMMWebAPIUtil();
            //var list = util.GetItemList(fileName);
            //if (list.Count > 0) {
            //    ret = list[0];
            //    //コメントはWebから取得
            //    //HACK 年齢認証の問題を解消する必要がある。
            //    //await AppObject.CefSharpPanel.LoadPageAsync(ret.pageUrl);
            //    //var comment = await AppObject.CefSharpPanel.GetTextContentByXPath("//*[@id='mu']/div/table/tbody/tr/td[1]/div[4]/p");
            //    //ret.Comment = comment;
            //    ret.Comment = "監督:" + ret.Director + Environment.NewLine + "メーカー:" + ret.Maker;
            //}

            ////名前を最適化して再検索
            //var match = Regex.Match(fileName, "^[a-z].*[0-9]");
            //if (match.Value == "") {
            //    return ret;
            //}
            //list = util.GetItemList(match.Value);
            //if (list.Count > 0) {
            //    ret = list[0];
            //    //コメントはWebから取得
            //    //HACK 年齢認証の問題を解消する必要がある。
            //    //await AppObject.CefSharpPanel.LoadPageAsync(ret.pageUrl);
            //    //var comment = await AppObject.CefSharpPanel.GetTextContentByXPath("//*[@id='mu']/div/table/tbody/tr/td[1]/div[4]/p");
            //    //ret.Comment = comment;
            //    ret.Comment = "監督:" + ret.Director + Environment.NewLine + "メーカー:" + ret.Maker;
            //}

            return ret;
        }

        public async Task<TagInfo> GetTagInfoFromFanza2(FileInfo file) {
            string url = "https://www.dmm.co.jp/mono/dvd/";

            TagInfo info = new TagInfo();

            try {
                await AppObject.CefSharpPanel.LoadPageAsync(url);

                //年齢確認(以下では失敗する場合が多いので手動で認証)
                //string test = await AppObject.CefSharpPanel.GetTextContentByClassName("ageCheck__linkText");
                //await AppObject.CefSharpPanel.AnchorClickByClassName("ageCheck__link ageCheck__link--r18");
                //var list = await AppObject.CefSharpPanel.GetLinks();
                //await AppObject.CefSharpPanel.LoadPageAsync(list[1]);

                //キーワードセット
                string fileName = file.Name.Replace(file.Extension, "");
                await AppObject.CefSharpPanel.SetInputValueById("naviapi-search-text", fileName);
                Thread.Sleep(1000);
                //検索
                await AppObject.CefSharpPanel.ClickById("naviapi-search-submit");
                Thread.Sleep(3000);

                //検索結果
                //await AppObject.CefSharpPanel.ClickByXPath("//*[@id=\'list\']/li[1]/div/p[2]/a");
                var searchList = await AppObject.CefSharpPanel.GetLinks();
                string tmpUrl = searchList.Where(x => x.Contains("ord=1")).First();
                await AppObject.CefSharpPanel.LoadPageAsync(tmpUrl);
                Thread.Sleep(3000);

                //タイトルを取得
                //var title = await AppObject.CefSharpPanel.GetTextContentById("title");
                var title = await AppObject.CefSharpPanel.GetTextContentById("//*[@id='title']/");
                info.Title = title;
                //演者
                var performer = await AppObject.CefSharpPanel.GetTextContentById("performer");
                info.Performers = performer;
                //ジャンル
                var genreList = await AppObject.CefSharpPanel.GetTextContentsByXPath("//td[text()=\'ジャンル：\']/../td[2]/a");
                foreach (string tmp in genreList) {
                    info.Genres += tmp + " ";
                }

                //try {
                //    var resultListElem = _webDriver.FindElements(By.XPath("//*[@id='list']/li[1]/div/p[2]/a"));
                //    foreach (var element in resultListElem.AsEnumerable()) {
                //        element.Click();

                //        //情報を取得
                //        var title = _webDriver.FindElement(By.XPath("//*[@id='title']"));
                //        info.Title = title.Text;
                //        var performer = _webDriver.FindElement(By.XPath("//*[@id='performer']/a"));
                //        info.Performers = performer.Text;

                //        //ジャンル
                //        try {
                //            var genreList = _webDriver.FindElements(By.XPath("//td[text()='ジャンル：']/../td[2]/a"));
                //            foreach (var genre in genreList.AsEnumerable()) {
                //                info.Genres += genre.Text + " ";
                //            }
                //        } catch (NoSuchElementException) {
                //            //何もしない
                //        }

                //        //イメージ
                //        try {
                //            var imageDiv = _webDriver.FindElement(By.XPath("//*[@id='sample-video']"));
                //            var image = imageDiv.FindElement(By.XPath("div/a/img"));
                //            info.ImageUrl = image.GetAttribute("src");
                //        } catch (NoSuchElementException) {
                //            //何もしない
                //        }

                //        //コメント
                //        try {
                //            var commentDiv = _webDriver.FindElement(By.XPath("//*[@id='mu']/div/table/tbody/tr/td[1]/div[2]"));
                //            var comment = commentDiv.FindElement(By.XPath("div/p"));
                //            info.Comment = comment.Text.Split('\n')[0];
                //        } catch (NoSuchElementException) {
                //            //何もしない
                //        }

                //        break;
                //    }

            } finally {

            }
            return info;
        }

    }
}
