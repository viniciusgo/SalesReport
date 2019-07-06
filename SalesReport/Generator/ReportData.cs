using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Generator
{
    public class ReportData
    {
        public int ClientsCount { get; set; }
        public int SalesManCount { get; set; }
        public string MostExpensiveSaleID { get; set; }
        public string WorstSallesMan { get; set; }
    }
}
