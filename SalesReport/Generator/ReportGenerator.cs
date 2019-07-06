using SalesReport.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Generator
{
    public class ReportGenerator
    {
        public static void Generate(List<IDataType> data, string outputFolder, string fileName)
        {
            var reportData = new ReportData
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

            using (var outputFileStream = File.OpenWrite($"{outputFolder}\\{fileName}"))
            {
                using (var streamWriter = new StreamWriter(outputFileStream))
                {
                    streamWriter.WriteLine($"Quantidade de clientes: {reportData.ClientsCount}");
                    streamWriter.WriteLine($"Quantidade de vendedores: {reportData.SalesManCount}");
                    streamWriter.WriteLine($"ID da venda mais cara: {reportData.MostExpensiveSaleID}");
                    streamWriter.WriteLine($"O pior vendedor: {reportData.WorstSallesMan}");
                }
            }
        }

        public static List<IDataType> Parse(string filePath, string separator)
        {
            List<IDataType> data = new List<IDataType>();

            using (var file = File.OpenText(filePath))
            {
                string line = null;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var typeIdentifier = line.TrimStart().Substring(0, line.IndexOf(separator));
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
            }
            return data;
        }
    }
}
