using System.Collections.Generic;
using System.Linq;
using RatesParsingWeb.Domain;
using System.IO;
using System.Xml.Serialization;

namespace RatesParsingWeb.Storage.SerializeXml
{
    /// <summary>
    /// Средства для получения данных валюты из XML файла.
    /// </summary>
    public class CurrencySerializer
    {
        /// <summary>
        /// Получить данные типов валют из файла ресурсов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CurrencyXmlItem> GetCurrenciesFromXml()
        {
            CurrencyXmlData currenciesFromXml;
            using (var reader = new StringReader(Properties.Resources.ISO4217))
            {
                var serializer = new XmlSerializer(typeof(CurrencyXmlData));
                currenciesFromXml = (CurrencyXmlData)serializer.Deserialize(reader);
            }
            var currenciesFromXmlNoDuplicates = currenciesFromXml.CcyNtry.Distinct(new CurrencyEqualityComparer());
            return currenciesFromXmlNoDuplicates;
        }
    }
}
