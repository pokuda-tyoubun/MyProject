using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.SandBox {
    public partial class ThumbnailForm : Form {
        public ThumbnailForm() {
            InitializeComponent();
        }

        private void PreviewButton_Click(object sender, EventArgs e) {

        }


        private Bitmap CreateThumbnail(string path, int scale) {
            // ファイルが存在した場合
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                ShellFile shellFile = ShellFile.FromFilePath(path);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;
                int w = (int)(bmp.Width * scale);
                int h = (int)(bmp.Height * scale);
                return bmp;
            }
 
            // ファイルが存在しない場合はデフォルト表示
            return SampleThumbnailView.Properties.Resources.Message;
        }
    }
}
