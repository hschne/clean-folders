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

        public static CleanFolderSettings Load() {
            try {
                TextReader textReader = new StreamReader(Constants.XMLLOCATION + "\\" + Constants.SETTINGSFILE);
                instance = (CleanFolderSettings)Serializer.Deserialize(textReader);
                textReader.Dispose();
                return instance;
            }
            catch (FileNotFoundException) {
                return new CleanFolderSettings();
            }
            
        }
    }
}