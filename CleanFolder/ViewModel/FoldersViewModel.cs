using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using CleanFolder.Model;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CleanFolder.ViewModel
{
    public class FoldersViewModel : ViewModelBase  {

        private Folders folders;

        public FolderViewModel SelectedFolder { get; set; }

        public ObservableCollection<FolderViewModel> FolderList { get; set; }

        public ICommand AddFolderCommand { get; set; }

        public ICommand RemoveFolderCommand { get; set; }


        public FoldersViewModel()
        {
            folders = Folders.GetInstance;
            FolderList = new ObservableCollection<FolderViewModel>(folders.FolderList.Select(p => new FolderViewModel(p)));
            FolderList.CollectionChanged += DelegateChanges;
            foreach(FolderViewModel folder in FolderList)
            {
                folder.RequestDeletion += RemoveFolder;

            }
            AddFolderCommand = new RelayCommand(param => AddFolder());
            RemoveFolderCommand = new RelayCommand(param => RemoveFolder(SelectedFolder));
        }

        private void RemoveFolder(FolderViewModel folder) {
            if (SelectedFolder != null) {
                await FireShowMessage(Constants.YESNODIALOG, "Are you sure you want to stop watching the folder " + folder.Name + "?"+ Environment.NewLine +"All settings will be lost.");
            }
        }


        private void AddFolder()
        {
            String path = OpenFolderBrowser();

            if(!String.IsNullOrEmpty(path))
            {
                if (FolderAlreadyWatched(path)) {
                    FireShowMessage(Constants.ERRORMESSAGE, "Whoops! You can't add the same folder twice!");
                }
                else {
                    FolderList.Add(new FolderViewModel(new Folder(path, 5)));
                }
            }
        }


        private bool FolderAlreadyWatched(String path) {
            if (FolderList.Any(x => x.Path == path)) {
                return true;
            }
            return false;
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


        private void DelegateChanges( object sender, NotifyCollectionChangedEventArgs e ) {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(FolderViewModel vm in e.NewItems)
                {
                    folders.FolderList.Add(vm.Folder);
                    vm.RequestDeletion += RemoveFolder;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(FolderViewModel vm in e.OldItems)
                {
                    folders.FolderList.Remove(vm.Folder);
                    vm.RequestDeletion -= RemoveFolder;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                folders.FolderList.Clear();
            }
        }
    }
}
