using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IBankService : ICrudService<BankDto>
    {
        /// <summary>
        /// Получить все объекты банка, включая основную валюту банка.
        /// </summary>
        /// <returns></returns>      
        Task<IEnumerable<BankDto>> GetBankListAsync();

        /// <summary>
        /// Получить банк с основной валютой.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetBankAsync(int id);

        /// <summary>
        /// Обновить данные банка.
        /// </summary>
        /// <param name="bankToUpdate"></param>
        /// <returns></returns>
        Task<bool> UpdateBankAsync(BankDto bankToUpdate);
    }
}
