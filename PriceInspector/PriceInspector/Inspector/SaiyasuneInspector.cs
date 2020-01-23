using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceInspector.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FxCommonLib.Utils;
using System.Data;

namespace PriceInspector.Inspector {


    public class SaiyasuneInspector {

        private List<Item> _profitCandidates = new List<Item>();

        private IWebDriver _webDriver = new ChromeDriver();

        public SaiyasuneInspector() {
            _webDriver.Url = @"https://www.saiyasune.com/";
        }

        public void SetSaiyasunePrice(List<Item> itemList) {

            foreach (var item in itemList) {
                Trace.WriteLine(item.ItemName);
                Trace.WriteLine(item.MercariPrice);

                var keyword = _webDriver.FindElement(By.XPath("//input[@name='keywords']"));
                keyword.Clear();
                keyword.SendKeys(item.ItemName);
                var submit = _webDriver.FindElement(By.XPath("//input[@name='submit']"));
                submit.Click();

                //検索結果より価格取得
                var goodsList = _webDriver.FindElements(By.ClassName("goods_content_j"));
                int i = 0;
                int max = 1;
                foreach (var element in goodsList.AsEnumerable()) {
                    var button = element.FindElement(By.XPath("a/div/div[@class='btn_btn_i']"));
                    button.Click();

                    i++;
                    if (i >= max) {
                        try {
                            var janCode = _webDriver.FindElement(By.XPath("//div[@id='j_ti_l']"));
                            item.Code = janCode.Text;
                        } catch {
                            //スルー
                        }
                        
                        var tmp = _webDriver.FindElement(By.XPath("//div[@id='kakaku_r_saiyasu']"));
                        //Trace.WriteLine(tmp.TagName);
                        try {
                            var newPrice = tmp.FindElement(By.XPath("span/a/span[@id='krs3']"));
                            Trace.WriteLine(newPrice.Text);
                            item.SaiyasuneNewPrice = Decimal.Parse(StringUtil.NullBlankToZero(newPrice.Text.Replace("¥", "").Replace(",", "")));
                        } catch {
                            //スルー
                        }

                        tmp = _webDriver.FindElement(By.XPath("//div[@id='mslt_r_t1u']"));
                        try { 
                            var oldPrice = tmp.FindElement(By.XPath("a"));
                            item.SaiyasuneOldPrice = Decimal.Parse(StringUtil.NullBlankToZero(oldPrice.Text.Replace("¥", "").Replace(",", "")));
                        } catch {
                            //スルー
                        }

                        if (item.Profit() > 2000) {
                            item.SaiyasuneUrl = _webDriver.Url;
                            item.TraceAll();
                            _profitCandidates.Add(item);
                        }
                        break;
                    }
                }
            }
        }
        public void WriteCandidateCsv() {
            DataTable csv = Item.CreateBlankTable();
            foreach (var item in _profitCandidates) {
                DataRow dr = csv.NewRow();
                dr[0] = item.Code;
                dr[1] = item.ItemName;
                dr[2] = item.MercariPrice;
                dr[3] = item.MercariUrl;
                dr[4] = item.SaiyasuneNewPrice;
                dr[5] = item.SaiyasuneOldPrice;
                dr[6] = item.SaiyasuneUrl;
                dr[7] = item.Profit();
                csv.Rows.Add(dr);
            }
            csv.AcceptChanges();
            var csvUtil = new CSVUtil();
            csvUtil.WriteCsv(csv, @".\購入候補_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv", true);
        }
    }
}
