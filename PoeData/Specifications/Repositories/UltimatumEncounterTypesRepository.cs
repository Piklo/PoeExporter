using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UltimatumEncounterTypesDat"/> related data and helper methods.
/// </summary>
public sealed class UltimatumEncounterTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UltimatumEncounterTypesDat> Items { get; }

    private Dictionary<string, List<UltimatumEncounterTypesDat>>? byId;
    private Dictionary<string, List<UltimatumEncounterTypesDat>>? byName;
    private Dictionary<string, List<UltimatumEncounterTypesDat>>? byProgressBarText;
    private Dictionary<bool, List<UltimatumEncounterTypesDat>>? byUnknown24;
    private Dictionary<bool, List<UltimatumEncounterTypesDat>>? byUnknown25;
    private Dictionary<int, List<UltimatumEncounterTypesDat>>? byNormalAchievements;
    private Dictionary<int, List<UltimatumEncounterTypesDat>>? byInscribedAchievement;

    /// <summary>
    /// Initializes a new instance of the <see cref="UltimatumEncounterTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UltimatumEncounterTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UltimatumEncounterTypesDat? item)
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
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
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumEncounterTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<string, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumEncounterTypesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<string, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.ProgressBarText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgressBarText(string? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgressBarText(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.ProgressBarText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgressBarText(string? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byProgressBarText is null)
        {
            byProgressBarText = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProgressBarText;

                if (!byProgressBarText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProgressBarText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProgressBarText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byProgressBarText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumEncounterTypesDat>> GetManyToManyByProgressBarText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<string, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgressBarText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UltimatumEncounterTypesDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<bool, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(bool? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(bool? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UltimatumEncounterTypesDat>> GetManyToManyByUnknown25(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<bool, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.NormalAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNormalAchievements(int? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNormalAchievements(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.NormalAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNormalAchievements(int? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byNormalAchievements is null)
        {
            byNormalAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.NormalAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byNormalAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNormalAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNormalAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byNormalAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumEncounterTypesDat>> GetManyToManyByNormalAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<int, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNormalAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.InscribedAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInscribedAchievement(int? key, out UltimatumEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInscribedAchievement(key, out var items))
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
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.InscribedAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInscribedAchievement(int? key, out IReadOnlyList<UltimatumEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        if (byInscribedAchievement is null)
        {
            byInscribedAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.InscribedAchievement;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInscribedAchievement.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInscribedAchievement.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInscribedAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumEncounterTypesDat"/> with <see cref="UltimatumEncounterTypesDat.byInscribedAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumEncounterTypesDat>> GetManyToManyByInscribedAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumEncounterTypesDat>>();
        }

        var items = new List<ResultItem<int, UltimatumEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInscribedAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UltimatumEncounterTypesDat[] Load()
    {
        const string filePath = "Data/UltimatumEncounterTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumEncounterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProgressBarText
            (var progressbartextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NormalAchievements
            (var tempnormalachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var normalachievementsLoading = tempnormalachievementsLoading.AsReadOnly();

            // loading InscribedAchievement
            (var inscribedachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumEncounterTypesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                ProgressBarText = progressbartextLoading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                NormalAchievements = normalachievementsLoading,
                InscribedAchievement = inscribedachievementLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
