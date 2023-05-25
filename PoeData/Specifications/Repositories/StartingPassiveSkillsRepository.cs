using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StartingPassiveSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class StartingPassiveSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StartingPassiveSkillsDat> Items { get; }

    private Dictionary<string, List<StartingPassiveSkillsDat>>? byId;
    private Dictionary<int, List<StartingPassiveSkillsDat>>? byPassiveSkills;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartingPassiveSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StartingPassiveSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out StartingPassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<StartingPassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StartingPassiveSkillsDat>();
            return false;
        }

        if (byId is null)
        {
            byId = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id;

                if (!byId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<StartingPassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StartingPassiveSkillsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StartingPassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, StartingPassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StartingPassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.PassiveSkills"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkills(int? key, out StartingPassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkills(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.PassiveSkills"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkills(int? key, out IReadOnlyList<StartingPassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StartingPassiveSkillsDat>();
            return false;
        }

        if (byPassiveSkills is null)
        {
            byPassiveSkills = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkills;
                foreach (var listKey in itemKey)
                {
                    if (!byPassiveSkills.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPassiveSkills.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPassiveSkills.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StartingPassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StartingPassiveSkillsDat"/> with <see cref="StartingPassiveSkillsDat.byPassiveSkills"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StartingPassiveSkillsDat>> GetManyToManyByPassiveSkills(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StartingPassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, StartingPassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkills(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StartingPassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StartingPassiveSkillsDat[] Load()
    {
        const string filePath = "Data/StartingPassiveSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StartingPassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveSkills
            (var temppassiveskillsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillsLoading = temppassiveskillsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StartingPassiveSkillsDat()
            {
                Id = idLoading,
                PassiveSkills = passiveskillsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
