using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Z.Tackle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.fl_top.MouseMove += MainForm_MouseMove;
            this.fl_top.MouseDown += MainForm_MouseDown;
            this.label1.MouseMove += MainForm_MouseMove;
            this.label1.MouseDown += MainForm_MouseDown;

            this.textBox1.Text = "请选择文件夹路径";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.comboBox1.SelectedIndex = 0;
            this._extend = this.comboBox1.SelectedItem.ToString();
            button2.Focus();
            progressBar1.Minimum = 0;
        }

        private bool _isDirty = false;//是否正在解析
        private string _folder = string.Empty;//用户选择的文件夹路径
        private string _extend = ".bmp";//转换后的文件格式

        #region Form methods

        private Point _mousePoint;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = MousePosition.Y - _mousePoint.Y;
                Left = MousePosition.X - _mousePoint.X;
            }
        }


        #endregion

        #region Event methods

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this._isDirty)
            {
                if (MessageBox.Show("", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._folder = dialog.SelectedPath;
                this.textBox1.Text = this._folder;
                this.toolTip1.SetToolTip(this.textBox1, this.textBox1.Text);
            }
            dialog.Dispose();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            this._extend = cb.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            CommandTackle();
            button2.Enabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 图片格式转换
        /// </summary>
        public async void CommandTackle()
        {
            progressBar1.Visible = true;
            if (string.IsNullOrEmpty(this._folder))
            {
                MessageBox.Show("请选择文件夹路径", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this._extend))
            {
                MessageBox.Show("请选择转换格式", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this._isDirty = true;

            List<Task> tasks = new List<Task>();
            List<FileInfo> files = new List<FileInfo>();//选择文件夹以及所有子文件夹下的文件
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(this._folder);
            while(queue.Count > 0)
            {
                var currentfolder = queue.Dequeue();
                DirectoryInfo dir = new DirectoryInfo(currentfolder);
                //添加当前文件夹下的图片文件
                files.AddRange(dir.GetFiles());
                //添加当前文件夹下的子文件夹
                foreach(var item in dir.GetDirectories())
                {
                    queue.Enqueue(item.FullName);
                }
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = files.Count;
            foreach (var file in files)
            {
                tasks.Add(Task.Factory.StartNew((obj) =>
                {
                    try
                    {
                        var filePath = obj.ToString();
                        var extend = Path.GetExtension(filePath);
                        if (!this._extend.Equals(extend) && IsImage(extend))
                        {//将不是指定格式的文件转换为指定格式的文件
                            var newFilePath = filePath.Replace(extend, this._extend);
                            Bitmap bmp = new Bitmap(filePath);
                            bmp.Save(newFilePath);
                        }
                        progressBar1.Value += 1;
                    }
                    catch { }
                }, file.FullName, System.Threading.CancellationToken.None));
            }
            await Task.WhenAll(tasks);
            progressBar1.Value = progressBar1.Maximum;
            progressBar1.Visible = false;
            this._isDirty = false;
            MessageBox.Show("转换完成", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsImage(string extension)
        {
            var imageExtension = new string[]
            {
                ".bmp",".tiff",".jpg",".png"
            };
            return imageExtension.Contains(extension);
        }

        #endregion

    }
}
