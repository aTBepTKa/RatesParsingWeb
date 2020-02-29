using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    /// <summary>
    /// Средства для работы с валютой.
    /// </summary>
    public interface ICurrencyService : ICrudService<CurrencyDto>
    {
        CurrencyDto GetCurrencyByTextCode(string textCode);
    }
}
