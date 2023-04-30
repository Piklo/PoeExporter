﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PoeExporterGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

/// <summary>
/// Class used to generate to LuaConverter methods.
/// </summary>
[Generator]
internal sealed class LuaConverterGenerator : IIncrementalGenerator
{
    private const string AttributeName = "LuaItem";
    private const string AttributeFullName = "LuaItemAttribute";
    private const string ToLuaStringMethodName = "ToLuaString";
    private const string ToLuaStringsMethodName = "ToLuaStrings";
    private const string LuaConverterClassName = "LuaConverter";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var markedClasses = context.SyntaxProvider.CreateSyntaxProvider(ShouldVisit, GetClassData);

        context.RegisterSourceOutput(markedClasses, Output);
    }

    private static bool ShouldVisit(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax)
        {
            return false;
        }

        var attributes = classDeclarationSyntax.AttributeLists;

        if (!Helpers.HasAttribute(attributes, AttributeName, AttributeFullName, cancellationToken))
        {
            return false;
        }

        return true;
    }

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    private readonly record struct ClassData(string Namespace, string ClassName, IReadOnlyList<PropertyData> Properties);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    private readonly record struct PropertyData(string PropertyName, string LuaPropertyName);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

    private static ClassData GetClassData(GeneratorSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        if (syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.Node) is not INamedTypeSymbol symbol)
        {
            throw new ArgumentException($"symbol isnt a {nameof(INamedTypeSymbol)}");
        }

        var members = symbol.GetMembers();
        var properties = new List<PropertyData>();
        foreach (var member in members)
        {
            if (member is not IPropertySymbol propertySymbol)
            {
                continue;
            }

            var propertyName = propertySymbol.Name;
            var luaName = GetLuaPropertyName(propertySymbol);

            if (luaName is null)
            {
                continue;
            }

            var propertyData = new PropertyData(propertyName, luaName);
            properties.Add(propertyData);
        }

        var namespaceName = symbol.ContainingNamespace.ToString();
        var className = symbol.Name;

        var classData = new ClassData(namespaceName, className, properties);
        return classData;
    }

    private static string? GetLuaPropertyName(IPropertySymbol propertySymbol)
    {
        const string LuaAttributeFullName = "LuaPropertyNameAttribute";
        var attributes = propertySymbol.GetAttributes();

        foreach (var attribute in attributes)
        {
            if (attribute.AttributeClass is null || attribute.AttributeClass.Name != LuaAttributeFullName)
            {
                continue;
            }

            var luaName = attribute.ConstructorArguments[0].Value as string;

            return luaName;
        }

        return null;
    }

    private static void Output(SourceProductionContext sourceProductionContext, ClassData classData)
    {
        var builder = new StringBuilder();
        builder.AppendLine($$"""
                    // <auto-generated/>

                    using System.Text;
                    using {{classData.Namespace}};

                    namespace PoeExporter.WikiExporters.Lua.Helpers;

                    internal static partial class {{LuaConverterClassName}}
                    {
                    """);

        AddToLuaStringMethod(classData, builder);
        AddToLuaStringsMethod(classData, builder);

        builder.AppendLine("""
                    }
                    """);

        var resultClassName = $"{classData.ClassName}{LuaConverterClassName}.g.cs";
        var str = builder.ToString();

        sourceProductionContext.AddSource(resultClassName, str);
    }

    private static void AddToLuaStringMethod(ClassData classData, StringBuilder builder)
    {
        builder.AppendLine($$"""
                /// <summary>
                /// Converts a list of {{classData.ClassName}} to lua string.
                /// </summary>
                /// <param name="items">items.</param>
                /// <returns>converted objects in lua string.</returns>
                public static string {{ToLuaStringMethodName}}(IReadOnlyList<{{classData.ClassName}}> items)
                {
                    var builder = new StringBuilder();
                    builder.AppendLine("local data = {");
                    var baseIndentation = 1; // from the local data indentation

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

    private static void AddToLuaStringsMethod(ClassData classData, StringBuilder builder)
    {
        builder.AppendLine($$"""

                /// <summary>
                /// Converts {{classData.ClassName}} to lua strings.
                /// </summary>
                /// <param name="item">item to convert.</param>
                /// <param name="currentIndentation">current indentation.</param>
                /// <returns>converted {{classData.ClassName}} in lua strings.</returns>
                public static LuaString[] {{ToLuaStringsMethodName}}({{classData.ClassName}} item, int currentIndentation)
                {
                    var strings = new List<LuaString>();

                    var bracket = new LuaString("{", currentIndentation);
                    strings.Add(bracket);
                    currentIndentation++;
                            
            """);

        foreach (var property in classData.Properties)
        {
            var actualPropertyName = property.PropertyName;
            var variableName = $"{actualPropertyName.ToLower()}LuaStrings";
            builder.AppendLine($"""
                        var {variableName} = LuaString.Generate("{property.LuaPropertyName}", item.{actualPropertyName}, currentIndentation);
                        strings.AddRange({variableName});
                                
                """);
        }

        builder.AppendLine("""
                    currentIndentation--;
                    var endBracket = new LuaString("},", currentIndentation);
                    strings.Add(endBracket);

                    return strings.ToArray();
                }
            """);
    }
}
