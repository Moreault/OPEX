namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IReadOnlyList<TSource> GetManyRandoms<TSource>(this IEnumerable<TSource> source, int count)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (count < 0) throw new ArgumentException(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, count));
        var list = source.ToList();

        count = Math.Clamp(count, 0, list.Count);
        if (count == 0) return Array.Empty<TSource>();
        if (count == list.Count) return list;

        var output = new List<TSource>();
        for (var i = 0; i < count; i++)
        {
            var random = list.GetRandom()!;
            list.Remove(random);
            output.Add(random);
        }
        return output;
    }
}