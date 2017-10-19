namespace DeleteEmptyFolders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            string folder = Console.ReadLine();
            bool deletedSomething = true;
            while (deletedSomething)
            {
                Queue<string> folders = new Queue<string>();
                if (Directory.Exists(folder))
                {
                    folders.Enqueue(folder);
                }

                deletedSomething = false;
                while (folders.Count > 0)
                {
                    string current = folders.Dequeue();

                    bool hasNoFiles = Directory.EnumerateFiles(current).Count() == 0;
                    string[] subdirectories = Directory.EnumerateDirectories(current).ToArray();
                    if (hasNoFiles && subdirectories.Length == 0)
                    {
                        Directory.Delete(current);
                        deletedSomething = true;
                    }
                    else
                    {
                        foreach (var subdirectory in subdirectories)
                        {
                            folders.Enqueue(subdirectory);
                        }
                    }
                }
            }
        }
    }
}