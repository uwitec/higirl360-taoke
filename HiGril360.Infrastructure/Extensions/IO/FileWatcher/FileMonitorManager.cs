using System;
using System.Collections.Generic;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO.FileWatcher
{
    public class FileMonitorManager : IFileMonitorManager
    {
        public static class FileMonitorManagerHolder
        {
            public static FileMonitorManager Instance = new FileMonitorManager();
        }

        public static FileMonitorManager Instance
        {
            get { return FileMonitorManagerHolder.Instance; }
        }

        #region IFileMonitorManager 成员

        public bool TryAcquireMonitor(string directoryPath, string filter, bool isContinued, Action<IFileMonitor> fileChangeCallback, WatcherChangeTypes changeTypes, ref IFileMonitor fileMonitor)
        {
            try
            {
                fileMonitor= new FileMonitor(directoryPath, filter, isContinued, fileChangeCallback, changeTypes);

                return true;
            }
            catch
            {
                fileMonitor = null;

                return false;
            }
        }

        #endregion
    }
}
