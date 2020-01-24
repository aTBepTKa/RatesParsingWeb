using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ParsingSettingsModel
    {
        public int Id { get; set; }

        /// <summary>
        /// XPath для текстового кода валюты.
        /// </summary>
        [DisplayName("XPath для текстового кода")]
        public string TextCodeXpath { get; set; }

        /// <summary>
        /// XPath для единицы измерения валюты.
        /// </summary>
        [DisplayName("XPath для единицы измерения")]
        public string UnitXpath { get; set; }

        /// <summary>
        /// XPath для значения обменного курса валюты.
        /// </summary>
        [DisplayName("Xpath для обменного курса")]
        public string ExchangeRateXpath { get; set; }

        /// <summary>
        /// Изменяемая часть XPath.
        /// </summary>
        [DisplayName("Изменяемая часть XPath")]
        public string VariablePartOfXpath { get; set; }

        /// <summary>
        /// Номер первой строки для парсинга.
        /// </summary>
        [DisplayName("Номер первой строки для парсинга")]
        public int StartXpathRow { get; set; }

        /// <summary>
        /// Номер последней строки для парсинга.
        /// </summary>
        [DisplayName("Номер последней строки для парсинга")]
        public int EndXpathRow { get; set; }

        /// <summary>
        /// Символ разделения десятичной части числа.
        /// </summary>
        [DisplayName("Разделитель десятичной части")]
        public char NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Символ разделения групп разрядов числа.
        /// </summary>
        [DisplayName("Разделитель групп разрядов")]
        public char NumberGroupSeparator { get; set; }
    }
}
