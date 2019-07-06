using SalesReport.Parser;
using SalesReport.Parser.Attributes;
using SalesReport.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Entities
{
    public class Client : IDataTypeParser<Client>, IDataType
    {
        [Collumn(0)]
        public string DataID { get; set; }

        [Collumn(1)]
        public string CNPJ { get; set; }
        [Collumn(2)]
        public string Name { get; set; }
        [Collumn(3)]
        public string BusinessArea { get; set; }

        public Client Parse(string data)
        {
            return SeparatorParser.Parse(data, this);
        }
    }
}
