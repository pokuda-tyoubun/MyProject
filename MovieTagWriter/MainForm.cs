using C1.Win.C1FlexGrid;
using FxCommonLib.Controls;
using FxCommonLib.Utils;
using FxCommonLib.Win32API;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MovieTagWriter {
    public partial class MainForm : Form {

        #region Constants
        /// <summary>行ヘッダー数</summary>
        private const int RowHeaderCount = 1;
        /// <summary>書込みステータス</summary>
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
            [EnumLabel("書込失敗")]
            Failed,
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
            [EnumLabel("発売日")]
            ReleaseDate,
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
            [EnumLabel("URL")]
            PageUrl,
            [EnumLabel("画像URL")]
            ImageUrl,
            [EnumLabel("画像")]
            Image,
        }
        #endregion Constants

        #region MemberVariables
        private System.Windows.Threading.Dispatcher _dispatcher = null;
        private int _selectedRow = 0;
        #endregion MemberVariables

        #region Constractors
        public MainForm() {
            InitializeComponent();

            _dispatcher = Dispatcher.CurrentDispatcher;

            CreateHeader();

            this.ItemLinkLabel.Enabled = false;


            this.PathText.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
#if DEBUG
            this.PathText.Text = @"C:\Users\078134995";
#endif
        }
        #endregion Constractors

        #region PublicMethods
        public void ReportProgress(BackgroundWorker bw, int molecule, int denominator, string msg) {
            //プログレスバー更新
            int p = (int)(((double)molecule / (double)denominator) * 100);
            bw.ReportProgress(p, molecule.ToString() + "/" + denominator.ToString() + " " + msg);

            TaskbarManager.Instance.SetProgressValue(molecule, denominator);
        }
        #endregion PublicMethods

        #region EventHandlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchMovieButton_Click(object sender, EventArgs e) {
            string targetDir = StringUtil.NullToBlank(this.PathText.Text);
            if (targetDir == "" || !new DirectoryInfo(targetDir).Exists) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_DIR_NOT_FOUND),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try {
                Search(targetDir);
            } finally {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// タグ書込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteTagButton_Click(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            ProgressDialog pd = new ProgressDialog("書込み中...", 
                new DoWorkEventHandler(WriteTag_DoWork), this.TargetGrid);

            DialogResult result;
            try {
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// タグ書込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteTag_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            var grid = (FlexGridEx)e.Argument;
            var rowList = new List<Row>();

            foreach (Row r in grid.Rows) {
                if (r.Index >= RowHeaderCount) {
                    if (bool.Parse(r[(int)ColIndex.TargetCheck].ToString())) {
                        rowList.Add(r);
                    }
                }
            }

            int molecule = 0;
            int denominator = rowList.Count;
            ReportProgress(bw, molecule, denominator, "タグ情報書込み中...");
            foreach (Row r in rowList) {
                try {
                    r[(int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Writting);
                    //タグ付け
                    SaveTag(r);
                    molecule++;
                    r[(int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Finished);
                } catch (Exception ex) {
                    r[(int)ColIndex.Status] = EnumUtil.GetLabel(StatusEnum.Failed);
                    string fileName = StringUtil.NullToBlank(r[(int)ColIndex.FileName]);
                    string msg = ex.Message + Environment.NewLine +
                        "エラーが発生したため[" + fileName + "]のタグ付けをスキップします。";
                    MessageBox.Show(msg, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (bw.CancellationPending) {
                    return;
                }

                ReportProgress(bw, molecule, denominator, "タグ情報書込み中...");
            }
            ReportProgress(bw, molecule, denominator, "完了");
        }
        /// <summary>
        /// 参照ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("検索対象フォルダ選択", this.PathText.Text);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.PathText.Text = selectedPath;
            }
        }

        /// <summary>
        /// グリッドフィルタリング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterGridButton_Click(object sender, EventArgs e) {
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.TargetGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_FILTER));
        }

        /// <summary>
        /// フィルタリングクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearFilterButton_Click(object sender, EventArgs e) {
            this.SearchGridText.Text = "";
            AppObject.FilterHelper.SetGridFilter(this.Cursor, this.TargetGrid, this.SearchGridText.Text, 
                AppObject.GetMsg(AppObject.Msg.ACT_RESET_FILTER));
        }
        /// <summary>
        /// Excel抽出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 適用ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e) {
            this.TargetGrid[_selectedRow, (int)ColIndex.Title] = this.TitleText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.ReleaseDate] = this.ReleaseDate.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Artist] = this.ArtistText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Genres] = this.GenresText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Maker] = this.MakerText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Director] = this.DirectorText.Text;
            this.TargetGrid[_selectedRow, (int)ColIndex.Comment] = this.CommentText.Text;
        }
        /// <summary>
        /// タイトルの必須チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleText_Validated(object sender, EventArgs e) {
            if (this.TitleText.Text == "") {
                ErrorProvider.SetError(this.TitleText, "タイトルは必須です。");
            } else {
                ErrorProvider.Clear();
            }
        }

        /// <summary>
        /// 製品紹介ページリンクをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            string url = StringUtil.NullToBlank(this.TargetGrid[_selectedRow, (int)ColIndex.PageUrl]);
            Process.Start(url);
        }
        /// <summary>
        /// ヘルプボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton_Click(object sender, EventArgs e) {
            Process.Start("https://erogrammer.wordpress.com/mp4tagwriter/");
        }
        private void DMMCreditLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://affiliate.dmm.com/api/");
        }
        private void FanzaCreditLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://affiliate.dmm.com/api/");
        }
        #endregion EventHandlers

        #region PrivateMethods
        /// <summary>
        /// 動画ファイル検索＆タグ情報取得
        /// </summary>
        /// <param name="targetDir"></param>
        private void Search(string targetDir) {
            this.Cursor = Cursors.WaitCursor;

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            ProgressDialog pd = new ProgressDialog("検索中...", 
                new DoWorkEventHandler(SearchTarget_DoWork), targetDir);
            try {
                DialogResult result;
                result = pd.ShowDialog(this);
                if (result == DialogResult.Abort) {
                    //失敗
                    Exception ex = pd.Error;
                    MessageBox.Show(ex.Message, AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } finally {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                pd.Dispose();
                this.Cursor = Cursors.Default;
            }

        }
        /// <summary>
        /// 動画ファイル検索＆タグ情報取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTarget_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = (BackgroundWorker)sender;
            var targetDir = (string)e.Argument;

            var mp4wd = new MP4WebDriver();
            var resultList = mp4wd.GetTagInfo(targetDir, bw);

            //グリッドに表示
            _dispatcher.BeginInvoke(new Action(() => {
                this.TargetGrid.Rows.Count = RowHeaderCount;

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
                        this.TargetGrid.Rows[i].AllowEditing = true;
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
                    this.TargetGrid[i, (int)ColIndex.ReleaseDate] = StringUtil.NullToBlank(ti.ReleaseDate);
                    this.TargetGrid[i, (int)ColIndex.Artist] = StringUtil.NullToBlank(ti.Performers);
                    this.TargetGrid[i, (int)ColIndex.Genres] = StringUtil.NullToBlank(ti.Genres);
                    this.TargetGrid[i, (int)ColIndex.Maker] = StringUtil.NullToBlank(ti.Maker);
                    this.TargetGrid[i, (int)ColIndex.Director] = StringUtil.NullToBlank(ti.Director);
                    this.TargetGrid[i, (int)ColIndex.Comment] = StringUtil.NullToBlank(ti.Comment);
                    this.TargetGrid[i, (int)ColIndex.PageUrl] = StringUtil.NullToBlank(ti.PageUrl);
                    this.TargetGrid[i, (int)ColIndex.ImageUrl] = StringUtil.NullToBlank(ti.ImageUrl);
                    this.TargetGrid.SetCellImage(i, (int)ColIndex.Image, bu.GetBitmapFromURL(ti.ImageUrl));

                    i++;
                }
                //先頭行を選択
                if (this.TargetGrid.Rows.Count > RowHeaderCount) {
                    this.TargetGrid.Select(RowHeaderCount, (int)ColIndex.TargetCheck);
                }
            }));
        }

        /// <summary>
        /// TargetGridのヘッダ行作成
        /// </summary>
        private void CreateHeader() {
            //データクリア
            this.TargetGrid.Rows.Count = RowHeaderCount;
            this.TargetGrid.Cols.Count = Enum.GetValues(typeof(ColIndex)).Length + 1;

            this.TargetGrid[0, (int)ColIndex.TargetCheck] = EnumUtil.GetLabel(ColIndex.TargetCheck);
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].DataType = typeof(bool);
            this.TargetGrid.Cols[(int)ColIndex.TargetCheck].AllowEditing = true;
            this.TargetGrid[0, (int)ColIndex.Status] = EnumUtil.GetLabel(ColIndex.Status);
            this.TargetGrid.Cols[(int)ColIndex.Status].Width = 70;
            this.TargetGrid.Cols[(int)ColIndex.Status].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.FileIcon] = EnumUtil.GetLabel(ColIndex.FileIcon);
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].Width = 30;
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].ImageAlign = ImageAlignEnum.CenterCenter;
            this.TargetGrid.Cols[(int)ColIndex.FileIcon].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.FileName] = EnumUtil.GetLabel(ColIndex.FileName);
            this.TargetGrid.Cols[(int)ColIndex.FileName].Width = 200;
            this.TargetGrid.Cols[(int)ColIndex.FileName].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.FullPath] = EnumUtil.GetLabel(ColIndex.FullPath);
            this.TargetGrid.Cols[(int)ColIndex.FullPath].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.FullPath].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Extension] = EnumUtil.GetLabel(ColIndex.Extension);
            this.TargetGrid.Cols[(int)ColIndex.Extension].Width = 40;
            this.TargetGrid.Cols[(int)ColIndex.Extension].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.UpdateDate] = EnumUtil.GetLabel(ColIndex.UpdateDate);
            this.TargetGrid.Cols[(int)ColIndex.UpdateDate].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.UpdateDate].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Title] = EnumUtil.GetLabel(ColIndex.Title);
            this.TargetGrid.Cols[(int)ColIndex.Title].Width = 80;
            this.TargetGrid.Cols[(int)ColIndex.Title].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.ReleaseDate] = EnumUtil.GetLabel(ColIndex.ReleaseDate);
            this.TargetGrid.Cols[(int)ColIndex.ReleaseDate].Width = 80;
            this.TargetGrid.Cols[(int)ColIndex.ReleaseDate].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Artist] = EnumUtil.GetLabel(ColIndex.Artist);
            this.TargetGrid.Cols[(int)ColIndex.Artist].Width = 40;
            this.TargetGrid.Cols[(int)ColIndex.Artist].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Genres] = EnumUtil.GetLabel(ColIndex.Genres);
            this.TargetGrid.Cols[(int)ColIndex.Genres].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.Genres].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Maker] = EnumUtil.GetLabel(ColIndex.Maker);
            this.TargetGrid.Cols[(int)ColIndex.Maker].Width = 80;
            this.TargetGrid.Cols[(int)ColIndex.Maker].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Director] = EnumUtil.GetLabel(ColIndex.Director);
            this.TargetGrid.Cols[(int)ColIndex.Director].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.Director].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Comment] = EnumUtil.GetLabel(ColIndex.Comment);
            this.TargetGrid.Cols[(int)ColIndex.Comment].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.Comment].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.PageUrl] = EnumUtil.GetLabel(ColIndex.PageUrl);
            this.TargetGrid.Cols[(int)ColIndex.PageUrl].Width = 1;
            this.TargetGrid.Cols[(int)ColIndex.PageUrl].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.ImageUrl] = EnumUtil.GetLabel(ColIndex.ImageUrl);
            this.TargetGrid.Cols[(int)ColIndex.ImageUrl].Width = 1;
            this.TargetGrid.Cols[(int)ColIndex.ImageUrl].AllowEditing = false;
            this.TargetGrid[0, (int)ColIndex.Image] = EnumUtil.GetLabel(ColIndex.Image);
            this.TargetGrid.Cols[(int)ColIndex.Image].Width = 100;
            this.TargetGrid.Cols[(int)ColIndex.Image].AllowEditing = false;

            this.TargetGrid.Cols.Frozen = (int)ColIndex.FileName;
        }
        /// <summary>
        /// タグ書込み
        /// </summary>
        /// <param name="row"></param>
        private void SaveTag(Row row) {
            string path = StringUtil.NullToBlank(row[(int)ColIndex.FullPath]);
            string extension = StringUtil.NullToBlank(row[(int)ColIndex.Extension]);
            string title =  StringUtil.NullToBlank(row[(int)ColIndex.Title]);
            uint year = 0;
            DateTime releaseDate = DateTime.Parse("1900/1/1");
            string date =  StringUtil.NullToBlank(row[(int)ColIndex.ReleaseDate]);
            if (DateTime.TryParse(date, out releaseDate)) {
                year = (uint)releaseDate.Year;
            }
            if (extension.ToLower() == ".mp4") {
                //MP4のみ記入
                var tagFile = TagLib.File.Create(path);
                tagFile.Tag.Title = title;
                tagFile.Tag.Year = year;
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
            }
            //リネーム
            var f = new FileInfo(path);
            f.MoveTo(f.DirectoryName + @"\" + StringUtil.ReplaceWindowsFileNGWord2Wide(title) + extension.ToLower()); 
        }
        #endregion PrivateMethods

        /// <summary>
        /// TargetGridの選択行変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string releaseDate = StringUtil.NullToBlank(row[(int)ColIndex.ReleaseDate]);
                this.ReleaseDate.Text = releaseDate;
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
                this.ItemLinkLabel.Enabled = true;

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
                this.ItemLinkLabel.Enabled = false;
            }
        }

        /// <summary>
        /// 全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetGrid.Rows) {
                if (r.Index >= RowHeaderCount) {
                    if (r.AllowEditing) {
                        r[(int)ColIndex.TargetCheck] = true;
                    }
                }
            }
        }

        /// <summary>
        /// 全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseAllButton_Click(object sender, EventArgs e) {
            foreach (Row r in this.TargetGrid.Rows) {
                if (r.Index >= RowHeaderCount) {
                    r[(int)ColIndex.TargetCheck] = false;
                }
            }
        }

        #region EventHandlers
        /// <summary>
        /// ファイルを開くメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileMenu_Click(object sender, EventArgs e) {
            OpenFile();
        }
        /// <summary>
        /// 親フォルダを開くメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenParentMenu_Click(object sender, EventArgs e) {
            if (this.TargetGrid.Selection.TopRow < this.TargetGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.TargetGrid[this.TargetGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            Process.Start(System.IO.Directory.GetParent(path).FullName);
        }

        /// <summary>
        /// 切り取りメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutMenu_Click(object sender, EventArgs e) {
            this.TargetGrid.CutEx();
        }
        /// <summary>
        /// コピーメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMenu_Click(object sender, EventArgs e) {
            this.TargetGrid.CopyEx();
        }
        /// <summary>
        /// 貼り付けメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteMenu_Click(object sender, EventArgs e) {
            this.TargetGrid.PasteEx();
        }
        /// <summary>
        /// Windows標準のファイルプロパティダイアログを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPropertiesMenu_Click(object sender, EventArgs e) {
            string path = StringUtil.NullToBlank(this.TargetGrid[this.TargetGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            if (File.Exists(path)) {
                Shell32.SHObjectProperties(IntPtr.Zero, (uint)Shell32.SHOP.SHOP_FILEPATH, path, string.Empty);
            }
        }
        /// <summary>
        /// 対象グリッドダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetGrid_DoubleClick(object sender, EventArgs e) {
            MouseEventArgs me = (MouseEventArgs)e;
            if (this.TargetGrid.HitTest(me.X, me.Y).Row < this.TargetGrid.Rows.Fixed) {
                //ヘッダ部は処理しない。
                return;
            }
            OpenFile();
        }
        #endregion EventHandlers

        #region PrivateMethods
        /// <summary>
        /// 選択行のファイルを開く
        /// </summary>
        private void OpenFile() {
            if (this.TargetGrid.Selection.TopRow < this.TargetGrid.Rows.Fixed) {
                return;
            }
            string path = StringUtil.NullToBlank(this.TargetGrid[this.TargetGrid.Selection.TopRow, (int)ColIndex.FullPath]);
            if (File.Exists(path)) {
                Process.Start(path);
            } else {
                //HACK メッセージ
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_FILE_NOT_FOUND),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion PrivateMethods

        private void PokudaSearchLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://pokuda-tyoubun.blogspot.com/p/pokuda-search-pro.html");
        }
    }
}
