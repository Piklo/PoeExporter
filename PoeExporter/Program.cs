using Microsoft.Extensions.Configuration;
using PoeData;
using PoeData.Specifications;
using Serilog;
using Serilog.Core;
using System.Text;
using System.Text.Json.Serialization;

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

internal sealed class BlightCraftingRecipesItems
{
    [JsonPropertyName("ordinal")]
    public required int Ordinal { get; init; }
    [JsonPropertyName("recipe_id")]
    public required string RecipeId { get; init; }
    [JsonPropertyName("item_id")]
    public required string ItemId { get; init; }

    public string[] ToLuaStrings()
    {
        var res = new string[]
        {
            "{",
            $"""ordinal = {Ordinal},""",
            $"""recipe_id = "{RecipeId}",""",
            $"""item_id = "{ItemId}",""",
            "}"
        };

        return res;
    }

    public static string ToLuaString(List<BlightCraftingRecipesItems> items)
    {
        var builder = new StringBuilder();
        builder.AppendLine("local data = {");
        foreach (var item in items)
        {
            var strings = item.ToLuaStrings();

            builder.AppendLine($"\t{strings[0]}");

            for (var i = 1; i < strings.Length - 1; i++)
            {
                var str = strings[i];
                builder.AppendLine($"\t\t{str}");
            }

            builder.AppendLine($"\t{strings[^1]},");

        }
        builder.AppendLine("}");
        builder.AppendLine("return data");

        var result = builder.ToString();
        return result;
    }
}
