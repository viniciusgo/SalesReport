using SalesReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SalesReportService
{
    public partial class Service1 : ServiceBase
    {
        private SalesReportGenerator salesReportGenerator { get; set; }
        public Service1()
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
            salesReportGenerator = new SalesReportGenerator(
                Environment.ExpandEnvironmentVariables(args[0]),
                Environment.ExpandEnvironmentVariables(args[1]));
            salesReportGenerator.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
