using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    public partial class SearchBox : UserControl {
        public SearchBox() {
            InitializeComponent();
        }
        public override string  Text {
	        get { return _txtSearch.Text; }
            set { _txtSearch.Text = value; }
        }
        public int Delay {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }
        public event EventHandler Search;
        protected virtual void OnSearch(EventArgs e) {
            if (Search != null) {
                Search(this, e);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);

            Rectangle rc = ClientRectangle;
            rc.Inflate(-1, -1);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            DrawRoundRect(e.Graphics, SystemPens.ControlDark, SystemBrushes.Window, rc);
        }
        protected override void OnLayout(LayoutEventArgs e) {
            base.OnLayout(e);

            int h = ClientSize.Height;
            int w = ClientSize.Width - _lblIcon.Width - _lblClear.Width;

            _lblWatermark.Bounds = new Rectangle(
                _lblIcon.Width, (h - _lblWatermark.Height) / 2,
                w, _lblWatermark.Height);

            _txtSearch.Bounds = new Rectangle(
                _lblIcon.Width, (h - _txtSearch.Height) / 2 + 1,
                w, _txtSearch.Height);
        }

        // TextChanged イベントでクリアラベルを更新します。
        void _txtSearch_TextChanged(object sender, EventArgs e) {
            _lblClear.Visible = _txtSearch.Text.Length > 0;
            _timer.Stop();
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e) {
            _timer.Stop();
            OnSearch(EventArgs.Empty);
        }

        // 検索テキストをクリアします。
        void _lblClear_Click(object sender, EventArgs e) {
            _txtSearch.Text = string.Empty;
            _timer.Stop();
            OnSearch(EventArgs.Empty);
        }

        // ウォーターマークの表示・非表示にします。
        void _lblWatermark_Click(object sender, EventArgs e) {
            _txtSearch.Select();
        }
        void _txtSearch_Enter(object sender, EventArgs e) {
            _lblWatermark.Visible = false;
        }
        void _txtSearch_Leave(object sender, EventArgs e) {
            _lblWatermark.Visible = _txtSearch.Text.Length == 0;
        }

        // 角の丸い四角形を描画・塗りつぶします。
        static void DrawRoundRect(Graphics g, Pen p, Brush b, Rectangle rc) {
            // 左側の四角形
            Rectangle rcl = rc;
            rcl.Width = Math.Min(rc.Height, rc.Width / 3);

            // 右側の四角形
            Rectangle rcr = rcl;
            rcr.X = rc.Right - rcl.Width;

            // 中央の四角形
            rc.X += rc.Height;
            rc.Width -= 2 * rc.Height;

            // パスを作成します。
            using (GraphicsPath gp = new GraphicsPath()) {
                gp.AddLine(rc.X, rc.Y, rc.Right, rc.Y);
                gp.AddArc(rcr, 270, 180);
                gp.AddLine(rc.Right, rc.Bottom, rc.X, rc.Bottom);
                gp.AddArc(rcl, 90, 180);
                gp.CloseFigure();

                // パスにある四角形の内側を塗りつぶします。
                if (b != null) {
                    g.FillPath(b, gp);
                }

                // パスを描画します。
                if (p != null) {
                    g.DrawPath(p, gp);
                }
            }
        }
    }
}
