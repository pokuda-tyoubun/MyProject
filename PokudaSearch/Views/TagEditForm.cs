using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PokudaSearch.WebDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class TagEditForm : Form {

        private TagLib.File _file = null;

        public TagEditForm() {
            InitializeComponent();
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            SaveTag();
            //this.Close();
        }

        private void ShowTag(TagLib.File file) {
            this.TitleText.Text = file.Tag.Title;
            this.ArtistText.Text = string.Join(", ", file.Tag.Performers);
            this.GenresText.Text = string.Join(", ", file.Tag.Genres);
            this.LylicsText.Text = file.Tag.Lyrics;
            this.CommentText.Text = file.Tag.Comment;
        }

        private void ShowFileInfo(FileInfo fi) {
            var file = ShellFile.FromFilePath(fi.FullName);

        　　this.TitleText.Text = StringUtil.NullToBlank(file.Properties.System.Title.Value);
            this.ArtistText.Text = string.Join(", ", StringUtil.NullToBlank(file.Properties.System.Author.Value));
            this.GenresText.Text = "";
            this.LylicsText.Text = "";
            this.CommentText.Text = string.Join(", ", StringUtil.NullToBlank(file.Properties.System.Comment.Value));
        }

        private void SaveTag() {
            if (_file != null) {
                _file.Tag.Title = this.TitleText.Text;
                //アーティスト
                _file.Tag.Performers = new string[] { this.ArtistText.Text };
                //ジャンル
                _file.Tag.Genres = new string[] { this.GenresText.Text };
                //歌詞
                _file.Tag.Lyrics = this.LylicsText.Text;
                //コメント
                _file.Tag.Comment = this.CommentText.Text;

                //画像
                string imgPath = this.ImagePathText.Text;
                if (File.Exists(imgPath) && (
                    new FileInfo(imgPath).Extension.ToLower() == ".jpg" ||
                    new FileInfo(imgPath).Extension.ToLower() == ".jpeg")) {

                    var ic = new System.Drawing.ImageConverter();
                    var ba = (byte[])ic.ConvertTo(Image.FromFile(imgPath), typeof(byte[]));
                    var byteVector = new TagLib.ByteVector(ba);
                    var pic = new TagLib.Picture(byteVector);
                    pic.Type = TagLib.PictureType.FrontCover;
                    pic.Description = "Cover";
                    pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                    _file.Tag.Pictures = new TagLib.IPicture[] { pic };
                } else {
                    if (this.PackagePicture.Image != null) {
                        var ic = new System.Drawing.ImageConverter();
                        var ba = (byte[])ic.ConvertTo(this.PackagePicture.Image, typeof(byte[]));
                        var byteVector = new TagLib.ByteVector(ba);
                        var pic = new TagLib.Picture(byteVector);
                        pic.Type = TagLib.PictureType.FrontCover;
                        pic.Description = "Cover";
                        pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                        _file.Tag.Pictures = new TagLib.IPicture[] { pic };
                    }
                }
                _file.Save();
            } else {
                var file = ShellFile.FromFilePath(this.TargetPathText.Text);

                ShellPropertyWriter propertyWriter =  file.Properties.GetPropertyWriter();
                propertyWriter.WriteProperty(SystemProperties.System.Title, new string[] { this.TitleText.Text });
                propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { this.ArtistText.Text });
                propertyWriter.WriteProperty(SystemProperties.System.Comment, new string[] { this.GenresText.Text });
                propertyWriter.Close();
            }

            //リネーム
            var f = new FileInfo(this.TargetPathText.Text);
            f.MoveTo(f.DirectoryName + @"\" + this.TitleText.Text + ".mp4"); 
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 差分ツール選択ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefButton_Click(object sender, EventArgs e) {
        }

        private void TargetPathText_Leave(object sender, EventArgs e) {
            string path = this.TargetPathText.Text;
            if (File.Exists(path)) {
                try {
                    _file = TagLib.File.Create(path);
                    ShowTag(_file);
                } catch {
                    //ヘッダー情報が無い場合
                }
                var fi = new FileInfo(path);
                ShowFileInfo(fi);
            }
        }

        private void GetInfoButton_Click(object sender, EventArgs e) {
            var mp4wd = new MP4WebDriver();
            TagInfo ti = mp4wd.GetTagInfoFromFanza(new FileInfo(this.TargetPathText.Text));

            this.TitleText.Text = ti.Title;
            this.ArtistText.Text = ti.Performers;
            this.GenresText.Text = ti.Genres;
            this.CommentText.Text = ti.Comment;
            if (StringUtil.NullToBlank(ti.ImageUrl) != "") {
                this.PackagePicture.ImageLocation = ti.ImageUrl;
            }
        }

        private void MainPanel_DragEnter(object sender, DragEventArgs e) {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainPanel_DragDrop(object sender, DragEventArgs e) {
           string[] fileName = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            this.TargetPathText.Text = fileName[0];
        }
    }
}
