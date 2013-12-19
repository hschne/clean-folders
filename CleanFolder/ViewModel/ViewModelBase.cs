using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

using CleanFolder.Model;

using MahApps.Metro.Controls.Dialogs;

namespace CleanFolder.ViewModel {
    public class ViewModelBase : INotifyPropertyChanged {

        public delegate MessageDialogResult ShowMessageEventHandler(String messageType, String message);

        public event PropertyChangedEventHandler PropertyChanged;

        public event ShowMessageEventHandler ShowMessage;

        protected virtual void OnPropertyChanged( string propertyName ) {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        [DebuggerStepThrough]
        public void VerifyPropertyName( string propertyName ) {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                string msg = "Invalid property name: " + propertyName;
                throw new Exception(msg);
            }
        }

        protected async Task<MessageDialogResult> FireShowMessage(String type, String message)
        {
            ShowMessageEventHandler handler = ShowMessage;
            if (handler != null) {
                var result = han
                return handler(type, message);
            }
            return MessageDialogResult.Negative;
        }
    }
}