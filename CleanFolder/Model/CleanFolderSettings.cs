using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Serialization;

using CleanFolder.Properties;

namespace CleanFolder.Model
{
    public class CleanFolderSettings {

        public String XmlDirectory { get; set; }

        public String FileName { get; set; }

        private readonly XmlSerializer serializer;

        public int CleaningInterval { get; set; }

        private static CleanFolderSettings instance;

        public static CleanFolderSettings GetInstance {
            get {
                if (instance == null) {
                    instance = new CleanFolderSettings();
                }
                return instance;
            }
        }

        private CleanFolderSettings() {
            serializer = new XmlSerializer(typeof(CleanFolderSettings));
            XmlDirectory = Settings.Default.XmlDirectory;
            CleaningInterval = Settings.Default.CleaningInterval;
            FileName = "CleanFolderSettings.xml";
        }

        public void Save() {
            TextWriter textWriter = new StreamWriter(XmlDirectory + "\\" + FileName);
            serializer.Serialize(textWriter, this);
            textWriter.Close();
            textWriter.Dispose(); 
        }

        public void Load() {
            TextReader textReader = new StreamReader(XmlDirectory + "\\" + FileName);
            instance = (CleanFolderSettings)serializer.Deserialize(textReader);
            textReader.Dispose();
        }
    }
}