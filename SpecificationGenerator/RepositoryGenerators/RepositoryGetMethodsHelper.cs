using SpecificationGenerator.ColumnGenerators;
using System.Text;

namespace SpecificationGenerator.RepositoryGenerators;

/// <summary>
/// Class containing methods used to generate get by column methods.
/// </summary>
internal static class RepositoryGetMethodsHelper
{
    public static IReadOnlyList<LineOfCode> GetSingleMethod(string datClassName, IParsedColumn column, bool isNullableValueTypeKey = false)
    {
        var getManyMethodName = GenerateGetManyMethodName(column);
        var code = $$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
            /// </summary>
            /// <param name="key">key.</param>
            /// <param name="item">returned item if found.</param>
            /// <returns>true if item with a given key was found, false otherwise.</returns>
            public bool TryGetBy{{column.ClassPropertyName}}({{column.ClassPropertyUnderlyingType}}? key, out {{datClassName}}? item)
            {
                if (key is null)
                {
                    item = null;
                    return false;
                }

                if (!{{getManyMethodName}}(key, out var items))
                {
                    item = null;
                    return false;
                }

                if (items.Count == 0)
                {
                    logger.Warning("failed to find item with key = {key}", {{(isNullableValueTypeKey ? "key.Value" : "key")}});
                    item = null;
                    return false;
                }

                if (items.Count > 1)
                {
                    logger.Warning("found too many items with key = {key}", {{(isNullableValueTypeKey ? "key.Value" : "key")}});
                    item = null;
                    return false;
                }

                item = items[0];
                return true;
            }
            """;

        var lines = LineOfCode.Split(code);

        return lines;
    }

    private static void AppendGetManyStart(StringBuilder builder, string datClassName, string fieldName, IParsedColumn column, string methodName)
    {
        builder.AppendLine($$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
            /// </summary>
            /// <param name="key">key.</param>
            /// <param name="items">returned items if found.</param>
            /// <returns>true if item with a given key was found, false otherwise.</returns>
            public bool {{methodName}}({{column.ClassPropertyUnderlyingType}}? key, out IReadOnlyList<{{datClassName}}> items)
            {
                if (key is null)
                {
                    items = Array.Empty<{{datClassName}}>();
                    return false;
                }

                if ({{fieldName}} is null)
                {
                    {{fieldName}} = new();
            """);
    }

    private static void AppendGetManyEnd(StringBuilder builder, string datClassName, string fieldName, bool isNullableValueTypeKey = false)
    {
        builder.Append($$"""
                }

                if (!{{fieldName}}.TryGetValue({{(isNullableValueTypeKey ? "key.Value" : "key")}}, out var temp))
                {
                    items = Array.Empty<{{datClassName}}>();
                    return false;
                }

                items = temp;
                return true;
            }
            """);
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodNonNullableValueType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName, isValueType: true);

        AppendGetManyEnd(builder, datClassName, fieldName, isNullableValueTypeKey: true);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodNullableValueType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName, isNullable: true, isValueType: true);

        AppendGetManyEnd(builder, datClassName, fieldName, isNullableValueTypeKey: true);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodNonNullableReferenceType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName);

        AppendGetManyEnd(builder, datClassName, fieldName);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodNullableReferenceType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName, isNullable: true);

        AppendGetManyEnd(builder, datClassName, fieldName);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodValueArrayType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName, isArray: true);

        AppendGetManyEnd(builder, datClassName, fieldName, isNullableValueTypeKey: true);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    public static IReadOnlyList<LineOfCode> GetManyMethodReferenceArrayType(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var builder = new StringBuilder();

        AppendGetManyStart(builder, datClassName, fieldName, column, methodName);

        AppendToDictionaryParsing(builder, fieldName, column, datClassName, isArray: true);

        AppendGetManyEnd(builder, datClassName, fieldName);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    private static string GenerateGetManyMethodName(IParsedColumn column)
    {
        return $"TryGetManyBy{column.ClassPropertyName}";
    }

    private static void AppendToDictionaryParsing(StringBuilder builder, string fieldName, IParsedColumn column, string datClassName, bool isNullable = false, bool isValueType = false, bool isArray = false)
    {
        builder.AppendLine($$"""
                        foreach (var item in Items)
                        {
                            var itemKey = item.{{column.ClassPropertyName}};
                """);

        if (isNullable)
        {
            builder.AppendLine("""
                            if (itemKey is null)
                            {
                                continue;
                            }
                """);
        }

        if (isArray)
        {
            builder.AppendLine($$"""
                            foreach (var listKey in itemKey)
                            {
                                if (!{{fieldName}}.TryGetValue(listKey, out var list))
                                {
                                    list = new();
                                    {{fieldName}}.TryAdd(listKey, list);
                                }
                
                                list.Add(item);
                            }
                        }
                """);
        }
        else
        {
            builder.AppendLine($$"""

                            if (!{{fieldName}}.TryGetValue(itemKey{{(isNullable && isValueType ? ".Value" : string.Empty)}}, out var list))
                            {
                                list = new();
                                {{fieldName}}.TryAdd(itemKey{{(isNullable && isValueType ? ".Value" : string.Empty)}}, list);
                            }

                            list.Add(item);
                        }
                """);
        }
    }

    public static IReadOnlyList<LineOfCode> GetManyToMany(string datClassName, string fieldName, IParsedColumn column)
    {
        var getManyMethodName = GenerateGetManyMethodName(column);
        var methodName = $"GetManyToManyBy{column.ClassPropertyName}";
        var type = column.ClassPropertyUnderlyingType;
        var code = $$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{fieldName}}"/> equal to a given keys.
            /// </summary>
            /// <param name="keys">keys.</param>
            /// <returns>found items.</returns>
            public IReadOnlyList<ResultItem<{{type}}, {{datClassName}}>> {{methodName}}(IReadOnlyList<{{type}}>? keys)
            {
                if (keys is null || keys.Count == 0)
                {
                    return Array.Empty<ResultItem<{{type}}, {{datClassName}}>>();
                }

                var items = new List<ResultItem<{{type}}, {{datClassName}}>>();

                foreach (var key in keys)
                {
                    if (!{{getManyMethodName}}(key, out var tempItems))
                    {
                        continue;
                    }

                    foreach (var item in tempItems)
                    {
                        var resultItem = new ResultItem<{{type}}, {{datClassName}}>(key, item);
                        items.Add(resultItem);
                    }
                }

                return items;
            }
            """;

        var lines = LineOfCode.Split(code);

        return lines;
    }
}
