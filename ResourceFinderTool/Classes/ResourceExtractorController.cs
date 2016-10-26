using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFinderTool.Classes
{
    public class ResourceExtractorController
    {
        public string Path { get; private set; }

        public event EventHandler<string> LogMessage;

        protected void OnLogMessage(string message)
        {
            if (LogMessage != null)
            {
                LogMessage.Invoke(this, message);
            }
        }

        public ResourceExtractorController(string path)
        {
            Path = path;
        }

        public void StartExtraction()
        {
            try
            {
                InnerStartExtraction();
            }
            catch (Exception ex)
            {
                OnLogMessage("Произошла ошибка!");
                OnLogMessage(ex.Message);
                if (ex.InnerException != null) OnLogMessage(ex.InnerException.Message);
            }
        }

        private void InnerStartExtraction()
        {
            OnLogMessage(string.Format("Начато сканирование папки {0}", Path));
            var finder = new FileFinder(Path);
            var files = finder.Find("cs");

            OnLogMessage(string.Format("Найдено файлов: {0}", files.Count));

            var extractor = new ResourceExtractor("\"(?<data>.*[а-яА-Я]+.*)\"");
            var results = extractor.Extract(files);

            OnLogMessage(string.Format("Найдено строк: {0}", results.Count));

            var writer = new StreamWriter("result.txt");
            foreach (var resource in results)
            {
                writer.WriteLine(resource.String);
            }

            OnLogMessage("Сканирование завершено!");
        }
    }
}
