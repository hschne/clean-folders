
using System.Collections.ObjectModel;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class SettingsViewModel : ViewModelBase {

        private CleanFolderSettings settings;

        public bool ActivateAutoClean
        {
            get {
                return settings.ActivateAutoClean;
            }
            set {
                settings.ActivateAutoClean = value; 
                OnPropertyChanged("ActivateAutoClean");
            }
        }

        public bool CleanOnStart {
            get {
                return settings.CleanOnStart;
            }
            set {
                settings.CleanOnStart = value;
                OnPropertyChanged("CleanOnStart");
            }
        }

        public bool NeedsConfirmation {
            get {
                return settings.NeedsConfirmation;
            }
            set {
                settings.NeedsConfirmation = value;
                OnPropertyChanged("NeedsConfirmation");
            }
        }

        public bool MoveInsteadOfClean {
            get {
                return settings.MoveInsteadOfClean;
                OnPropertyChanged("MoveInsteadOfClean");
            }
            set {
                settings.MoveInsteadOfClean = value;
                OnPropertyChanged("MoveInsteadOfClean");
            }
        }

        public bool ActivateExtensionIgnore {
            get {
                return settings.ActivateExtensionIgnore;
            }
            set {
                settings.ActivateExtensionIgnore = value;
                OnPropertyChanged("ActivateExtensionIgnore");
            }
        }

        public int CleaningInterval {
            get {
                return settings.CleaningInterval;
            }
            set {
                   settings.CleaningInterval = value;
                   OnPropertyChanged("CleaningInterval");
            }
        }
        public SettingsViewModel() {
            settings = CleanFolderSettings.GetInstance;
        }


    }
}
