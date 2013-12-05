using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

        public int CleanDuration {
            get {
                return cleanFolderResult.CleanDuration.Milliseconds;
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
            foreach (string deletedItem in DeletedItems) {
                String fileName = deletedItem.Split('\\').Last();
                result += fileName + "; ";
            }
            result = result.TrimEnd(';');
            return result;
        }
    }
}
