using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.DataSeeding;

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
        public DbSet<CommandFieldName> AssignmentFieldNames { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandParameter> CommandParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Установить свойства для Bank.
            modelBuilder.Entity<Bank>().Property(i => i.SwiftCode).IsRequired();
            modelBuilder.Entity<Bank>().Property(i => i.SwiftCode).HasMaxLength(11).IsFixedLength();
            modelBuilder.Entity<Bank>().HasIndex(i => i.SwiftCode).IsUnique();
            modelBuilder.Entity<Bank>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Bank>().Property(i => i.Name).HasMaxLength(50);
            modelBuilder.Entity<Bank>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Bank>().Property(i => i.BankUrl).HasMaxLength(2000);

            // Установить свойства для ParsingSettings.
            modelBuilder.Entity<ParsingSettings>().Property(i => i.RatesUrl).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.RatesUrl).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.TextCodeXpath).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.UnitXpath).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.ExchangeRateXpath).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.VariablePartOfXpath).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.NumberDecimalSeparator).IsRequired();
            modelBuilder.Entity<ParsingSettings>().Property(i => i.TextCodeXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.UnitXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.ExchangeRateXpath).HasMaxLength(2000);
            modelBuilder.Entity<ParsingSettings>().Property(i => i.VariablePartOfXpath).HasMaxLength(50);

            // Установить свойства для ExchangeRate.
            modelBuilder.Entity<ExchangeRate>().Property(i => i.ExchangeRateValue).IsRequired();
            modelBuilder.Entity<ExchangeRate>().Property(i => i.ExchangeRateValue).HasColumnType("decimal(18, 4)");

            // Установить свойства для Currency.
            modelBuilder.Entity<Currency>().Property(i => i.TextCode).IsRequired();
            modelBuilder.Entity<Currency>().Property(i => i.TextCode).HasMaxLength(3).IsFixedLength();
            modelBuilder.Entity<Currency>().HasIndex(i => i.TextCode).IsUnique();
            modelBuilder.Entity<Currency>().Property(i => i.Name).HasMaxLength(100);
            // Запретить удаление строки валюты, если есть связанные записи.
            modelBuilder.Entity<Currency>().HasMany(i => i.Banks).WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Currency>().HasMany(i => i.ExchangeRates).WithOne(i => i.Currency)
                .OnDelete(DeleteBehavior.Restrict);

            // Установить свойства для Command.
            modelBuilder.Entity<Command>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Command>().Property(i => i.Name).HasMaxLength(50);            
            modelBuilder.Entity<Command>().Property(i => i.Description).HasMaxLength(200);

            // Установить свойства для CommandParemeter.
            modelBuilder.Entity<CommandParameter>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<CommandParameter>().Property(i => i.Name).HasMaxLength(50);
            modelBuilder.Entity<CommandParameter>().Property(i => i.Description).HasMaxLength(200);
            modelBuilder.Entity<CommandParameter>().Property(i => i.Value).HasMaxLength(50);

            // Установить свойства для CommandFieldName.
            modelBuilder.Entity<CommandFieldName>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<CommandFieldName>().Property(i => i.Name).HasMaxLength(20);

            // Заполнить базу данных начальными данными.
            var seedData = new SeedBase(modelBuilder);
            seedData.SeedAll();
        }
    }
}
