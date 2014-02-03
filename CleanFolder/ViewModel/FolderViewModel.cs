using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class FolderViewModel : ViewModelBase {

        private Visibility settingsVisibility;

        public Visibility SettingsVisibility
        {
            get {
                return settingsVisibility;
            }
            set {
                settingsVisibility = value;
                OnPropertyChanged("SettingsVisibility");
            }
        }

        public Image FolderImage { get; set; }

        public String Name {
            get {
                return Folder.Name;
            }
        }

        public String Path {
            get {
                return Folder.Path;
            }
            set {
                if (value != null) {
                    Folder.Path = value;
                    OnPropertyChanged("Path");
                    OnPropertyChanged("Name");
                }
            }
        }

        public int DaysToDeletion
        {
            get {
                return Folder.DaysToDeletion;
            }
            set {
                Folder.DaysToDeletion = value;
                OnPropertyChanged("DaysToDeletion");
            }
        }

        public Folder Folder { get; set; }

        public ICommand ChangePathCommand { get; set; }

        public ICommand DeleteFolderCommand { get; set; }

        public ICommand RequestCleaningCommand { get; set; }

        public ICommand ShowFolderSettingsCommand { get; set; }

        public ICommand HandleClickCommand { get; set; }

        public delegate void RequestDeletionHandler(FolderViewModel vm);

        public event RequestDeletionHandler RequestDeletion;

        public delegate void RequestCleaningHandler( Folder folder );

        public FolderViewModel(Folder folder) {
            Folder = folder; 
            ChangePathCommand = new RelayCommand(param => ChangePath());
            DeleteFolderCommand = new RelayCommand(param => Delete());
            RequestCleaningCommand =new RelayCommand(param => Cleaner.CleanSingleFolder(Folder));
            ShowFolderSettingsCommand = new RelayCommand(param => ShowFolderSettings());
            SettingsVisibility = Visibility.Collapsed;
           
        }

        private void ChangePath() {
            String path = OpenFolderBrowser();
            Path = path; 
        }

        private void Delete() {
            RequestDeletionHandler handler = RequestDeletion;
            if (handler != null) {
                handler(this);
            }
        }

        private String OpenFolderBrowser()
        {
            FolderBrowserDialog objDialog = new FolderBrowserDialog();
            String path = null;
            objDialog.Description = "Choose a folder";
            objDialog.SelectedPath = @"C:\";
            DialogResult objResult = objDialog.ShowDialog();
            if(objResult == DialogResult.OK)
            {
                path = objDialog.SelectedPath;
            }
            return path;
        }

        private void ShowFolderSettings() {
            if (settingsVisibility == Visibility.Collapsed) {
                SettingsVisibility = Visibility.Visible;
            }
            else {
                SettingsVisibility = Visibility.Collapsed;
            }
            
        }


    }
}
