using SalesReport.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace SalesReport
{
    public class SalesReportGenerator
    {
        private enum DataTypes { Seller = 0, Client = 1, Sell = 2 }
        private static string[] TypeIdentifiers = { "001", "002", "003" };
        public static string Separator { get; set; } = "ç";
        private FileSystemWatcher watcher { get; set; }

        public string inputFolder { get; set; }
        public string outputFolder { get; set; }

        public SalesReportGenerator(string inputFolder, string outputFolder)
        {
            this.inputFolder = inputFolder ?? throw new ArgumentNullException(nameof(inputFolder));
            this.outputFolder = outputFolder ?? throw new ArgumentNullException(nameof(outputFolder));

            watcher = new FileSystemWatcher();
        }

        private void CreateIfNotExists(string folder)
        {
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
            watcher.Created += (s, e) => OnChanged(s, e, this);

            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if (watcher != null)
                watcher.Dispose();
        }

        private static void OnChanged(object source, FileSystemEventArgs e, SalesReportGenerator salesReportGenerator)
        {
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

            List<IDataType> data = new List<IDataType>();

            using (var file = File.OpenText(e.FullPath))
            {
                
                string line = null;
                while((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var typeIdentifier = line.TrimStart().Substring(0, line.IndexOf(SalesReportGenerator.Separator));
                    switch (typeIdentifier)
                    {
                        case "001":
                            var sallesMan = new SallesMan();
                            sallesMan.Parse(line);
                            data.Add(sallesMan);
                            break;
                        case "002":
                            var client = new Client();
                            client.Parse(line);
                            data.Add(client);
                            break;
                        case "003":
                            var sale = new Sale();
                            sale.Parse(line);
                            data.Add(sale);
                            break;
                    }
                }

                var report = new
                {
                    ClientsCount = data.OfType<Client>().Count(),
                    SalesManCount = data.OfType<SallesMan>().Count(),
                    MostExpensiveSaleID = data.OfType<Sale>().OrderByDescending(s => s.Total).FirstOrDefault()?.SaleID,
                    WorstSallesMan = data.OfType<Sale>()
                        .GroupBy(s => s.SalesManName,
                                    (key, g) => new {
                                        SalesManName = key,
                                        Total = g.Sum(si => si.Total)
                                    }
                                ).OrderBy(g => g.Total).FirstOrDefault()?.SalesManName    
                };

                using (var outputFileStream = File.OpenWrite($"{salesReportGenerator.outputFolder}\\{e.Name}"))
                {
                    using (var streamWriter = new StreamWriter(outputFileStream))
                    {
                        streamWriter.WriteLine($"Quantidade de clientes: {report.ClientsCount}");
                        streamWriter.WriteLine($"Quantidade de vendedores: {report.SalesManCount}");
                        streamWriter.WriteLine($"ID da venda mais cara: {report.MostExpensiveSaleID}");
                        streamWriter.WriteLine($"O pior vendedor: {report.WorstSallesMan}");
                    }
                }
            }                
        }

    }
}
