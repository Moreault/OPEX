namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int GetRandomIndex<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source as IList<TSource> ?? source.ToArray();
        return list.Count == 0 ? -1 : new Random().Next(list.Count);
    }
}