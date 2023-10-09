using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        /// <summary>
        /// Gets the discount for a given product name
        /// </summary>
        /// <param name="productName">user name argument</param>
        /// <returns>The <see cref="CouponModel"/>.</returns>
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
