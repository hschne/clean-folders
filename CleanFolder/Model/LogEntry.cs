using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanFolder.Model
{
    public class LogEntry {

        public DateTime TimeOfCleaning { get; set; }

        public TimeSpan TimeTaken { get; set; }

        public int DeletedItemsCount { get; set; }

        public List<CleanFolderResult> FolderResults { get; set; }

        public LogEntry(List<CleanFolderResult> folderResults) {
            FolderResults = folderResults;
            TimeOfCleaning = DateTime.Now;
            TimeTaken = new TimeSpan(
            folderResults.Sum(x => x.CleanDuration.Ticks));
            DeletedItemsCount = folderResults.Sum(x => x.DeletedItems.Count);
        }

        public LogEntry() {
            
        }

    }
}
