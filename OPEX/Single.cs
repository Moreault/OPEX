namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the only element of a sequence, and throws an exception with a custom message if there is not exactly one element in the sequence.
    /// </summary>
    public static TSource Single<TSource>(this IEnumerable<TSource> source, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.Single();
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }

    /// <summary>
    /// Returns the only element of a sequence, and throws an exception with a custom message if there is not exactly one element in the sequence.
    /// </summary>
    public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.Single(predicate);
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }
}