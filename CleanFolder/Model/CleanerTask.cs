using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

using CleanFolder.Properties;

using Application = System.Windows.Application;

namespace CleanFolder.Model
{
    public class CleanerTask {

        private readonly ManualResetEvent shutDownEvent = new ManualResetEvent(false);

        private readonly ManualResetEvent pauseEvent = new ManualResetEvent(false);

        private readonly ManualResetEvent suspendEvent = new ManualResetEvent(true);

        private Object thisLock = new Object();

        private Thread cleanerThread;

        public delegate void CleaningFinishedHandler();

        public event CleaningFinishedHandler CleaningFinished;

        public CleanFolderSettings settings; 

        public int Interval {
            get {
                return settings.CleaningInterval;
            }
        }

        public CleanerTask() {
            settings = CleanFolderSettings.GetInstance;
            cleanerThread = new Thread((ThreadStart)RunThread);
            cleanerThread.Name = "CleanerThread";
        }

        public void Start() {
            cleanerThread.Start();
        }

        public void Stop() {
            shutDownEvent.Set();
            pauseEvent.Set();
            suspendEvent.Set();
        }

        public void Suspend() {
            suspendEvent.Reset();
        }

        public void Resume() {
            suspendEvent.Set();
        }

        public void ExecuteNow() {
            pauseEvent.Set();
        }

        private void RunThread() {
            while(true) {
                suspendEvent.WaitOne();
                if(pauseEvent.WaitOne(0))
                {
                    pauseEvent.Reset();
                }
                CleanAndSetResult();
                NotifyCleaningFinished();
                pauseEvent.WaitOne(TimeSpan.FromHours(Interval));
                if(shutDownEvent.WaitOne(0))
                {
                    break;
                }
            }
        }

        private void CleanAndSetResult() {
            lock (thisLock) {
                Cleaner.Clean();
            }
        }

        private void NotifyCleaningFinished() {
            CleaningFinishedHandler handler = CleaningFinished;
            if (handler != null) {
                handler();
            }
        }

    }
}
