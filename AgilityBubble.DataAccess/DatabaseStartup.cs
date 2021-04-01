using System;
using System.Configuration;
using System.Data.SqlClient;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgilityBubble.UI
{
    public static class DatabaseStartup
    {
        public static void EnsureDatabaseExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var originalDatabaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            using (SqlConnection connection = new SqlConnection(builder.ToString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from sys.databases where name  = '{originalDatabaseName}'";
                var dbCount = (int) command.ExecuteScalar();
                if (dbCount > 0)
                    return;

                command.CommandText = $"create database {originalDatabaseName}";
                command.ExecuteNonQuery();
            }
        }

        public static void RunMigrations(IServiceProvider serviceProvider, string connectionString)
        {
            var migrationsRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
            DatabaseStartup.EnsureDatabaseExists(connectionString);
            migrationsRunner.MigrateUp();
        }
    }
}