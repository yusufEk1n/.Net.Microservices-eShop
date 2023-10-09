using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.API.Extensions
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
        /// <param name="seeder">Seeder function</param>
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
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
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    var retry = Policy.Handle<SqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}");

                                });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);

                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }
            return host;
        }

        /// <summary>
        /// Invoke the seeder function
        /// </summary>
        /// <param name="seeder">Seeder function</param>
        /// <param name="context">Context</param>
        /// <param name="services">Services</param>
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
