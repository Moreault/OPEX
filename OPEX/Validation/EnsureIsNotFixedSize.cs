namespace ToolBX.OPEX.Validation;

internal static class ValidationExtensions
{
    public static bool IsFixedSize<TSource>(this IEnumerable<TSource> source)
    {
        return source is TSource[];
    }

}