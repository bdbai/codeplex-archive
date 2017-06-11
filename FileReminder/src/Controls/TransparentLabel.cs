using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// 背景透明的标签。
    /// </summary>
    /// <remarks>http://www.cnblogs.com/WuCountry/archive/2007/06/27/797907.html</remarks>
    public class TransparentLabel : System.Windows.Forms.PictureBox
    {
        #region 公共属性
        /// <summary>
        /// 表示文本相对位置。
        /// </summary>
        [Browsable(true)]
        public Point TextLocation { get; set; }

        /// <summary>
        /// 表示字体颜色刷。
        /// </summary>
        public Brush ColorBrush = Brushes.Black;
        #endregion

        #region 重写方法
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SizeF m_size = e.Graphics.MeasureString(this.Text, this.Font); //测量文本显示矩形大小。
            e.Graphics.DrawString(this.Text, this.Font, ColorBrush, new RectangleF(this.TextLocation, m_size)); //绘制文本。
        }
        #endregion
    }
}
