using SalesReport.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Entities
{
    public abstract class SeparatorParser
    {

        public static T Parse<T>(string line, T typeInstance, string separator = "ç") where T : new()
        {
            if(typeInstance == null) typeInstance = new T();
            line = (line == null || string.IsNullOrEmpty(line)) ? throw new ArgumentNullException(nameof(line)) : line;

            var lineDataArray = line.Split(new string[] { separator }, StringSplitOptions.None);

            var columnProperties = typeInstance.GetType().GetProperties()
                .Where(pi => pi.GetCustomAttributes(true).OfType<CollumnAttribute>().Any())
                .OrderBy(pi => pi.GetCustomAttributes(true).OfType<CollumnAttribute>().First().order)
                .ToArray();

            if (columnProperties.Count() > lineDataArray.Count())
            {
                throw new Exception("Quantidade de dados insuficientes na linha");
            }

            Parallel.For(0, columnProperties.Count(), i =>
            {
                var pi = columnProperties[i];
                pi.SetValue(typeInstance, Convert.ChangeType(lineDataArray[i], pi.PropertyType));
            });

            return typeInstance;
        }
    }
}
