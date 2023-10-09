using DotNetEnv;
using Npgsql;
using Polly;

namespace Discount.API.Extensions
{
    /// <summary>
    /// Host extension class used to migrate the database
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// Migrate the database
        /// </summary>
        /// <param name="host">Host</param>
        /// <returns>Host</returns>
        public static IHost MigrateDatabase<TContext>(this IHost host) //"this IHost host" this code expands the IHost class
        {
            /*
            --- create service scope for store the migration logs record ---
            --- use 5 times retry policy ---
            --- execute the migration ---
            */
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migration postgresql database");

                    var retry = Policy.Handle<NpgsqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, retryCount, context) =>
                            {
                                logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                            });

                    retry.Execute(() => ExecuteMigrations());
                }
                catch(NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occured while migrating the postresql database");
                }

                return host;
            }
        }

        /// <summary>
        /// Execute the migration
        /// </summary>
        /// <returns></returns>
        private static void ExecuteMigrations()
        {
            string connectionString = BuildConnectionString();
            
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon(
                                                Id          SERIAL PRIMARY KEY NOT NULL,
                                                ProductName VARCHAR(24) NOT NULL,
                                                Description TEXT NOT NULL,
                                                Amount      INT NOT NULL)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100)";
            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Build the connection string
        /// </summary>
        /// <returns>Connection string</returns>
        private static string BuildConnectionString()
        {
            var dbHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            var dbPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
            {
                return $"Server={dbHost}; Port={dbPort}; Database={dbName}";
            }

            return $"Server={dbHost}; Port={dbPort}; Database={dbName}; User Id={dbUser}; Password={dbPassword}";
        }
    }
}
