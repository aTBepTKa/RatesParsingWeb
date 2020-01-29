using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IBankService : ICrudService<BankDto>
    {
        /// <summary>
        /// Получить все элементы.
        /// </summary>
        /// <returns></returns>      
        Task<IEnumerable<BankDto>> GetAll();

        /// <summary>
        /// Получить банк с основной валютой.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetByIdAsync(int id);

        /// <summary>
        /// Обновить данные банка.
        /// </summary>
        /// <param name="bankToUpdate"></param>
        /// <returns></returns>
        Task<bool> UpdateBankAsync(BankDto bankToUpdate);
    }
}
