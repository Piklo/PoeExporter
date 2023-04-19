using Microsoft.Extensions.Configuration;
using PoeData;
using PoeData.Specifications;
using Serilog;
using Serilog.Core;

namespace PoeExporter;

internal sealed class Program
{
    static async Task Main()
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false);

        IConfiguration config = builder.Build();

        var parsedConfig = config.Get<Config>();
        if (parsedConfig is null) throw new ArgumentNullException(nameof(parsedConfig));

        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = Serilog.Events.LogEventLevel.Verbose
        };
        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .WriteTo.Console()
            .CreateLogger();

        //using var client = new WikiClient("https://www.poewiki.net/w/api.php", logger);
        //await client.GetPageContent("Module:Blight/blight crafting recipes").ConfigureAwait(true);

        var specification = new Specification(parsedConfig, logger);

        //var abyss = specification.GetAbyssObjectsDat();


        //var serialized = System.Text.Json.JsonSerializer.Serialize(abyss[^1], new System.Text.Json.JsonSerializerOptions()
        //{
        //    WriteIndented = true,
        //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        //});

        //logger.Verbose(serialized);

        var recipes = specification.LoadBlightCraftingRecipesDat();
        var craftingItems = specification.LoadBlightCraftingItemsDat();
        var baseItems = specification.LoadBaseItemTypesDat();

        var serialized = System.Text.Json.JsonSerializer.Serialize(recipes[0], new System.Text.Json.JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });

        logger.Verbose(serialized);

        var results = new List<BlightCraftingRecipesItems>();
        foreach (var recipe in recipes)
        {
            for (var i = 0; i < recipe.BlightCraftingItemsKeys.Count; i++)
            {
                var ordinal = i + 1;
                var recipeId = recipe.Id;

                var blightCraftingItemKey = recipe.BlightCraftingItemsKeys[i];
                var blightCraftingItem = craftingItems[blightCraftingItemKey];
                var oil = blightCraftingItem.Oil;
                if (oil is null)
                {
                    throw new ArgumentNullException("null oil");
                }
                var itemId = baseItems[(int)oil].Id;

                var res = new BlightCraftingRecipesItems()
                {
                    ItemId = itemId,
                    Ordinal = ordinal,
                    RecipeId = recipeId
                };

                results.Add(res);
            }
        }

        var formatted = BlightCraftingRecipesItems.ToLuaString(results);

        File.WriteAllText("mine_blight_crafting_recipes_items.lua", formatted);
    }
}

public class Config : IConfig
{
    public required string PoePath { get; init; }
}
