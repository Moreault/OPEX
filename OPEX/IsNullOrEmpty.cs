namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? u) => u == null || !u.Any();
}