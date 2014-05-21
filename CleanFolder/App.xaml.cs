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
using Microsoft.Win32;

namespace CleanFolder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private CleanFolderSettings cleanFolderSettings;

        private Folders folders;

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
            cleanerTask.Start();
        }

        private void InitializeDataLayer() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            folders = Folders.GetInstance;
            folders.Load();
            folders = Folders.GetInstance;
        }

        private void InitializeViewLayer() {
            trayIconViewModel = new TrayIconViewModel();
            iconView = (TaskbarIcon)FindResource("NotifyIcon");
            trayIconViewModel.icon = iconView;
            iconView.DataContext = trayIconViewModel;
            mainWindowViewModel = new MainWindowViewModel();

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
            if(mainWindowView != null) mainWindowView.Close();
            cleanerTask.Stop();
            Shutdown();
        }





    }


}
