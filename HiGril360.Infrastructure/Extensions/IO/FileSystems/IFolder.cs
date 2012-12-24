using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HiGirl360.Infrastructure.Extensions.IO.FileSystems
{
    /// <summary>
    /// 当前对象方法中的参数path是相对于IFolder.Root的path，即如果path中用的是绝对路径，那么绝对路径的根目录就是IFolder.Root
    /// 如： /directory1/1.txt 与 directory/1.txt 指向同一个文件 即：root.RootPath + "directory/1.txt" 
    /// </summary>
    public interface IFolder
    {
        IFolderRoot Root { get; }

        IEnumerable<string> GetFiles(string path,bool recursive);
        IEnumerable<string> GetDirectories(string path);

        /// <summary>
        /// 合并生成一个相对于Root的绝对路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        string Combine(params string[] paths);

        bool FileExists(string path);
        Stream CreateFile(string path);
        Stream OpenFile(string path);

        bool DirectoryExists(string path);
        void CreateDirectory(string path);

        /// <summary>
        /// 删除文件或目录，当为删除目录时recursive才生效
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        void Delete(string path, bool recursive);

        void CopyTo(string sourcePath, string destinationPath);

        void MoveTo(string sourcePath, string destinationPath);

        DateTime GetFileLastWriteTimeUtc(string path);
    }

    public static class IFolderExtensions
    {
        public static IEnumerable<string> GetFiles(this IFolder folder, string path)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            return folder.GetFiles(path, false);
        }

        public static void CreateFile(this IFolder folder, string path, string content)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            using (var stream = folder.CreateFile(path))
            {
                using (var tw = new StreamWriter(stream))
                {
                    tw.Write(content);
                }
            }
        }

        public static string ReadFile(this IFolder folder, string path)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            using (var stream = folder.OpenFile(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void Delete(this IFolder folder, string path)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            folder.Delete(path, true);
        }

        public static string CombineToPhysicalPath(this IFolder folder,params string[] paths)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            var path = folder.Combine(paths);

            return PathUtility.VirtualPathSeparatorCharConvertToPhysicsPathSeparatorChar(Path.Combine(folder.Root.RootPhysicalPath, path.TrimStart('/')));
        }
        public static string CombineToAppPath(this IFolder folder, params string[] paths)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");
            folder.Root.ArgumentNullExceptionByNullOrEmpty("folder.Root");

            return PathUtility.PhysicsPathSeparatorCharConvertToVirtualPathSeparatorChar(Path.Combine(folder.Root.RootPath, folder.Combine(paths).TrimStart('/')));
        }

        public static bool TryDelete(this IFolder folder, string path, bool recursive)
        {
            folder.ArgumentNullExceptionByNullOrEmpty("folder");

            try
            {
                folder.Delete(path, recursive);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool TryDelete(this IFolder folder, string path)
        {
            return IFolderExtensions.TryDelete(folder, path, true);
        }
    }
}
