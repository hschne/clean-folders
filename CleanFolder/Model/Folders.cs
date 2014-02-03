using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace CleanFolder.Model
{
    public class Folders {

        public String XmlDirectory { get; set; }

        public String FileName { get; set; }

        private readonly XmlSerializer serializer;

        private CleanFolderSettings cleanFolderSettings; 

        private static Folders instance;

        public ObservableCollection<Folder> FolderList { get; set; } 

        public static Folders GetInstance {
            get {
                if (instance == null) {
                    instance = new Folders();
                }
                return instance;
            }
        }

        private Folders() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            serializer = new XmlSerializer(typeof(Folders));
            XmlDirectory = Constants.XMLLOCATION;
            FileName = "Folders.xml";
            FolderList = new ObservableCollection<Folder>();
        }

        public void Save()
        {
            try {
                TextWriter textWriter = new StreamWriter(XmlDirectory + "\\" + FileName);
                serializer.Serialize(textWriter, this);
                textWriter.Close();
                textWriter.Dispose();
            }
            catch (FileNotFoundException) {
                
            }
                
        }

        public void Load()
        {
            
            try {
                TextReader textReader = new StreamReader(XmlDirectory + "\\" + FileName);
                instance = (Folders)serializer.Deserialize(textReader);
                textReader.Dispose();
            }
            catch (FileNotFoundException) {
   
            }
            
        }


      
       
    }
}
