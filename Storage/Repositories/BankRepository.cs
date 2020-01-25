﻿using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {        
        public BankRepository(BankRatesContext context) :
            base(context)
        { }

        /// <summary>
        /// Возвращает список банков с основной валютой банка.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Bank>> GetAll() =>
            await _dbSet.Include(i => i.Currency).ToArrayAsync();

        /// <summary>
        /// Возвращает банк по Id с основной валютой банка.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<Bank> GetByIdAsync(int id) =>
            _dbSet
            .Include(i => i.Currency)
            .FirstOrDefaultAsync(i=>i.Id == id);

        public Task<Bank> GetByIdWithSettings(int id) =>
            _dbSet
            .Include(i => i.Currency)
            .Include(i => i.ParsingSettings)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}