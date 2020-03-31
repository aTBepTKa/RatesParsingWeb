using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.ParsingService
{
    /// <summary>
    /// Результат парсинга страницы.
    /// </summary>
    public class ParsingResultDto
    {
        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public IEnumerable<ExchangeRateDto> ExchangeRates { get; set; }

        /// <summary>
        /// Парсинг выполнен успешно.
        /// </summary>
        public bool IsSuccesfullParsed { get; set; }

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}
