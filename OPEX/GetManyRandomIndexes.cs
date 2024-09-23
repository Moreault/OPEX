namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns multiple unique indexes from a collection or throws if count is greater than collection count.
    /// </summary>
    public static IReadOnlyList<int> GetManyRandomIndexes<TSource>(this IEnumerable<TSource> source, int count)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        var list = source as IList<TSource> ?? source.ToArray();
        if (count > list.Count) throw new ArgumentOutOfRangeException(nameof(count));
        return list.TryGetManyRandomIndexes(count);
    }

    public static IReadOnlyList<int> GetManyRandomIndexes<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return source.Where(predicate).GetManyRandomIndexes(count);
    }

    /// <summary>
    /// Attempts to return multiple unique indexes from a collection or all indexes if count is greater than collection count.
    /// </summary>
    public static IReadOnlyList<int> TryGetManyRandomIndexes<TSource>(this IEnumerable<TSource> source, int count)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (count <= 0) return Array.Empty<int>();

        var list = source as IList<TSource> ?? source.ToArray();
        if (count >= list.Count)
            return Enumerable.Range(0, list.Count).ToArray();

        var found = 0;
        var indexes = new List<int>();
        while (found < count)
        {
            var index = list.GetRandomIndex();
            if (indexes.Contains(index)) continue;
            indexes.Add(index);
            found++;
        }

        return indexes;
    }

    public static IReadOnlyList<int> TryGetManyRandomIndexes<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return source.Where(predicate).TryGetManyRandomIndexes(count);
    }

}