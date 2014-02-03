using System;
using System.Threading;


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
            cleanerThread = new Thread(RunThread) { Name = "CleanerThread" };
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
            if(settings.CleanOnStart) CleanAndSetResult();
            while(settings.ActivateAutoClean) {
                suspendEvent.WaitOne();
                if(pauseEvent.WaitOne(0))
                {
                    pauseEvent.Reset();
                }
                pauseEvent.WaitOne(TimeSpan.FromHours(Interval));
                CleanAndSetResult();
                NotifyCleaningFinished();
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
