using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaPriceInspector {
    public partial class MainFrameForm : Form {

        //private IWebDriver _webDriver = new ChromeDriver();
        //private IWebDriver _webDriver = new InternetExplorerDriver();

        public MainFrameForm() {
            InitializeComponent();


            //IWebDriver ie = new InternetExplorerDriver();
            //ie.Url = @"https://www.google.co.jp";
            //IWebElement element = ie.FindElement(By.Name("q"));
            //element.SendKeys("Cheese!");
            //element.Submit();


            //_webDriver.Url = @"https://www.amazon.co.jp/";

            //IWebElement element = _webDriver.FindElement(By.Id("twotabsearchtextbox"));
            //element.SendKeys("selenium");
            //Thread.Sleep(1000);

            // 上記取得した要素に対してテキストを入力してサブミット

        }

        private void MainFrameForm_Load(object sender, EventArgs e) {
        }

        private void RunButton_Click(object sender, EventArgs e) {
            //NOTE 画面上では動作しない。コンソールアプリで実装する必要があるようだ

            IWebDriver _webDriver = new InternetExplorerDriver();
            _webDriver.Url = @"https://www.amazon.co.jp/";

            // #lst-ibの要素を取得する
            IWebElement element = _webDriver.FindElement(By.Id("twotabsearchtextbox"));
            element.SendKeys("selenium");
        }
    }
}
