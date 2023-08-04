using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using DotNetEnv;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public CatalogContext()
        {
            InitializeEnvironmentVariables();
            var connectionString = BuildConnectionString();

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

        private string BuildConnectionString()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUserId = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME");
            var dbPassword = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");

            if (string.IsNullOrEmpty(dbUserId) || string.IsNullOrEmpty(dbPassword))
            {
                return $"mongodb://{dbHost}:{dbPort}";
            }

            return $"mongodb://{dbUserId}:{dbPassword}@{dbHost}:{dbPort}/{dbName}?authSource=admin";
        }
    }
}