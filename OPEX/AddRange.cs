namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Adds the specified items to the end of the collection.
    /// </summary>
    public static void AddRange<TSource>(this IList<TSource> source, params TSource[] items) => source.AddRange((IEnumerable<TSource>)items);

    /// <summary>
    /// Adds the specified items to the end of the collection.
    /// </summary>
    public static void AddRange<TSource>(this IList<TSource> source, IEnumerable<TSource> items)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (items == null) throw new ArgumentNullException(nameof(items));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(AddRange)));
        foreach (var item in items)
            source.Add(item);
    }
}