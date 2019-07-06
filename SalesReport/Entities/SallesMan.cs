using SalesReport.Parser;
using SalesReport.Parser.Attributes;
using SalesReport.Parser.Interfaces;

namespace SalesReport.Entities
{
    public class SallesMan : IDataTypeParser<SallesMan>, IDataType
    {
        [Collumn(0)]
        public string DataID { get; set; }

        [Collumn(1)]
        public string CPF { get; set; }
        [Collumn(2)]
        public string Name { get; set; }
        [Collumn(3)]
        public decimal Salary { get; set; }

        public SallesMan Parse(string data)
        {
            return SeparatorParser.Parse(data, this);
        }
    }
}
