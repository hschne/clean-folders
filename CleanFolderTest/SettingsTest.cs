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
        }

        [TestMethod]
        public void SaveSettings()
        {
            CreateDummySettings();
            cleanFolderSettings.Save();
            Assert.IsTrue(File.Exists(Constants.XMLLOCATION + "\\" + Constants.SETTINGSFILE));
        }

        [TestMethod]
        public void LoadSettings() {
            CreateDummySettings();
            Assert.IsTrue(cleanFolderSettings.CleaningInterval == 5);
        }
    }
}
