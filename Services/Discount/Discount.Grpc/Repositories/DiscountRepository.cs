using Dapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories.Interfaces;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public async Task<Coupon> GetDiscount(string productName)
        {
            string connectionString = BuildConnectionString();
            using var connection = new NpgsqlConnection(connectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @productName", new { ProductName = productName });

            return coupon == null ? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" }
                                  : coupon;       
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            string connectionString = BuildConnectionString();
            using var connection = new NpgsqlConnection(connectionString);

            var couponAffected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return couponAffected == 1 ? true : false;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            string connectionString = BuildConnectionString();
            using var connection = new NpgsqlConnection(connectionString);

            var couponUpdated = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            return couponUpdated == 1 ? true : false;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            string connectionString = BuildConnectionString();
            using var connection = new NpgsqlConnection(connectionString);

            var couponDeleted = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return couponDeleted == 1 ? true : false;
        }

        private string BuildConnectionString()
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
