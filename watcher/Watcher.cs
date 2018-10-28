using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

using McMaster.Extensions.CommandLineUtils;

namespace Watcher
{
    class Program
    {
        [Option("-g|--glob",
            "Glob that specifies files to mirror. (Default is all files).",
            CommandOptionType.SingleValue)]
        public static String Glob { get; } = "";

        [Required]
        [Option(
            "-i|--source",
            "Folder to synchronize files from",
            CommandOptionType.SingleValue)]
        [DirectoryExists]
        public static String Source { get; }

        [Required]
        [Option(
            "-o|--sink",
            "Folder to synchronize files to",
            CommandOptionType.SingleValue)]
        [DirectoryExists]
        public static String Sink { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            var watcher = new FileSystemWatcher(Source, Glob);
            watcher.NotifyFilter = 
                NotifyFilters.FileName |
                NotifyFilters.LastWrite | 
                NotifyFilters.DirectoryName;

            watcher.Created += RegularChange;
            watcher.Deleted += RegularChange;
            watcher.Changed += RegularChange;
            watcher.Renamed += RenameChange;
            watcher.Error += ErrorLogger;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            while(Console.Read() != 'q');
        }

        private void RegularChange(object source, FileSystemEventArgs e)
        {
            var targetFile = Path.Combine(Sink, e.Name);
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                case WatcherChangeTypes.Created:
                    try
                    {
                        File.Copy(e.FullPath, targetFile, true);
                        Console.WriteLine($"MIRRORED CHANGE :: {e.FullPath} to {targetFile}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(
                            $"ERROR :: Changed file {e.FullPath} couldn't be mirrored. Exception reported:{ex.Message}");
                    }
                    break;
                case WatcherChangeTypes.Deleted:
                    try
                    {
                        File.Delete(targetFile);
                        Console.WriteLine($"MIRRORED CHANGE :: Deleted {targetFile}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(
                            $"ERROR :: File {targetFile} couldn't be mirrored. Exception reported:{ex.Message}");
                    }
                    break;
                case WatcherChangeTypes.All:
                case WatcherChangeTypes.Renamed:
                    throw new ArgumentException($"Change of type {e.ChangeType}");
            }
        }

        private static void RenameChange(object source, RenamedEventArgs e)
        {
            // First case, if it's a rename, 
            var newName = Path.Combine(Sink, e.Name);
            var potentialRename = Path.Combine(Sink, e.OldName);
            if (File.Exists(potentialRename))
            {
                try
                {
                    File.Move(potentialRename, newName);
                    Console.WriteLine($"MIRRORED CHANGE :: Rename {e.OldName} to {e.Name}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(
                        $"ERROR :: Renamed file {e.FullPath} couldn't be mirrored. Exception reported:{ex.Message}");
                }
                return;
            }

            // Second case being a cut from another folder.
            try
            {
                File.Copy(e.FullPath, newName, true);
                Console.WriteLine($"MIRRORED CHANGE :: Copied new file {e.FullPath}.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"ERROR :: Changed file {e.FullPath} couldn't be mirrored. Exception reported:{ex.Message}");
            }
        }

        private static void ErrorLogger(object source, ErrorEventArgs e)
        {
            Console.Error.WriteLine($"ERROR :: {e.GetException().Message}");
        }
    }
}
