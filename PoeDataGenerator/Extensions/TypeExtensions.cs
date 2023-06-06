namespace PoeDataGenerator.Extensions;

/// <summary>
/// Extends <see cref="Type"/>.
/// </summary>
internal static class TypeExtensions
{
    /// <summary>
    /// Gets c# name of the type.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>type string.</returns>
    /// <remarks>https://stackoverflow.com/a/5971598.</remarks>
    public static string GetCSharpRepresentation(this Type type)
    {
        return GetCSharpRepresentation(type, new Queue<Type>(type.GetGenericArguments()));
    }

    private static string GetCSharpRepresentation(Type type, Queue<Type> availableArguments)
    {
        string value = type.Name;

        if (type.IsGenericParameter)
        {
            return value;
        }

        if (type.DeclaringType != null)
        {
            // This is a nested type, build the parent type first
            value = GetCSharpRepresentation(type.DeclaringType, availableArguments) + "+" + value;
        }

        if (type.IsGenericType)
        {
            value = value.Split('`')[0];

            // Build the type arguments (if any)
            string argString = string.Empty;
            var thisTypeArgs = type.GetGenericArguments();
            for (int i = 0; i < thisTypeArgs.Length && availableArguments.Count > 0; i++)
            {
                if (i != 0)
                {
                    argString += ", ";
                }

                argString += GetCSharpRepresentation(availableArguments.Dequeue());
            }

            // If there are type arguments, add them with < >
            if (argString.Length > 0)
            {
                value += "<" + argString + ">";
            }
        }

        return value;
    }

    /// <summary>
    /// Checks whether <paramref name="type"/> is nullable.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>true if nullable, false otherwise.</returns>
    public static bool IsNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
}