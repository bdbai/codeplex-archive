using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// “聪聪生快”界面
    /// </summary>
    public partial class HappyBirthdaySpeForm : Form
    {
        #region 构造函数
        public HappyBirthdaySpeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 拖动鼠标时鼠标在窗体上的相对初始横坐标。
        /// </summary>
        private int mousePositionX;

        /// <summary>
        /// 拖动鼠标时鼠标在窗体上的相对初始横坐标。
        /// </summary>
        private int mousePositionY;

        /// <summary>
        /// 计时器每个计时周期改变窗体透明度的增量。
        /// </summary>
        private double opacityDelta = 0.02;

        /// <summary>
        /// 动画结束的同步事件。
        /// </summary>
        private EventWaitHandle animationCompletedHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        #endregion

        #region 事件响应函数
        private void HappyBirthdaySpeForm_Load(object sender, EventArgs e)
        {
            transparentLabel21.Text = "聪聪" + Environment.NewLine + "生快";
            transparentLabel21.ColorBrush = Brushes.Chocolate;
            transparentLabel21.Parent = pictureBox1;
            transparentLabel22.ColorBrush = Brushes.DarkGray;
            transparentLabel22.Parent = pictureBox1;
            transparentLabel23.ColorBrush = Brushes.DarkGray;
            transparentLabel23.Parent = pictureBox1;
            transparentLabel24.ColorBrush = Brushes.DarkGray;
            transparentLabel24.Parent = pictureBox1;
            transparentLabel25.ColorBrush = Brushes.Red;
            transparentLabel25.Parent = pictureBox1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += opacityDelta;
            if (this.Opacity == 1f || this.Opacity == 0f)
            {
                opacityDelta = -opacityDelta;
                timer1.Enabled = false;
                animationCompletedHandle.Set();
            }
        }

        private void transparentLabel23_Click(object sender, EventArgs e)
        {
            visitWebsite("http://filereminder.codeplex.com");
        }

        private void transparentLabel24_Click(object sender, EventArgs e)
        {
            visitWebsite("http://bdbai.22web.org/blog/cat/projects/dotnet/filereminder");
        }

        private void transparentLabel25_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            animationCompletedHandle.Reset();
            Thread exitHelperThread = new Thread(new ThreadStart(() =>
            {
                animationCompletedHandle.WaitOne();
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Close();
                }));
            }));
            exitHelperThread.Start();
        }

            #region 窗体拖动
            private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    mousePositionX = e.X;
                    mousePositionY = e.Y;
                    pictureBox1.MouseMove += pictureBox1_MouseMove;
                }
            }

            private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
            {
                this.Left += e.X - mousePositionX;
                this.Top += e.Y - mousePositionY;
            }

            private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
            {
                pictureBox1.MouseMove -= pictureBox1_MouseMove;
            }
            #endregion

        #endregion

        #region 私有方法
        /// <summary>
        /// 打开网页。
        /// </summary>
        /// <param name="url">要打开的网址。</param>
        private void visitWebsite(string url)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                Process.Start(url);
            }));
            t.Start();
        }
        #endregion
    }
}
