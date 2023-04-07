// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterProjectileSpell.dat data.
/// </summary>
public sealed partial class MonsterProjectileSpellDat : IDat<MonsterProjectileSpellDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Projectile.</summary>
    /// <remarks> references <see cref="ProjectilesDat"/> on <see cref="Specification.GetProjectilesDat"/> index.</remarks>
    public required int? Projectile { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.GetAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets a value indicating whether Unknown36 is set.</summary>
    public required bool Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets Unknown38.</summary>
    public required int Unknown38 { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }

    /// <inheritdoc/>
    public static MonsterProjectileSpellDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/MonsterProjectileSpell.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

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
            (var projectileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

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
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
