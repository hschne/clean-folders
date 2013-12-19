using System;
using System.Collections.Generic;

using CleanFolder.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanFolderTest
{
    [TestClass]
    public  class LogTests {
        private  Log log;

        private  CleanFolderResult CreateFolderResults() {
            return new CleanFolderResult(
                "folderName", "folderPath", new List<String> (new string[]{ "deleted item", "another deleted item" }), new TimeSpan(0,0,0,0,5));
        }

        private  void CreateDummyLog() {
            log = Log.GetInstance;
            log.Clear();
            List<CleanFolderResult> list = new List<CleanFolderResult>(new CleanFolderResult[] {CreateFolderResults(), CreateFolderResults()});
            log.Entries.Add(new LogEntry(list));
        }

        [TestMethod]
        public void AddedEntries() {
            CreateDummyLog();
            Assert.IsTrue(log.EntryCount == 1);
        }

        [TestMethod]
        public void CleanLog()
        {
            CreateDummyLog();
            log.Clear();
            Assert.IsTrue(log.Entries.Count == 0);
        }
    }
}
