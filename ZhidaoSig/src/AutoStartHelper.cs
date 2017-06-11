using System;
using System.IO;
using System.Windows.Forms;
using TaskScheduler;

namespace FuckZhidaoSig
{
    internal class AutoStartHelper
    {
        /// <summary>
        /// AppData的程序目录。
        /// </summary>
        public static string DataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ZhidaoSig";

        /// <summary>
        /// 主程序路径。
        /// </summary>
        public static string ProgramPath = DataDirectory + @"\ZhidaoSig.exe";

        /// <summary>
        /// 计划任务COM组件路径。
        /// </summary>
        public static string TaskSchedulerPath = DataDirectory + @"\Interop.TaskScheduler.dll";

        /// <summary>
        /// 检查、创建AppData程序目录。
        /// </summary>
        internal  static bool dirPrepare()
        {
            bool ret = false;
            ret = Directory.Exists(DataDirectory);
            if (!ret)
            {
                Directory.CreateDirectory(DataDirectory);
            }
            return ret;
        }

        /// <summary>
        /// 一个计划任务（管理器）COM实例。
        /// </summary>
        private static TaskSchedulerClass scheduler = new TaskSchedulerClass();

        public static void TaskAdd()
        {
            try
            {
                dirPrepare();
                File.Copy(Application.StartupPath + @"\Interop.TaskScheduler.dll", TaskSchedulerPath);
                File.Copy(Application.ExecutablePath, ProgramPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("拷贝出错：" + ex.Message);
                return;
            }
            //计划任务相关。
            //文档请参见：
            //http://msdn.microsoft.com/zh-cn/library/aa383608(v=vs.85).aspx

            //连接到本地计划任务服务。
            scheduler.Connect(null, null, null, null);

            //得到一个空任务定义对象来填充设置和属性。
            ITaskDefinition task = scheduler.NewTask(0);

            //相关设置。
            task.RegistrationInfo.Author = "包布丁";
            task.RegistrationInfo.Description = "百度知道签到的计划任务。";
            //仅网络可用时开始。
            task.Settings.RunOnlyIfNetworkAvailable = true;
            //若已经超过计划的运行时间，立即运行。
            //每日系统启动时将会立即运行。
            task.Settings.StartWhenAvailable = true;
            //如果任务失败，尝试重新启动最多3次。
            task.Settings.RestartCount = 3;
            //如果任务失败，按2分钟一次的频率重新启动。
            task.Settings.RestartInterval = "PT2M";
            //如果任务运行时间超过1分钟，停止任务。
            task.Settings.ExecutionTimeLimit = "PT1M";
            //若此任务已经运行，则停止现有实例。
            task.Settings.MultipleInstances = _TASK_INSTANCES_POLICY.TASK_INSTANCES_STOP_EXISTING;

            //每日触发的触发器。
            IDailyTrigger trigger = (IDailyTrigger)task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
            //延迟2分钟，为了防止本地计算机数秒的时间误差。
            trigger.StartBoundary = "2014-08-01T00:02:00";

            //启动程序的操作。
            IExecAction action = (IExecAction)task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
            action.Path = ProgramPath;

            ITaskFolder folder = scheduler.GetFolder("\\");

            //注册任务。
            IRegisteredTask regTask = folder.RegisterTaskDefinition(
                "zhidaosig",
                task,
                (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE,
                null, //user
                null, // password
                _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN,
                ""); //sddl [in, optional] The security descriptor that is associated with the registered task. You can specify the access control list (ACL) in the security descriptor for a task in order to allow or deny certain users and groups access to a task.

            MessageBox.Show("完成" + Environment.NewLine + "程序已经拷贝至AppData目录下，且每天会运行。", "自动启动", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
