using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    /// <summary>
    /// Discount profile used for mapping between Coupon and CouponModel
    /// </summary>
    public class DiscountProfile : Profile
    {
        public DiscountProfile() 
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
