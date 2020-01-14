using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Список обменных курсов банка.
    /// </summary>
    public class ExchangeRateList
    {
        public int Id { get; set; }
        public int BankId { get; set; }

        /// <summary>
        /// Дата и время получения данных об обменных курсах.
        /// </summary>
        public DateTime DateTimeStamp { get; set; }

        /// <summary>
        /// Ссылка на банк.
        /// </summary>
        public virtual Bank Bank { get; set; }

        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }

    }
}
