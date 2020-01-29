using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface ICurrencyService : ICrudService<CurrencyDto>
    {
    }
}
