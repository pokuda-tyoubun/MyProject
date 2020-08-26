using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Tests.Controls.Demo {
    public partial class TextBoxExDemo : Form {
        public TextBoxExDemo() {
            InitializeComponent();
        }

        private void TextBoxExDemo_Load(object sender, EventArgs e) {
            this.textBoxEx1.NGWordRegex = "a";
        }
    }
}
