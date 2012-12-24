using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO
{
    public static class PathUtility
    {
        /// <summary>
        /// 验证mapedPath是否为basePath的有效子路径
        /// 如： basePath= /directory1  mappedPath=/directory1/childDirectory1  
        ///      basePath=/directory1   mappedPath=/directory2                  
        /// </summary>
        /// <param name="basePath">基路径</param>
        /// <param name="mappedPath">子路径</param>
        public static bool IsMappedPath(string basePath, string mappedPath)
        {
            bool valid = false;

            try
            {
                valid = Path.GetFullPath(mappedPath).StartsWith(Path.GetFullPath(basePath), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// 将path中的物理路径分割符，转换为虚拟路径分割符
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PhysicsPathSeparatorCharConvertToVirtualPathSeparatorChar(string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, '/');
        }

        /// <summary>
        /// 将vrtualPath中的虚拟路径分割符，转换为物理路径分割符
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string VirtualPathSeparatorCharConvertToPhysicsPathSeparatorChar(string virtualPath)
        {
            return virtualPath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
