using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeXml;

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
            modelBuilder.Entity<Currency>().HasMany(i => i.Banks).WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Currency>().HasMany(i => i.ExchangeRates).WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);

            // Установить свойства для Bank.
            modelBuilder.Entity<Bank>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Bank>().Property(i => i.RatesUrl).IsRequired();
            modelBuilder.Entity<Bank>().Property(i => i.Name).HasMaxLength(50);
            modelBuilder.Entity<Bank>().Property(i => i.BankUrl).HasMaxLength(2000);
            modelBuilder.Entity<Bank>().Property(i => i.RatesUrl).HasMaxLength(2000);

            // Установить свойства для Script.
            modelBuilder.Entity<Script>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Script>().Property(i => i.Name).HasMaxLength(50);

            // Установить свойства для ParsingSettings.
            modelBuilder.Entity<ParsingSettings>().Property(i => i.TextCodeXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.UnitXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.ExchangeRateXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.VariablePartOfXpath).HasMaxLength(50);

            // Установить свойства для ExchangeRate.
            modelBuilder.Entity<ExchangeRate>().Property(i => i.ExchangeRateValue).IsRequired();

            // Установить свойства для Currency.
            modelBuilder.Entity<Currency>().Property(i => i.TextCode).IsRequired();
            modelBuilder.Entity<Currency>().Property(i => i.Name).HasMaxLength(100);
            modelBuilder.Entity<Currency>().Property(i => i.TextCode);

            // Установить свойства UnitScript.
            modelBuilder.Entity<UnitScriptParameter>().Property(i => i.Value).IsRequired();
            modelBuilder.Entity<UnitScriptParameter>().Property(i => i.Value).HasMaxLength(50);

            // Установить свойства для TextCode.
            modelBuilder.Entity<TextCodeScriptParameter>().Property(i => i.Value).IsRequired();
            modelBuilder.Entity<TextCodeScriptParameter>().Property(i => i.Value).HasMaxLength(50);

            // Заполнить базу данных начальными данными.

            // Заполнить таблицу Currencies.
            var currencySerializer = new CurrencySerializer();
            IEnumerable<CurrencyXmlItem> currenciesXml = currencySerializer.GetCurrenciesFromXml();
            var currencies = new List<Currency>(currenciesXml.Count());
            foreach (var cur in currenciesXml)
            {
                var newCurrency = new Currency();
                if (int.TryParse(cur.CcyNbr, out int numCodeTemp))
                {
                    newCurrency.Id = numCodeTemp;
                    newCurrency.Name = cur.CcyNm;
                    newCurrency.TextCode = cur.Ccy;
                    currencies.Add(newCurrency);
                }                
            }
            modelBuilder.Entity<Currency>().HasData(currencies);


            // Заполнить таблицу Banks.        
            int bankId = 1;
            modelBuilder.Entity<Bank>().HasData(
                new Bank
                {
                    Id = bankId++,
                    Name = "National Bank of Georgia",
                    CurrencyId = 981, // GEL
                    BankUrl = "https://www.nbg.gov.ge",
                    RatesUrl = "https://www.nbg.gov.ge/index.php?m=582&lng=eng"
                },
                new Bank
                {
                    Id = bankId++,
                    Name = "National Bank of Poland",
                    CurrencyId = 985, // PLN
                    BankUrl = "https://www.nbp.pl",
                    RatesUrl = "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html"
                },
                new Bank
                {
                    Id = bankId++,
                    Name = "The Central Bank of the Russian Federation",
                    CurrencyId = 643, // RUB
                    BankUrl = "https://www.cbr.ru",
                    RatesUrl = "https://www.cbr.ru/eng/currency_base/daily/"
                },
                new Bank
                {
                    Id = bankId++,
                    Name = "European Central Bank",
                    CurrencyId = 978, // EUR
                    BankUrl = "https://www.ecb.europa.eu",
                    RatesUrl = "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html"
                }
            );
        }
    }
}
