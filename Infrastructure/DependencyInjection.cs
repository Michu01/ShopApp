using Application.Common.Interfaces;

using Infrastructure.Data;
using Infrastructure.Data.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        var connectionString = isDevelopment ?
            configuration.GetConnectionString("Default") :
            configuration["ShopAppDbConnectionString"];

        services.AddScoped<IInterceptor, TimestampedEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
