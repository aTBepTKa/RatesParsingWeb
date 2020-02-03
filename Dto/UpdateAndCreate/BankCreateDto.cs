using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.UpdateAndCreate
{
    public class BankCreateDto : IUpdateCreateFields
    {
        public int Id { get; set; }
        public string SwiftCode { get; set; }
        public string Name { get; set; }
        public string BankUrl { get; set; }
        public string RatesUrl { get; set; }
        public int CurrencyId { get; set; }
    }
}
