using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// “设置”界面。
    /// </summary>
    public partial class PreferenceForm : Form
    {
        #region 构造函数
        /// <summary>
        /// 初始化设置界面。
        /// </summary>
        public PreferenceForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 存放已读取的提醒。
        /// </summary>
        private Collection<Reminder> cachedReminders = null;
        #endregion

        #region 事件响应方法
        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            load();
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = monthComboBox.SelectedIndex;
            dayComboBox.Items.Clear();
            dayComboBox.Items.Add("所有");
            if (month != 0)
            {
                for (int day = 1; day <= DateTime.DaysInMonth(2012, month); day++)
                {
                    dayComboBox.Items.Add(day.ToString());
                }
            }
            dayComboBox.SelectedIndex = 0;
        }

        private void dayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadReminder();
        }

        private void reminderListView_DoubleClick(object sender, EventArgs e)
        {
            editReminder(false);
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            editReminder(true);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            removeReminder(true);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addReminder();
        }

        private void todayLabel_Click(object sender, EventArgs e)
        {
            using (TodayForm tf = new TodayForm())
            {
                tf.ShowDialog();
                loadToday();
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 整个窗体数据加载。
        /// </summary>
        private void load()
        {
            monthComboBox.SelectedIndex = DateTime.Now.Month;
            dayComboBox.SelectedIndex = DateTime.Now.Day;

            loadToday();
        }

        /// <summary>
        /// 加载“今日事项”迷你框。
        /// </summary>
        private void loadToday()
        {
            Today today = Today.GetToday();
            int undoneCount = today.UndoneTodayReminders.Count;
            int totalCount = today.TodayReminders.Count;
            if (totalCount == 0)
            {
                todayLabel.Text = "今天没有提醒。";
            }
            else
            {
                todayLabel.Text = "今天共有" + totalCount.ToString() + "个提醒";
                if (undoneCount == 0)
                {
                    todayLabel.ForeColor = SystemColors.ControlText;
                    todayLabel.Font = new Font(todayLabel.Font, FontStyle.Regular);
                    todayLabel.Text += "。";
                }
                else
                {
                    todayLabel.ForeColor = SystemColors.HotTrack;
                    todayLabel.Font = new Font(todayLabel.Font, FontStyle.Bold);
                    todayLabel.Text += "，" + undoneCount.ToString() + "个还未查看。";
                }
            }
        }

        /// <summary>
        /// 加载提醒。
        /// </summary>
        private void loadReminder()
        {
            int selectedMonth = monthComboBox.SelectedIndex;
            int selectedDay = dayComboBox.SelectedIndex;

            reminderListView.Items.Clear();

            cachedReminders = ReminderManager.GetReminders();

            foreach (Reminder reminder in cachedReminders)
            {
                if (((selectedMonth == 0) || (reminder.Month == selectedMonth)) &&
                    ((selectedDay == 0) || (reminder.Day == selectedDay)))
                {
                    addReminderToList(reminder);
                }
            }
        }

        /// <summary>
        /// 将指定的提醒显示到列表中。
        /// </summary>
        /// <param name="reminder">要显示的提醒。</param>
        private void addReminderToList(Reminder reminder)
        {
            ListViewItem item = new ListViewItem();
            item.Text = reminder.Month.ToString();
            item.SubItems.Add(reminder.Day.ToString());
            item.SubItems.Add(reminder.Name);
            item.SubItems.Add(reminder.IsEnabled ? "启用" : "禁用");
            item.SubItems.Add(reminder.FilePath);
            item.Tag = reminder;
            reminderListView.Items.Add(item);
        }

        /// <summary>
        /// 添加一个提醒。
        /// </summary>
        private void addReminder()
        {
            using (ReminderForm rf = new ReminderForm())
            {
                rf.ShowDialog();
                if (rf.HasChanged)
                {
                    cachedReminders.Add(rf.Reminder);
                    ReminderManager.SaveReminders(cachedReminders);
                    if (rf.Reminder.IsEnabled && rf.Reminder.IsFileLocked)
                    {
                        FileLockerManager.LockFile(rf.Reminder.FilePath);
                    }
                    MessageBox.Show(string.Format("已添加。{0}{0}当这一天到来时，我们会用这个文件提醒你。", Environment.NewLine), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    monthComboBox.SelectedIndex = rf.Reminder.Month;
                    dayComboBox.SelectedIndex = rf.Reminder.Day;
                    loadReminder();
                    loadToday();
                }
            }
        }

        /// <summary>
        /// 编辑一个提醒。
        /// </summary>
        /// <param name="warnIfUnselected">指定当用户未选定任何提醒时，是否发出警告。</param>
        private void editReminder(bool warnIfUnselected)
        {
            if (reminderListView.SelectedItems.Count > 0)
            {
                Reminder r = reminderListView.SelectedItems[0].Tag as Reminder;
                if (r != null)
                {
                    using (ReminderForm rf = new ReminderForm(r))
                    {
                        rf.ShowDialog();
                        if (rf.HasChanged)
                        {
                            ReminderManager.UpdateReminders();
                            loadToday();
                            DateTime now = DateTime.Now;
                            if (rf.Reminder.Month == now.Month && rf.Reminder.Day == now.Day
                                || !rf.Reminder.IsEnabled
                                || !rf.Reminder.IsFileLocked)
                            {
                                FileLockerManager.UnlockFile(r.FilePath);
                            }
                            else
                            {
                                FileLockerManager.LockFile(r.FilePath);
                            }
                            MessageBox.Show("已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            monthComboBox.SelectedIndex = rf.Reminder.Month;
                            dayComboBox.SelectedIndex = rf.Reminder.Day;
                            loadReminder();
                            loadToday();
                        }
                    }
                }
            }
            else if (warnIfUnselected)
            {
                MessageBox.Show("请选择一项来修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除一个提醒。
        /// </summary>
        /// <param name="warnIfUnselected">指定当用户未选定任何提醒时，是否发出警告。</param>
        private void removeReminder(bool warnIfUnselected)
        {
            if (reminderListView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Reminder r = reminderListView.SelectedItems[0].Tag as Reminder;
                    if (r != null)
                    {
                        cachedReminders.Remove(r);
                        ReminderManager.SaveReminders(cachedReminders);
                        FileLockerManager.UnlockFile(r.FilePath);
                        loadToday();
                        loadReminder();
                        MessageBox.Show("已删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (warnIfUnselected)
            {
                MessageBox.Show("请选择一项来删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
