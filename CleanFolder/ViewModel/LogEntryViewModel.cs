using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class LogEntryViewModel : ViewModelBase {

        private LogEntry logEntry;

        private bool isExpanded;

        public DateTime TimeOfCleaning {
            get {
                return logEntry.TimeOfCleaning;
            }
        }

        public String FolderNames {
            get {
                return "-";
            }
        }

        public String TimeTaken {
            get {
                return logEntry.TimeTaken.Milliseconds + " ms";
            }
        }

        public int DeletedItemsCount {
            get {
                return logEntry.DeletedItemsCount;
            }
        }

        public ObservableCollection<CleanFolderResultViewModel> FolderResults { get; set; }

        public LogEntryViewModel(LogEntry entry) {
            
            logEntry = entry;
            FolderResults = new ObservableCollection<CleanFolderResultViewModel>(entry.FolderResults.Select(x => new CleanFolderResultViewModel(x)));
            isExpanded = false;
        }





    }
}
