using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(ICustomerRepository), typeof(CustomerRepository));
        }
    }
}
