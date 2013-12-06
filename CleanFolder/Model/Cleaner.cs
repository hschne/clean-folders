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

        private static Log log;

        public delegate void CleaningFoldersFinishedHandler();

        public static event CleaningFoldersFinishedHandler CleaningFoldersFinished;


        static Cleaner() {
            cleanFolderSettings = CleanFolderSettings.GetInstance;
            folders = Folders.GetInstance;
            log = Log.GetInstance;
        }

        public static void Clean()
        {
            List < CleanFolderResult > results = new List<CleanFolderResult>();
            foreach (Folder folder in folders.FolderList) {
                results.Add(Clean(folder));
            }
            log.Add(new LogEntry(results));
            FireCleaningFolderFinished();
        }

        public static void CleanSingleFolder(Folder folder) {
            List<CleanFolderResult> results = new List<CleanFolderResult>();
            results.Add(Clean(folder));
            log.Add(new LogEntry(results));
            FireCleaningFolderFinished();
        }

        public static CleanFolderResult Clean(Folder folder)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<String> FolderContents = GetFolderContents(folder.Path);
            List<String> DeletionList = GetDeletionList(FolderContents, folder.DaysToDeletion);
            DeleteFiles(DeletionList);
            sw.Stop();
            return  new CleanFolderResult(folder.Name, folder.Path, DeletionList, sw.Elapsed);
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

        private static void FireCleaningFolderFinished() {
            CleaningFoldersFinishedHandler handler = CleaningFoldersFinished;
            if (handler != null) {
                handler();
            }
        }

    }
}
