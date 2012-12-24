using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace HiGirl360.Infrastructure.Extensions.IO.FileSystems
{
    public class FolderRoot : IFolderRoot
    {
        public FolderRoot() : this(string.Empty) { }

        public FolderRoot(string path)
        {
            path = PathUtility.PhysicsPathSeparatorCharConvertToVirtualPathSeparatorChar(path);
            path = path.TrimEnd('/');

            this.RootPath = Path.Combine("/", path);
            this.RootPhysicalPath = Environment.CurrentDirectory + PathUtility.VirtualPathSeparatorCharConvertToPhysicsPathSeparatorChar(this.RootPath.TrimEnd('/'));
        }

        #region IFolderRoot 成员

        public string RootPath { get; private set; }

        public string RootPhysicalPath { get; private set; }

        #endregion
    }
}
