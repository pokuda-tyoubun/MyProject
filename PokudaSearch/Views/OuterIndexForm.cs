using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PokudaSearch.Views.IndexBuildForm;

namespace PokudaSearch.Views {
    public partial class OuterIndexForm : Form {


        #region Constants
        private const int HeaderRowCount = 1;
        #endregion Constants

        #region Properties
        /// <summary>接続文字列</summary>
        public string ConnectString;
        /// <summary>外部参照パス</summary>
        public string OuterPath;
        /// <summary>ローカルパス</summary>
        public string LocalPath;
        /// <summary>インデックスパス</summary>
        public string IndexStorePath;
        /// <summary>対象ファイル数</summary>
        public int TargetCount = 0;
        #endregion Properties

        #region MemberVariables
        /// <summary>リモートのSQLiteへのパス</summary>
        private FileInfo _dbFile = null;
        #endregion MemberVariables

        #region Constractors
        public OuterIndexForm(FileInfo dbFile) {
            InitializeComponent();

            ConnectString = AppObject.GetConnectString(dbFile.FullName);
            _dbFile = dbFile;
            OuterPath = "";
            LocalPath = "";
            IndexStorePath = "";
        }
        #endregion Constractors

        #region EventHandlers
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterIndexForm_Load(object sender, EventArgs e) {
            IndexBuildForm.LoadActiveIndex(ConnectString, this.ActiveIndexGrid, appendCheckBox:false);
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e) {
            string storePath = this.OuterStorePathText.Text;
            string outerPath = this.OuterTargetPathText.Text;
            string localPath = this.LocalPathText.Text;
            //末尾の\を除去
            storePath = StringUtil.RemoveLastChar(storePath, '\\');
            outerPath = StringUtil.RemoveLastChar(outerPath, '\\');
            localPath = StringUtil.RemoveLastChar(localPath, '\\');

            OuterPath = outerPath;
            LocalPath = localPath;
            IndexStorePath = storePath;

            DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            OuterPath = "";
            LocalPath = "";
            IndexStorePath = "";
            TargetCount = 0;

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ローカルパス選択ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("ローカルパス選択", this.LocalPathText.Text);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.LocalPathText.Text = selectedPath;
            }
        }


        private void ActiveIndexGrid_SelChange(object sender, EventArgs e) {
            if (this.ActiveIndexGrid.Selection.TopRow < HeaderRowCount) {
                this.OuterStorePathText.Text = "";
                this.OuterTargetPathText.Text = "";
                return;
            }

            string storePath = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, 
                                    (int)ActiveIndexColIdx.IndexStorePath + 1]);
            string tmp1 = _dbFile.FullName.Substring(0, _dbFile.FullName.IndexOf(@"bin\DB"));
            string tmp2 = storePath.Substring(storePath.IndexOf(@"bin\IndexStore"));
            this.OuterStorePathText.Text = tmp1 + tmp2;

            string remotePath = StringUtil.NullToBlank(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, 
                                    (int)ActiveIndexColIdx.IndexedPath + 1]);
            this.OuterTargetPathText.Text = remotePath;
            TargetCount = int.Parse(StringUtil.NullToZero(this.ActiveIndexGrid[this.ActiveIndexGrid.Selection.TopRow, 
                                    (int)ActiveIndexColIdx.TargetCount + 1]));
        }

        private void OuterIndexForm_Shown(object sender, EventArgs e) {
            //先頭行を選択
            if (this.ActiveIndexGrid.Rows.Count >= HeaderRowCount) {
                this.ActiveIndexGrid.Select(1, 2);
            }
        }
        #endregion EventHandlers
    }
}
