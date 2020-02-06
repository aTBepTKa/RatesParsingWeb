using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.SerializeJson.Json
{
    /// <summary>
    /// Список обменных курсов банка.
    /// </summary>
    public class ExchangeRateListJson
    {
        /// <summary>
        /// Дата и время получения данных об обменных курсах.
        /// </summary>
        public DateTime DateTimeStamp { get; set; }

        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public virtual ICollection<ExchangeRateJson> ExchangeRates { get; set; }

    }
}
