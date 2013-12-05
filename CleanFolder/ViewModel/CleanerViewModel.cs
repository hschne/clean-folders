using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class CleanerViewModel : ViewModelBase
    {

        public ICommand CleanNowCommand { get; set; }

        public CleanerViewModel() {
            CleanNowCommand = new RelayCommand(
                param => Cleaner.Clean());
        }

    }
}
