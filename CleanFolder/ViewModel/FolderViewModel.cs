using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class FolderViewModel : ViewModelBase
    {

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

        public int DaysToDeletion {
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

        public delegate bool RequestDeletionHandler(FolderViewModel vm);

        public event RequestDeletionHandler RequestDeletion; 

        public FolderViewModel(Folder folder) {
            Folder = folder; 
            ChangePathCommand = new RelayCommand(param => ChangePath());
            DeleteFolderCommand = new RelayCommand(param => Delete(this));
        }

        private void ChangePath() {
            String path = OpenFolderBrowser();
            Path = path; 
        }

        private void Delete(FolderViewModel vm) {
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


    }
}
