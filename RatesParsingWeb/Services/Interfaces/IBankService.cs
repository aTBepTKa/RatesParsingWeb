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
        Task<BankDto> GetBankAllData(int id);

        /// <summary>
        /// Получить банк по свифт коду.
        /// </summary>
        /// <param name="swiftCode"></param>
        /// <returns></returns>
        Task<BankDto> GetBankBySwiftCode(string swiftCode);

        /// <summary>
        /// Получить банк с Currency.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetBankCurrency(int id);

        /// <summary>
        /// Получить банк с настройками парсинга.
        /// </summary>
        /// <param name="id">Id банка</param>
        /// <returns></returns>
        Task<BankDto> GetBankWithParsingSettings(int id);

        /// <summary>
        /// Получить банк с настройками парсинга.
        /// </summary>
        /// <param name="swiftCode">SWIFT-код банка.</param>
        /// <returns></returns>
        Task<BankDto> GetBankWithParsingSettings(string swiftCode);

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
