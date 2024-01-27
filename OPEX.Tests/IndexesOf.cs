namespace OPEX.Tests;

[TestClass]
public sealed class IndexesOfWithArrayOfDummyTests : IndexesOfTester<Garbage[]>
{

}

[TestClass]
public sealed class IndexesOfWithListOfDummyTests : IndexesOfTester<List<Garbage>>
{

}

[TestClass]
public sealed class IndexesOfWithWriteOnlyListOfDummyTests : IndexesOfTester<WriteOnlyList<Garbage>>
{

}

[TestClass]
public sealed class IndexesOfWithImmutableListOfDummyTests : IndexesOfTester<ImmutableList<Garbage>>
{

}

public abstract class IndexesOfTester<TCollection> : TestBase where TCollection : class, IEnumerable<Garbage>
{
    [TestMethod]
    public void WhenUsingItemAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.IndexesOf(Dummy.Create<Garbage>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambdaAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.IndexesOf(Dummy.Create<Func<Garbage, bool>>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambdaAndLambdaIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.IndexesOf(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("match");
    }

    [TestMethod]
    public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Garbage>().To<TCollection, Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Garbage>().To<TCollection, Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
    {
        //Arrange
        var originalSource = Dummy.CreateMany<Garbage>().ToList();
        var source = originalSource.To<TCollection, Garbage>();
        var itemIndex = source.GetRandomIndex();
        var item = originalSource[itemIndex];

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { itemIndex });
    }

    [TestMethod]
    public void WhenUsingLambdaAndThereIsOneCorrespondingItem_ReturnSingleItem()
    {
        //Arrange
        var originalSource = Dummy.CreateMany<Garbage>().ToList();
        var source = originalSource.To<TCollection, Garbage>();
        var itemIndex = source.GetRandomIndex();
        var item = originalSource[itemIndex];

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { itemIndex });
    }

    [TestMethod]
    public void WhenUsingItemAndThereAreMultipleOccurences_ReturnAllOccurences()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Concat(item, Dummy.Create<Garbage>(), item, item, Dummy.Create<Garbage>(), Dummy.Create<Garbage>()).To<TCollection, Garbage>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
    }

    [TestMethod]
    public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Concat(item, Dummy.Create<Garbage>(), item, item, Dummy.Create<Garbage>(), Dummy.Create<Garbage>()).To<TCollection, Garbage>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
    }
}