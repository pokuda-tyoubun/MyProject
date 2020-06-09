using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrapySharpTest {
    public partial class Form1 : Form {

        private string _rootUrl = "http://078134995:Ais5vs2004@192.168.13.67/docs/html/kitei/index.htm";

        public Form1() {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e) {
            var browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            var pageReult = browser.NavigateToPage(new Uri(_rootUrl));

            this.ResultText.Text = pageReult.Content;
        }
    }
}
