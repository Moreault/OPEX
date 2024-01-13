namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static TSource? SingleOrDefault<TSource>(this IEnumerable<TSource> source, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.SingleOrDefault();
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }

    public static TSource? SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.SingleOrDefault(predicate);
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }
}