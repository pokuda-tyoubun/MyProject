using C1.Win.C1FlexGrid;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Taskbar;
using MovieTagWriter.WebDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MovieTagWriter {
    public partial class MainForm : Form {

        #region Constants
        private const int RowHeaderCount = 1;
        private enum StatusEnum : int {
            [EnumLabel("未取得")]
            None = 1,
            [EnumLabel("書込可")]
            Avairable,
            [EnumLabel("書込不可")]
            UnAvairable,
            [EnumLabel("書込中")]
            Writting,
            [EnumLabel("済み")]
            Finished,
        }
        /// <summary>列定義</summary>
        private enum ColIndex : int {
            [EnumLabel("対象")]
            TargetCheck = 1,
            [EnumLabel("ステータス")]
            Status,
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
            [EnumLabel("画像URL")]
            ImageUrl,
            [EnumLabel("画像")]
            Image,
        }
        #endregion Constants

        #region MemberVariables
        private System.Windows.Threading.Dispatcher _dispather = null;
        private int _selectedRow = 0;
        #endregion MemberVariables

        public MainForm() {
            InitializeComponent();

            _dispather = Dispatcher.CurrentDispatcher;

            CreateHeader();

#if DEBUG
            this.PathText.Text = @"C:\Users\078134995";
#endif
        }

        private void Search(string targetDir) {
            ProgressDialog pd = new ProgressDialog("検索中...", 
                new DoWorkEventHandler(SearchTarget_DoWork), targetDir);

            DialogResult result;
            this.Cursor = Cursors.WaitCursor;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }
        }
        private void SearchTarget_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            var targetDir = (string)e.Argument;

            var mp4wd = new MP4WebDriver();
            var resultList = mp4wd.GetTagInfo(targetDir, bw);

            //グリッドに表示
            _dispather.BeginInvoke(new Action(() => {
                var bu = new BitmapUtil();
                this.TargetGrid.Rows.Count = resultList.Count + RowHeaderCount;
                this.DenominatorLabel.Text = resultList.Count.ToString() + "件";
                int i = RowHeaderCount;
                foreach (var row in resultList) {
                    var fi = row.Key;
                    var ti = row.Value;

                    //対象
                    if (StringUtil.NullToBlank(ti.Title) != "") {
                        this.TargetGrid[i, (int)ColIndex.TargetCheck] = true;
                        this.TargetGrid.Rows[i].AllowEditing = false;
                        this.TargetGrid[i, (int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Avairable);
                    } else {
                        this.TargetGrid[i, (int)ColIndex.TargetCheck] = false;
                        this.TargetGrid.Rows[i].AllowEditing = false;
                        this.TargetGrid.Rows[i].StyleNew.BackColor = Color.Gainsboro;
                        this.TargetGrid[i, (int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.UnAvairable);
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
                    this.TargetGrid[i, (int)ColIndex.ImageUrl] = StringUtil.NullToBlank(ti.ImageUrl);
                    this.TargetGrid.SetCellImage(i, (int)ColIndex.Image, bu.GetBitmapFromURL(ti.ImageUrl));

                    i++;
                }
            }));
        }

        private void SearchMovieButton_Click(object sender, EventArgs e) {
            string targetDir = StringUtil.NullToBlank(this.PathText.Text);
            if (targetDir == "" || !new DirectoryInfo(targetDir).Exists) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_DIR_NOT_FOUND),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Search(targetDir);

        }

        private void WriteTagButton_Click(object sender, EventArgs e) {
            ProgressDialog pd = new ProgressDialog("書込み中...", 
                new DoWorkEventHandler(WriteTag_DoWork), this.TargetGrid);

            DialogResult result;
            this.Cursor = Cursors.WaitCursor;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }
        }

        private void WriteTag_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            var grid = (FlexGridEx)e.Argument;
            var rowList = new List<Row>();

            foreach (Row r in grid.Rows) {
                if (r.Index > 0) {
                    if (bool.Parse(r[(int)ColIndex.TargetCheck].ToString())) {
                        rowList.Add(r);
                    }
                }
            }

            int molecule = 0;
            int denominator = rowList.Count;
            ReportProgress(bw, molecule, denominator, "タグ情報書込み中...");
            foreach (Row r in rowList) {
                r[(int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Writting);
                //タグ付け
                SaveTag(r);
                molecule++;
                r[(int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Finished);

                if (bw.CancellationPending) {
                    return;
                }

                ReportProgress(bw, molecule, denominator, "タグ情報書込み中...");
            }
            ReportProgress(bw, molecule, denominator, "完了");
        }
        public void ReportProgress(BackgroundWorker bw, int molecule, int denominator, string msg) {
            //プログレスバー更新
            int p = (int)(((double)molecule / (double)denominator) * 100);
            bw.ReportProgress(p, molecule.ToString() + "/" + denominator.ToString() + " " + msg);
        }
        private void CreateHeader() {
            //データクリア
            this.TargetGrid.Rows.Count = RowHeaderCount;
            this.TargetGrid.Cols.Count = Enum.GetValues(typeof(ColIndex)).Length + 1;

            this.TargetGrid[0, (int)ColIndex.TargetCheck] = EnumUtil.GetLabel(ColIndex.TargetCheck);
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].DataType = typeof(bool);
            this.TargetGrid[0, (int)ColIndex.Status] = EnumUtil.GetLabel(ColIndex.Status);
            this.TargetGrid.Cols[(int)ColIndex.Status].Width = 70;
            this.TargetGrid[0, (int)ColIndex.FileIcon] = EnumUtil.GetLabel(ColIndex.FileIcon);
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].ImageAlign = ImageAlignEnum.CenterCenter;
            this.TargetGrid[0, (int)ColIndex.FileName] = EnumUtil.GetLabel(ColIndex.FileName);
            this.TargetGrid.Cols[(int)ColIndex.FileName].Width = 200;
            this.TargetGrid[0, (int)ColIndex.FullPath] = EnumUtil.GetLabel(ColIndex.FullPath);
            this.TargetGrid.Cols[(int)ColIndex.FullPath].Width = 100;
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
            this.TargetGrid[0, (int)ColIndex.ImageUrl] = EnumUtil.GetLabel(ColIndex.ImageUrl);
            this.TargetGrid.Cols[(int)ColIndex.ImageUrl].Width = 1;
            this.TargetGrid[0, (int)ColIndex.Image] = EnumUtil.GetLabel(ColIndex.Image);
            this.TargetGrid.Cols[(int)ColIndex.Image].Width = 100;

            this.TargetGrid.Cols.Frozen = (int)ColIndex.FileName;
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

        private void RefButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("検索対象フォルダ選択", this.PathText.Text);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.PathText.Text = selectedPath;
            }
        }

        private void BreakButton_Click(object sender, EventArgs e) {
        }

        private void FilterGridButton_Click(object sender, EventArgs e) {
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.TargetGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_FILTER));
        }

        private void ClearFilterButton_Click(object sender, EventArgs e) {
            this.SearchGridText.Text = "";
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.TargetGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_RESET_FILTER));
        }

        private void WriteExcelButton_Click(object sender, EventArgs e) {
            ProgressDialog pd = new ProgressDialog(AppObject.GetMsg(AppObject.Msg.ACT_EXTRACT),
                new DoWorkEventHandler(Extract2Excel_DoWork), 0);

            DialogResult result;
            this.Cursor = Cursors.WaitCursor;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Excel抽出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extract2Excel_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            ExcelUtil eu = new ExcelUtil();
            eu.ExtractToExcel(this.TargetGrid, bw, AppObject.MLUtil);
        }

        private void TargetGrid_SelChange(object sender, EventArgs e) {
            if (this.TargetGrid.Selection.TopRow < this.TargetGrid.Rows.Fixed) {
                return;
            }
            var row = this.TargetGrid.Rows[this.TargetGrid.Selection.TopRow];
            string title = StringUtil.NullToBlank(row[(int)ColIndex.Title]);
            if (title != "") {
                _selectedRow = row.Index;
                //選択行のタグ情報を左側の入力域に表示
                this.TitleText.Text = title;
                string artist = StringUtil.NullToBlank(row[(int)ColIndex.Artist]);
                this.ArtistText.Text = artist;
                string genres = StringUtil.NullToBlank(row[(int)ColIndex.Genres]);
                this.GenresText.Text = genres;
                string maker = StringUtil.NullToBlank(row[(int)ColIndex.Maker]);
                this.MakerText.Text = maker;
                string director = StringUtil.NullToBlank(row[(int)ColIndex.Director]);
                this.DirectorText.Text = director;
                string comment = StringUtil.NullToBlank(row[(int)ColIndex.Comment]);
                this.CommentText.Text = comment;
                string imageUrl = StringUtil.NullToBlank(row[(int)ColIndex.ImageUrl]);
                this.PackagePicture.ImageLocation = imageUrl;

                this.SelectedRowIndexLabel.Text = _selectedRow.ToString() + "行目";
            } else {
                //クリア
                this.SelectedRowIndexLabel.Text = "";
                this.TitleText.Text = "";
                this.ArtistText.Text = "";
                this.GenresText.Text = "";
                this.MakerText.Text = "";
                this.DirectorText.Text = "";
                this.CommentText.Text = "";
                this.PackagePicture.ImageLocation = "";
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            this.TargetGrid[_selectedRow, (int)ColIndex.Title] = this.TitleText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Artist] = this.ArtistText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Genres] = this.GenresText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Maker] = this.MakerText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Director] = this.DirectorText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Comment] = this.CommentText.Text;
        }

        private void TitleText_Validated(object sender, EventArgs e) {
            if (this.TitleText.Text == "") {
                ErrorProvider.SetError(this.TitleText, "タイトルは必須です。");
            } else {
                ErrorProvider.Clear();
            }
        }

        private void ItemLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://dobon.net");
        }
    }
}
