using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillGemInfoDat"/> related data and helper methods.
/// </summary>
public sealed class SkillGemInfoRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillGemInfoDat> Items { get; }

    private Dictionary<string, List<SkillGemInfoDat>>? byId;
    private Dictionary<string, List<SkillGemInfoDat>>? byDescription;
    private Dictionary<string, List<SkillGemInfoDat>>? byVideoURL1;
    private Dictionary<int, List<SkillGemInfoDat>>? bySkillGemsKey;
    private Dictionary<string, List<SkillGemInfoDat>>? byVideoURL2;
    private Dictionary<int, List<SkillGemInfoDat>>? byCharactersKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillGemInfoRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillGemInfoRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SkillGemInfoDat? item)
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
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
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemInfoDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<string, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out SkillGemInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemInfoDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<string, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.VideoURL1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVideoURL1(string? key, out SkillGemInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVideoURL1(key, out var items))
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.VideoURL1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVideoURL1(string? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        if (byVideoURL1 is null)
        {
            byVideoURL1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.VideoURL1;

                if (!byVideoURL1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVideoURL1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVideoURL1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.byVideoURL1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemInfoDat>> GetManyToManyByVideoURL1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<string, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVideoURL1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.SkillGemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillGemsKey(int? key, out SkillGemInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillGemsKey(key, out var items))
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.SkillGemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillGemsKey(int? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        if (bySkillGemsKey is null)
        {
            bySkillGemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillGemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySkillGemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySkillGemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillGemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.bySkillGemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemInfoDat>> GetManyToManyBySkillGemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<int, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillGemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.VideoURL2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVideoURL2(string? key, out SkillGemInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVideoURL2(key, out var items))
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.VideoURL2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVideoURL2(string? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        if (byVideoURL2 is null)
        {
            byVideoURL2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.VideoURL2;

                if (!byVideoURL2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVideoURL2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVideoURL2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.byVideoURL2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemInfoDat>> GetManyToManyByVideoURL2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<string, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVideoURL2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.CharactersKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKeys(int? key, out SkillGemInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharactersKeys(key, out var items))
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
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.CharactersKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKeys(int? key, out IReadOnlyList<SkillGemInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        if (byCharactersKeys is null)
        {
            byCharactersKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharactersKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byCharactersKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharactersKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharactersKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemInfoDat"/> with <see cref="SkillGemInfoDat.byCharactersKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemInfoDat>> GetManyToManyByCharactersKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemInfoDat>>();
        }

        var items = new List<ResultItem<int, SkillGemInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillGemInfoDat[] Load()
    {
        const string filePath = "Data/SkillGemInfo.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillGemInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading VideoURL1
            (var videourl1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillGemsKey
            (var skillgemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading VideoURL2
            (var videourl2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKeys
            (var tempcharacterskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var characterskeysLoading = tempcharacterskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillGemInfoDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                VideoURL1 = videourl1Loading,
                SkillGemsKey = skillgemskeyLoading,
                VideoURL2 = videourl2Loading,
                CharactersKeys = characterskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
