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
using System;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using ParsingMessages.Parsing;
using ParsingMessages.Command;

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
            services.AddScoped<IParsingSettingsRepository, ParsingSettingsRepository>();

            // Слой сервиса.
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExchangeRateListService, ExchangeRateListService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IParsingService, ParsingService>();

            // Добавить MassTrantis.            
            services.AddMassTransit(x =>
                {
                    x.AddBus(provider =>
                        Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host(Configuration["RabbitMqSettings:RabbitMqHost"]);
                        }));
                    AddRequestClient<IParsingRequest>(x, new Uri(Configuration["RabbitMqSettings:ParsingServiceAdress"]));
                    AddRequestClient<ICommandRequest>(x, new Uri(Configuration["RabbitMqSettings:CommandServiceAdress"]));
                }
            );
            services.AddMassTransitHostedService();
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

        // Добавить клиент MassTransit для обработки ответа на запрос.
        private void AddRequestClient<T>(IServiceCollectionConfigurator configurator, Uri serviceAdress) where T : class =>
            configurator.AddRequestClient<T>(serviceAdress, RequestTimeout.After(s: 10));
    }
}
