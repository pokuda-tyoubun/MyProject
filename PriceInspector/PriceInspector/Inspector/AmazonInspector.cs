using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceInspector.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInspector.Inspector {


    public class AmazonInspector {

        private IWebDriver _webDriver = new ChromeDriver();

        public AmazonInspector(string targetUrl) {
            _webDriver.Url = targetUrl;
        }

        public List<Item> CreateItemList(string keyword, int count) {
            List<Item> ret = new List<Item>();

            //キーワードで検索
            var keywordElem = _webDriver.FindElement(By.XPath("//input[@name='field-keywords']"));
            keywordElem.Clear();
            keywordElem.SendKeys(keyword);
            var submit = _webDriver.FindElement(By.XPath("//input[@type='submit']"));
            submit.Click();

            //品目リストを取得
            var itemList = _webDriver.FindElements(By.XPath("//div[@class='sg-row']"));
            int i = 1;
            foreach (var element in itemList.AsEnumerable()) {
                var item = new Item();
                try {
                    var span = element.FindElement(By.XPath("div[3]/div/div[1]/h2/a/span"));
                    item.ItemName = span.Text;
                    var price = element.FindElement(By.XPath("div[4]/div/div[1]/div[2]/div/a/span/span[2]/span[2]"));
                    item.AmazonJpPrice = Decimal.Parse(price.Text.Replace("¥", ""));

                    ret.Add(item);
                    i++;
                } catch (NoSuchElementException) {
                    //何もしない
                }

                //TODO 相対パスで子孫のなかから絞り込む方法を調査

                if (i > count) {
                    break;
                }
            }
            return ret;
        }

        public List<Item> CreateMercariItemList() {
            List<Item> ret = new List<Item>();

            var root = _webDriver.FindElement(By.XPath("//div[@class='items-box-content clearfix category-brand-list']"));
            var itemList = root.FindElements(By.ClassName("items-box"));
            foreach (var element in itemList.AsEnumerable()) {
                //<div class="items-box-body">
                //  <h3 class="items-box-name font-2">
                //  <div class="xx">
                //    <div class="items-box-price font-5">
                var i = new Item();
                var title = element.FindElement(By.XPath("a/div/h3[@class='items-box-name font-2']"));
                i.ItemName = title.Text;
                var price = element.FindElement(By.XPath("a/div/div/div[@class='items-box-price font-5']"));
                i.MercariPrice = Decimal.Parse(price.Text.Replace("¥", ""));

                var a = element.FindElement(By.XPath("a"));
                i.MercariUrl = a.GetAttribute("href");

                Trace.WriteLine(i.ItemName);
                Trace.WriteLine(i.MercariPrice);
                Trace.WriteLine(i.MercariUrl);
                ret.Add(i);
            }

            return ret;
        }
    }
}
