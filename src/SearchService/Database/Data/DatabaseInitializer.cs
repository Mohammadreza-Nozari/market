using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Database.Document;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SearchService.Database.Data
{
    public class DatabaseInitializer
    {
        public static async Task init(WebApplication app)
        {
            await DB.InitAsync(
                "SearchDb",
                MongoClientSettings.FromConnectionString(
                    app.Configuration.GetConnectionString("MongoDbConnection")
                )
            );

            await DB.Index<ItemDocument>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<ItemDocument>();

            if (count == 0)
            {
                Console.WriteLine("No data - will attempt to seed");
                var itemData = await File.ReadAllTextAsync("Database/Data/Seed.json");

                var options  = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var items = JsonSerializer.Deserialize<List<ItemDocument>>(itemData, options);


                await DB.SaveAsync(items);

            }
        }
    }
}
