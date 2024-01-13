namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static Result<TSource> TryPopLast<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.TryPopAt(source.LastIndex());
    }

    public static Result<TSource> TryPopLast<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var index = source.LastIndexOf(item);
        return source.TryPopAt(index);
    }

    public static Result<TSource> TryPopLast<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var index = source.LastIndexOf(match);
        return source.TryPopAt(index);
    }
}