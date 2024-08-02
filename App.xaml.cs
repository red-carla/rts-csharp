using RecruitmentApp.Views;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DefaultNamespace;
using RecruitmentApp.Data;
using RecruitmentApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            if (loginWindow.ShowDialog() == true)
            {
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(
                AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("D:\\Documents\\RecruitmentApp\\appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("RecruitmentContext");

            services.AddDbContext<RecruitmentContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();
        }
    }
}
