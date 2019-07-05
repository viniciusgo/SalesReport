using SalesReport.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Entities
{
    public class Sale : IDataTypeParser<Sale>, IDataType
    {
        [Collumn(0)]
        public string DataID { get; set; }

        [Collumn(1)]
        public string SaleID { get; set; }
        [Collumn(2)]
        public string _saleItemString { get; set; }
        [Collumn(3)]
        public string SalesManName { get; set; }

        public List<SaleItem> SaleItems { get; set; }

        public decimal Total
        {
            get
            {
                return (this.SaleItems != null)  
                    ? SaleItems.Sum(si => si.ItemPrice * si.ItemQuantity)
                    : decimal.Zero;
            }
        }

        public Sale Parse(string data)
        {
            SeparatorParser.Parse(data, this);

            this.SaleItems = new List<SaleItem>();

            var itemsToParse = this._saleItemString.Trim('[', ']');
            var items = itemsToParse.Split(',');

            foreach (var item in items)
            {
                var saleItem = new SaleItem();
                this.SaleItems.Add(saleItem.Parse(item));
            }

            return this;
        }
    }

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
