using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Содержит данные для выполнения парсинга страницы.
    /// </summary>
    public class ParsingSettings
    {       
        public int Id { get; set; }

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
        /// Изменяемая часть XPath.
        /// </summary>
        public string VariablePartOfXpath { get; set; }

        /// <summary>
        /// Номер первой строки для парсинга.
        /// </summary>
        public int StartXpathRow { get; set; }

        /// <summary>
        /// Номер последней строки для парсинга.
        /// </summary>
        public int EndXpathRow { get; set; }

        /// <summary>
        /// Символ разделения десятичной части числа.
        /// </summary>
        public char NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Символ разделения групп разрядов числа.
        /// </summary>
        public char NumberGroupSeparator { get; set; }

        /// <summary>
        /// Команды для обработки единицы измерения валюты.
        /// </summary>
        public virtual ICollection<UnitCommandAssignment> UnitCommands { get; set; }

        /// <summary>
        /// Команды для обработки текстового кода валюты.
        /// </summary>
        public virtual ICollection<TextCodeCommandAssignment> TextCodeCommands { get; set; }
               
        public int BankId { get; set; }
        /// <summary>
        /// Ссылка на банк.
        /// </summary>
        public virtual Bank Bank { get; set; }
    }
}
