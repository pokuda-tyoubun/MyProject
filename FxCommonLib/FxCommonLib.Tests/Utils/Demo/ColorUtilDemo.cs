using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Tests.Utils.Demo {
    public partial class ColorUtilDemo : Form {
        private ColorUtil _cu = new ColorUtil();

        public ColorUtilDemo() {
            InitializeComponent();
        }

        private void GetColorButton_Click(object sender, EventArgs e) {
            Color result = _cu.GetColor(this.Param1.Text);
            this.Result1.BackColor = result;
        }

        private void GetColorStringButton_Click(object sender, EventArgs e) {
            string result = _cu.GetColorString(this.Param2.BackColor);
            this.Result2.Text = result;
        }

        private void GetPaleRandomColorButton_Click(object sender, EventArgs e) {
            this.Result3_1.BackColor = _cu.GetPaleRandomColor(new Random(1));
            this.Result3_2.BackColor = _cu.GetPaleRandomColor(new Random(2));
            this.Result3_3.BackColor = _cu.GetPaleRandomColor(new Random(3));
        }

        private void GetRandomColorButton_Click(object sender, EventArgs e) {
            this.Result4_1.BackColor = _cu.GetRandomColor(new Random(1));
            this.Result4_2.BackColor = _cu.GetRandomColor(new Random(2));
            this.Result4_3.BackColor = _cu.GetRandomColor(new Random(3));
        }

        private void GetXorColorButton_Click(object sender, EventArgs e) {
            this.Result5.BackColor = _cu.GetXorColor(Param5.BackColor);
            this.Result5.Text = _cu.GetColorString(Result5.BackColor);
        }
    }
}
