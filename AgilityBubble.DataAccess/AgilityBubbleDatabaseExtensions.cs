using AgilityBubble.Logic;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace AgilityBubble.UI
{
    public static class AgilityBubbleDatabaseExtensions
    {
        public static void AddAgilityBubbleDatabaseSupport(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2008()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AgilityBubbleDatabaseExtensions).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }
    }
}