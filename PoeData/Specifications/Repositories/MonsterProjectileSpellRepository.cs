using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterProjectileSpellDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterProjectileSpellRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterProjectileSpellDat> Items { get; }

    private Dictionary<int, List<MonsterProjectileSpellDat>>? byId;
    private Dictionary<int, List<MonsterProjectileSpellDat>>? byProjectile;
    private Dictionary<int, List<MonsterProjectileSpellDat>>? byAnimation;
    private Dictionary<bool, List<MonsterProjectileSpellDat>>? byUnknown36;
    private Dictionary<bool, List<MonsterProjectileSpellDat>>? byUnknown37;
    private Dictionary<int, List<MonsterProjectileSpellDat>>? byUnknown38;
    private Dictionary<bool, List<MonsterProjectileSpellDat>>? byUnknown42;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterProjectileSpellRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterProjectileSpellRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out MonsterProjectileSpellDat? item)
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileSpellDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Projectile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProjectile(int? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProjectile(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Projectile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProjectile(int? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byProjectile is null)
        {
            byProjectile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Projectile;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProjectile.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProjectile.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProjectile.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byProjectile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileSpellDat>> GetManyToManyByProjectile(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProjectile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAnimation(int? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAnimation(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAnimation(int? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byAnimation is null)
        {
            byAnimation = new();
            foreach (var item in Items)
            {
                var itemKey = item.Animation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAnimation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAnimation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAnimation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byAnimation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileSpellDat>> GetManyToManyByAnimation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAnimation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(bool? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(bool? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileSpellDat>> GetManyToManyByUnknown36(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(bool? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown37(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(bool? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;

                if (!byUnknown37.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown37.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown37.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileSpellDat>> GetManyToManyByUnknown37(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown38"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown38(int? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown38(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown38"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown38(int? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byUnknown38 is null)
        {
            byUnknown38 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown38;

                if (!byUnknown38.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown38.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown38.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byUnknown38"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileSpellDat>> GetManyToManyByUnknown38(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown38(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown42(bool? key, out MonsterProjectileSpellDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown42(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown42(bool? key, out IReadOnlyList<MonsterProjectileSpellDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        if (byUnknown42 is null)
        {
            byUnknown42 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown42;

                if (!byUnknown42.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown42.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown42.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileSpellDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileSpellDat"/> with <see cref="MonsterProjectileSpellDat.byUnknown42"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileSpellDat>> GetManyToManyByUnknown42(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileSpellDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileSpellDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown42(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileSpellDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterProjectileSpellDat[] Load()
    {
        const string filePath = "Data/MonsterProjectileSpell.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterProjectileSpellDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Projectile
            (var projectileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var unknown38Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterProjectileSpellDat()
            {
                Id = idLoading,
                Projectile = projectileLoading,
                Animation = animationLoading,
                Unknown36 = unknown36Loading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown42 = unknown42Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
