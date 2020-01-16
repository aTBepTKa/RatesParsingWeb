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
        public IEnumerable<Currency> GetCurrenciesFromXml()
        {
            // Получить xml файл из папки проекта.
            string workingDirectory = Directory.GetCurrentDirectory();
            string xmlRelativeFilePath = @"\App_data\ISO4217.xml";
            string fileName = string.Concat(workingDirectory, xmlRelativeFilePath);

            // Получить данные из XML файла и удалить дубликаты.
            CurrencyXmlData currenciesFromXml;
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(CurrencyXmlData));
                currenciesFromXml = (CurrencyXmlData)serializer.Deserialize(fileStream);
            }
            var currenciesFromXmlNoDuplicates = currenciesFromXml.CcyNtry.Distinct(new CurrencyEqualityComparer());
            
            // Конвертировать полученные данные в формат для БД.
            var currencies = new List<Currency>();
            foreach (var cur in currenciesFromXmlNoDuplicates)
            {                
                var newCurrency = new Currency
                {
                    CurrencyName = cur.CcyNm,
                    TextCode = cur.Ccy,
                };
                if (int.TryParse(cur.CcyNbr, out int numCodeTemp))
                {
                    newCurrency.NumCode = numCodeTemp;
                    currencies.Add(newCurrency);
                }
            }

            return currencies;
        }
    }
}
