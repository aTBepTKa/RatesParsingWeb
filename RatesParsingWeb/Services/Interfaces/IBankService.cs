using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IBankService : ICrudService<BankDto>
    {
        /// <summary>
        /// Получить все объекты банка, включая основную валюту банка.
        /// </summary>
        /// <returns></returns>      
        Task<IEnumerable<BankDto>> GetList();

        /// <summary>
        /// Получить банк со связанными сущностями.
        /// </summary>
        /// <param name="id">ID получаемого банка.</param>
        /// <returns></returns>
        Task<BankDto> GetBank(int id);

        /// <summary>
        /// Получить банк с Currency.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetBankCurrency(int id);

        /// <summary>
        /// Получить банк с ParsingSettings.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetBankParsingSettings(int id);

        /// <summary>
        /// Создать новый банк.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(BankCreateDto createDto);

        /// <summary>
        /// Обновить данные банка.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BankUpdateDto updateDto);

        /// <summary>
        /// Удалить банк по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
