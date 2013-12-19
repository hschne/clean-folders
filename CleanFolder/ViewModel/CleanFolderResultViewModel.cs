using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class CleanFolderResultViewModel : ViewModelBase {

        public String FolderName {
            get {
                return cleanFolderResult.FolderName;
            }
        }

        public String FolderPath {
            get {
                return cleanFolderResult.FolderPath;
            }
        }

        public String CleanDuration {
            get {
                return cleanFolderResult.CleanDuration.Milliseconds + " ms";
            }
        }

        public String DeletedItemsString {
            get {
                return ParseDeletedItems();
            }
        }

        public ObservableCollection<String> DeletedItems { get; set; }

        private CleanFolderResult cleanFolderResult;

        public CleanFolderResultViewModel(CleanFolderResult result) {
            cleanFolderResult = result;
            DeletedItems = new ObservableCollection<string>(result.DeletedItems);
        }

        private String ParseDeletedItems() {
            String result = String.Empty;
            if (DeletedItems.Count == 0) {
                return "No items deleted.";
            }
            foreach (string deletedItem in DeletedItems) {
                String fileName = deletedItem.Split('\\').Last();
                result += fileName + "; ";
                if (result.Length >= 15) {
                    result += "...";
                    break;
                }
            }
            result = result.TrimEnd(';');
            return result;
        }
    }
}
