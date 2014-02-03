using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using CleanFolder.Model;
using CleanFolder.ViewModel;

using Hardcodet.Wpf.TaskbarNotification;

namespace CleanFolder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private CleanFolderSettings cleanFolderSettings;

        private Folders folders;

        private Log log;

        private MainWindowViewModel mainWindowViewModel;

        private TrayIconViewModel trayIconViewModel;

        private CleanerTask cleanerTask = new CleanerTask();

        private MainWindow mainWindowView;

        private TaskbarIcon iconView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            InitializeDataLayer();

            InitializeViewLayer();

            LinkTrayToApp();
            LinkTrayToCleanerTask();

            cleanerTask.Start();
        }

        private void InitializeDataLayer() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            folders = Folders.GetInstance;
            folders.Load();
            folders = Folders.GetInstance;
            log = Log.GetInstance;
        }

        private void InitializeViewLayer() {
            trayIconViewModel = new TrayIconViewModel();
            iconView = (TaskbarIcon)FindResource("NotifyIcon");
            trayIconViewModel.icon = iconView;
            iconView.DataContext = trayIconViewModel;
            mainWindowViewModel = new MainWindowViewModel();

        }

        private void LinkMainToTrayIcon() {
            
        }

        private void LinkTrayToCleanerTask() {
            trayIconViewModel.Clean += cleanerTask.ExecuteNow;
            Cleaner.CleaningFoldersFinished += trayIconViewModel.ShowCleaningFinishedStatus;
        }

        private void LinkMainToCleanerTask() {
            
        }

        private void LinkTrayToApp() {
            trayIconViewModel.OpenWindow += OpenMainWindow;
            trayIconViewModel.CloseApplication += CloseApplication;
        }

        private void OpenMainWindow() {
            if (mainWindowView == null) {
                mainWindowView = new MainWindow();
                mainWindowView.DataContext = mainWindowViewModel;
            }
            mainWindowView.Visibility = Visibility.Visible;
            mainWindowView.Show();

        }


        private void CloseApplication() {
            cleanFolderSettings.Save();
            folders.Save();
            log.Save();
            if(mainWindowView != null) mainWindowView.Close();
            cleanerTask.Stop();
            Shutdown();
        }





    }


}
