using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb.Storage
{
    public class BankRatesContext : DbContext
    {
        public BankRatesContext(DbContextOptions<BankRatesContext> options)
            : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<ParsingSettings> ParsingSettings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRateList> ExchangeRateLists { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<TextCodeScriptAssignment> TextCodeScriptAssignments { get; set; }
        public DbSet<TextCodeScriptParameter> TextCodeScriptParameters { get; set; }
        public DbSet<UnitScriptAssignment> UnitScriptAssignments { get; set; }
        public DbSet<UnitScriptParameter> UnitScriptParameters { get; set; }
        public DbSet<Script> Scripts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Запретить удаление строки валюты, если есть связанные записи.
            modelBuilder.Entity<Currency>()
                .HasMany(i => i.Banks)
                .WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Currency>()
                .HasMany(i => i.ExchangeRates)
                .WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);

            // Установить обязательные свойства.
            modelBuilder.Entity<Bank>().Property(i => i.BankName).IsRequired();
            modelBuilder.Entity<Bank>().Property(i => i.RatesUrl).IsRequired();
            modelBuilder.Entity<Script>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<UnitScriptParameter>().Property(i => i.Value).IsRequired();
            modelBuilder.Entity<TextCodeScriptParameter>().Property(i => i.Value).IsRequired();
            modelBuilder.Entity<ExchangeRateList>().Property(i => i.DateTimeStamp).IsRequired();
            modelBuilder.Entity<ExchangeRate>().Property(i => i.ExchangeRateValue).IsRequired();
            modelBuilder.Entity<Currency>().Property(i => i.TextCode).IsRequired();
        }
    }
}
