
using System.Collections.ObjectModel;

using CleanFolder.Model;

namespace CleanFolder.ViewModel
{
    public class SettingsViewModel : ViewModelBase {

        private CleanFolderSettings settings;

        public int CleaningInterval {
            get {
                return settings.CleaningInterval;
            }
            set {
                   settings.CleaningInterval = value; 
            }
        }
        public SettingsViewModel() {
            settings = CleanFolderSettings.GetInstance;
        }


    }
}
