using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.EntityFramework;
using RTS.HostBuilders;

namespace RTS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;


        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddDbContext()
                .AddConfiguration();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            RecruitmentDbContextFactory contextFactory =
                _host.Services.GetRequiredService<RecruitmentDbContextFactory>();
            using (RecruitmentDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }
        }
           
    }

}
