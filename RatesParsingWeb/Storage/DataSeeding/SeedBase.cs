using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeJson;
using RatesParsingWeb.Storage.SerializeXml;
using System.Collections.Generic;
using System.Linq;

namespace RatesParsingWeb.Storage.DataSeeding
{
    /// <summary>
    /// Представляет средства для заполнения базы данных начальными данными.
    /// </summary>
    public class SeedBase
    {
        public SeedBase(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        private readonly ModelBuilder ModelBuilder;

        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        public void SeedAll()
        {
            var seedBank = new SeedBank(ModelBuilder);
            var banks = seedBank.SeedAll();

            var seedSettings = new SeedParsingSettings(ModelBuilder, banks);
            seedSettings.SeedAll();
        }

        
    }
}
