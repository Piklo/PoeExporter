using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PantheonSoulsDat"/> related data and helper methods.
/// </summary>
public sealed class PantheonSoulsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PantheonSoulsDat> Items { get; }

    private Dictionary<int, List<PantheonSoulsDat>>? byWorldArea;
    private Dictionary<int, List<PantheonSoulsDat>>? byCapturedVessel;
    private Dictionary<int, List<PantheonSoulsDat>>? byQuestFlag;
    private Dictionary<int, List<PantheonSoulsDat>>? byCapturedMonster;
    private Dictionary<int, List<PantheonSoulsDat>>? byPanelLayout;

    /// <summary>
    /// Initializes a new instance of the <see cref="PantheonSoulsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PantheonSoulsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldArea(int? key, out PantheonSoulsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldArea(key, out var items))
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
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldArea(int? key, out IReadOnlyList<PantheonSoulsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        if (byWorldArea is null)
        {
            byWorldArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldArea;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.byWorldArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonSoulsDat>> GetManyToManyByWorldArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonSoulsDat>>();
        }

        var items = new List<ResultItem<int, PantheonSoulsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonSoulsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.CapturedVessel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCapturedVessel(int? key, out PantheonSoulsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCapturedVessel(key, out var items))
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
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.CapturedVessel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCapturedVessel(int? key, out IReadOnlyList<PantheonSoulsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        if (byCapturedVessel is null)
        {
            byCapturedVessel = new();
            foreach (var item in Items)
            {
                var itemKey = item.CapturedVessel;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCapturedVessel.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCapturedVessel.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCapturedVessel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.byCapturedVessel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonSoulsDat>> GetManyToManyByCapturedVessel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonSoulsDat>>();
        }

        var items = new List<ResultItem<int, PantheonSoulsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCapturedVessel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonSoulsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out PantheonSoulsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestFlag(key, out var items))
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
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<PantheonSoulsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        if (byQuestFlag is null)
        {
            byQuestFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestFlag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestFlag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestFlag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonSoulsDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonSoulsDat>>();
        }

        var items = new List<ResultItem<int, PantheonSoulsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonSoulsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.CapturedMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCapturedMonster(int? key, out PantheonSoulsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCapturedMonster(key, out var items))
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
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.CapturedMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCapturedMonster(int? key, out IReadOnlyList<PantheonSoulsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        if (byCapturedMonster is null)
        {
            byCapturedMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.CapturedMonster;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCapturedMonster.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCapturedMonster.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCapturedMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.byCapturedMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonSoulsDat>> GetManyToManyByCapturedMonster(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonSoulsDat>>();
        }

        var items = new List<ResultItem<int, PantheonSoulsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCapturedMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonSoulsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.PanelLayout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPanelLayout(int? key, out PantheonSoulsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPanelLayout(key, out var items))
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
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.PanelLayout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPanelLayout(int? key, out IReadOnlyList<PantheonSoulsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        if (byPanelLayout is null)
        {
            byPanelLayout = new();
            foreach (var item in Items)
            {
                var itemKey = item.PanelLayout;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPanelLayout.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPanelLayout.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPanelLayout.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonSoulsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonSoulsDat"/> with <see cref="PantheonSoulsDat.byPanelLayout"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonSoulsDat>> GetManyToManyByPanelLayout(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonSoulsDat>>();
        }

        var items = new List<ResultItem<int, PantheonSoulsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPanelLayout(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonSoulsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PantheonSoulsDat[] Load()
    {
        const string filePath = "Data/PantheonSouls.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PantheonSoulsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CapturedVessel
            (var capturedvesselLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CapturedMonster
            (var capturedmonsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PanelLayout
            (var panellayoutLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PantheonSoulsDat()
            {
                WorldArea = worldareaLoading,
                CapturedVessel = capturedvesselLoading,
                QuestFlag = questflagLoading,
                CapturedMonster = capturedmonsterLoading,
                PanelLayout = panellayoutLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
