using System;
using System.Collections.Generic;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO.FileWatcher
{
    public interface IFileMonitorManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath">监控目录路径</param>
        /// <param name="filter">监控筛选字符串，用于确定在目录中监视哪些文件 如：*.* / *.jpg / logo.gif</param>
        /// <param name="isContinued">是否持续监控，True持续监控，False则文件发生变生触发事件后监控失效</param>
        /// <param name="fileChangeCallback">文件发生变化后回调的方法</param>
        /// <param name="actionOptions"></param>
        /// <returns></returns>
        bool TryAcquireMonitor(string directoryPath, string filter, bool isContinued, Action<IFileMonitor> fileChangeCallback, WatcherChangeTypes changeTypes,ref IFileMonitor fileMonitor);
    }

    public static class FileMonitorManagerExtension
    {
        public static bool TryAcquireMonitor(this IFileMonitorManager col, string directoryPath,string filter, Action<IFileMonitor> fileChangeCallback)
        {
            IFileMonitor fileMonitor = null;
            return col.TryAcquireMonitor(directoryPath, filter, true, fileChangeCallback, WatcherChangeTypes.All, ref fileMonitor);
        }
        public static bool TryAcquireMonitor(this IFileMonitorManager col, string path, Action<IFileMonitor> fileChangeCallback)
        {
            IFileMonitor fileMonitor = null;
            return col.TryAcquireMonitor(Path.GetDirectoryName(path), Path.GetFileName(path), true, fileChangeCallback, WatcherChangeTypes.All, ref fileMonitor);
        }
    }
}
