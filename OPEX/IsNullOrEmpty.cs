namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource>? source) => source == null || !source.Any();
}