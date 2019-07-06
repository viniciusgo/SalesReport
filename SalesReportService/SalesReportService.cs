using System;
using System.Linq;
using System.ServiceProcess;

namespace SalesReportService
{
    public partial class SalesReportService : ServiceBase
    {
        private SalesReport.SalesReport salesReportGenerator { get; set; }
        public SalesReportService()
        {
            InitializeComponent();
        }

        internal void Test(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            if (args.Count() == 2)
                salesReportGenerator = new SalesReport.SalesReport(
                    Environment.ExpandEnvironmentVariables(args[0]),
                    Environment.ExpandEnvironmentVariables(args[1]));
            else
                salesReportGenerator = new SalesReport.SalesReport();

            salesReportGenerator.Start();
        }

        protected override void OnStop()
        {
            if (salesReportGenerator != null)
                salesReportGenerator.Stop();
        }
    }
}
