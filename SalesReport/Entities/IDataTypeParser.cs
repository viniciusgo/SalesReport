using SalesReport.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Entities
{
    public interface IDataTypeParser<T>
    {
        T Parse(string data);
    }
}
