using AuctionService.Contract;
using AuctionService.Database.Entities;
using AutoMapper;

namespace AuctionService.Utilities
{
    public class AuctionMapper : Profile
    {
        public AuctionMapper()
        {
            CreateMap<AuctionEntity, AuctionContract>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
            .PreserveReferences();

            CreateMap<ItemEntity, ItemContract>()
                .ForMember(dest => dest.Auction, opt => opt.MapFrom(src => src.Auction))
                .PreserveReferences();

        }
    }
}
