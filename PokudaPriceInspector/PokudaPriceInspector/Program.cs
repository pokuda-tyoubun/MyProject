using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using PokudaPriceInspector.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaPriceInspector {
    static class Program {


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainFrameForm());
            Application.Run(new PriceCompareForm());

        }
    }
}
