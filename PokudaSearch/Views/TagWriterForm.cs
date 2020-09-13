using C1.Win.C1FlexGrid;
using FxCommonLib.Utils;
using PokudaSearch.WebDriver;
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

namespace PokudaSearch.Views {
    public partial class TagWriterForm : Form {

        #region Constants
        private const int RowHeaderCount = 1;
        /// <summary>列定義</summary>
        private enum ColIndex : int {
            [EnumLabel("対象")]
            TargetCheck = 1,
            [EnumLabel("種類")]
            FileIcon,
            [EnumLabel("ファイル名")]
            FileName,
            [EnumLabel("パス")]
            FullPath,
            [EnumLabel("拡張子")]
            Extension,
            [EnumLabel("更新日")]
            UpdateDate,
            [EnumLabel("タイトル")]
            Title,
            [EnumLabel("アーティスト")]
            Artist,
            [EnumLabel("ジャンル")]
            Genres,
            [EnumLabel("メーカー")]
            Maker,
            [EnumLabel("監督")]
            Director,
            [EnumLabel("コメント")]
            Comment,
            [EnumLabel("画像")]
            Image,
        }
        #endregion Constants

        public TagWriterForm() {
            InitializeComponent();

            CreateHeader();

            this.PathText.Text = @"C:\Users\078134995";
        }
        private void CreateHeader() {
            //データクリア
            this.TargetGrid.Rows.Count = RowHeaderCount;
            this.TargetGrid.Cols.Count = Enum.GetValues(typeof(ColIndex)).Length + 1;

            this.TargetGrid[0, (int)ColIndex.TargetCheck] = EnumUtil.GetLabel(ColIndex.TargetCheck);
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].DataType = typeof(bool);
            this.TargetGrid[0, (int)ColIndex.FileIcon] = EnumUtil.GetLabel(ColIndex.FileIcon);
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].ImageAlign = ImageAlignEnum.CenterCenter;
            this.TargetGrid[0, (int)ColIndex.FileName] = EnumUtil.GetLabel(ColIndex.FileName);
            this.TargetGrid.Cols[(int)ColIndex.FileName].Width = 200;
            this.TargetGrid[0, (int)ColIndex.FullPath] = EnumUtil.GetLabel(ColIndex.FullPath);
            this.TargetGrid.Cols[(int)ColIndex.FullPath].Width = 400;
            this.TargetGrid[0, (int)ColIndex.Extension] = EnumUtil.GetLabel(ColIndex.Extension);
            this.TargetGrid.Cols[(int)ColIndex.Extension].Width = 40;
            this.TargetGrid[0, (int)ColIndex.UpdateDate] = EnumUtil.GetLabel(ColIndex.UpdateDate);
            this.TargetGrid.Cols[(int)ColIndex.UpdateDate].Width = 100;
            this.TargetGrid[0, (int)ColIndex.Title] = EnumUtil.GetLabel(ColIndex.Title);
            this.TargetGrid.Cols[(int)ColIndex.Title].Width = 80;
            this.TargetGrid[0, (int)ColIndex.Artist] = EnumUtil.GetLabel(ColIndex.Artist);
            this.TargetGrid.Cols[(int)ColIndex.Artist].Width = 40;
            this.TargetGrid[0, (int)ColIndex.Genres] = EnumUtil.GetLabel(ColIndex.Genres);
            this.TargetGrid.Cols[(int)ColIndex.Genres].Width = 100;
            this.TargetGrid[0, (int)ColIndex.Maker] = EnumUtil.GetLabel(ColIndex.Maker);
            this.TargetGrid.Cols[(int)ColIndex.Maker].Width = 80;
            this.TargetGrid[0, (int)ColIndex.Director] = EnumUtil.GetLabel(ColIndex.Director);
            this.TargetGrid.Cols[(int)ColIndex.Director].Width = 100;
            this.TargetGrid[0, (int)ColIndex.Comment] = EnumUtil.GetLabel(ColIndex.Comment);
            this.TargetGrid.Cols[(int)ColIndex.Comment].Width = 100;
            this.TargetGrid[0, (int)ColIndex.Image] = EnumUtil.GetLabel(ColIndex.Image);
            this.TargetGrid.Cols[(int)ColIndex.Image].Width = 100;

