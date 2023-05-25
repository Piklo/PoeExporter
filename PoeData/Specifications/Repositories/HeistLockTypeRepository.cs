using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistLockTypeDat"/> related data and helper methods.
/// </summary>
public sealed class HeistLockTypeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistLockTypeDat> Items { get; }

    private Dictionary<string, List<HeistLockTypeDat>>? byId;
    private Dictionary<int, List<HeistLockTypeDat>>? byHeistJobsKey;
    private Dictionary<string, List<HeistLockTypeDat>>? bySkillIcon;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistLockTypeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistLockTypeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HeistLockTypeDat? item)
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
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HeistLockTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistLockTypeDat>();
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
            items = Array.Empty<HeistLockTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistLockTypeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistLockTypeDat>>();
        }

        var items = new List<ResultItem<string, HeistLockTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistLockTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKey(int? key, out HeistLockTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKey(int? key, out IReadOnlyList<HeistLockTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistLockTypeDat>();
            return false;
        }

        if (byHeistJobsKey is null)
        {
            byHeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistLockTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.byHeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistLockTypeDat>> GetManyToManyByHeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistLockTypeDat>>();
        }

        var items = new List<ResultItem<int, HeistLockTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistLockTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.SkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillIcon(string? key, out HeistLockTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillIcon(key, out var items))
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
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.SkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillIcon(string? key, out IReadOnlyList<HeistLockTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistLockTypeDat>();
            return false;
        }

        if (bySkillIcon is null)
        {
            bySkillIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillIcon;

                if (!bySkillIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistLockTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistLockTypeDat"/> with <see cref="HeistLockTypeDat.bySkillIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistLockTypeDat>> GetManyToManyBySkillIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistLockTypeDat>>();
        }

        var items = new List<ResultItem<string, HeistLockTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistLockTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistLockTypeDat[] Load()
    {
        const string filePath = "Data/HeistLockType.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistLockTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SkillIcon
            (var skilliconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistLockTypeDat()
            {
                Id = idLoading,
                HeistJobsKey = heistjobskeyLoading,
                SkillIcon = skilliconLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
