using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb.Data
{
    public class BankRatesContext : DbContext
    {
        public BankRatesContext (DbContextOptions<BankRatesContext> options)
            : base(options)
        {
        }

        public DbSet<RatesParsingWeb.Domain.Bank> Banks { get; set; }
    }
}
