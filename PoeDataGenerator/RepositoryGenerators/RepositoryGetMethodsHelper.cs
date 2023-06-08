using PoeDataGenerator.GeneratorHelpers;
using PoeDataGenerator.ParsedColumns.Helpers;
using System.Text;

namespace PoeDataGenerator.RepositoryGenerators;

/// <summary>
/// Class containing methods used to generate get by column methods.
/// </summary>
internal static class RepositoryGetMethodsHelper
{
    /// <summary>
    /// Generates code for GetByXYZ methods.
    /// </summary>
    /// <param name="datClassName">dat file class name.</param>
    /// <param name="fieldName">name of the field where the data is stored.</param>
    /// <param name="column">column for which we are storing the data.</param>
    /// <returns>generated code.</returns>
    public static IReadOnlyList<LineOfCode> GenerateGetByMethod(string datClassName, string fieldName, IParsedColumn column)
    {
        var typeData = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0] : column.Type;
        var type = typeData.IsNullable ? typeData.Type : $"{typeData.Type}?";
        var builder = new StringBuilder();
        builder.AppendLine($$"""
            /// <summary>
            /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
            /// </summary>
            /// <param name="key">key.</param>
            /// <returns>item if found, null otherwise.</returns>
            public {{datClassName}}? {{GenerateGetByMethodName(column.ClassPropertyName)}}({{type}} key)
            {
                if (key is null)
                {
                    return null;
                }

                {{fieldName}} ??= {{GetLoadFieldMethodName(fieldName)}}();

                if ({{fieldName}}.TryGetValue({{(typeData.IsValueType ? "key.Value" : "key")}}, out var item))
                {
                    return item;
                }
                else 
                {
                    return null;
                }
            }
            """);

        AppendLoadFieldMethod(builder, datClassName, fieldName, column);

        var code = builder.ToString();

        var lines = LineOfCode.Split(code);

        return lines;
    }

    /// <summary>
    /// Generated method name for GetByXYZ.
    /// </summary>
    /// <param name="propertyName">name of the property by which we try to get data.</param>
    /// <returns>method name.</returns>
    internal static string GenerateGetByMethodName(string propertyName)
    {
        return $"GetBy{propertyName}";
    }

    private static string GetLoadFieldMethodName(string fieldName)
    {
        return $"Load{fieldName}";
    }

    private static void AppendLoadFieldMethod(StringBuilder builder, string datClassName, string fieldName, IParsedColumn column)
    {
        var fieldType = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0].Type : column.Type.Type;
        var methodName = GetLoadFieldMethodName(fieldName);

        builder.AppendLine($$"""

            private Dictionary<{{fieldType}}, {{datClassName}}> {{methodName}}()
            {
                var dict = new Dictionary<{{fieldType}}, {{datClassName}}>();

                foreach (var item in Items)
                {
                    dict.Add(item.{{column.ClassPropertyName}}, item);
                }

                return dict;
            }
            """);
    }
}
