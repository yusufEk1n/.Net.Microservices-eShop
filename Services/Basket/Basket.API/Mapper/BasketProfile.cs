using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events;

namespace Basket.API.Mapper
{
    ///<summary>
    /// The basket profile used to map the basket checkout object to the basket checkout event object
    /// </summary>
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
