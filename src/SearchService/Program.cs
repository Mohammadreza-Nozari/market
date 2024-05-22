
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Database.Data;
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
