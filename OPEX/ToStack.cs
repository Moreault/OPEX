namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Converts the collection into a generic stack.
    /// </summary>
    public static Stack<T> ToStack<T>(this IEnumerable<T> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        
        var stack = new Stack<T>();
        foreach (var item in source)
            stack.Push(item);
        return stack;
    }
}