            this.TargetGrid.Cols.Frozen = (int)ColIndex.FileName;
        }

        private void SearchButton_Click(object sender, EventArgs e) {
            string path = this.PathText.Text;
            var bu = new BitmapUtil();
            var mp4wd = new MP4WebDriver();

            Cursor.Current = Cursors.WaitCursor;
            try {

                var fileList = FileUtil.GetAllFileInfo(path);

                var rowList = new List<KeyValuePair<FileInfo, TagInfo>>();
                foreach (var fi in fileList) {
                    if (fi.Extension.ToLower() == ".mp4") {
                        TagInfo ti = mp4wd.GetTagInfoFromFanza(fi);
                        rowList.Add(new KeyValuePair<FileInfo, TagInfo>(fi, ti));
                    }
                }

                this.TargetGrid.Rows.Count = rowList.Count + RowHeaderCount;
                int i = RowHeaderCount;
                foreach (var row in rowList) {
                    var fi = row.Key;
                    var ti = row.Value;

                    //対象
                    if (StringUtil.NullToBlank(ti.Title) != "") {
                        this.TargetGrid[i, (int)ColIndex.TargetCheck] = true;
                    } else {
                        this.TargetGrid[i, (int)ColIndex.TargetCheck] = false;
                    }
                    //ファイルアイコン
                    Bitmap bmp = null;
                    bmp = Properties.Resources.File16;
                    try {
                        bmp = Icon.ExtractAssociatedIcon(fi.FullName).ToBitmap();
                    } catch {
                        //プレビューを取得できない場合は、デフォルトアイコンを表示
                    }
                    bmp.MakeTransparent();
                    this.TargetGrid.SetCellImage(i, (int)ColIndex.FileIcon, bu.Resize(bmp, 16, 16));
                    //ファイル名
                    this.TargetGrid[i, (int)ColIndex.FileName] = fi.Name;
                    this.TargetGrid[i, (int)ColIndex.FullPath] = fi.FullName;
                    this.TargetGrid[i, (int)ColIndex.Extension] = fi.Extension;
                    this.TargetGrid[i, (int)ColIndex.UpdateDate] = fi.LastWriteTime;
                    //タグ情報
                    this.TargetGrid[i, (int)ColIndex.Title] = StringUtil.NullToBlank(ti.Title);
                    this.TargetGrid[i, (int)ColIndex.Artist] = StringUtil.NullToBlank(ti.Performers);
                    this.TargetGrid[i, (int)ColIndex.Genres] = StringUtil.NullToBlank(ti.Genres);
                    this.TargetGrid[i, (int)ColIndex.Maker] = StringUtil.NullToBlank(ti.Maker);
                    this.TargetGrid[i, (int)ColIndex.Director] = StringUtil.NullToBlank(ti.Director);
                    this.TargetGrid[i, (int)ColIndex.Comment] = StringUtil.NullToBlank(ti.Comment);
                    this.TargetGrid.SetCellImage(i, (int)ColIndex.Image, bu.GetBitmapFromURL(ti.ImageUrl));

                    i++;
                }
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        private void WriteTagButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetGrid.Rows) {
                if (r.Index > 0) {
                    if (bool.Parse(r[(int)ColIndex.TargetCheck].ToString())) {
                        //タグ付け
                        SaveTag(r);
                    }
                }
            }
        }
        private void SaveTag(Row row) {
            string path = StringUtil.NullToBlank(row[(int)ColIndex.FullPath]);
            var tagFile = TagLib.File.Create(path);
            string title =  StringUtil.NullToBlank(row[(int)ColIndex.Title]);
            tagFile.Tag.Title = title;
            tagFile.Tag.Performers =  new string[] { StringUtil.NullToBlank(row[(int)ColIndex.Artist]) };
            tagFile.Tag.Genres =  new string[] { StringUtil.NullToBlank(row[(int)ColIndex.Genres]) };
            tagFile.Tag.Comment =  StringUtil.NullToBlank(row[(int)ColIndex.Comment]);
            //画像
            Image img = this.TargetGrid.GetCellImage(row.Index, (int)ColIndex.Image);
            var ic = new System.Drawing.ImageConverter();
            var ba = (byte[])ic.ConvertTo(img, typeof(byte[]));
            var byteVector = new TagLib.ByteVector(ba);
            var pic = new TagLib.Picture(byteVector);
            pic.Type = TagLib.PictureType.FrontCover;
            pic.Description = "Cover";
            pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
            tagFile.Tag.Pictures = new TagLib.IPicture[] { pic };
            tagFile.Save();

            //リネーム
            var f = new FileInfo(path);
            f.MoveTo(f.DirectoryName + @"\" + title + ".mp4"); 
        }
    }
}
