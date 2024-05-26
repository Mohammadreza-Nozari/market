using AutoMapper;
using Contracts;
using SearchService.Database.Document;

namespace SearchService.Database.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AuctionCreatedContract, ItemDocument>();
        }
    }
}
