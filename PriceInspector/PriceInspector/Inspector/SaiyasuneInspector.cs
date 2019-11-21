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


    public class SaiyasuneInspector {

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
                var goodsList = _webDriver.FindElements(By.ClassName("goods_content"));
                for (int i = 0; i < 1; i++) {

                }
            }
        } 
    }
}
