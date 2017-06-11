using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileReminder
{
    static class Program
    {
        #region 主函数
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string programPath = ReminderManager.getRemindersPath() + @"\";
            string exePath = programPath + Application.ProductName + ".exe";
            string startupAddress = "\"" + exePath + "\"" + @" run"; //带参的运行路径。

            try
            {
                //在系统任意处运行一次程序，可以达到更新的目的。
                if (Application.ExecutablePath != exePath)
                {
                    //杀掉老进程。
                    int currentProcessId = Process.GetCurrentProcess().Id;
                    foreach (Process p in Process.GetProcessesByName(Application.ProductName))
                    {
                        Debug.WriteLine(p.ProcessName);
                        if (p.Id != currentProcessId)
                        {
                            p.Kill();
                        }
                    }
                update://等待进程完全结束。
                    try
                    {
                        Thread.Sleep(100);
                        File.Copy(Application.ExecutablePath, exePath, true);
                    }
                    catch (Exception)
                    {
                        goto update;
                    }
                }
                RegistryKey run;
                run = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                string runitem = run.GetValue(Application.ProductName) as string;
                if (string.IsNullOrEmpty(runitem) || runitem != startupAddress)
                {
                    MessageBox.Show(string.Format("接下来会添加启动项，{0}请在安全软件的提示框中允许此操作。", Environment.NewLine), "FileReminder：用一个文件纪念特殊的日子。", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    run.SetValue(Application.ProductName, startupAddress);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (args.Length > 0)
            {
                if (args[0] == "run")
                {
                    //检测到第一个参数为“run”。
                    //作为后台进程运行。
                    runCore();
                    return;
                }
            }
            else
            {
                //没有额外参数。
                //显示设置界面后，运行一个新的后台进程。
                Application.Run(new PreferenceForm());
                Process.Start(programPath + Application.ProductName + ".exe", "run");
            }
        }
        #endregion

        #region 后台过程
        /// <summary>
        /// 打开一个提醒对应的文件。
        /// </summary>
        /// <param name="r">要读取的提醒。</param>
        public static void DoReminder(Reminder r)
        {
            Thread worker = new Thread(new ThreadStart(() =>
            {
                if (!r.IsEnabled) return;
                Process process;
                try
                {
                    process = Process.Start(r.FilePath);
                }
                catch (Exception)
                {
                    MessageBox.Show("打开文件失败：" + r.FilePath);
                    return;
                }
            }));
            worker.Start();
            r.LastRunYear = DateTime.Now.Year;
            ReminderManager.UpdateReminders();
        }

        /// <summary>
        /// 锁定所有提醒对应的文件。
        /// </summary>
        internal static void lockAll()
        {
            foreach (Reminder reminder in ReminderManager.GetReminders())
            {
                if (reminder.IsFileLocked && reminder.IsEnabled)
                {
                    try
                    {
                        FileLockerManager.LockFile(reminder.FilePath);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// 解锁今日提醒对应的文件。
        /// </summary>
        internal static void unlockToday()
        {
            Today today = Today.GetToday();
            foreach (Reminder reminder in today.TodayReminders)
            {
                try
                {
                    FileLockerManager.UnlockFile(reminder.FilePath);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// 特别版特殊处理。
        /// </summary>
        internal static void runSpe()
        {
            if (!Application.ProductName.Contains("FileReminderSpe")) return;
            DateTime now = DateTime.Now;
            if (now.Month == 12 && now.Day == 20)
            {
                using (HappyBirthdaySpeForm hbsf = new HappyBirthdaySpeForm())
                {
                    hbsf.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 主循环核心。
        /// </summary>
        internal static void runCore()
        {
            //创建一个单独的线程以供消息循环。
            Thread iconThread = new Thread(new ThreadStart(iconWorker));
            iconThread.SetApartmentState(ApartmentState.STA);
            iconThread.Start();

            //主循环。
            while (true)
            {
                runSpe();
                lockAll();
                unlockToday();
                Today today = Today.GetToday();

                if (today.UndoneTodayReminders.Count == 1)
                {
                    DoReminder(today.UndoneTodayReminders[0]);
                }
                else if (today.UndoneTodayReminders.Count > 1)
                {
                    using (TodayForm tf = new TodayForm())
                    {
                        tf.ShowDialog();
                    }
                }

                //挂起主循环线程直到第二天。
                DateTime now = DateTime.Now;
                DateTime tomorrow = now.AddDays(1f);
                DateTime nextDay = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 1);
                TimeSpan gap = nextDay - now;
                Thread.Sleep(gap);
            }
        }
        #endregion

        #region 图标和UI
        /// <summary>
        /// 程序托盘图标。
        /// </summary>
        internal static NotifyIcon Icon;

        /// <summary>
        /// 托盘图标管理和托盘图标、UI消息循环。
        /// </summary>
        internal static void iconWorker()
        {
            Icon = new NotifyIcon();
            Icon.Text = "FileReminder";
            Icon.Icon = FileReminder.Properties.Resources.icon;

            MenuItem todayMenu = new MenuItem("今日事项", (s, e) =>
            {
                using (TodayForm tf = new TodayForm())
                {
                    tf.ShowDialog();
                }
            });
            todayMenu.DefaultItem = true;
            MenuItem preferenceMenu = new MenuItem("设置", (s, e) =>
            {
                using (PreferenceForm pf = new PreferenceForm())
                {
                    pf.ShowDialog();
                }
            });
            MenuItem exitMenu = new MenuItem("暂时退出", (s, e) =>
            {
                FileLockerManager.UnlockAll();
                exit(0);
            });
            MenuItem uninstallMenu = new MenuItem("停止FileReminder", (s, e) =>
            {
                if (MessageBox.Show("停止吗？新的提醒将不会再显示了。", "FileReminder停止", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        RegistryKey run;
                        run = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                        run.DeleteValue(Application.ProductName);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    FileLockerManager.UnlockAll();
                    exit(0);
                }

            });

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(todayMenu);
            menu.MenuItems.Add(preferenceMenu);
            menu.MenuItems.Add(uninstallMenu);
            menu.MenuItems.Add(exitMenu);
            Icon.ContextMenu = menu;

            Icon.DoubleClick += (s, e) =>
            {
                using (TodayForm tf = new TodayForm())
                {
                    tf.ShowDialog();
                }
            };

            Icon.Visible = true;

            Application.Run(); //执行消息循环。
        }
        #endregion

        #region 结束
        /// <summary>
        /// 退出程序。
        /// </summary>
        /// <param name="exitCode">退出代码。</param>
        internal static void exit(int exitCode)
        {
            try
            {
                Icon.Visible = false;
            }
            catch (Exception) { }
            Environment.Exit(exitCode);
        }
        #endregion
    }
}
