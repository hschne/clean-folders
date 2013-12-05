using System;
using System.IO;

using CleanFolder.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanFolderTest
{
    [TestClass]
    public class SettingsTest {
        private CleanFolderSettings cleanFolderSettings;

        private void CreateDummySettings() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            cleanFolderSettings.CleaningInterval = 5;
            cleanFolderSettings.FileName = "CleanFolderSettings.xml";
        }

        [TestMethod]
        public void SaveSettings()
        {
            CreateDummySettings();
            cleanFolderSettings.Save();
            Assert.IsTrue(File.Exists(cleanFolderSettings.XmlDirectory + "\\" + cleanFolderSettings.FileName));
        }

        [TestMethod]
        public void LoadSettings() {
            CreateDummySettings();
            cleanFolderSettings.Load();
            Assert.IsTrue(cleanFolderSettings.CleaningInterval == 5);
            Assert.IsTrue(cleanFolderSettings.FileName == "CleanFolderSettings.xml");
        }
    }
}
