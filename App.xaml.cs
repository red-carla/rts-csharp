using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.EntityFramework;
using RTS.HostBuilders;
using RTS.ViewModels;
using RTS.Views;

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
                .AddDatabaseSeeder()
                .AddConfiguration()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
               
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

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}