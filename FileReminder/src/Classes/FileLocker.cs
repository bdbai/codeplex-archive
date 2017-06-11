using System;
using System.IO;

namespace FileReminder
{
    /// <summary>
    /// 通过独占实现的文件锁。
    /// </summary>
    public class FileLocker
    {
        private FileLocker() { }

        /// <summary>
        /// 初始化文件锁。
        /// </summary>
        /// <param name="file"></param>
        public FileLocker(string file)
        {
            FilePath = file;
            IsLocked = false;
        }

        /// <summary>
        /// 独占文件流（代表句柄）。
        /// </summary>
        private FileStream lockStream = null;

        /// <summary>
        /// 文件路径。
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 已锁标志。
        /// </summary>
        public bool IsLocked { get; private set; }

        /// <summary>
        /// 锁定文件。
        /// </summary>
        public void Lock()
        {
            Unlock();

            try
            {
                lockStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                IsLocked = true;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("文件找不到或仍处于打开状态。");
            }

        }

        /// <summary>
        /// 解锁文件。
        /// </summary>
        public void Unlock()
        {
            if (lockStream != null)
            {
                try
                {
                    lockStream.Close();
                    lockStream.Dispose();
                    lockStream = null;
                    IsLocked = false;
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~FileLocker()
        {
            Unlock();
        }
    }
}
