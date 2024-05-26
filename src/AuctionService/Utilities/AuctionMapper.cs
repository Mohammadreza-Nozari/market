using AuctionService.Contract;
using AuctionService.Contract.Request;
using AuctionService.Database.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.Utilities
{
    public class AuctionMapper : Profile
    {
        public AuctionMapper()
        {
            CreateMap<AuctionEntity, AuctionContract>().IncludeMembers(x => x.Item);
            CreateMap<ItemEntity, AuctionContract>();
            CreateMap<CreateAuctionRequestContract, AuctionEntity>().ForMember(d => d.Item, o => o.MapFrom(s => s));
            CreateMap<CreateAuctionRequestContract, ItemEntity>();
            CreateMap<AuctionContract, AuctionCreatedContract>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
              .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.ToString()));
        }
    }
}
