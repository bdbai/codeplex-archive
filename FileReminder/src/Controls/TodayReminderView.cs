using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// 今日提醒控件。
    /// </summary>
    public partial class TodayReminderView : UserControl
    {
        #region 构造函数
        private TodayReminderView() { }

        /// <summary>
        /// 构造今日提醒控件。
        /// </summary>
        /// <param name="r">要显示的提醒。</param>
        public TodayReminderView(Reminder r)
        {
            InitializeComponent();
            Reminder = r;
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 表示提醒是否高亮。
        /// </summary>
        private bool highlighted = false;
        #endregion

        #region 事件响应方法
        /// <summary>
        /// 响应鼠标移入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void c_MouseEnter(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = SystemColors.GradientInactiveCaption;
        }

        /// <summary>
        /// 响应鼠标移出。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void c_MouseLeave(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = highlighted ? Color.Gold : SystemColors.Control;
        }

        /// <summary>
        /// 响应所有控件单击事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raiseClick(object sender, EventArgs e)
        {
            if (TodayReminderViewClick != null)
            {
                TodayReminderViewClick(this, new EventArgs());
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 正在显示的提醒。
        /// </summary>
        private Reminder _Reminder = null;
        /// <summary>
        /// 正在显示的提醒。
        /// </summary>
        public Reminder Reminder
        {
            get
            {
                return _Reminder;
            }
            set
            {
                _Reminder = value;
                nameLabel.Text = value.Name;
                fileLabel.Text = value.FilePath;
                if (!Today.HasReminderDone(value)) Highlight();
            }
        }
        #endregion

        #region 公共事件
        public event EventHandler TodayReminderViewClick;
        #endregion

        #region 公共方法
        /// <summary>
        /// 使提醒高亮。
        /// </summary>
        public void Highlight()
        {
            highlighted = true;
            nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
            tableLayoutPanel1.BackColor = Color.Gold;
        }

        /// <summary>
        /// 取消提醒高亮。
        /// </summary>
        public void Grey()
        {
            highlighted = false;
            nameLabel.Font = new Font(nameLabel.Font, FontStyle.Regular);
            tableLayoutPanel1.BackColor = SystemColors.Control;
        }
        #endregion

    }
}
