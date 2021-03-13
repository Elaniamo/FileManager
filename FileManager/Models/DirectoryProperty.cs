using System;
using System.IO;

namespace FileManager.Models
{
    public class DirectoryProperty
    {
        public double Size { get; set; }
        public int FilesCount { get; set; }
        public int FoldersCount { get; set; }

        public DirectoryProperty(DirectoryInfo DirectoryInfo)
        {
            GetProperty(DirectoryInfo);
        }

        private void GetProperty(DirectoryInfo DirectoryInfo)
        {
            try
            {
                FileInfo[] fis = DirectoryInfo.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    Size += fi.Length;
                    FilesCount++;
                }

                // Add subdirectory sizes.
                DirectoryInfo[] dis = DirectoryInfo.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    FoldersCount += 1;
                    GetProperty(di);
                }
            }
            catch (Exception) { }
        }
    }
}
