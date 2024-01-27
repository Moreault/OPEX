namespace OPEX.Tests;

[TestClass]
public class ConcatWithArrayOfDummyTests : ConcatTester<Garbage[], Garbage>
{

}

[TestClass]
public class ConcatWithListOfDummyTests : ConcatTester<List<Garbage>, Garbage>
{

}

[TestClass]
public class ConcatWithWriteOnlyListOfDummyTests : ConcatTester<WriteOnlyList<Garbage>, Garbage>
{

}

[TestClass]
public class ConcatWithDictionaryOfDummyTests : ConcatTester<Dictionary<int, Garbage>, KeyValuePair<int, Garbage>>
{

}

public abstract class ConcatTester<TCollection, TSource> : TestBase where TCollection : class, IEnumerable<TSource>
{
    [TestMethod]
    public void WhenCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var items = Dummy.CreateMany<TSource>().ToArray();

        //Act
        var action = () => source.Concat(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("first");
    }

    [TestMethod]
    public void WhenItemsIsEmpty_DoNothing()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
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
        var source = Dummy.Create<TCollection>();
        var original = source.ToArray();
        var items = Dummy.CreateMany<TSource>().ToArray();

        var expected = original.ToList();
        foreach (var item in items)
            expected.Add(item);

        //Act
        var result = source.Concat(items);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
}