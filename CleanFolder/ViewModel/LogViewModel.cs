using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class LogViewModel : ViewModelBase {

        private Log log;

        public int EntryCount {
            get {
                return log.EntryCount;
            }
        }

        public ICommand RefreshCommand { get; set; }

        public ObservableCollection<LogEntryViewModel> LogEntries { get; set; }

        public LogViewModel() {
            log = Log.GetInstance;
            LogEntries = new ObservableCollection<LogEntryViewModel>(log.Entries.Select(x => new LogEntryViewModel(x)));
            RefreshCommand = new RelayCommand(param => {
                LogEntries = new ObservableCollection<LogEntryViewModel>(log.Entries.Select(x => new LogEntryViewModel(x)));
                OnPropertyChanged("LogEntries");
            });
        }
        



    }
}
