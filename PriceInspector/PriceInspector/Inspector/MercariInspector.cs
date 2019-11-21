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


    public class MercariInspector {

        private IWebDriver _webDriver = new ChromeDriver();

        public MercariInspector() {
            _webDriver.Url = @"https://www.mercari.com/jp/category/5/";
        }

        public List<Item> CreateMercariItemList() {
            List<Item> ret = new List<Item>();

            var root = _webDriver.FindElement(By.XPath("//div[@class='items-box-content clearfix category-brand-list']"));
            var itemList = root.FindElements(By.ClassName("items-box-body"));
            foreach (var element in itemList.AsEnumerable()) {
                //<div class="items-box-body">
                //  <h3 class="items-box-name font-2">
                //  <div class="xx">
                //    <div class="items-box-price font-5">
                var i = new Item();
                var title = element.FindElement(By.XPath("h3[@class='items-box-name font-2']"));
                i.ItemName = title.Text;
                var price = element.FindElement(By.XPath("div/div[@class='items-box-price font-5']"));
                i.MercariPrice = Decimal.Parse(price.Text.Replace("¥", ""));

                Trace.WriteLine(i.ItemName);
                Trace.WriteLine(i.MercariPrice);
                ret.Add(i);
            }

            return ret;
        }
    }
}
