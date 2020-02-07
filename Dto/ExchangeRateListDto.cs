﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    public class ExchangeRateListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата и время получения данных об обменных курсах.
        /// </summary>
        public DateTime DateTimeStamp { get; set; }

        public int BankId { get; set; }

        /// <summary>
        /// Ссылка на банк.
        /// </summary>
        public virtual BankDto Bank { get; set; }

        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public virtual ICollection<ExchangeRateDto> ExchangeRates { get; set; }
    }
}
