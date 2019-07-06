using SalesReport.Parser.Attributes;

namespace SalesReport.Entities
{
    public interface IDataType
    {
        [Collumn(0)]
        string DataID { get; set; }
    }
}
