using System.Collections;

namespace OPEX.Tests.Dummies;

/// <summary>
/// Collection that does not implement IList or IReadOnlyList and only IEnumerable
/// </summary>
public record WeirdCollection<T> : IEnumerable<T>
{
    private readonly List<T> _items;

    public T this[int index] => _items[index];

    public WeirdCollection()
    {
        _items = new List<T>();
    }

    public WeirdCollection(IEnumerable<T> source)
    {
        _items = source?.ToList() ?? throw new ArgumentNullException(nameof(source));
    }

    public void Add(T item) => _items.Add(item);

    public void Clear() => _items.Clear();

    public void AddRange(params T[] items) => _items.AddRange(items);

    public void Insert(int index, T item) => _items.Insert(index, item);

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}