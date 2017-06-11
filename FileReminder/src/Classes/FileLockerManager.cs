using System;
using System.Collections.Generic;
using System.Linq;

namespace FileReminder
{
    /// <summary>
    /// 全局文件锁管理。
    /// </summary>
    public static class FileLockerManager
    {
        /// <summary>
        /// 所有的文件锁。
        /// </summary>
        private static Dictionary<string, FileLocker> lockers = new Dictionary<string, FileLocker>();

        /// <summary>
        /// 锁定文件。
        /// </summary>
        /// <param name="file">文件路径。</param>
        /// <returns>使用的文件锁。</returns>
        public static FileLocker LockFile(string file)
        {
            FileLocker locker;
            if (CheckIfLocked(file))
            {
                locker = lockers[file];
            }
            else
            {
                locker = new FileLocker(file);
            }
            try
            {
                locker.Lock();
                lockers.Add(file, locker);
                return locker;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 解锁文件。
        /// </summary>
        /// <param name="file">文件路径。</param>
        public static void UnlockFile(string file)
        {
            if (CheckIfLocked(file))
            {
                FileLocker locker = lockers[file];
                lockers.Remove(file);
                try
                {
                    locker.Unlock();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 检查文件是否已锁。
        /// </summary>
        /// <param name="file">文件路径。</param>
        /// <returns></returns>
        public static bool CheckIfLocked(string file)
        {
            return lockers.Keys.Contains<string>(file);
        }

        /// <summary>
        /// 全部解锁。
        /// </summary>
        public static void UnlockAll()
        {
            foreach (KeyValuePair<string, FileLocker> item in lockers)
            {
                FileLocker locker = item.Value;
                try
                {
                    locker.Unlock();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            lockers.Clear();
        }
    }
}
