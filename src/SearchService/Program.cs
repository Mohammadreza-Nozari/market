
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Database.Document;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();


            app.MapControllers();


            await DB.InitAsync("SearchDb", MongoClientSettings
                .FromConnectionString(builder.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<ItemDocument>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();



            app.Run();
        }
    }
}
