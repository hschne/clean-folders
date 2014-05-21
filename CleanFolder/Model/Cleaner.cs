using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CleanFolder.Model
{
    public static class Cleaner {

        private static readonly Folders Folders;

        static Cleaner() {
            Folders = Folders.GetInstance;
        }

        public static void Clean()
        {
            foreach (Folder folder in Folders.FolderList) {
               Clean(folder);
            }
        }
        public static void Clean(Folder folder)
        {
            List<String> folderContents = GetFolderContents(folder.Path);
            IEnumerable<string> deletionList = GetDeletionList(folderContents, folder.DaysToDeletion);
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

        private static IEnumerable<string> GetFolderPaths(String path)
        {
            List<String> result = Directory.GetDirectories(path, "*").ToList();
            return result;
        }

        private static IEnumerable<string> GetDeletionList(IEnumerable<string> folderContents, int daysToDeletion)
        {
            var deletionList =new List<string>();
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
            var info = new FileInfo(filePath);
            return info;
        }

        private static void DeleteFiles(IEnumerable<string> deletionList)
        {
            foreach(String file in deletionList)
            {
                var info = new FileInfo(file);
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
