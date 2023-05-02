﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;
using System.Threading;

namespace PoeExporterGenerator;

/// <summary>
/// Class used to generate to LuaString generate methods.
/// </summary>
[Generator]
internal class LuaStringGenerator : IIncrementalGenerator
{
    private const string AttributeName = "LuaItem";
    private const string AttributeFullName = "LuaItemAttribute";
    private const string LuaStringClassName = "LuaString";

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

    private readonly record struct ClassData(string Namespace, string ClassName);
    private static ClassData GetClassData(GeneratorSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        if (syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.Node) is not INamedTypeSymbol symbol)
        {
            throw new ArgumentException($"symbol isnt a {nameof(INamedTypeSymbol)}");
        }

        var namespaceName = symbol.ContainingNamespace.ToString();
        var className = symbol.Name;

        var classData = new ClassData(namespaceName, className);
        return classData;
    }

    private static void Output(SourceProductionContext sourceProductionContext, ClassData classData)
    {
        var builder = new StringBuilder();

        builder.AppendLine($$"""
            // <auto-generated/>

            using {{classData.Namespace}};

            namespace PoeExporter.WikiExporters.Lua.Helpers;

            internal readonly partial record struct {{LuaStringClassName}}
            {
                /// <summary>
                /// Generates lua strings from <see cref="{{classData.ClassName}}"/>.
                /// </summary>
                /// <param name="name">name of a lua variable.</param>
                /// <param name="exporter">exporter to generater values from.</param>
                /// <param name="indentation">indentation of lua strings.</param>
                /// <returns>parsed lua strings.</returns>
                internal static LuaString[] Generate(string name, {{classData.ClassName}} exporter, int indentation)
                {
                    var strings = LuaConverter.ToLuaStrings(exporter, indentation);
                    var first = strings[0];
                    var overridenFirst = new LuaString($"{name} = {first.Value}", indentation);
                    strings[0] = overridenFirst;

                    return strings;
                }
            }
            """);

        var resultClassName = $"{classData.ClassName}{LuaStringClassName}.g.cs";
        var str = builder.ToString();

        sourceProductionContext.AddSource(resultClassName, str);
    }
}
