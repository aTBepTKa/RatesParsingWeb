using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ExchangeRateListModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата и время получения данных об обменных курсах.
        /// </summary>
        [DisplayName("Дата получения обменных курсов")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime DateTimeStamp { get; set; }
    }
}
