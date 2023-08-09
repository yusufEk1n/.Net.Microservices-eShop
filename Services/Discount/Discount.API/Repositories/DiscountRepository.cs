using Dapper;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Npgsql;
using System.Diagnostics;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(getConnectionString());

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @productName", new { ProductName = productName });

            return coupon == null ? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" }
                                  : coupon;       
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(getConnectionString());

            var couponAffected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return couponAffected == 1 ? true : false;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(getConnectionString());

            var couponUpdated = await connection.ExecuteAsync
                ("UPDATE FROM Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            return couponUpdated == 1 ? true : false;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(getConnectionString());

            var couponDeleted = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return couponDeleted == 1 ? true : false;
        }

        private string getConnectionString()
        {
            return $"Server={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                   $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                   $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                   $"User Id={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                   $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}";
        }
    }
}
