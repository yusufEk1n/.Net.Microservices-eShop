using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = buildConnectionString(configuration);
            
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSetting>(c =>
            {
                configuration.GetSection("EmailSetting").Bind(c);
                c.ApiKey = c.ApiKey ?? configuration.GetValue<string>("API_KEY");
            });

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

        private static string buildConnectionString(IConfiguration configuration)
        {
            var dbHost = configuration.GetValue<string>("DB_HOST");
            var dbName = configuration.GetValue<string>("DB_NAME");
            var dbUserId = configuration.GetValue<string>("DB_USER_ID");
            var dbPassword = configuration.GetValue<string>("DB_PASSWORD");

            return $"Server={dbHost};Database={dbName};User Id={dbUserId};Password={dbPassword}; TrustServerCertificate=true";
        }
    }
}
