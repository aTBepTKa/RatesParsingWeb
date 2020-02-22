using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RatesParsingWeb.Services;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using MassTransit.AspNetCoreIntegration;
using MassTransit;

namespace RatesParsingWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<BankRatesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BankRatesContext")));

            // Слой репозитория.
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IParsingSettingsRepository, ParsingSettingsRepository>();
            services.AddScoped<IExchangeRateListRepository, ExchangeRateListRepository>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped<ICommandRepository, CommandRepository>();

            // Слой сервиса.
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IParsingSettingsService, ParsingSettingsService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExchangeRateListService, ExchangeRateListService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<ICommandService, CommandService>();

            // Добавить MassTrantis.
            services.AddMassTransit(CreateBus());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private IBusControl CreateBus() =>
            Bus.Factory.CreateUsingRabbitMq(cfg =>
                cfg.Host("rabbitmq://localhost"));
    }
}
