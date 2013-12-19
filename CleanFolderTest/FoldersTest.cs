using System;
using System.IO;

using CleanFolder.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanFolderTest
{
    [TestClass]
    public class FoldersTest {
        private Folders folders;

        private void CreateDummyFolders() {
            folders = Folders.GetInstance;
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 5));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 4));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 3));
            folders.FolderList.Add(new Folder(TestConstants.DUMMYDIRECTORY, 2));
        }

        [TestMethod]
        public void Save()
        {
            CreateDummyFolders();
            folders.Save();
            Assert.IsTrue(File.Exists(folders.XmlDirectory + "\\" + folders.FileName));
        }

        [TestMethod]
        public void Load() {
            CreateDummyFolders();
            folders.Load();
            Assert.IsTrue(folders.FileName == folders.FileName);
            Assert.IsTrue(folders.FolderList.Count == 4);
            Assert.IsTrue(folders.FolderList[0].DaysToDeletion == 5);
        }
    }
}
