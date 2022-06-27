namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, params T[] items) => collection.Concat((IEnumerable<T>)items);
}