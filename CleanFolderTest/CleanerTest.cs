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
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 5));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 4));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 3));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 2));

        }

        [TestMethod]
        public void Clean() {
            CreateObjects();
            Cleaner.Clean();
            Assert.IsTrue(log.Entries[0].FolderResults.Count == 4);
        }
    }
}
