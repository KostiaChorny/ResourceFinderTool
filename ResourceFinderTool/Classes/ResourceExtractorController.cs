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

            OnLogMessage("Сканирование завершено!");
        }
    }
}
