using System;
using System.IO;
using SalesReport.Generator;

namespace SalesReport
{
    public class SalesReport
    {
        public static string Separator { get; set; } = "ç";
        private FileSystemWatcher watcher { get; set; }

        public string inputFolder { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\data\\in";
        public string outputFolder { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\data\\out";

        public SalesReport()
        {
            watcher = new FileSystemWatcher();
        }

        public SalesReport(string inputFolder, string outputFolder)
        {
            this.inputFolder = inputFolder ?? throw new ArgumentNullException(nameof(inputFolder));
            this.outputFolder = outputFolder ?? throw new ArgumentNullException(nameof(outputFolder));

            watcher = new FileSystemWatcher();
        }

        private void CreateIfNotExists(string folder)
        {
            if (folder.StartsWith("\\\\"))
                folder.Remove(0, 2);
            System.IO.Directory.CreateDirectory(folder);
        }

        public void Start()
        {
            this.CreateIfNotExists(this.inputFolder);
            this.CreateIfNotExists(this.outputFolder);

            watcher.Path = this.inputFolder;
            watcher.NotifyFilter = NotifyFilters.LastWrite 
                                    | NotifyFilters.FileName
                                    | NotifyFilters.CreationTime
                                    | NotifyFilters.LastAccess;
            watcher.Filter = "*.dat";

            //watcher.Changed += OnChanged;
            watcher.Created += (source, e) =>
            {
                var data = ReportGenerator.Parse(e.FullPath, SalesReport.Separator);
                ReportGenerator.Generate(data, this.outputFolder, e.Name);
            };

            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if (watcher != null)
                watcher.Dispose();
        }
    }
}
