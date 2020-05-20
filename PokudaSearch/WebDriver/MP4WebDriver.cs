using FxCommonLib.Consts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.WebDriver {
    public struct TagInfo {
        public string Title;
        public string Performers;
        public string Genres;
        public string Comment;
        public string ImageUrl;
    }

    public class MP4WebDriver {
        private IWebDriver _webDriver = new ChromeDriver();

        public MP4WebDriver() {
        }

        public TagInfo GetTagInfoFromFanza(FileInfo file) {
            _webDriver.Url = "https://www.dmm.co.jp/mono/dvd/";
            TagInfo info = new TagInfo();

            try {
                string fileName = file.Name.Replace(file.Extension, "");

                var keywordElem = _webDriver.FindElement(By.XPath("//*[@id='searchstr']"));
                keywordElem.Clear();
                keywordElem.SendKeys(fileName);
                var submit = _webDriver.FindElement(By.XPath("//*[@id='frmSearch']/fieldset/div/div[2]/input"));
                submit.Click();
                Thread.Sleep(1000);

                try {
                    var resultListElem = _webDriver.FindElements(By.XPath("//*[@id='list']/li[1]/div/p[2]/a"));
                    foreach (var element in resultListElem.AsEnumerable()) {
                        element.Click();

                        //情報を取得
                        var title = _webDriver.FindElement(By.XPath("//*[@id='title']"));
                        info.Title = title.Text;
                        var performer = _webDriver.FindElement(By.XPath("//*[@id='performer']/a"));
                        info.Performers = performer.Text;

                        //ジャンル
                        try {
                            var genreList = _webDriver.FindElements(By.XPath("//td[text()='ジャンル：']/../td[2]/a"));
                            foreach (var genre in genreList.AsEnumerable()) {
                                info.Genres += genre.Text + " ";
                            }
                        } catch (NoSuchElementException) {
                            //何もしない
                        }

                        //イメージ
                        try {
                            var imageDiv = _webDriver.FindElement(By.XPath("//*[@id='sample-video']"));
                            var image = imageDiv.FindElement(By.XPath("div/a/img"));
                            info.ImageUrl = image.GetAttribute("src");
                        } catch (NoSuchElementException) {
                            //何もしない
                        }

                        //コメント
                        try {
                            var commentDiv = _webDriver.FindElement(By.XPath("//*[@id='mu']/div/table/tbody/tr/td[1]/div[2]"));
                            var comment = commentDiv.FindElement(By.XPath("div/p"));
                            info.Comment = comment.Text.Split('\n')[0];
                        } catch (NoSuchElementException) {
                            //何もしない
                        }

                        break;
                    }

                    return info;
                } catch (NoSuchElementException) {
                    MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_CANNOT_GET_INFO), 
                        AppObject.GetMsg(AppObject.Msg.TITLE_WARN), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return info;
                }
            } finally {
                _webDriver.Close();
                _webDriver.Quit();
            }

        }

    }
}
