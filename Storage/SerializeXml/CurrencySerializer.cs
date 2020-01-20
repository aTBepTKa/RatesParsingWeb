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
        /// Получить данные типов валют из файла.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CurrencyXmlItem> GetCurrenciesFromXml()
        {
            // Получить xml файл из папки проекта.
            string workingDirectory = Directory.GetCurrentDirectory();
            string xmlRelativeFilePath = @"\App_data\ISO4217.xml";
            string fileName = Path.Combine(workingDirectory, xmlRelativeFilePath);
            // Временно, ибо спать хочется.
            fileName = @"d:\Projects\WEB\ASP\ExchangeRatesParsing\RatesParsingWeb\App_Data\ISO4217.xml";

            // Получить данные из XML файла и удалить дубликаты.
            CurrencyXmlData currenciesFromXml;
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(CurrencyXmlData));
                currenciesFromXml = (CurrencyXmlData)serializer.Deserialize(fileStream);
            }
            var currenciesFromXmlNoDuplicates = currenciesFromXml.CcyNtry.Distinct(new CurrencyEqualityComparer());

            return currenciesFromXmlNoDuplicates;
        }
    }
}
