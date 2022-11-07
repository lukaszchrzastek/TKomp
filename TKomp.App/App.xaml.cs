using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Windows;
using TKomp.DataAccessLayer;
using TKomp.App.ViewModels;
using TKomp.Domain;
using GalaSoft.MvvmLight.Messaging;

namespace TKomp.App
{    
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
          
            _host = Host.CreateDefaultBuilder()               
               .ConfigureServices((context, services) =>
               {                   
                   services.AddSingleton<ISqlConnectionProvider>(provider => new SqlConnectionProvider(connectionString));
                   services.AddSingleton<IColumnInfoRepository, ColumnInfoRepository>();
                   services.AddSingleton<IMessenger, Messenger>();
                   services.AddSingleton<ConnectionViewModel>();
                   services.AddSingleton<ColumnsViewModel>();
                   services.AddSingleton<MainWindowViewModel>();
                   services.AddSingleton((services) => new MainWindow()
                   {
                       DataContext = services.GetRequiredService<MainWindowViewModel>()
                   });
               })
               .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}