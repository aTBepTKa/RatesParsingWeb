﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Services.Interfaces;

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
        /// Получить банк с подчиненными данными.
        /// </summary>
        /// <param name="id">ID получаемого банка.</param>
        /// <returns></returns>
        new Task<BankDto> GetByIdAsync(int id);

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
