using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FileReminder
{
    /// <summary>
    /// 提醒信息管理类。
    /// </summary>
    public static class ReminderManager
    {
        /// <summary>
        /// 缓存的提醒集合。
        /// </summary>
        private static Collection<Reminder> reminderCaches = null;

        /// <summary>
        /// 获得、准备提醒路径。
        /// </summary>
        /// <returns></returns>
        internal static string getRemindersPath()
        {
            string orig = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + Application.ProductName;
            if (!Directory.Exists(orig))
            {
                try
                {
                    Directory.CreateDirectory(orig);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return orig;

        }

        /// <summary>
        /// 读取提醒信息。
        /// </summary>
        /// <returns>读取到的提醒信息集合。</returns>
        public static Collection<Reminder> GetReminders()
        {
            if (reminderCaches == null)
            {
                Collection<Reminder> reminders = new Collection<Reminder>();
                if (File.Exists(getRemindersPath() + @"\reminders.xml"))
                {
                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Collection<Reminder>));
                        reminders = new Collection<Reminder>();
                        using (StreamReader tr = new StreamReader(getRemindersPath() + @"\reminders.xml"))
                        {
                            reminders = xs.Deserialize(tr) as Collection<Reminder>;
                            reminderCaches = reminders;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    /*if (reminders != null)
                    {
                        foreach (Reminder item in reminders)
                        {
                            yield return item;
                        }
                    }*/
                }
                //yield break;
                return reminders;
            }
            else
            {
                return reminderCaches;
            }
        }

        /// <summary>
        /// 保存提醒信息。
        /// </summary>
        /// <param name="reminders">要保存的提醒信息集合。</param>
        public static void SaveReminders(Collection<Reminder> reminders)
        {
            reminderCaches = reminders;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Collection<Reminder>));
                using (StreamWriter tr = new StreamWriter(getRemindersPath() + @"\reminders.xml", false))
                {
                    xs.Serialize(tr, reminders);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 更新提醒。
        /// </summary>
        /// <remarks>适用于仅修改而未添加、删除的情况。</remarks>
        public static void UpdateReminders()
        {
            if (reminderCaches == null)
            {
                GetReminders();
            }
            SaveReminders(reminderCaches);
        }
    }
}
