// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ChestEffects.dat data.
/// </summary>
public sealed partial class ChestEffectsDat : ISpecificationFile<ChestEffectsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Normal_EPKFile.</summary>
    public required string Normal_EPKFile { get; init; }

    /// <summary> Gets Normal_Closed_AOFile.</summary>
    public required string Normal_Closed_AOFile { get; init; }

    /// <summary> Gets Normal_Open_AOFile.</summary>
    public required string Normal_Open_AOFile { get; init; }

    /// <summary> Gets Magic_EPKFile.</summary>
    public required string Magic_EPKFile { get; init; }

    /// <summary> Gets Unique_EPKFile.</summary>
    public required string Unique_EPKFile { get; init; }

    /// <summary> Gets Rare_EPKFile.</summary>
    public required string Rare_EPKFile { get; init; }

    /// <summary> Gets Magic_Closed_AOFile.</summary>
    public required string Magic_Closed_AOFile { get; init; }

    /// <summary> Gets Unique_Closed_AOFile.</summary>
    public required string Unique_Closed_AOFile { get; init; }

    /// <summary> Gets Rare_Closed_AOFile.</summary>
    public required string Rare_Closed_AOFile { get; init; }

    /// <summary> Gets Magic_Open_AOFile.</summary>
    public required string Magic_Open_AOFile { get; init; }

    /// <summary> Gets Unique_Open_AOFile.</summary>
    public required string Unique_Open_AOFile { get; init; }

    /// <summary> Gets Rare_Open_AOFile.</summary>
    public required string Rare_Open_AOFile { get; init; }

    /// <inheritdoc/>
    public static ChestEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ChestEffects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ChestEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_EPKFile
            (var normal_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_Closed_AOFile
            (var normal_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_Open_AOFile
            (var normal_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_EPKFile
            (var magic_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_EPKFile
            (var unique_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_EPKFile
            (var rare_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_Closed_AOFile
            (var magic_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_Closed_AOFile
            (var unique_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_Closed_AOFile
            (var rare_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_Open_AOFile
            (var magic_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_Open_AOFile
            (var unique_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_Open_AOFile
            (var rare_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ChestEffectsDat()
            {
                Id = idLoading,
                Normal_EPKFile = normal_epkfileLoading,
                Normal_Closed_AOFile = normal_closed_aofileLoading,
                Normal_Open_AOFile = normal_open_aofileLoading,
                Magic_EPKFile = magic_epkfileLoading,
                Unique_EPKFile = unique_epkfileLoading,
                Rare_EPKFile = rare_epkfileLoading,
                Magic_Closed_AOFile = magic_closed_aofileLoading,
                Unique_Closed_AOFile = unique_closed_aofileLoading,
                Rare_Closed_AOFile = rare_closed_aofileLoading,
                Magic_Open_AOFile = magic_open_aofileLoading,
                Unique_Open_AOFile = unique_open_aofileLoading,
                Rare_Open_AOFile = rare_open_aofileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
