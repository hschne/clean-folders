using System;

using CleanFolder.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanFolderTest
{
    [TestClass]
    public class CleanerTest {

        private Log log;


        private Folders folders; 

        private void CreateObjects() {
            log = Log.GetInstance;
            folders = Folders.GetInstance;
            folders.FolderList.Add(new Folder(@"D:\Documents\My SourceCode\Projects\CleanFolder\CleanFolderTest\dummyFolder", 5));
            folders.FolderList.Add(new Folder(@"D:\Documents\My SourceCode\Projects\CleanFolder\CleanFolderTest\dummyFolder", 4));
            folders.FolderList.Add(new Folder(@"D:\Documents\My SourceCode\Projects\CleanFolder\CleanFolderTest\dummyFolder", 3));
            folders.FolderList.Add(new Folder(@"D:\Documents\My SourceCode\Projects\CleanFolder\CleanFolderTest\dummyFolder", 2));

        }

        [TestMethod]
        public void TestMethod1() {
            CreateObjects();
            Cleaner.Clean();
            Assert.IsTrue(log.EntryCount == 1);
            Assert.IsTrue(log.Entries[0].FolderResults.Count == 4);
        }
    }
}
