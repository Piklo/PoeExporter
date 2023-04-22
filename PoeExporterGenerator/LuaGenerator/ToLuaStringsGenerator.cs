﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace PoeExporterGenerator.LuaGenerator;

/// <summary>
/// Class used to generate to lua strings methods.
/// </summary>
[Generator]
internal sealed class ToLuaStringsGenerator : ISourceGenerator
{
    private const string ToLuaStringMethodName = "ToLuaString";
    private const string ToLuaStringsMethodName = "ToLuaStrings";
    private const string AttributeName = "LuaPropertyName";
    private const string LuaConverterClassName = "LuaConverter";

    /// <inheritdoc/>
    public void Execute(GeneratorExecutionContext context)
    {
        var classes = LuaStringHelpers.GetClassDeclarations(context);

        var builder = new StringBuilder();

        builder.AppendLine($$"""
            // <auto-generated/>
                        
            using System.Text;

            namespace PoeExporter.WikiExporters.Lua.Helpers;
                        
            internal static partial class {{LuaConverterClassName}}
            {
            """);

        foreach (var classDeclaration in classes)
        {
            AddToLuaStringMethod(classDeclaration, builder);
            AddToLuaStringsMethod(classDeclaration, builder);
        }

        builder.AppendLine("""
            }
            """);

        var str = builder.ToString();

        context.AddSource($"{LuaConverterClassName}.g.cs", str);
    }

    /// <inheritdoc/>
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    private void AddToLuaStringMethod(ClassDeclarationSyntax classDeclaration, StringBuilder builder)
    {
        var className = classDeclaration.Identifier.ValueText;

        builder.AppendLine($$"""

                /// <summary>
                /// Converts a list of {{className}} to lua string.
                /// </summary>
                /// <param name="items">items.</param>
                /// <returns>converted objects in lua string.</returns>
                public static string {{ToLuaStringMethodName}}(IReadOnlyList<{{className}}> items)
                {
                    var builder = new StringBuilder();
                    builder.AppendLine("local data = {");
                    var baseIndentation = 1; // from the data indentation

                    foreach (var item in items)
                    {
                        var strings = {{ToLuaStringsMethodName}}(item, baseIndentation);
                        foreach (var line in strings)
                        {
                            builder.AppendLine($"{new string('\t', line.Indentation)}{line.Value}");
                        }
                    }

                    builder.AppendLine("}");
                    builder.AppendLine("return data");

                    var str = builder.ToString();
                    return str;
                }
            """);
    }

    private void AddToLuaStringsMethod(ClassDeclarationSyntax classDeclaration, StringBuilder builder)
    {
        var className = classDeclaration.Identifier.ValueText;

        builder.AppendLine($$"""
            
                /// <summary>
                /// Converts {{className}} to lua strings.
                /// </summary>
                /// <param name="item">item to convert.</param>
                /// <param name="currentIndentation">current indentation.</param>
                /// <returns>converted {{className}} in lua strings.</returns>
                public static LuaString[] {{ToLuaStringsMethodName}}({{className}} item, int currentIndentation)
                {
                    var strings = new List<LuaString>();

                    var bracket = new LuaString("{", currentIndentation);
                    strings.Add(bracket);
                    currentIndentation++;

            """);

        foreach (var member in classDeclaration.Members)
        {
            if (member is not PropertyDeclarationSyntax property)
            {
                continue;
            }

            var luaPropertyName = GetLuaPropertyName(property);
            var actualPropertyName = property.Identifier.ValueText;
            var variableName = $"{actualPropertyName.ToLower()}LuaStrings";

            builder.AppendLine($"""
                        var {variableName} = LuaString.Generate("{luaPropertyName}", item.{actualPropertyName}, currentIndentation);
                        strings.AddRange({variableName});

                """);
        }

        builder.AppendLine("""
                    currentIndentation--;
                    var endBracket = new LuaString("}", currentIndentation);
                    strings.Add(endBracket);

                    return strings.ToArray();
                }
            """);
    }

    private static string GetLuaPropertyName(PropertyDeclarationSyntax property)
    {
        var attributesList = property.AttributeLists;
        foreach (var attributes in attributesList)
        {
            foreach (var attribute in attributes.Attributes)
            {
                if (attribute.Name is not IdentifierNameSyntax name || name.Identifier.ValueText != AttributeName)
                {
                    continue;
                }

                if (attribute.ArgumentList is null)
                {
                    continue;
                }

                var overridenNameSyntax = attribute.ArgumentList.Arguments[0];

                if (overridenNameSyntax.Expression is not LiteralExpressionSyntax expression)
                {
                    continue;
                }

                var overridenName = expression.Token.ValueText;

                return overridenName;
            }
        }

        // if the property name is not overriden with the attribute default to actual property name
        return property.Identifier.ValueText;
    }
}
