using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using FileManager.Commands;
using FileManager.ViewModels.Base;

namespace FileManager.ViewModels
{
    class MainViewModel : ViewModel
    {
        private string _ElapsedTime = String.Empty;
        public string ElapsedTime { get => _ElapsedTime; private set => Set(ref _ElapsedTime, value); }

        private DirectoryViewModel _DiskRootDir;
        public DirectoryViewModel DiskRootDir { get => _DiskRootDir; private set => Set(ref _DiskRootDir, value); }

        public ICommand Close { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        public ICommand GetStartDirectory { get; }
        private bool CanGetStartDirectoryApplicationCommandExecute(object p) => true;
        private void OnGetStartDirectoryApplicationCommandExecuted(object p)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                new Thread(() => GetDiskRootDir(dialog.FileName)) { IsBackground = true }.Start();
                ElapsedTime = "Done";
            }
        }

        private void GetDiskRootDir(string StartDir)
        {
            var sw = Stopwatch.StartNew();

            DiskRootDir = new DirectoryViewModel(StartDir);

            sw.Stop();
            ElapsedTime = $"Last scan time: " + sw.Elapsed.TotalSeconds.ToString();

        }

        public MainViewModel()
        {
            Close = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            GetStartDirectory = new LambdaCommand(OnGetStartDirectoryApplicationCommandExecuted, CanGetStartDirectoryApplicationCommandExecute);
        }
    }
}
