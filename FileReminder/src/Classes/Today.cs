using System;
using System.Collections.ObjectModel;

namespace FileReminder
{
    /// <summary>
    /// 今日相关类。
    /// </summary>
    public class Today
    {
        private Today() { }

        /// <summary>
        /// 检查提醒事项是否已读。
        /// </summary>
        /// <param name="r">要检查的提醒事项。</param>
        /// <returns></returns>
        public static bool HasReminderDone(Reminder r)
        {
            DateTime now = DateTime.Now;
            DateTime scheduledRunTime = new DateTime(r.LastRunYear + 1, r.Month, r.Day);
            return now < scheduledRunTime;
        }
        
        /// <summary>
        /// 获得“今日相关”提醒事项。
        /// </summary>
        /// <returns>今日相关类的实例。</returns>
        public static Today GetToday()
        {
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            Today ret = new Today();
            Collection<Reminder> allReminders = ReminderManager.GetReminders();
            foreach (Reminder r in allReminders)
            {
                if (r.IsEnabled && r.Month == month && r.Day == day)
                {
                    ret.TodayReminders.Add(r);
                    if (!HasReminderDone(r))
                    {
                        ret.UndoneTodayReminders.Add(r);
                    }
                }
            }
            return ret;
        }
        
        /// <summary>
        /// 今日未读提醒。
        /// </summary>
        public Collection<Reminder> UndoneTodayReminders = new Collection<Reminder>();

        /// <summary>
        /// 今日所有提醒。
        /// </summary>
        public Collection<Reminder> TodayReminders = new Collection<Reminder>();
    }
}
