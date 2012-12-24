using System;
using System.Collections.Generic;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO.FileWatcher
{
    public interface IFileMonitor
    {
        /// <summary>
        /// 监控目录路径
        /// </summary>
        string DirectoryPath { get; }

        /// <summary>
        /// 监控筛选字符串，用于确定在目录中监视哪些文件
        /// </summary>
        string Filter { get; }

        /// <summary>
        /// 监控的动作类型
        /// </summary>
        WatcherChangeTypes ActionTypes { get; }

        /// <summary>
        /// 是否持续监控
        /// </summary>
        bool IsContinued { get; }

        /// <summary>
        /// 释放监控
        /// </summary>
        void Release();
    }
}
