using Discount.Grpc.Protos;
using Discount.Grpc.Repositories.Interfaces;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IDiscountRepository _repository;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository repository)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }

            _logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Amount = coupon.Amount,
                Description = coupon.Description,
            };
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponAffected = await _repository.CreateDiscount(new Entities.Coupon
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
            });

            if(!couponAffected)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Discount could not be created."));
            }

            _logger.LogInformation("Discount is succesfully created. ProductName: {productName}", request.Coupon.ProductName);

            return request.Coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponUpdated = await _repository.UpdateDiscount(new Entities.Coupon
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
            });

            if(!couponUpdated)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Discount could not be updated"));
            }

            _logger.LogInformation("Discount is succesfully updated. ProductName: {productName}", request.Coupon.ProductName);

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var couponDeleted = await _repository.DeleteDiscount(request.ProductName);

            var response = new DeleteDiscountResponse
            {
                Success = couponDeleted
            };

            return response;
        }
    }
}
