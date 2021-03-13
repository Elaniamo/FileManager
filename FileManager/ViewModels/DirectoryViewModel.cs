using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.ViewModels.Base;

namespace FileManager.ViewModels
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;
        private DirectoryProperty _FolderFileProp;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                       .EnumerateDirectories().AsParallel()
                       .Select(dir_info => new DirectoryViewModel(dir_info.FullName)).OrderBy(x => x.Name);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }
        }
        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                       .EnumerateFiles().AsParallel()
                       .Select(file => new FileViewModel(file.FullName));
                    return files.OrderBy(x => x.Name);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return new ObservableCollection<object>();
            }
        }

        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime LastWriteTime => _DirectoryInfo.LastWriteTime;

        private double _Size = 0;
        public double Size { get => _Size; private set => Set(ref _Size, value); }

        private double _FilesCount = 0;
        public double FilesCount { get => _FilesCount; private set => Set(ref _FilesCount, value); }

        private double _FoldersCount = 0;
        public double FoldersCount { get => _FoldersCount; private set => Set(ref _FoldersCount, value); }

        public DirectoryViewModel(string Path)
        {
            _DirectoryInfo = new DirectoryInfo(Path);
            Task.Run(async () => await GetFolderFileProp());
        }

        private async Task GetFolderFileProp()
        {
            await Task.Run(() => _FolderFileProp = new DirectoryProperty(_DirectoryInfo));
            Size = Math.Ceiling(_FolderFileProp.Size / 1024 / 1024 / 1024 * 10) / 10;
            FilesCount = _FolderFileProp.FilesCount;
            FoldersCount = _FolderFileProp.FoldersCount;
        }
    }

    class FileViewModel : ViewModel
    {
        private readonly FileInfo _FileInfo;

        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public double Size => Math.Ceiling((double)_FileInfo.Length / 1024 / 1024 * 10) / 10;
        public int FilesCount => 1;
        public int FoldersCount => 0;
        public DateTime LastWriteTime => _FileInfo.LastWriteTime;

        public FileViewModel(string Path)
        {
            _FileInfo = new FileInfo(Path);
        }
    }
}
