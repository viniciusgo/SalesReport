using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class CollumnAttribute : Attribute
    {
        public int order { get; set; }

        public CollumnAttribute(int order)
        {
            this.order = order;
        }
    }
}
