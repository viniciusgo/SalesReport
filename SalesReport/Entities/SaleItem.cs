using SalesReport.Parser;
using SalesReport.Parser.Attributes;
using SalesReport.Parser.Interfaces;

namespace SalesReport.Entities
{
    public class SaleItem : IDataTypeParser<SaleItem>
    {
        [Collumn(0)]
        public string ItemID { get; set; }
        [Collumn(1)]
        public int ItemQuantity { get; set; }
        [Collumn(2)]
        public decimal ItemPrice { get; set; }

        public SaleItem Parse(string data)
        {
            return SeparatorParser.Parse(data, this, "-");
        }
    }
}
