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
        public BankRatesContext (DbContextOptions<BankRatesContext> options)
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
            // Установить ключ для реализации связи 1:1.
            modelBuilder.Entity<ParsingSettings>().HasKey(k => k.BankId);

            // Отключить каскадное удаление для исключения циклических каскадных путей.
            modelBuilder.Entity<Currency>()
                .HasMany(i => i.Banks)
                .WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Currency>()
                .HasMany(i => i.ExchangeRates)
                .WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }


    }
}
