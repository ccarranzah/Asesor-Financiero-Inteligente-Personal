using Microsoft.EntityFrameworkCore;
using SmartFinanceAI.Blazor.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartFinanceAI.Blazor.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SmartFinanceAIAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SmartFinanceAIAppContext>>()))
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData.json");
            // throw error if the file does not exist
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The file {jsonFilePath} does not exist.");
            }

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var userData = JsonSerializer.Deserialize<AppUser>(File.ReadAllText(jsonFilePath), jsonOptions);
            
            if (userData == null)
            {
                throw new InvalidOperationException($"The file {jsonFilePath} does not contain valid JSON.");
            }

            if (context == null || context.AppUser == null)
            {
                throw new ArgumentNullException("Null SmartFinanceAIAppContext");
            }

            // Look for any movies.
            if (context.AppUser.Any())
            {
                return;   // DB has been seeded
            }

            context.AddRange(
                userData
            );
            context.SaveChanges();
        }
    }
}