using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

using CleanFolder.Model.Interfaces;

namespace CleanFolder.Model
{
    public class Log 
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Log));

        public int EntryCount {
            get {
                return Entries.Count;
            }
        }

        private TimeSpan totalTimeTaken;

        public TimeSpan TotalTimeTaken {
            get {
                totalTimeTaken = new TimeSpan(Entries.Sum(x => x.TimeTaken.Ticks));
                return totalTimeTaken;
            }
        }

        public ObservableCollection<LogEntry> Entries { get; set; }

        private static Log instance; 

        public static Log GetInstance {
            get {
                if (instance == null) {
                    instance = LoadInstance();
                }
                return instance;
            }
        }
        
        private Log() {

            Entries = new ObservableCollection<LogEntry>();
        }

        public void Add(LogEntry entry) {
            Entries.Add(entry);
        }

        public void Clear() {
            Entries.Clear();
        }

        public void Save()
        {
            try
            {
                TextWriter textWriter = new StreamWriter(Constants.XMLLOCATION + "\\" + Constants.LOGFILE);
                Serializer.Serialize(textWriter, instance);
                textWriter.Close();
                textWriter.Dispose();
            }
            catch(FileNotFoundException)
            {

            }

        }

        private static Log LoadInstance()
        {
            try
            {
                TextReader textReader = new StreamReader(Constants.XMLLOCATION + "\\" + Constants.LOGFILE);
                Log log = (Log)Serializer.Deserialize(textReader);
                textReader.Dispose();
                return log; 
            }
            catch(FileNotFoundException) {
                return new Log();
            }

        }

        
    }
}
