using System;
using System.Linq;

namespace CleanFolder.Model
{
    public class Folder  {

        public string Name {
            get {
                return Path.Split('\\').ToList().Last();
            }
        }

        public string Path { get; set; }

        public int DaysToDeletion { get; set; }

        public Folder() {
        }

        public Folder(String folderpath ):this() {
            Path = folderpath;
        }

        public Folder( String folderpath, int daysToDeletion )
            : this(folderpath) {
            DaysToDeletion = daysToDeletion;
        }
    }
}
