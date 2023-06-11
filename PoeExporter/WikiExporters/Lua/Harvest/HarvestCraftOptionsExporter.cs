using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Harvest;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Harvest/harvest_craft_options.
/// </summary>
internal sealed class HarvestCraftOptionsExporter : IExporter<HarvestCraftOptionsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "harvest_craft_options";

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestCraftOptionsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public HarvestCraftOptionsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static HarvestCraftOptionsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new HarvestCraftOptionsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetHarvestCraftOptions();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<HarvestCraftOption> GetHarvestCraftOptions()
    {
        logger.Verbose("running {method}", nameof(GetHarvestCraftOptions));
        var results = new List<HarvestCraftOption>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var craftOptions = specification.LoadHarvestCraftOptionsRepository();

        foreach (var craft in craftOptions.Items)
        {
            var harvestCraftTierKey = craft.Unknown16 ?? throw new NullItemException();

            var obj = new HarvestCraftOption()
            {
                Id = craft.Id,
                Text = ParseText(craft.Text),
                Tier = harvestCraftTierKey,
                Effect = craft.Description,
                IsEnchant = craft.Unknown76,
                CostLifeforceType = craft.LifeforceType,
                CostLifeforce = craft.LifeforceCost,
                CostSacred = craft.SacredCost,
                Command = craft.Command,
                Parameters = craft.Parameters.Length > 0 ? craft.Parameters.Split() : null,
            };

            results.Add(obj);
        }

        return results;
    }

    private static string ParseText(string text)
    {
        text = text.Replace("{", string.Empty).Replace("}", "}}");

        var lessThanIndex = text.IndexOf('<');
        while (lessThanIndex != -1)
        {
            var moreThanIndex = text.IndexOf('>');
            var tag = text[(lessThanIndex + 1)..moreThanIndex];
            var newTag = GetNewTag(tag);
            text = text.Replace($"<{tag}>", $"{{{{c|{newTag}|");
            lessThanIndex = text.IndexOf('<', lessThanIndex);
        }

        return text;
    }

    private static string GetNewTag(string tag) => tag switch
    {
        "white" => "white",
        "craftingred" => "red",
        "craftingblue" => "blue",
        "craftinggreen" => "green",
        "craftingcaster" => "purple",
        "craftingphysical" => "tan",
        "craftingfire" => "orange",
        "craftinglightning" => "yellow",
        "craftingcold" => "blue",
        "craftingchaos" => "purple",
        "unique" => "orange",
        "magic" => "blue",
        "rare" => "yellow",
        "craftingspeed" => "green",
        "craftingattack" => "white",
        "craftinglife" => "red",
        "craftingcrit" => "blue",
        "craftingdefences" => "white",
        "enchanted" => "white",
        "fuchsia" => "magenta",
        "yellow" => "yellow",
        "aqua" => "cyan",
        _ => throw new NotImplementedException($"unknown tag = {tag}"),
    };
}
