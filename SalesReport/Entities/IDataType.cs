using SalesReport.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Entities
{
    public interface IDataType
    {
        [Collumn(0)]
        string DataID { get; set; }
    }
}
