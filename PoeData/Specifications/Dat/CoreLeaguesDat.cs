// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CoreLeagues.dat data.
/// </summary>
public sealed partial class CoreLeaguesDat : IDat<CoreLeaguesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets Unknown10.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown10 { get; init; }

    /// <summary> Gets Unknown26.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown26 { get; init; }

    /// <summary> Gets Unknown42.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown42 { get; init; }

    /// <summary> Gets Unknown58.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown58 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown74 { get; init; }

    /// <summary> Gets Unknown90.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown90 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown106 { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int? Unknown122 { get; init; }

    /// <summary> Gets Unknown138.</summary>
    public required int Unknown138 { get; init; }

    /// <summary> Gets a value indicating whether Unknown142 is set.</summary>
    public required bool Unknown142 { get; init; }

    /// <summary> Gets Unknown143.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown143 { get; init; }

    /// <summary> Gets Unknown159.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown159 { get; init; }

    /// <summary> Gets a value indicating whether Unknown175 is set.</summary>
    public required bool Unknown175 { get; init; }

    /// <summary> Gets a value indicating whether Unknown176 is set.</summary>
    public required bool Unknown176 { get; init; }

    /// <summary> Gets Unknown177.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown177 { get; init; }

    /// <inheritdoc/>
    public static CoreLeaguesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/CoreLeagues.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CoreLeaguesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown10
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown26
            (var tempunknown26Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown26Loading = tempunknown26Loading.AsReadOnly();

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown90
            (var tempunknown90Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown90Loading = tempunknown90Loading.AsReadOnly();

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown138
            (var unknown138Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown159
            (var tempunknown159Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown159Loading = tempunknown159Loading.AsReadOnly();

            // loading Unknown175
            (var unknown175Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown177
            (var tempunknown177Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown177Loading = tempunknown177Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CoreLeaguesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Unknown26 = unknown26Loading,
                Unknown42 = unknown42Loading,
                Unknown58 = unknown58Loading,
                Unknown74 = unknown74Loading,
                Unknown90 = unknown90Loading,
                Unknown106 = unknown106Loading,
                Unknown122 = unknown122Loading,
                Unknown138 = unknown138Loading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown159 = unknown159Loading,
                Unknown175 = unknown175Loading,
                Unknown176 = unknown176Loading,
                Unknown177 = unknown177Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
