using RatesParsingWeb.Dto.Bank.Command;

namespace RatesParsingWeb.Dto.Bank
{
    public class ParsingSettingsDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        public string RatesUrl { get; set; }

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
        public string NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Символ разделения групп разрядов числа.
        /// </summary>
        public string NumberGroupSeparator { get; set; }

        /// <summary>
        /// Команды для обработки текстовых данных.
        /// </summary>
        public CommandDto[] Commands { get; set; }
    }
}
