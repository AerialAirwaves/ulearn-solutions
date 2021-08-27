using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask 
    {
        internal class Folder 
        {
            public string Name;
            public readonly Dictionary<string, Folder> Subfolders = new Dictionary<string, Folder>();
        }

        public static List<string> Solve(List<string> fullPaths)
        {
            var root = new Folder();
            foreach (var path in fullPaths) 
            {
                var current = root;
                foreach (var name in path.Split('\\'))
                {
                    if (!current.Subfolders.TryGetValue(name, out var value))
                        current.Subfolders[name] = value = new Folder{Name = name};
                    current = value;
                }
            }

            return EnumerateSubfolders(root, 0).ToList();
        }

        private static IEnumerable<string> EnumerateSubfolders(Folder folder, int nestingLevel)
        {
            if (nestingLevel > 0)
                yield return folder.Name.PadLeft(folder.Name.Length - 1 + nestingLevel);
                
            foreach (var e in folder.Subfolders.Values
                    .OrderBy(x => x.Name, StringComparer.Ordinal)
                    .SelectMany(x => EnumerateSubfolders(x, nestingLevel + 1)))
                yield return e;
        }
    }
}