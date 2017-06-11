using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TaskScheduler;

namespace XiamiSigLite
{
    /// <summary>
    /// 虾米帐户收集。
    /// </summary>
    public partial class InfoCollector : Form
    {

        /// <summary>
        /// AppData的程序目录。
        /// </summary>
        public static string DataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\XiamiSig";

        /// <summary>
        /// 主程序路径。
        /// </summary>
        public static string ProgramPath = DataDirectory + @"\XiamiSigLite.exe";

        /// <summary>
        /// Email数据路径。
        /// </summary>
        public static string EmailPath = DataDirectory + @"\email.txt";

        /// <summary>
        /// 密码数据路径。
        /// </summary>
        public static string PasswordPath = DataDirectory + @"\password.txt";

        /// <summary>
        /// 静默签到的日志路径。
        /// </summary>
        /// <remarks>请使用WriteLog方法来写日志。</remarks>
        private static string logPath = DataDirectory + @"\litelog.txt";

        /// <summary>
        /// 获得帐户邮箱。
        /// </summary>
        public static string Email
        {
            get
            {
                dirPrepare();
                return File.ReadAllText(EmailPath);
            }
            set
            {
                dirPrepare();
                File.WriteAllText(EmailPath, value);
            }
        }

        /// <summary>
        /// 获得帐户密码。
        /// </summary>
        public static string Password
        {
            get
            {
                dirPrepare();
                return File.ReadAllText(PasswordPath);
            }
            set
            {
                dirPrepare();
                File.WriteAllText(PasswordPath, value);
            }
        }

        /// <summary>
        /// 写日志。
        /// </summary>
        /// <param name="log">日志内容。</param>
        public static void WriteLog(string log)
        {
            dirPrepare();
            try
            {
                File.AppendAllText(logPath, string.Format("{0},{1:3}--{2}{3}", DateTime.Now.ToString(), DateTime.Now.Millisecond.ToString(), log, Environment.NewLine));
            }
            catch (Exception ex)
            {
                Console.WriteLine("写日志错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 收集虾米帐户。
        /// </summary>
        /// <returns>指示是否收集到帐户。</returns>
        public static bool Collect()
        {
            using (InfoCollector collector = new InfoCollector())
            {
                return collector.ShowDialog();
            }
        }

        /// <summary>
        /// 检查、创建AppData程序目录。
        /// </summary>
        private static void dirPrepare()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
            if (!File.Exists(EmailPath))
            {
                File.Create(EmailPath);
            }
            if (!File.Exists(PasswordPath))
            {
                File.Create(PasswordPath);
            }
        }

        /// <summary>
        /// 指示是否收集到帐户。
        /// </summary>
        private bool collected = false;

        /// <summary>
        /// 一个计划任务（管理器）COM实例。
        /// </summary>
        private TaskSchedulerClass scheduler = new TaskSchedulerClass();

        /// <summary>
        /// 窗体加载。
        /// </summary>
        public InfoCollector()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);
            InitializeComponent();
            button3.Enabled = Application.ExecutablePath != ProgramPath;
        }

        /// <summary>
        /// 显示模态窗口。
        /// </summary>
        /// <returns>数据是否已输入无误。</returns>
        public new bool ShowDialog()
        {
            base.ShowDialog();
            return collected;
        }

        /// <summary>
        /// 检查已存的数据的正确性。
        /// </summary>
        /// <returns>是否正确无误。</returns>
        public static bool CheckData()
        {
            return CheckData(Email, Password);
        }

        /// <summary>
        /// 检查数据的正确性。
        /// </summary>
        /// <param name="email">要检查的邮箱。</param>
        /// <param name="password">要检查的密码。</param>
        /// <returns>是否正确无误。</returns>
        public static bool CheckData(string email, string password)
        {
            //空值检查。
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            //邮箱正确性检查。
            Regex r = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool emailValid = r.IsMatch(email);
            if (!emailValid) return false;

            return true;
        }

        /// <summary>
        /// 保存输入的数据。
        /// </summary>
        /// <returns>输入是否有误。</returns>
        private bool saveData()
        {
            if (CheckData(textBox1.Text, textBox2.Text) == false) return false;
            string email = textBox1.Text;
            string password = textBox2.Text;

            dirPrepare();

            //写入数据。
            File.WriteAllText(EmailPath, email);
            File.WriteAllText(PasswordPath, password);
            return true;
        }

        /// <summary>
        /// “保存”按钮点击。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>检查数据、写入数据。</remarks>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!saveData())
            {
                MessageBox.Show("输入有误，请检查。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            collected = true;
            this.Close();
        }

        /// <summary>
        /// 关窗。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closing(object sender, EventArgs e)
        {
            collected = false;
            Program.Exit(0);
        }

        /// <summary>
        /// 加载帐户数据到窗体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoCollector_Load(object sender, EventArgs e)
        {
            textBox1.Text = Email;
            textBox2.Text = Password;
        }

        /// <summary>
        /// “保存+自启”按钮点击。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //检查并保存数据。
                if (!saveData())
                {
                    MessageBox.Show("输入有误，请检查。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //将程序复制至AppData程序目录下。
                dirPrepare();
                File.Copy(Application.ExecutablePath, ProgramPath, true);
                //if (File.Exists(Application.ExecutablePath + ".config"))
                //{
                //    File.Copy(Application.ExecutablePath + ".config", programPath + ".config", true);
                //}

                //计划任务相关。
                scheduler.Connect(null, null, null, null);
                ITaskDefinition task = scheduler.NewTask(0);
                task.RegistrationInfo.Author = "包布丁";
                task.RegistrationInfo.Description = "虾米签到的计划任务。";
                //仅网络可用时开始。
                task.Settings.RunOnlyIfNetworkAvailable = true;
                //若已经超过计划的运行时间，立即运行。
                //每日系统启动时将会立即运行。
                task.Settings.StartWhenAvailable = true;

                //每日触发的触发器。
                IDailyTrigger trigger = (IDailyTrigger)task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                //延迟1分钟，为了防止本地计算机数秒的时间误差。
                trigger.StartBoundary = "2014-08-01T00:01:00";

                //启动程序的操作。
                IExecAction action = (IExecAction)task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
                action.Path = ProgramPath;
                //参数任意，但必须有。见Program.cs。
                action.Arguments = "sign";
                ITaskFolder folder = scheduler.GetFolder("\\");

                //注册任务。
                IRegisteredTask regTask = folder.RegisterTaskDefinition(
                    "xiamisig",
                    task,
                    (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE,
                    null, //user
                    null, // password
                    _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN,
                    ""); //sddl [in, optional] The security descriptor that is associated with the registered task. You can specify the access control list (ACL) in the security descriptor for a task in order to allow or deny certain users and groups access to a task.

                MessageBox.Show("完成" + Environment.NewLine + "程序已经拷贝至AppData目录下，且每天会运行。", "自动启动", MessageBoxButtons.OK, MessageBoxIcon.Information);
                collected = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错：" + ex.Message);
            }
        }

    }
}
