﻿using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes_items.
/// </summary>
internal sealed class BlightCraftingRecipesItemsExporter : IExporter<BlightCraftingRecipesItemsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_crafting_recipes_items";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingRecipesItemsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public BlightCraftingRecipesItemsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static BlightCraftingRecipesItemsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new BlightCraftingRecipesItemsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var recipes = GetBlightCraftingRecipesItem();

        var results = LuaConverter.ToLuaString(recipes);

        return results;
    }

    private IReadOnlyList<BlightCraftingRecipesItem> GetBlightCraftingRecipesItem()
    {
        logger.Verbose("running {method}", nameof(GetBlightCraftingRecipesItem));
        var results = new List<BlightCraftingRecipesItem>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var recipes = specification.LoadBlightCraftingRecipesRepository();

        foreach (var recipe in recipes.Items)
        {
            var craftingItemsTemp = recipe.GetItemsForBlightCraftingItemsKeys();
            for (var i = 0; i < craftingItemsTemp.Count; i++)
            {
                var ordinal = i + 1;

                var recipeId = recipe.Id;

                var blightCraftingItem = craftingItemsTemp[i].Value;

                var item = blightCraftingItem.GetItemForOil() ?? throw new NullItemException();

                var itemId = item.Id;

                var res = new BlightCraftingRecipesItem()
                {
                    ItemId = itemId,
                    Ordinal = ordinal,
                    RecipeId = recipeId,
                };

                results.Add(res);
            }
        }

        return results;
    }
}
