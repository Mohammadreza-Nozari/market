
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Database.Data;
using SearchService.Database.Document;
using MassTransit;
using SearchService.Consumers;

namespace SearchService
{
    public class Program
    {
        public static async   Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddMassTransit(x =>
            {

                x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search",false));

                x.UsingRabbitMq((context, cfg) =>
                {

                    cfg.ConfigureEndpoints(context);

                });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                await DatabaseInitializer.init(app);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            app.Run();
        }
    }
}
