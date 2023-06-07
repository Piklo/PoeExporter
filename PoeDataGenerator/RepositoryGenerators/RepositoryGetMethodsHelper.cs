using PoeDataGenerator.ParsedColumns.Helpers;
using System.Text;

namespace PoeDataGenerator.RepositoryGenerators;

/// <summary>
/// Class containing methods used to generate get by column methods.
/// </summary>
internal static class RepositoryGetMethodsHelper
{
    public static IReadOnlyList<LineOfCode> GetSingleMethod(string datClassName, IParsedColumn column)
    {
        var getManyMethodName = GenerateGetManyMethodName(column);
        var typeData = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0] : column.Type;
        var type = typeData.IsNullable ? typeData.Type : $"{typeData.Type}?";
        var code = $$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
            /// </summary>
            /// <param name="key">key.</param>
            /// <param name="item">returned item if found.</param>
            /// <returns>true if item with a given key was found, false otherwise.</returns>
            public bool TryGetBy{{column.ClassPropertyName}}({{type}} key, out {{datClassName}}? item)
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
                    logger.Warning("failed to find item with key = {key}", {{(typeData.IsValueType ? "key.Value" : "key")}});
                    item = null;
                    return false;
                }

                if (items.Count > 1)
                {
                    logger.Warning("found too many items with key = {key}", {{(typeData.IsValueType ? "key.Value" : "key")}});
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

    public static IReadOnlyList<LineOfCode> GetManyMethod(string datClassName, string fieldName, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var typeData = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0] : column.Type;
        var type = typeData.IsNullable ? typeData.Type : $"{typeData.Type}?";
        var builder = new StringBuilder();
        builder.AppendLine($$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
            /// </summary>
            /// <param name="key">key.</param>
            /// <param name="items">returned items if found.</param>
            /// <returns>true if item with a given key was found, false otherwise.</returns>
            public bool {{methodName}}({{type}} key, out IReadOnlyList<{{datClassName}}> items)
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

        AppendToDictionaryParsing(builder, fieldName, column);

        builder.Append($$"""
                }

                if (!{{fieldName}}.TryGetValue({{(typeData.IsValueType ? "key.Value" : "key")}}, out var temp))
                {
                    items = Array.Empty<{{datClassName}}>();
                    return false;
                }

                items = temp;
                return true;
            }
            """);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    private static void AppendToDictionaryParsing(StringBuilder builder, string fieldName, IParsedColumn column)
    {
        var typeData = column.Type;
        var isNullable = typeData.IsNullable;
        var isValueType = typeData.IsValueType;
        var isList = typeData.IsList;

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

        if (isList)
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

    private static string GenerateGetManyMethodName(IParsedColumn column)
    {
        return $"TryGetManyBy{column.ClassPropertyName}";
    }

    public static IReadOnlyList<LineOfCode> GetManyToMany(string datClassName, string fieldName, IParsedColumn column)
    {
        var getManyMethodName = GenerateGetManyMethodName(column);
        var methodName = $"GetManyToManyBy{column.ClassPropertyName}";
        var type = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0].Type : column.Type.Type;
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
