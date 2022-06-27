namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void AddRange<T>(this IList<T> collection, params T[] items) => collection.AddRange((IEnumerable<T>)items);

    public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (items == null) throw new ArgumentNullException(nameof(items));
        if (collection.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(AddRange)));
        foreach (var item in items)
            collection.Add(item);
    }
}