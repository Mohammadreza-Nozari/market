using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Database.Document;

namespace SearchService.Consumers
{
    // Consumer for Rabbitmq
    public class AuctionCreatedConsumer : IConsumer<AuctionCreatedContract>
    {
        private readonly IMapper _mapper;

        public AuctionCreatedConsumer(IMapper mapper)
        {
            this._mapper = mapper;
        }

        // get data 2
        public async Task Consume(ConsumeContext<AuctionCreatedContract> context)
        {
            Console.WriteLine("------> Consuming auction created: " + context.Message.Id);

            var item = _mapper.Map<ItemDocument>(context.Message);


            await item.SaveAsync();
        }
    }
}
