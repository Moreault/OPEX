namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns a random item from the collection.
    /// </summary>
    public static TSource PopRandom<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var index = source.GetRandomIndex();
        return source.PopAt(index);
    }
}