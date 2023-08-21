using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories.Interfaces;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository repository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }

            _logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponAffected = await _repository.CreateDiscount(_mapper.Map<Coupon>(request.Coupon));

            if(!couponAffected)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Discount could not be created."));
            }

            _logger.LogInformation("Discount is succesfully created. ProductName: {productName}", request.Coupon.ProductName);

            return request.Coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponUpdated = await _repository.UpdateDiscount(_mapper.Map<Coupon>(request.Coupon));

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
