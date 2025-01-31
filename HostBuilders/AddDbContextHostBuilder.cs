﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.EntityFramework;

namespace RTS.HostBuilders;

public static class AddDbContextHostBuilder
{
    public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("SqlServer");
            Action<DbContextOptionsBuilder> configureDbContext = o =>
            {
                if (connectionString != null) o.UseSqlServer(connectionString);
            };

            services.AddDbContext<RecruitmentDbContext>(configureDbContext);
            services.AddSingleton(new RecruitmentDbContextFactory(configureDbContext));
        });
        return hostBuilder;
    }
}