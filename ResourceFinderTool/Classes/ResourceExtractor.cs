using System;
using System.Collections.Generic;
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

        public ResourceExtractor(Regex pattern)
        {
            Pattern = pattern;
        }

        public ResourceExtractor(string pattern)
        {
            Pattern = new Regex(pattern);
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
                        String = match.Value
                    });
            }
        }

    }
}
