using System.Text;
using System.Text.Json.Serialization;

namespace PoeExporter.WikiExporters;

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
