using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace D.Control
{
    public partial class FormBase : Form
    {
        protected Button btn_Close = new Button();

        public FormBase()
        {
            InitializeComponent();

            //采用双缓冲技术的控件必需的设置
            DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            //设置阴影
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);

            //设置默认背景颜色
            BackColor = Color.FromArgb(214, 219, 233);

            //添加关闭按钮
            btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            btn_Close.BackColor = System.Drawing.Color.Transparent;
            btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            btn_Close.FlatAppearance.BorderSize = 0;
            btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn_Close.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            btn_Close.Location = new System.Drawing.Point(this.Width - 30, 0);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(30, 30);
            btn_Close.TabIndex = 1;
            btn_Close.Text = "X";
            btn_Close.UseVisualStyleBackColor = false;
            this.Controls.Add(btn_Close);

            //添加关闭按钮
            btn_Close.Click += Btn_Close_Click;
            btn_Close.MouseMove += Btn_Close_MouseMove;
            btn_Close.MouseLeave += Btn_Close_MouseLeave;

            //设置窗体起始位置
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region Private Members

        protected const int CS_DropSHADOW = 0x20000;
        protected const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        protected static extern int ReleaseCapture();

        protected const int WM_NCLBUTTONDBLCLK = 0x00a3;
        protected const int WM_NCHITTEST = 0x0084;
        protected const int WM_LBUTTONDOWN = 0x0201;
        protected const int WM_NCLBUTTONDOWN = 0x00a1;

        protected const int Guying_HTLEFT = 10;
        protected const int Guying_HTRIGHT = 11;
        protected const int Guying_HTTOP = 12;
        protected const int Guying_HTTOPLEFT = 13;
        protected const int Guying_HTTOPRIGHT = 14;
        protected const int Guying_HTBOTTOM = 15;
        protected const int Guying_HTBOTTOMLEFT = 0x10;
        protected const int Guying_HTBOTTOMRIGHT = 17;
        #endregion

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCLBUTTONDBLCLK:
                    if (this.MaximizeBox) { base.WndProc(ref m); this.Invalidate(); }
                    break;
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                    {
                        if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        }
                        else if (vPoint.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        }
                        else
                        {
                            m.Result = (IntPtr)Guying_HTLEFT;
                        }
                    }
                    else if (vPoint.X >= ClientSize.Width - 5)
                    {
                        if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        }
                        else if (vPoint.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        }
                        else
                        {
                            m.Result = (IntPtr)Guying_HTRIGHT;
                        }
                    }
                    else if (vPoint.Y <= 5)
                    {
                        m.Result = (IntPtr)Guying_HTTOP;
                    }
                    else if (vPoint.Y >= ClientSize.Height - 5)
                    {
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    }
                    break;
                case WM_LBUTTONDOWN:
                    m.Msg = WM_NCLBUTTONDOWN;
                    m.LParam = IntPtr.Zero;
                    m.WParam = new IntPtr(2);
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //绘制背景
            SolidBrush brush = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(brush, new Rectangle(0, 0, this.Width, this.Height));
            //添加图标
            e.Graphics.DrawIcon(this.Icon, new Rectangle(5, 5, 25, 25));
            //绘制标题栏
            e.Graphics.DrawString(this.Text, new Font(new FontFamily("微软雅黑"), 9), new SolidBrush(Color.FromArgb(58, 58, 58)), 35, 10);
            
        }

        //关闭窗口
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //鼠标移入时
        private void Btn_Close_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.DarkRed;
            btn.ForeColor = Color.White;
        }

        //鼠标移出时
        private void Btn_Close_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.Black;
        }

    }
}
