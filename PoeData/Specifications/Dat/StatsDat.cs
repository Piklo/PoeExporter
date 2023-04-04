// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Stats.dat data.
/// </summary>
public sealed partial class StatsDat : IDat<StatsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether IsLocal is set.</summary>
    public required bool IsLocal { get; init; }

    /// <summary> Gets a value indicating whether IsWeaponLocal is set.</summary>
    public required bool IsWeaponLocal { get; init; }

    /// <summary> Gets Semantics.</summary>
    /// <remarks> references <see cref="StatSemanticsDat"/> on <see cref="Specification.GetStatSemanticsDat"/> index.</remarks>
    public required int Semantics { get; init; }

    /// <summary> Gets a value indicating whether Unknown15 is set.</summary>
    public required bool Unknown15 { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether IsVirtual is set.</summary>
    public required bool IsVirtual { get; init; }

    /// <summary> Gets MainHandAlias_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? MainHandAlias_StatsKey { get; init; }

    /// <summary> Gets OffHandAlias_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? OffHandAlias_StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets BelongsActiveSkillsKey.</summary>
    /// <remarks> references <see cref="ActiveSkillsDat"/> on <see cref="ActiveSkillsDat.Id"/>.</remarks>
    public required ReadOnlyCollection<string> BelongsActiveSkillsKey { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="PassiveSkillStatCategoriesDat"/> on <see cref="Specification.GetPassiveSkillStatCategoriesDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets a value indicating whether IsScalable is set.</summary>
    public required bool IsScalable { get; init; }

    /// <summary> Gets ContextFlags.</summary>
    /// <remarks> references <see cref="VirtualStatContextFlagsDat"/> on <see cref="Specification.GetVirtualStatContextFlagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ContextFlags { get; init; }

    /// <inheritdoc/>
    public static StatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Stats.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLocal
            (var islocalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsWeaponLocal
            (var isweaponlocalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Semantics
            (var semanticsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown15
            (var unknown15Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsVirtual
            (var isvirtualLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MainHandAlias_StatsKey
            (var mainhandalias_statskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading OffHandAlias_StatsKey
            (var offhandalias_statskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BelongsActiveSkillsKey
            (var tempbelongsactiveskillskeyLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var belongsactiveskillskeyLoading = tempbelongsactiveskillskeyLoading.AsReadOnly();

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsScalable
            (var isscalableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ContextFlags
            (var tempcontextflagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var contextflagsLoading = tempcontextflagsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StatsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                IsLocal = islocalLoading,
                IsWeaponLocal = isweaponlocalLoading,
                Semantics = semanticsLoading,
                Unknown15 = unknown15Loading,
                Text = textLoading,
                Unknown24 = unknown24Loading,
                IsVirtual = isvirtualLoading,
                MainHandAlias_StatsKey = mainhandalias_statskeyLoading,
                OffHandAlias_StatsKey = offhandalias_statskeyLoading,
                Unknown42 = unknown42Loading,
                HASH32 = hash32Loading,
                BelongsActiveSkillsKey = belongsactiveskillskeyLoading,
                Category = categoryLoading,
                Unknown79 = unknown79Loading,
                Unknown80 = unknown80Loading,
                IsScalable = isscalableLoading,
                ContextFlags = contextflagsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
