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
        var source = Fixture.CreateMany<Dummy>();

        //Act
        var result = source.To<List<Dummy>, Dummy>();

        //Assert
        result.Should().BeOfType<List<Dummy>>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToArray_ReturnArray()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>();

        //Act
        var result = source.To<Dummy[], Dummy>();

        //Assert
        result.Should().BeOfType<Dummy[]>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToWriteOnlyList_ReturnWriteOnlyList()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>();

        //Act
        var result = source.To<WriteOnlyList<Dummy>, Dummy>();

        //Assert
        result.Should().BeOfType<WriteOnlyList<Dummy>>();
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void ToImmutableList_ReturnImmutableList()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>();

        //Act
        var result = source.To<ImmutableList<Dummy>, Dummy>();

        //Assert
        result.Should().BeOfType<ImmutableList<Dummy>>();
        result.Should().BeEquivalentTo(source);
    }
}