// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TriggerBeam.dat data.
/// </summary>
public sealed partial class TriggerBeamDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    /// <remarks> references <see cref="MiscBeamsDat"/> on <see cref="Specification.LoadMiscBeamsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    /// <remarks> references <see cref="MiscBeamsDat"/> on <see cref="Specification.LoadMiscBeamsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown52 is set.</summary>
    public required bool Unknown52 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int Unknown53 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required int Unknown57 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets Unknown65.</summary>
    public required int Unknown65 { get; init; }

    /// <summary> Gets a value indicating whether Unknown69 is set.</summary>
    public required bool Unknown69 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required ReadOnlyCollection<int> Unknown70 { get; init; }

    /// <summary> Gets a value indicating whether Unknown86 is set.</summary>
    public required bool Unknown86 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary>
    /// Gets TriggerBeamDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TriggerBeamDat.</returns>
    internal static TriggerBeamDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TriggerBeam.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TriggerBeamDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var tempunknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown4Loading = tempunknown4Loading.AsReadOnly();

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var tempunknown70Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown70Loading = tempunknown70Loading.AsReadOnly();

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TriggerBeamDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
                Unknown53 = unknown53Loading,
                Unknown57 = unknown57Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown86 = unknown86Loading,
                Unknown87 = unknown87Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
