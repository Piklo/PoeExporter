// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Realms.dat data.
/// </summary>
public sealed partial class RealmsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Server.</summary>
    public required ReadOnlyCollection<string> Server { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Server2.</summary>
    public required ReadOnlyCollection<string> Server2 { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown57.</summary>
    /// <remarks> references <see cref="RealmsDat"/> on <see cref="Specification.GetRealmsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown57 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    /// <remarks> references <see cref="RealmsDat"/> on <see cref="Specification.GetRealmsDat"/> index.</remarks>
    public required int? Unknown73 { get; init; }

    /// <summary> Gets Unknown81.</summary>
    public required int Unknown81 { get; init; }

    /// <summary> Gets a value indicating whether IsGammaRealm is set.</summary>
    public required bool IsGammaRealm { get; init; }

    /// <summary> Gets SpeedtestUrl.</summary>
    public required ReadOnlyCollection<string> SpeedtestUrl { get; init; }

    /// <summary>
    /// Gets RealmsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RealmsDat.</returns>
    internal static RealmsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Realms.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RealmsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Server
            (var tempserverLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var serverLoading = tempserverLoading.AsReadOnly();

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Server2
            (var tempserver2Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var server2Loading = tempserver2Loading.AsReadOnly();

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown57
            (var tempunknown57Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown57Loading = tempunknown57Loading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsGammaRealm
            (var isgammarealmLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpeedtestUrl
            (var tempspeedtesturlLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var speedtesturlLoading = tempspeedtesturlLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RealmsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Server = serverLoading,
                IsEnabled = isenabledLoading,
                Server2 = server2Loading,
                ShortName = shortnameLoading,
                Unknown57 = unknown57Loading,
                Unknown73 = unknown73Loading,
                Unknown81 = unknown81Loading,
                IsGammaRealm = isgammarealmLoading,
                SpeedtestUrl = speedtesturlLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
