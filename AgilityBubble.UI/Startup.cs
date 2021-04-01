using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner.Initialization;


namespace AgilityBubble.UI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddScoped<MultiDictionaryViewModel>();
            services.AddAgilityBubbleDatabaseSupport(_configuration.GetConnectionString("AgilityBubble"));
        }
    }
}