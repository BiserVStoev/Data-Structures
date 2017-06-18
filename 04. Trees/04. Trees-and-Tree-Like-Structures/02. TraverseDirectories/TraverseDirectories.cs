using System;
using System.IO;

namespace _02TraverseDirectories
{
    public class TraverseDirectories
    {
        public static void Main(string[] args)
        {
            string path = @"C:\Windows";


            DirectoryInfo current = new DirectoryInfo(path);
            var currentFolder = new Folder(current.Name);

            FillTree(current, currentFolder);

            //print
            currentFolder.Print(0);
            Console.WriteLine("Total size: {0} KB",currentFolder.CalculateSize() / 1024);
        }

        private static void FillTree(DirectoryInfo current, Folder currentFolder)
        {
            var windowsFiles = current.GetFiles();
            var files = new File[windowsFiles.Length];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new File(windowsFiles[i].Name, windowsFiles[i].Length);
            }

            currentFolder.Files = files;

            var wFolders = current.GetDirectories();
            currentFolder.Folders = new Folder[wFolders.Length];
            for (int i = 0; i < wFolders.Length; i++)
            {
                var folder = new Folder(wFolders[i].Name);
                FillTree(wFolders[i],folder);
                currentFolder.Folders[i] = folder;
            }
        }
    }
}
