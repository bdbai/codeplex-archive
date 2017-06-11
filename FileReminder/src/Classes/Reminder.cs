using System;
using System.Xml.Serialization;

namespace FileReminder
{
    /// <summary>
    /// 提醒事项类。
    /// </summary>
    public class Reminder
    {
        /// <summary>
        /// 提醒名称。
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 提醒的文件路径。
        /// </summary>
        [XmlAttribute("path")]
        public string FilePath { get; set; }

        /// <summary>
        /// 标记文件是否锁定。
        /// </summary>
        [XmlAttribute("locked")]
        public bool IsFileLocked { get; set; }

        /// <summary>
        /// 标记提醒是否启用。
        /// </summary>
        [XmlAttribute("enable")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 提醒日期-月。
        /// </summary>
        [XmlAttribute("month")]
        public int Month { get; set; }

        /// <summary>
        /// 提醒日期-日。
        /// </summary>
        [XmlAttribute("day")]
        public int Day { get; set; }

        /// <summary>
        /// 上一次打开提醒的年份。
        /// </summary>
        [XmlAttribute("lastrun")]
        public int LastRunYear { get; set; }
    }
}
