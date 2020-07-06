namespace RatesParsingWeb.Dto.Bank
{
    /// <summary>
    /// Содержит основные реквизиты банка.
    /// </summary>
    interface IBankValidity
    {
        string SwiftCode { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        string BankUrl { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        string RatesUrl { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        int CurrencyId { get; set; }

        /// <summary>
        /// Настройки парсинга.
        /// </summary>
        ParsingSettingsDto ParsingSettings { get; set; }
    }
}
