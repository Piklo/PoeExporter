using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveTreeExpansionSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveTreeExpansionSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveTreeExpansionSkillsDat> Items { get; }

    private Dictionary<int, List<PassiveTreeExpansionSkillsDat>>? byPassiveSkillsKey;
    private Dictionary<int, List<PassiveTreeExpansionSkillsDat>>? byMastery_PassiveSkillsKey;
    private Dictionary<int, List<PassiveTreeExpansionSkillsDat>>? byTagsKey;
    private Dictionary<int, List<PassiveTreeExpansionSkillsDat>>? byPassiveTreeExpansionJewelSizesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveTreeExpansionSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveTreeExpansionSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillsKey(int? key, out PassiveTreeExpansionSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillsKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillsKey(int? key, out IReadOnlyList<PassiveTreeExpansionSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        if (byPassiveSkillsKey is null)
        {
            byPassiveSkillsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPassiveSkillsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPassiveSkillsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveSkillsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.byPassiveSkillsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSkillsDat>> GetManyToManyByPassiveSkillsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.Mastery_PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMastery_PassiveSkillsKey(int? key, out PassiveTreeExpansionSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMastery_PassiveSkillsKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.Mastery_PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMastery_PassiveSkillsKey(int? key, out IReadOnlyList<PassiveTreeExpansionSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        if (byMastery_PassiveSkillsKey is null)
        {
            byMastery_PassiveSkillsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mastery_PassiveSkillsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMastery_PassiveSkillsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMastery_PassiveSkillsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMastery_PassiveSkillsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.byMastery_PassiveSkillsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSkillsDat>> GetManyToManyByMastery_PassiveSkillsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMastery_PassiveSkillsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKey(int? key, out PassiveTreeExpansionSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKey(int? key, out IReadOnlyList<PassiveTreeExpansionSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        if (byTagsKey is null)
        {
            byTagsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTagsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTagsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTagsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.byTagsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSkillsDat>> GetManyToManyByTagsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.PassiveTreeExpansionJewelSizesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveTreeExpansionJewelSizesKey(int? key, out PassiveTreeExpansionSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveTreeExpansionJewelSizesKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.PassiveTreeExpansionJewelSizesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveTreeExpansionJewelSizesKey(int? key, out IReadOnlyList<PassiveTreeExpansionSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        if (byPassiveTreeExpansionJewelSizesKey is null)
        {
            byPassiveTreeExpansionJewelSizesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveTreeExpansionJewelSizesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPassiveTreeExpansionJewelSizesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPassiveTreeExpansionJewelSizesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveTreeExpansionJewelSizesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSkillsDat"/> with <see cref="PassiveTreeExpansionSkillsDat.byPassiveTreeExpansionJewelSizesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSkillsDat>> GetManyToManyByPassiveTreeExpansionJewelSizesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveTreeExpansionJewelSizesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveTreeExpansionSkillsDat[] Load()
    {
        const string filePath = "Data/PassiveTreeExpansionSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Mastery_PassiveSkillsKey
            (var mastery_passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PassiveTreeExpansionJewelSizesKey
            (var passivetreeexpansionjewelsizeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionSkillsDat()
            {
                PassiveSkillsKey = passiveskillskeyLoading,
                Mastery_PassiveSkillsKey = mastery_passiveskillskeyLoading,
                TagsKey = tagskeyLoading,
                PassiveTreeExpansionJewelSizesKey = passivetreeexpansionjewelsizeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
