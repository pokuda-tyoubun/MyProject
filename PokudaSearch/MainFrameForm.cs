using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1Ribbon;
using PokudaSearch.Views;
using System.Diagnostics;
using PokudaSearch.SandBox;
using System.Data.SQLite;
using FxCommonLib.Utils;
using java.awt;

namespace PokudaSearch {
    public partial class MainFrameForm : Form {

        /// <summary>ファイルエクスプローラ画面</summary>
        public static FileExplorerForm FileExplorerForm;
        /// <summary>検索画面</summary>
        public static SearchForm SearchForm;

        /// <summary>SandBox用</summary>
        public static TestForm TestForm;

        #region MemberVariables
        /// <summary>ロックオブジェクト</summary>
        private static Object _lockObj = new Object();
        #endregion MemberVariables

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainFrameForm() {
            InitializeComponent();

            //ライセンス認証
#if DEBUG
# else
            this.FileExplorerFormButton.Visible = false;
            this.TagGroup.Visible = false;
            this.AnalyzeGroup.Visible = false;
            this.SandBoxGroup.Visible = false;
#endif
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrameForm_Load(object sender, EventArgs e) {
            VerifyLicense();

        }

        private void MainFrameForm_Shown(object sender, EventArgs e) {
            //NOTE MainFrameFormの表示後に子フォームを表示しないと、閉じるボタンが複数表示されてしまう。
            LoadForms();
            //LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// ステータスバーのメッセージ表示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isStart"></param>
        /// <param name="sw"></param>
        public void SetStatusMsg(string msg, bool isStart, Stopwatch sw) {
            if (isStart) {
                sw.Start();
            } else {
                sw.Stop();
                msg = " Time:" + sw.Elapsed.ToString().Substring(0, 12);
            }

            AppObject.Logger.Info(msg);
            StatusLabel.Text = msg;
            if (isStart) {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
            } else {
                try {
                    this.ProgressBar.Style = ProgressBarStyle.Blocks;
                    this.ProgressBar.Value = 0;
                } catch (NullReferenceException ne) {
                    //強制終了時にNullRefferenceになるので無視する。
                    AppObject.Logger.Info("Ignore->" + ne.Message);
                }
            }
        }

        /// <summary>
        /// ファイル検索画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFormButton_Click(object sender, EventArgs e) {
            if (SearchForm == null) {
                SearchForm = new SearchForm();
                LoadForm(SearchForm);
            } else {
                SearchForm.Activate();
            }
        }

        /// <summary>
        /// インデックス作成画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildFormButton_Click(object sender, EventArgs e) {
            var ibf = new IndexBuildForm();
            ibf.ShowDialog();
        }

        /// <summary>
        /// 前もって起動する処理
        /// </summary>
        public void LoadForms() {
            try {
                //ファイル検索画面
                //FileExplorerForm = new FileExplorerForm();
                //LoadForm(FileExplorerForm);
                SearchForm = new SearchForm();
                LoadForm(SearchForm);
            } finally {
            }
        }
        /// <summary>
        /// 初期表示画面起動
        /// </summary>
        /// <param name="frm"></param>

        private void LoadForm(Form frm) {
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void SandBoxGroup_DialogLauncherClick(object sender, EventArgs e) {

        }

        private void TestFormButton_Click(object sender, EventArgs e) {
            if (TestForm == null) {
                TestForm = new TestForm();
                LoadForm(TestForm);
            } else {
                TestForm.Activate();
            }
        }

        /// <summary>
        /// ファイルエクスプローラー画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileExplorerFormButton_Click(object sender, EventArgs e) {
            if (FileExplorerForm == null) {
                FileExplorerForm = new FileExplorerForm();
                LoadForm(FileExplorerForm);
            } else {
                FileExplorerForm.Activate();
            }
        }

        /// <summary>
        /// ヘルプサイト表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton1_Click(object sender, EventArgs e) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        /// <summary>
        /// 設定画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigButton_Click(object sender, EventArgs e) {
            var cf = new ConfigForm();
            cf.ShowDialog();
        }

        private void TagEditFormButton_Click(object sender, EventArgs e) {
            var tf = new TagEditForm();
            tf.ShowDialog();
        }

