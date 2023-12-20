using System.Collections;

namespace OPEX.Tests.TestTools;

public static class WriteOnlyListExtensions
{
    public static WriteOnlyList<T> ToWriteOnlyList<T>(this IEnumerable<T> collection) => new(collection);
}

//To test scenarios where someone would have created a custom collection that implements IList<T> while not implementing IReadOnlyList<T>
public class WriteOnlyList<T> : IList<T>
{
    private readonly List<T> _items;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
    public int Count => _items.Count;
    public bool IsReadOnly => ((IList<T>)_items).IsReadOnly;

    public WriteOnlyList()
    {
        _items = new List<T>();
    }

    public WriteOnlyList(IEnumerable<T> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));
        _items = items as List<T> ?? items.ToList();
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item) => _items.Add(item);

    public void Clear() => _items.Clear();

    public bool Contains(T item) => _items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _items.Remove(item);

    public int IndexOf(T item) => _items.IndexOf(item);

    public void Insert(int index, T item) => _items.Insert(index, item);

    public void RemoveAt(int index) => _items.RemoveAt(index);
}