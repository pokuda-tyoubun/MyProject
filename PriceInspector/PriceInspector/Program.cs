using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceInspector.Inspector;
using PriceInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInspector {
    class Program {

        static void Main(string[] args) {
            //メルカリ(本、DVD)最新情報取得
            var mi = new MercariInspector(@"https://www.mercari.com/jp/category/72/");
            var list = mi.CreateMercariItemList();
            //最安値ドットコムの価格と比較
            var si = new SaiyasuneInspector();
            si.SetSaiyasunePrice(list);
            si.WriteCandidateCsv();

            /*
            //メルカリ(本、DVD)最新情報取得
            var mi = new MercariInspector(@"https://www.mercari.com/jp/category/5/");
            var list = mi.CreateMercariItemList();
            //最安値ドットコムの価格と比較
            var si = new SaiyasuneInspector();
            si.SetSaiyasunePrice(list);
            si.WriteCandidateCsv();

            //メルカリ(おもちゃ)最新情報取得
            mi = new MercariInspector(@"https://www.mercari.com/jp/category/1328/");
            list = mi.CreateMercariItemList();
            //最安値ドットコムの価格と比較
            si = new SaiyasuneInspector();
            si.SetSaiyasunePrice(list);
            si.WriteCandidateCsv();

            //メルカリ(おもちゃ)最新情報取得
            mi = new MercariInspector(@"https://www.mercari.com/jp/category/8/");
            list = mi.CreateMercariItemList();
            //最安値ドットコムの価格と比較
            si = new SaiyasuneInspector();
            si.SetSaiyasunePrice(list);
            si.WriteCandidateCsv();
            */

            //--輸出用--------------------------------------
            //「日本のおもちゃ」、「アニメ関連」?
            //Amazon.co.jpで、任意のキーワードで検索
            //var ai = new AmazonInspector(@"https://www.amazon.co.jp/");
            //var list = ai.CreateItemList("C#", 10);
            //foreach (Item i in list) {
            //    i.TraceAll();
            //}
            //上位Ｎ件のASINと価格を取得
            //Amazon.comで価格を取得
            //eBayで価格を取得

            //----------------------------------------
            //Amazon.co.jpの並行輸入品リストを取得

        //メルカリ価格を調査
        }
    }
}
