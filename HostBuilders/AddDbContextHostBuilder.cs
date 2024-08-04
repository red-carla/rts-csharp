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
            string connectionString = context.Configuration.GetConnectionString("SqlServer");
            services.AddDbContext<RecruitmentDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        });

        return hostBuilder;
    }
}