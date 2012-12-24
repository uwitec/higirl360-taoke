using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace HiGirl360.Infrastructure.Extensions.IO.FileWatcher
{
    /// <summary>
    /// 通过Timer用于处理，同一change事件多次触发的问题。
    /// 后遗症：因为要收集多个触发事件，然后再通过Timer回调，所以FileSystemEventHandler要晚0.5秒触发
    /// </summary>
    public class FileWatcherTimer
    {
        private FileSystemEventHandler fileChangedHandler;
        private Timer timer;
        private IList<CallbackEntry> callbackEntries;
        private FileSystemWatcher fileSystemWatcher;

        public FileWatcherTimer(FileSystemEventHandler fileChangedHandler)
        {
            this.callbackEntries = new List<CallbackEntry>();
            this.fileChangedHandler = fileChangedHandler;
            this.timer = new Timer(new TimerCallback(this.OnTimer), this, Timeout.Infinite, Timeout.Infinite);
        }

        public void OnFileChangedCallback(object source, FileSystemEventArgs e)
        {
            this.fileSystemWatcher = source as FileSystemWatcher;

            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();

            var entry = new CallbackEntry()
            {
                FileWatcher = source as FileSystemWatcher,
                Name = e.Name,
                ChangeType = e.ChangeType,
                Directory = e.FullPath
            };

            if (!this.callbackEntries.Contains(entry))
            {
                this.callbackEntries.Add(entry);
            }

            mutex.ReleaseMutex();

            this.timer.Change(500, Timeout.Infinite);
        }

        private void OnTimer(object state)
        {
            List<CallbackEntry> backup = new List<CallbackEntry>();

            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            backup.AddRange(this.callbackEntries);
            this.callbackEntries.Clear();
            mutex.ReleaseMutex();

            foreach (var item in backup)
            {
                this.fileChangedHandler(item.FileWatcher, new FileSystemEventArgs(item.ChangeType, item.Directory, item.Name));
            }
        }

        public class CallbackEntry
        {
            public FileSystemWatcher FileWatcher { get; set; }

            public string Name { get; set; }

            public string Directory { get; set; }

            public WatcherChangeTypes ChangeType { get; set; }

            public override bool Equals(object entity)
            {
                return entity != null
                    && entity is CallbackEntry
                    && this == (CallbackEntry)entity;
            }

            public static bool operator ==(CallbackEntry left, CallbackEntry right)
            {
                //这里必须转换为object 如(object)left，要不然就进入死循环。转为object后就是调用Object.==来执行。
                if ((object)left == null && (object)right == null)
                {
                    return true;
                }

                if ((object)left == null || (object)right == null)
                {
                    return false;
                }

                return left.Name.Equals(right.Name);
            }

            public static bool operator !=(CallbackEntry left, CallbackEntry right)
            {
                return !(left == right);
            }

            public override int GetHashCode()
            {
                if (this.Name == null)
                    return 0;

                return this.Name.GetHashCode();
            }
        }
    }
}
