namespace Commenter
{
    using System;
    using System.IO;
    using System.Linq;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 2 || args.Length < 1)
            {
                Console.Error.WriteLine("Usage: Commenter Filename [CommentPrefix]");
            }
            else
            {
                string filename = args[0];
                string commentPrefix = (args.Length == 2) ? args[1] : "//";
                string[] lines = File.ReadAllLines(filename);
                int commentLocation = lines.Select((t) => GetLengthWithoutComment(t, commentPrefix)).Max() + 2;
                string[] commented = lines.Select(line => Comment(line, commentLocation, commentPrefix)).ToArray();
                File.WriteAllLines(filename, commented);
            }
        }

        private static int GetLengthWithoutComment(string line, string commentPrefix)
        {
            int originalCommentLocation = line.IndexOf(commentPrefix);
            return (originalCommentLocation == -1) ? line.Length : line.Substring(0, originalCommentLocation).TrimEnd().Length;
        }

        private static string Comment(string input, int commentLocation, string commentPrefix)
        {
            int currentCommentLocation = input.IndexOf(commentPrefix);

            if (currentCommentLocation == commentLocation)
            {
                return input;
            }
            else if (currentCommentLocation == 0)
            {
                return input;
            }
            else if (currentCommentLocation == -1)
            {
                return input + new string(' ', commentLocation - input.Length - commentPrefix.Length) + commentPrefix;
            }
            else
            {
                string prefix = input.Substring(0, currentCommentLocation).TrimEnd();
                string comment = input.Substring(currentCommentLocation);
                string spaces = new string(' ', commentLocation - prefix.Length - commentPrefix.Length);
                return prefix + spaces + comment;
            }
        }
    }
}
