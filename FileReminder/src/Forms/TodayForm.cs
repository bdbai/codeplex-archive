using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// “今日事项”窗体。
    /// </summary>
    public partial class TodayForm : Form
    {
        #region 构造函数
        /// <summary>
        /// 窗体构造函数。
        /// </summary>
        public TodayForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件响应方法
        /// <summary>
        /// 窗体加载响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodayForm_Load(object sender, EventArgs e)
        {
            loadToday();
        }

        /// <summary>
        /// 提醒控件单击响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todayReminderViewClick(object sender, EventArgs e)
        {
            TodayReminderView sc = sender as TodayReminderView;
            if (sc == null)
            {
                return;
            }
            else
            {
                sc.Grey();
            }
            Reminder reminder = sc.Reminder;
            if (reminder == null)
            {
                return;
            }
            else
            {
                FileReminder.Program.unlockToday();
                FileReminder.Program.DoReminder(reminder);
            }
            loadToday();
        }

        /// <summary>
        /// 窗体关闭响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.runSpe(); //☆
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载今日内容。
        /// </summary>
        private void loadToday()
        {
            panel1.Controls.Clear();
            Today today = Today.GetToday();
            int undoneCount = today.UndoneTodayReminders.Count;
            int totalCount = today.TodayReminders.Count;
            if (totalCount == 0)
            {
                todayLabel.Text = "今天没有提醒。";
            }
            else
            {
                StringBuilder tipBuilder = new StringBuilder();
                tipBuilder.AppendFormat("今天共有{0}个提醒", totalCount.ToString());
                if (undoneCount == 0)
                {
                    todayLabel.Font = new Font(todayLabel.Font, FontStyle.Regular);
                    tipBuilder.Append("。");
                }
                else
                {
                    todayLabel.Font = new Font(todayLabel.Font, FontStyle.Bold);
                    tipBuilder.AppendFormat("，{0}个还未查看。", undoneCount.ToString());
                }
                todayLabel.Text = tipBuilder.ToString();
                int topOffset = 3;
                foreach (Reminder reminder in today.TodayReminders)
                {
                    TodayReminderView trv = new TodayReminderView(reminder);
                    trv.TodayReminderViewClick += todayReminderViewClick;
                    trv.Size = new Size(398, 100);
                    trv.Location = new Point(3, topOffset);
                    topOffset += 103;
                    trv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panel1.Controls.Add(trv);
                }
            }
        }
        #endregion

    }
}
