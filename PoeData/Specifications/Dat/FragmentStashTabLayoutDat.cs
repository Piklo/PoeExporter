// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing FragmentStashTabLayout.dat data.
/// </summary>
public sealed partial class FragmentStashTabLayoutDat : IDat<FragmentStashTabLayoutDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PosX.</summary>
    public required int PosX { get; init; }

    /// <summary> Gets PosY.</summary>
    public required int PosY { get; init; }

    /// <summary> Gets Order.</summary>
    public required int Order { get; init; }

    /// <summary> Gets SizeX.</summary>
    public required int SizeX { get; init; }

    /// <summary> Gets SizeY.</summary>
    public required int SizeY { get; init; }

    /// <summary> Gets a value indicating whether Unknown28 is set.</summary>
    public required bool Unknown28 { get; init; }

    /// <summary> Gets Tab.</summary>
    public required int Tab { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets Subtab.</summary>
    public required int Subtab { get; init; }

    /// <summary> Gets FragmentItems.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FragmentItems { get; init; }

    /// <inheritdoc/>
    public static FragmentStashTabLayoutDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/FragmentStashTabLayout.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FragmentStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PosX
            (var posxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PosY
            (var posyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Order
            (var orderLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SizeX
            (var sizexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SizeY
            (var sizeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Tab
            (var tabLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Subtab
            (var subtabLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FragmentItems
            (var tempfragmentitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fragmentitemsLoading = tempfragmentitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FragmentStashTabLayoutDat()
            {
                Id = idLoading,
                PosX = posxLoading,
                PosY = posyLoading,
                Order = orderLoading,
                SizeX = sizexLoading,
                SizeY = sizeyLoading,
                Unknown28 = unknown28Loading,
                Tab = tabLoading,
                Unknown33 = unknown33Loading,
                IsDisabled = isdisabledLoading,
                Subtab = subtabLoading,
                FragmentItems = fragmentitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
