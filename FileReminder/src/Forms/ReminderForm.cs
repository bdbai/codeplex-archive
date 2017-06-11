using System;
using System.Windows.Forms;

namespace FileReminder
{
    /// <summary>
    /// 提醒事项窗体。
    /// </summary>
    public partial class ReminderForm : Form
    {
        #region 构造函数
        /// <summary>
        /// 用一个新的提醒初始化窗体。
        /// </summary>
        public ReminderForm()
        {
            InitializeComponent();

            Reminder r = new Reminder();
            r.IsFileLocked = true;
            r.IsEnabled = true;
            r.Month = DateTime.Now.Month;
            r.Day = DateTime.Now.Day;
            Reminder = r;
        }

        /// <summary>
        /// 用一个提醒初始化窗体。
        /// </summary>
        /// <param name="r"></param>
        public ReminderForm(Reminder r)
        {
            InitializeComponent();

            Reminder = r;
        }
        #endregion

        #region 字段
        /// <summary>
        /// 标志用户取消。
        /// </summary>
        private bool isCanceled = false;

        /// <summary>
        /// 即将进行修改的提醒事项。
        /// </summary>
        public Reminder Reminder = null;

        /// <summary>
        /// 标志该提醒是否已被修改。
        /// </summary>
        public bool HasChanged = false;
        #endregion

        #region 事件响应方法
        /// <summary>
        /// 窗体加载响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReminderForm_Load(object sender, EventArgs e)
        {
            loadReminder();
        }

        /// <summary>
        /// 月份切换响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dayChange();
        }

        /// <summary>
        /// 文件选择器响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filePickerButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePickerButton.Text = "已更改";
            }
        }

        /// <summary>
        /// 确定按钮响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptButton_Click(object sender, EventArgs e)
        {
            accept();
        }

        /// <summary>
        /// 窗体关闭响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReminderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isCanceled || MessageBox.Show("放弃修改？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                HasChanged = false;
            }
            else
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// 取消按钮响应。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            isCanceled = true;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载提醒。
        /// </summary>
        private void loadReminder()
        {
            nameBox.Text = Reminder.Name;
            monthComboBox.SelectedIndex = Reminder.Month - 1;
            dayComboBox.SelectedIndex = Reminder.Day - 1;
            if (!string.IsNullOrEmpty(Reminder.FilePath))
            {
                filePickerButton.Text = "已选择";
            }
            openFileDialog1.FileName = Reminder.FilePath;
            isFileLockedCheckBox.Checked = Reminder.IsFileLocked;
            isEnableCheckBox.Checked = Reminder.IsEnabled;
        }

        /// <summary>
        /// 根据选定的月份修改日列表。
        /// </summary>
        private void dayChange()
        {
            int monthSelected = monthComboBox.SelectedIndex + 1;
            dayComboBox.Items.Clear();
            for (int day = 1; day <= DateTime.DaysInMonth(2012, monthSelected); day++)
            {
                dayComboBox.Items.Add(day.ToString());
            }
            dayComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 提交修改。
        /// </summary>
        private void accept()
        {
            string name = nameBox.Text;
            int monthSelected = monthComboBox.SelectedIndex + 1;
            int daySelected = dayComboBox.SelectedIndex + 1;
            string filePicked = openFileDialog1.FileName;
            bool isFileLocked = isFileLockedCheckBox.Checked;
            bool isEnabled = isEnableCheckBox.Checked;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入标题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(filePicked))
            {
                MessageBox.Show("请选择文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Reminder.Name = name;
            Reminder.Month = monthSelected;
            Reminder.Day = daySelected;
            Reminder.IsFileLocked = isFileLocked;
            Reminder.FilePath = filePicked;
            Reminder.IsEnabled = isEnabled;

            DateTime now = DateTime.Now;
            Reminder.LastRunYear = now.Year - 1;
            HasChanged = true;
            this.FormClosing -= ReminderForm_FormClosing;
            Close();
        }
        #endregion
    }
}
