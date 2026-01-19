using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Services;
using ECommerce.DataAccess.Repositories;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.WebApi.Services;
using ECommerce.DataAccess.Context;

namespace ECommerce.WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Dapper
            services.AddScoped<DapperContext>();
            // Servisler
            services.AddScoped<TokenService>();
            services.AddScoped<IStokService, StokService>();
            services.AddScoped<ICariService, CariService>();
            services.AddScoped<IFaturaService, FaturaService>();
            // Repositoryler
            services.AddScoped<IStokRepository, StokRepository>();
            services.AddScoped<ICariRepository, CariRepository>();
            services.AddScoped<IFaturaRepository, FaturaRepository>();
            services.AddScoped<IFaturaSatirRepository, FaturaSatirRepository>();
            return services;
        }
    }
}
