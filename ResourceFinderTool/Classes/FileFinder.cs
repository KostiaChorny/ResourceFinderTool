using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFinderTool.Classes
{
    public class FileFinder
    {
        public string Path { get; private set; }

        public FileFinder(string path)
        {
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(string.Format("Папка {0} не существует!", path));

            Path = path;
        }

        public List<string> Find(string extention = "*", bool recursive = true)
        {
            return Find(new List<string>() {extention}, recursive);
        }

        public List<string> Find(List<string> extentions, bool recursive = true)
        {
            List<string> result = new List<string>();

            foreach (var extention in extentions)
            {
                var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = Directory.EnumerateFiles(Path, string.Format("*.{0}", extention), searchOption);
                result.AddRange(files);
            }

            return result;
        }
    }
}
