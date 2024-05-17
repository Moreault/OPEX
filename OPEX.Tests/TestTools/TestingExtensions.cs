namespace OPEX.Tests.TestTools;

public static class TestingExtensions
{
    public static TCollection To<TCollection, TSource>(this IEnumerable<TSource> source) where TCollection : class, IEnumerable<TSource>
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (typeof(TCollection) == typeof(List<TSource>)) return (source.ToList() as TCollection)!;
        if (typeof(TCollection) == typeof(TSource[])) return (source.ToArray() as TCollection)!;
        if (typeof(TCollection) == typeof(ImmutableList<TSource>)) return (source.ToImmutableList() as TCollection)!;
        if (typeof(TCollection) == typeof(WriteOnlyList<TSource>)) return (source.ToWriteOnlyList() as TCollection)!;
        throw new NotImplementedException();
    }
}

[TestClass]
public class TestingExtensionsTest : Tester
{
    [TestMethod]
    public void ToList_ReturnList()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>();

        //Act
        var result = source.To<List<Garbage>, Garbage>();

        //Assert
        result.Should().BeOfType<List<Garbage>>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToArray_ReturnArray()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>();

        //Act
        var result = source.To<Garbage[], Garbage>();

        //Assert
        result.Should().BeOfType<Garbage[]>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToWriteOnlyList_ReturnWriteOnlyList()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>();

        //Act
        var result = source.To<WriteOnlyList<Garbage>, Garbage>();

        //Assert
        result.Should().BeOfType<WriteOnlyList<Garbage>>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToImmutableList_ReturnImmutableList()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>();

        //Act
        var result = source.To<ImmutableList<Garbage>, Garbage>();

        //Assert
        result.Should().BeOfType<ImmutableList<Garbage>>();
        result.Should().BeEquivalentTo(source);
    }
}