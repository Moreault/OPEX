namespace OPEX.Tests;

[TestClass]
public sealed class IndexesOfWithArrayOfDummyTests : IndexesOfTester<Dummy[]>
{

}

[TestClass]
public sealed class IndexesOfWithListOfDummyTests : IndexesOfTester<List<Dummy>>
{

}

[TestClass]
public sealed class IndexesOfWithWriteOnlyListOfDummyTests : IndexesOfTester<WriteOnlyList<Dummy>>
{

}

[TestClass]
public sealed class IndexesOfWithImmutableListOfDummyTests : IndexesOfTester<ImmutableList<Dummy>>
{

}

public abstract class IndexesOfTester<TCollection> : Tester where TCollection : class, IEnumerable<Dummy>
{
    [TestMethod]
    public void WhenUsingItemAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.IndexesOf(Fixture.Create<Dummy>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambdaAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.IndexesOf(Fixture.Create<Func<Dummy, bool>>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambdaAndLambdaIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.IndexesOf(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("match");
    }

    [TestMethod]
    public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Dummy>().To<TCollection, Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Dummy>().To<TCollection, Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
    {
        //Arrange
        var originalSource = Fixture.CreateMany<Dummy>().ToList();
        var source = originalSource.To<TCollection, Dummy>();
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
        var originalSource = Fixture.CreateMany<Dummy>().ToList();
        var source = originalSource.To<TCollection, Dummy>();
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
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).To<TCollection, Dummy>();

        //Act
        var result = source.IndexesOf(item);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
    }

    [TestMethod]
    public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).To<TCollection, Dummy>();

        //Act
        var result = source.IndexesOf(x => x.Name == item.Name);

        //Assert
        result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
    }
}