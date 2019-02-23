using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class IndexBuilderConfigForm : Form {
        public IndexBuilderConfigForm() {
            InitializeComponent();
        }

        private void RAMBufferSizeBar_ValueChanged(object sender, EventArgs e) {
            this.RAMBufferSizeText.Text = this.RAMBufferSizeBar.Value.ToString();
        }
    }
}
