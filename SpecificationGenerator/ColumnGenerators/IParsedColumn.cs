﻿using SpecificationGenerator.RepositoryGenerators;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Interface containing methods and properties for parsed column classes.
/// </summary>
internal interface IParsedColumn : IRepositoryColumn
{
    /// <summary>
    /// Gets column offset.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Gets class property name.
    /// </summary>
    public string ClassPropertyName { get; }

    /// <summary>
    /// Gets class property underlying type.
    /// </summary>
    public string ClassPropertyUnderlyingType { get; }

    /// <summary>
    /// Gets class property type.
    /// </summary>
    public string ClassPropertyType { get; }

    /// <summary>
    /// Gets referenced table.
    /// </summary>
    public string? ReferencedTable { get; }

    /// <summary>
    /// Gets referenced column.
    /// </summary>
    public string? ReferencedColumn { get; }

    /// <summary>
    /// Gets loading property name.
    /// </summary>
    public string LoadingPropertyName { get; }

    /// <summary>
    /// Gets an array of not indented strings related to class properties for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetPropertyStrings();

    /// <summary>
    /// Gets an array of not indented strings related to loading the property data in LoadXyz method for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetLoading();
}
