using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceInspector.Inspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInspector {
    class Program {

        static void Main(string[] args) {
            var mi = new MercariInspector();
            var list = mi.CreateMercariItemList();

            var si = new SaiyasuneInspector();
            si.SetSaiyasunePrice(list);
        }
    }
}
