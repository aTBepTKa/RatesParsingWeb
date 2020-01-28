using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IBankService : IService<Bank>
    {
        Task<Bank> GetByIdWithSettings(int id);
    }
}