        private void VerifyLicense() {
            DataTable licenseTbl;

            lock (_lockObj) {
                AppObject.DbUtil.Open(AppObject.ConnectString);
                try {
                    DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.m_license.SELECT_ALL, null);
                    licenseTbl = ds.Tables[0];
                    if (licenseTbl.Rows.Count == 0) {
                        //試用開始
                        var param = new List<SQLiteParameter>();
                        param.Add(new SQLiteParameter("@利用開始日", DateTime.Now));
                        param.Add(new SQLiteParameter("@認証日", DBNull.Value));
                        param.Add(new SQLiteParameter("@認証キー", ""));
                        AppObject.DbUtil.ExecuteNonQuery(SQLSrc.m_license.INSERT, param.ToArray());
                        AppObject.IsTrial = true;
                        AppObject.RemainingDays = AppObject.TrialPeriod;
                        SetUIVerified();

                        AppObject.DbUtil.Commit();
                        return;
                    }
                } catch (Exception) {
                    AppObject.DbUtil.Rollback();
                    throw;
                } finally {
                    AppObject.DbUtil.Close();
                }
            }

            //ライセンス認証
            DateTime startDay = (DateTime)licenseTbl.Rows[0]["利用開始日"];
            string licenseKey = StringUtil.NullToBlank(licenseTbl.Rows[0]["認証キー"]);
            if (licenseKey == AppObject.LicenseKey) {
                //既に認証済み
                AppObject.IsTrial = false;
                SetUIVerified();
                return;
            }

            //期限確認
            startDay = DateTimeUtil.Truncate(startDay, TimeSpan.FromDays(1));
            DateTime today = DateTimeUtil.Truncate(DateTime.Now, TimeSpan.FromDays(1));
            AppObject.RemainingDays = (int)((startDay - today).TotalDays + AppObject.TrialPeriod);
            if (AppObject.RemainingDays < 0) {
                //期限切れなのでライセンス入力画面を表示
                var lvf = new LicenseVerificationForm(isExpired:true);
                var result = lvf.ShowDialog();
                if (result == DialogResult.OK) {
                    if (lvf.LicenseKey == AppObject.LicenseKey) {
                        //認証情報を保存
                        UpdateLicenseKey(lvf.LicenseKey);
                        //認証成功メッセージ
                        MessageBox.Show(AppObject.GetMsg(AppObject.Msg.MSG_LICENSE_VERIFIED),
                            AppObject.GetMsg(AppObject.Msg.TITLE_INFO), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AppObject.IsTrial = false;
                        SetUIVerified();

                        return;
                    } else {
                        //認証失敗メッセージ
                        MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_LICENSE_CANNOT_VERIFIED),
                            AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //強制終了
                        this.Close();
                    }
                } else {
                    //強制終了
                    this.Close();
                }
            }
            AppObject.IsTrial = true;
            SetUIVerified();
        }
        private void UpdateLicenseKey(string licenseKey) {
            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                //試用開始
                var param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@認証日", DateTime.Now));
                param.Add(new SQLiteParameter("@認証キー", licenseKey));
                AppObject.DbUtil.ExecuteNonQuery(SQLSrc.m_license.UPDATE_ALL, param.ToArray());

                AppObject.DbUtil.Commit();
            } catch(Exception) {
                AppObject.DbUtil.Rollback();
                throw;
            } finally {
                AppObject.DbUtil.Close();
            }
        }

        private void VerifyLicenseButton_Click(object sender, EventArgs e) {
            var lvf = new LicenseVerificationForm();
            var result = lvf.ShowDialog();
            if (result == DialogResult.OK) {
                if (lvf.LicenseKey == AppObject.LicenseKey) {
                    //認証情報を保存
                    UpdateLicenseKey(lvf.LicenseKey);
                    //認証成功メッセージ
                    MessageBox.Show(AppObject.GetMsg(AppObject.Msg.MSG_LICENSE_VERIFIED),
                        AppObject.GetMsg(AppObject.Msg.TITLE_INFO), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppObject.IsTrial = false;
                    SetUIVerified();

                    return;
                } else {
                    //認証失敗メッセージ
                    MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_LICENSE_CANNOT_VERIFIED),
                        AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetUIVerified() {
            //試用版表示
            string trialPeriod = "";
            if (AppObject.IsTrial) {
                //残日数を設定
                trialPeriod = " 試用版（残り：" + AppObject.RemainingDays.ToString() + "日）";
            } else {
                this.VerifyLicenseButton.Enabled = false;
                this.VerifyLicenseButton.Text = "ライセンス認証済み";
            }
            this.Text = "Pokuda Search Pro Ver." + AppObject.GetVersion() + trialPeriod;
        }
    }
}
