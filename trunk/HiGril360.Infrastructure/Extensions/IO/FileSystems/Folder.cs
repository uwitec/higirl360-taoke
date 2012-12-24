using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HiGirl360.Infrastructure.Extensions.IO.FileSystems
{
    public class Folder : IFolder
    {
        public Folder(IFolderRoot root)
        {
            root.ArgumentNullExceptionByNullOrEmpty("root");

            this.Root = root;
        }

        #region IFolder 成员

        public IFolderRoot Root { get; private set; }

        public IEnumerable<string> GetFiles(string path, bool recursive)
        {
            Func<string, IEnumerable<string>> acquireFilesByDirectory = d =>
            {
                var physicalPath = this.CombineToPhysicalPath(d);
                if (!Directory.Exists(physicalPath))
                {
                    return Enumerable.Empty<string>();
                }

                return Directory.GetFiles(physicalPath).Select(file =>
                {
                    var fileName = Path.GetFileName(file);
                    return this.Combine(d, fileName);
                });
            };

            return recursive
                ? acquireFilesByDirectory(path).Concat(this.GetDirectories(path).SelectMany(d => this.GetFiles(d, true)))
                : acquireFilesByDirectory(path);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            var physicalPath = this.CombineToPhysicalPath(path);
            if (!Directory.Exists(physicalPath))
            {
                return Enumerable.Empty<string>();
            }

            return Directory.GetDirectories(physicalPath)
                .Select(file =>
                {
                    var fileName = Path.GetFileName(file);
                    return this.Combine(path, fileName);
                });
        }

        public virtual string Combine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return string.Empty;
            }

            var resultPath = string.Empty;
            foreach (var path in paths)
            {
                resultPath = Path.Combine(resultPath,path);
            }

            return PathUtility.PhysicsPathSeparatorCharConvertToVirtualPathSeparatorChar(resultPath);
        }

        public bool FileExists(string path)
        {
            return File.Exists(this.CombineToPhysicalPath(path));
        }

        public Stream CreateFile(string path)
        {
            var physicalPath = this.CombineToPhysicalPath(path);
            var dirPhysicalPath = Path.GetDirectoryName(physicalPath);
            if (!Directory.Exists(dirPhysicalPath))
            {
                Directory.CreateDirectory(dirPhysicalPath);
            }

            return File.Create(physicalPath);
        }

        public Stream OpenFile(string path)
        {
            return File.OpenRead(this.CombineToPhysicalPath(path));
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(this.CombineToPhysicalPath(path));
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(this.CombineToPhysicalPath(path));
        }

        public virtual void Delete(string path, bool recursive)
        {
            this.MakeDestinationFileNameAvailable(this.CombineToPhysicalPath(path),recursive);
        }

        public void CopyTo(string sourcePath, string destinationPath)
        {
            this.TwoFileHandler(sourcePath, destinationPath, (left, right) => File.Copy(left, right));
        }

        public void MoveTo(string sourcePath, string destinationPath)
        {
            this.TwoFileHandler(sourcePath, destinationPath, (left, right) => File.Move(left, right));
        }

        public DateTime GetFileLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(this.CombineToPhysicalPath(path));
        }

        #endregion

        private void MakeDestinationFileNameAvailable(string destinationFileName, bool recursive)
        {
            bool isDirectory = Directory.Exists(destinationFileName);
            try
            {
                if (isDirectory)
                    Directory.Delete(destinationFileName, recursive);
                else
                    File.Delete(destinationFileName);
            }
            catch
            {
                // We land here if the file is in use, for example. Let's move on.
            }

            if (isDirectory && Directory.Exists(destinationFileName))
            {
                return;
            }

            if (!File.Exists(destinationFileName))
            {
                return;
            }

            // Try renaming destination to a unique filename
            const string extension = "deleted";
            for (int i = 0; i < 100; i++)
            {
                var newExtension = (i == 0 ? extension : string.Format("{0}{1}", extension, i));
                var newFileName = Path.ChangeExtension(destinationFileName, newExtension);
                try
                {
                    File.Delete(newFileName);
                    File.Move(destinationFileName, newFileName);

                    return;
                }
                catch
                {
                    // We need to try with another extension
                }
            }


            File.Delete(destinationFileName);
        }

        private void TwoFileHandler(string sourcePath, string destinationPath, Action<string, string> handler)
        {
            var sourcePhysicalPath = this.CombineToPhysicalPath(sourcePath);
            var destinationPhysicalPath = this.CombineToPhysicalPath(destinationPath);

            if (!File.Exists(sourcePhysicalPath))
            {
                return;
            }

            this.MakeDestinationFileNameAvailable(destinationPhysicalPath, true);

            handler(sourcePhysicalPath, destinationPhysicalPath);
        }
    }

}
