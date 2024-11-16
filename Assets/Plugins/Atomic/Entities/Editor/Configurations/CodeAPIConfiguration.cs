namespace Atomic.Entities
{
    public sealed class CodeAPIConfiguration
    {
        // public static string GetTypeName(Type type)
        // {
        //     if (type == typeof(int))
        //         return "int";
        //
        //     if (type == typeof(float))
        //         return "float";
        //
        //     if (type == typeof(bool))
        //         return "bool";
        //
        //     if (type == typeof(string))
        //         return "string";
        //
        //     string typeName = type.Name;
        //     if (!type.IsGenericType)
        //         return typeName;
        //
        //     Type[] genericTypes = type.GetGenericArguments();
        //     int genericCount = genericTypes.Length;
        //     StringBuilder genericArguments = new StringBuilder();
        //
        //     for (int i = 0; i < genericCount; i++)
        //     {
        //         Type genericType = genericTypes[i];
        //         string genericName = GetTypeName(genericType);
        //         genericArguments.Append(genericName);
        //         
        //         if (i < genericCount - 1) 
        //             genericArguments.Append(", ");
        //     }
        //
        //     return $"{typeName[..typeName.IndexOf("`", StringComparison.Ordinal)]}"
        //            + $"<{genericArguments}>";
        // }
    }
}