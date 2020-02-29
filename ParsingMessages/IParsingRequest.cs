using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    public interface IParsingRequest
    {
        string TaskName { get; set; }

        /// <summary>
        /// Ссылка на страницу с курсами.
        /// </summary>
        string RatesUrl { get; set; }


        /// <summary>
        /// XPath для текстового кода валюты.
        /// </summary>
        string TextCodeXpath { get; set; }

        /// <summary>
        /// XPath для единицы измерения валюты.
        /// </summary>
        string UnitXpath { get; set; }

        /// <summary>
        /// XPath для значения обменного курса валюты.
        /// </summary>
        string ExchangeRateXpath { get; set; }

        /// <summary>
        /// Переменная часть адреса XPath.
        /// </summary>
        string VariablePartOfXpath { get; set; }


        /// <summary>
        /// Разделитель десятичной части числа.
        /// </summary>
        string NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Разделитель групп разрядов числа.
        /// </summary>
        string NumberGroupSeparator { get; set; }

        /// <summary>
        /// Начальная строка для считывания.
        /// </summary>
        int StartXpathRow { get; set; }

        /// <summary>
        /// Последняя строка для считывания.
        /// </summary>
        int EndXpathRow { get; set; }

        /// <summary>
        /// Команды для обработки строки единицы измерения Unit.
        /// </summary>
        Dictionary<string, string[]> UnitCommands { get; set; }

        /// <summary>
        /// Команды для обработки строки текстового кода валюты TextCode.
        /// </summary>
        Dictionary<string, string[]> TextCodeCommands { get; set; }
    }
}
