using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using DotNetEnv;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private enum ConnectionType
        {
            Unencrypted,
            Encrypted
        }

        public IMongoCollection<Product> Products { get; }
        public CatalogContext()
        {
            InitializeEnvironmentVariables();
            var connectionString = BuildConnectionString(ConnectionType.Unencrypted);

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
            Products = database.GetCollection<Product>(Environment.GetEnvironmentVariable("DB_COLLECTION_NAME"));

            CatalogContextSeed.SeedData(Products);
        }

        private void InitializeEnvironmentVariables()
        {
            try
            {
                Env.Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading environment variables: " + ex.Message);
            }
        }

        private string BuildConnectionString(ConnectionType connectionType)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUserId = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME");
            var dbPassword = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");

            if (connectionType == ConnectionType.Unencrypted)
            {
                return $"mongodb://{dbHost}:{dbPort}";
            }
            else if (connectionType == ConnectionType.Encrypted)
            {
                return $"mongodb://{dbUserId}:{dbPassword}@{dbHost}:{dbPort}/{dbName}?authSource=admin";
            }
            else
            {
                throw new ArgumentException("Geçersiz baðlantý türü.", nameof(connectionType));
            }
        }
    }
}