using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace RatesParsingWeb.Storage
{
    /// <summary>
    /// Базовый класс для работы с сущностями БД.
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly BankRatesContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(DbContextOptions<BankRatesContext> options)
        {
            _context = new BankRatesContext(options);
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(T[] entity)
        {
            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where) =>
            await _dbSet.FirstOrDefaultAsync(where);

        public async Task<IEnumerable<T>> GetAll() =>
            await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where) =>
            await _dbSet.Where(where).ToListAsync();

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where) =>
            await _dbSet.AnyAsync(where);

        public async Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
                return await _dbSet.CountAsync();
            else
                return await _dbSet.CountAsync(where);
        }

        public IQueryable<T> Query() =>
            _dbSet.AsQueryable();

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
// Гуглить Generic Repository.