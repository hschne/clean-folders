
using System;
using System.Threading.Tasks;
using System.Windows;

using CleanFolder.Model;

using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CleanFolder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Task<MessageDialogResult> lastResult;

        public Task<MessageDialogResult> ShowMessage(string type, string message)
        {
            switch (type) {
                case(Constants.ERRORMESSAGE):
                    ShowErrorDialog(message);
                    return null;
                case (Constants.NOTIFICATION):
                    ShowNotification(message);
                    return null;
                case (Constants.YESNODIALOG):
                    return ShowYesNoDialog(message);
                default:
                    return null;
            }
        }

        public async void ShowErrorDialog(String message)
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
            await this.ShowMessageAsync("Error!", message, MessageDialogStyle.AffirmativeAndNegative);
        }

        public async void ShowNotification(String message)
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
            await this.ShowMessageAsync("Notification", message);
        }

        public async Task<MessageDialogResult> ShowYesNoDialog( String message ) {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
            return await this.ShowMessageAsync("Confirm", message, MessageDialogStyle.AffirmativeAndNegative);

        }




    }
}
