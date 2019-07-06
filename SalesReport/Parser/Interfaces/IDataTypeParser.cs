

namespace SalesReport.Parser.Interfaces
{
    public interface IDataTypeParser<T>
    {
        T Parse(string data);
    }
}
