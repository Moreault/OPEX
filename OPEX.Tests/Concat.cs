namespace OPEX.Tests;

[TestClass]
public class ConcatWithArrayOfDummyTests : ConcatTester<Dummy[], Dummy>
{

}

[TestClass]
public class ConcatWithListOfDummyTests : ConcatTester<List<Dummy>, Dummy>
{

}

[TestClass]
public class ConcatWithWriteOnlyListOfDummyTests : ConcatTester<WriteOnlyList<Dummy>, Dummy>
{

}

[TestClass]
public class ConcatWithDictionaryOfDummyTests : ConcatTester<Dictionary<int, Dummy>, KeyValuePair<int, Dummy>>
{

}

public abstract class ConcatTester<TCollection, TSource> : Tester where TCollection : class, IEnumerable<TSource>
{
    [TestMethod]
    public void WhenCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var items = Fixture.CreateMany<TSource>().ToArray();

        //Act
        var action = () => source.Concat(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("first");
    }

    [TestMethod]
    public void WhenItemsIsEmpty_DoNothing()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();
        var original = source.ToArray();
        var items = Array.Empty<TSource>();

        //Act
        var result = source.Concat(items);

        //Assert
        result.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenThereAreMultipleItems_AddThemAll()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();
        var original = source.ToArray();
        var items = Fixture.CreateMany<TSource>().ToArray();

        var expected = original.ToList();
        foreach (var item in items)
            expected.Add(item);

        //Act
        var result = source.Concat(items);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
}