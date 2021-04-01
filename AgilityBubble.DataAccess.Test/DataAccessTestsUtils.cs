using System;
using System.Data.SqlClient;
using AgilityBubble.Logic;
using AgilityBubble.UI;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace AgilityBubble.DataAccess.Test
{
    public class DataAccessTestsUtils : IDisposable
    {
        public string ConnectionString { get; }

        public DataAccessTestsUtils()
        {
            ConnectionString = @"Server=az-asus\sql2017;Database=AgilityBubbleTest;Integrated Security=SSPI";
            RecreateDatabase();
            RunMigrations();
            CreateTestData();
            SessionFactory.Init(ConnectionString);
        }
        public void Dispose()
        {
            SessionFactory.Close();
        }

        internal void RecreateDatabase()
        {
            RemoveDatabase();
            DatabaseStartup.EnsureDatabaseExists(ConnectionString);
        }

        internal void CreateTestData()
        {
            ExecuteNonQuery(String.Empty,@"insert into dbo.MultiDictionary values(1, 'First', 'First dictionary', 0)");
        }

        private void ExecuteNonQuery(string databaseName,string commandText)
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            if(!String.IsNullOrEmpty(databaseName))
                builder.InitialCatalog = databaseName;
            using (var connection = new SqlConnection(builder.ToString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.ExecuteNonQuery();
            }
        }

        internal void RemoveDatabase()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            var originalDatabaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            ExecuteNonQuery("master",$"if exists(select * from sys.databases where name = '{originalDatabaseName}') ALTER DATABASE [{originalDatabaseName}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE");
            ExecuteNonQuery("master",$"DROP DATABASE IF EXISTS [{originalDatabaseName}]");
        }

        internal void RunMigrations()
        {
            var services = new ServiceCollection();
            services.AddAgilityBubbleDatabaseSupport(ConnectionString);
            var runner = services.BuildServiceProvider().GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}