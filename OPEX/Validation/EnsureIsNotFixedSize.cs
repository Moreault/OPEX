namespace ToolBX.OPEX.Validation;

internal static class ValidationExtensions
{
    public static bool IsFixedSize<T>(this IEnumerable<T> collection)
    {
        return collection is T[];
    }

}