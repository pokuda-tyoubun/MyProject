using System.ComponentModel;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    public class FilePathTextBox : TextBox {

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FilePathTextBox() {
            InitializeComponent();

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public FilePathTextBox(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }
        #endregion Constractors


        #region EventHandlers
        /// <summary>
        /// DragDropイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePathTextBox_DragDrop(object sender, DragEventArgs e) {
            //ドロップされたファイルの一覧を取得
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileName.Length <= 0) {
                return;
            }

            //TextBoxの内容をファイル名に変更
            this.Text = fileName[0];
        }

        /// <summary>
        /// DragEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePathTextBox_DragEnter(object sender, DragEventArgs e) {
            //ファイルがドラッグされている場合、カーソルを変更する
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
            	e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion EventHandlers

        #region PrivateMethods
        /// <summary>
        /// 初期化
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // FilePathTextBox
            // 
            this.AllowDrop = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FilePathTextBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FilePathTextBox_DragEnter);
            this.ResumeLayout(false);

        }
        #endregion PrivateMethods
    }
}
