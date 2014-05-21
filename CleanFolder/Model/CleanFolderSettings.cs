using System;
using System.IO;
using System.Xml.Serialization;

namespace CleanFolder.Model
{
    public class CleanFolderSettings {

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(CleanFolderSettings));
       
        public bool ActivateAutoClean { get; set; }

        public int CleaningInterval { get; set; }

        public bool CleanOnStart { get; set; }

        public bool NeedsConfirmation { get; set; }

        public bool MoveInsteadOfClean { get; set; }

        public bool ActivateExtensionIgnore { get; set; }

        private static CleanFolderSettings instance;

        public static CleanFolderSettings GetInstance {
            get {
                if (instance == null) {
                    instance = Load();
                }
                return instance;
            }
        }

        private CleanFolderSettings() {
        }

        public void Save() {
            try {
                TextWriter textWriter = new StreamWriter(Constants.XMLLOCATION + "\\" + Constants.SETTINGSFILE);
                Serializer.Serialize(textWriter, this);
                textWriter.Close();
                textWriter.Dispose();
            }
            catch (FileNotFoundException) {
                
            }
            
        }

        public static CleanFolderSettings Load()
        {
            TextReader textReader = null;
            try
            {
                string settingsPath = Constants.XMLLOCATION + "\\" + Constants.SETTINGSFILE;
                textReader = new StreamReader(settingsPath);
                instance = (CleanFolderSettings) Serializer.Deserialize(textReader);
                return instance;
            }
            catch (Exception)
            {
                return new CleanFolderSettings();
            }
            finally
            {
                textReader.Dispose();
            }
            
        }
    }
}