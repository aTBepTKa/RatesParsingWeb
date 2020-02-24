using System;
using System.Xml.Serialization;

namespace RatesParsingWeb.Storage.SerializeXml
{
    /// <summary>
    /// Предоставляет свойства для выполнения десериализации данных типов валют.
    /// </summary>
    [Serializable()]
    public class CurrencyXmlItem
    {
        /// <summary>
        /// Полное название валюты.
        /// </summary>
        public string CcyNm { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        public string Ccy { get; set; }

        /// <summary>
        /// Цифровой код валюты.
        /// </summary>
        public string CcyNbr { get; set; }

        /// <summary>
        /// Пустой конструктор без параметров необходим для выполнения десериализации.
        /// </summary>
        public CurrencyXmlItem() { }
    }

    /// <summary>
    /// Содержит массив данных типа валюты.
    /// </summary>
    [Serializable()]
    [XmlRoot("ISO_4217")]
    public class CurrencyXmlData
    {
        [XmlArray("CcyTbl")]
        [XmlArrayItem("CcyNtry", typeof(CurrencyXmlItem))]
        public CurrencyXmlItem[] CcyNtry { get;set; }
    }
}
