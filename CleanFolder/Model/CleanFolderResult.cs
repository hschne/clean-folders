using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanFolder.Model
{
    public class CleanFolderResult
    {
        public String FolderName { get; set; }

        public String FolderPath { get; set; }

        public List<String> DeletedItems { get; set; }

        public TimeSpan CleanDuration { get; set; }

        public CleanFolderResult( String folderName, String folderPath, List<String> deletedItems, TimeSpan cleanduration ) {
            FolderName = folderName;
            FolderPath = folderPath;
            DeletedItems = deletedItems;
            CleanDuration = cleanduration;
        }

        public CleanFolderResult() {
            
        }
    }
}
