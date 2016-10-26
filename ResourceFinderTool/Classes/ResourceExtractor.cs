using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResourceFinderTool.Classes
{
    class ResourceExtractor
    {
        public Regex Pattern { get; private set; }

        private string dataParam;

        public ResourceExtractor(Regex pattern, string dataParam = null)
        {
            Pattern = pattern;
            this.dataParam = dataParam;
        }

        public ResourceExtractor(string pattern, string dataParam = null)
        {
            Pattern = new Regex(pattern);
            this.dataParam = dataParam;
        }

        public List<Resource> Extract(string path)
        {
            var result = new List<Resource>();

            var file = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(file);

            int linesCounter = 0;
            while (true)
            {
                var str = reader.ReadLine();
                if (str == null) return result;
                linesCounter++;

                result.AddRange(from Match match in Pattern.Matches(str)
                    select new Resource()
                    {
                        FileName = path, 
                        Line = linesCounter, 
                        Position = match.Index, 
                        String = dataParam == null ? match.Value : match.Groups[dataParam].Value
                    });
            }
        }

        public List<Resource> Extract(List<string> paths)
        {
            var result = new List<Resource>();

            foreach (var path in paths)
            {
                result.AddRange(Extract(path));
            }

            return result;
        }

    }
}
