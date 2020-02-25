using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Данные для запроса к банку.
    /// </summary>
    public class BankRequest
    {
        /// <summary>
        /// Ссылка на страницу с курсами.
        /// </summary>
        public string RatesUrlPage { get; set; }


        /// <summary>
        /// XPath для текстового кода валюты.
        /// </summary>
        public string TextCodeXpath { get; set; }

        /// <summary>
        /// XPath для единицы измерения валюты.
        /// </summary>
        public string UnitXpath { get; set; }

        /// <summary>
        /// XPath для значения обменного курса валюты.
        /// </summary>
        public string ExchangeRateXpath { get; set; }

        /// <summary>
        /// Переменная часть адреса XPath.
        /// </summary>
        public string VariablePartOfXpath { get; set; }


        /// <summary>
        /// Разделитель десятичной части числа.
        /// </summary>
        public string NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Разделитель групп разрядов числа.
        /// </summary>
        public string NumberGroupSeparator { get; set; }

        /// <summary>
        /// Начальная строка для считывания.
        /// </summary>
        public int StartXpathRow { get; set; }

        /// <summary>
        /// Последняя строка для считывания.
        /// </summary>
        public int EndXpathRow { get; set; }

        /// <summary>
        /// Команды для обработки строки единицы измерения Unit.
        /// </summary>
        public Dictionary<string, string[]> UnitScripts { get; set; }

        /// <summary>
        /// Команды для обработки строки текстового кода валюты TextCode.
        /// </summary>
        public Dictionary<string, string[]> TextCodeScripts { get; set; }
    }
}
