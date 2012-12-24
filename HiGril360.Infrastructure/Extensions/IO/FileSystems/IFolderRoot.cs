using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure.Extensions.IO.FileSystems
{
    public interface IFolderRoot
    {
        /// <summary>
        /// Root Folder virtual Path
        /// </summary>
        string RootPath { get; }

        /// <summary>
        /// Root Folder Physical Path
        /// </summary>
        string RootPhysicalPath { get; }
    }
}
