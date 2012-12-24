using System;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO.FileWatcher
{
    internal class FileMonitor : IFileMonitor
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        private readonly Action<IFileMonitor> fileChangedCallBack;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileChangeCallback">文件发生变化后回调的方法</param>
        /// <param name="directoryPath">监控目录物理路径</param>
        /// <param name="filter">监控文件筛选</param>
        /// <param name="isContinued">是否持续监控，True持续监控，False则文件发生变生触发事件后监控失效。</param>
        internal FileMonitor(string directoryPath, string filter, bool isContinued, Action<IFileMonitor> fileChangedCallback, WatcherChangeTypes actionTypes)
        {
            this.IsContinued = isContinued;
            this.Filter = string.IsNullOrEmpty(filter) ? "*.*" : filter;
            this.ActionTypes = actionTypes;
            this.DirectoryPath = directoryPath;

            this.fileChangedCallBack = fileChangedCallback;
            this.fileSystemWatcher = this.BuildFileWatcher();
        }

        private FileSystemWatcher BuildFileWatcher()
        {
            FileWatcherTimer watcherTimer = new FileWatcherTimer(WatchChangedHandler);
            // 创建 FileSystemWatcher 及相关属性.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.DirectoryPath;
            //监视时所包含的文件名
            watcher.Filter = this.Filter;
            watcher.IncludeSubdirectories = true;

            //过滤条件
            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            if (this.ActionTypes.Contains(WatcherChangeTypes.Changed))
            {
                watcher.Changed += new FileSystemEventHandler(watcherTimer.OnFileChangedCallback);
            }

            if (this.ActionTypes.Contains(WatcherChangeTypes.Created))
            {
                watcher.Created += new FileSystemEventHandler(watcherTimer.OnFileChangedCallback);
            }

            if (this.ActionTypes.Contains(WatcherChangeTypes.Deleted))
            {
                watcher.Deleted += new FileSystemEventHandler(watcherTimer.OnFileChangedCallback);
            }

            if (this.ActionTypes.Contains(WatcherChangeTypes.Renamed))
            {
                watcher.Renamed += new RenamedEventHandler(watcherTimer.OnFileChangedCallback);
            }

            // 启动监视
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        private void WatchChangedHandler(object source, FileSystemEventArgs e)
        {
            this.fileChangedCallBack(this);

            if (!this.IsContinued)
            {
                this.Release();
            }
        }

        #region IFileMonitor 成员

        public string DirectoryPath { get; private set; }

        public string Filter { get; private set; }

        public WatcherChangeTypes ActionTypes { get; private set; }

        public bool IsContinued { get; private set; }

        public void Release()
        {
            if (this.fileSystemWatcher != null)
            {
                this.fileSystemWatcher.EnableRaisingEvents = false;
                this.fileSystemWatcher.Dispose();
            }
        }

        #endregion
    }
}
