using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanFolder.Model
{
    public static class Cleaner {

        private static CleanFolderSettings cleanFolderSettings;

        private static Folders folders;

        public delegate void CleaningFoldersFinishedHandler();

        public static event CleaningFoldersFinishedHandler CleaningFoldersFinished;


        static Cleaner() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            folders = Folders.GetInstance;
        }

        public static void Clean()
        {
            foreach (Folder folder in folders.FolderList) {
               Clean(folder);
            }
        }
        public static void Clean(Folder folder)
        {
            List<String> folderContents = GetFolderContents(folder.Path);
            List<String> deletionList = GetDeletionList(folderContents, folder.DaysToDeletion);
            DeleteFiles(deletionList);
        }


        private static List<String> GetFolderContents(String path)
        {
            List<String> contents = GetFilePaths(path);
            contents.AddRange(GetFolderPaths(path));
            return contents;
        }

        public static List<String> GetFilePaths(String path)
        {
            List<String> result = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            return result;
        }

        private static List<String> GetFolderPaths(String path)
        {
            List<String> result = Directory.GetDirectories(path, "*").ToList();
            return result;
        }

        private static List<String> GetDeletionList(IEnumerable<string> folderContents, int daysToDeletion)
        {
            List<String> deletionList =new List<string>();
            foreach(String item in folderContents)
            {
                if(IsFreeToDelete(item, daysToDeletion))
                {
                    deletionList.Add(item);
                    
                }
            }
            return deletionList;
        }

        private static bool IsFreeToDelete(string filePath, int daysToDeletion)
        {
            FileInfo info = GetFileInfo(filePath);
            if(IsTooOld(info.LastAccessTime, info.LastWriteTime, daysToDeletion))
            {
                return true;
            }
            return false;
        }

        private static bool IsTooOld(DateTime lastModified, DateTime lastWriteTime, int daysToDeletion) {
            DateTime oldestAllowedDate = DateTime.Today.AddDays(-daysToDeletion);
            if(lastModified < oldestAllowedDate && lastWriteTime < oldestAllowedDate)
            {
                return true;
            }
            return false;
        }

        private static FileInfo GetFileInfo(String filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info;
        }

        private static void DeleteFiles(IEnumerable<string> deletionList)
        {
            foreach(String file in deletionList)
            {
                FileInfo info = new FileInfo(file);
                if(info.Attributes.Equals(FileAttributes.Directory))
                {
                    Directory.Delete(file,true);
                }
                else
                {
                    File.Delete(file);
                }
            }
        }

    }
}
