namespace OPEX.Tests;

[TestClass]
public sealed class TryPopFirst : Tester
{
    [TestMethod]
    public void WhenUsingParameterlessWithNullSource_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.TryPopFirst();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnEmptyCollection_ReturnDefault()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithOneElement_ShouldBeEmptyAfter()
    {
        //Arrange
        var source = new List<Dummy> { Fixture.Create<Dummy>() };

        //Act
        source.TryPopFirst();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithOneElement_ThenReturnRemovedElement()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy> { item };

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithMultipleElements_ShouldBeRemoved()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var item = source.First();

        //Act
        source.TryPopFirst();

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithMultipleElements_ThenReturnRemovedElement()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.First();

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.TryPopFirst(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Item_WhenSourceWithMultipleItemsButDoesNotContainItem_ReturnDefault()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_ReturnThatItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_RemoveFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var original = Fixture.CreateMany<Dummy>().ToList();
        var source = original.Append(item).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Item_WhenSourceContainsMultipleMatchingItems_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).Append(item).Append(item).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceContainsMultipleMatchingItems_RemoveOnlyTheFirstOccurence()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy>
        {
            Fixture.Create<Dummy>(),
            item,
            Fixture.Create<Dummy>(),
            Fixture.Create<Dummy>(),
            item,
            item
        };
        var original = source.ToList();

        //Act
        source.TryPopFirst(item);

        //Assert
        source.Should().BeEquivalentTo(new List<Dummy>
        {
            original[0],
            original[2],
            original[3],
            item,
            item
        });
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var predicate = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.TryPopFirst(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenMatchIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.TryPopFirst(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Dummy>();
        var predicate = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var result = source.TryPopFirst(predicate);

        //Assert
        result.Should().Be(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsButNoneThatMatchPredicate_ReturnDefault()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.TryPopFirst(x => x.Id < 0);

        //Assert
        result.Should().Be(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_ReturnItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.TryPopFirst(x => x.Name == item.Name);

        //Assert
        result.Should().Be(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_RemoveItemFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();
        var original = source.ToList();

        //Act
        source.TryPopFirst(x => x.Name == item.Name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Predicate_WhenContainsMultipleMatchingItems_ReturnTheFirstOne()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var itemsOfInterest = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var source = Fixture.CreateMany<Dummy>().Concat(itemsOfInterest).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(x => x.Name == name);

        //Assert
        result.Value.Should().BeOneOf(itemsOfInterest);
    }

    [TestMethod]
    public void Predicate_WhenContainsMultipleMatchingItems_RemoveItFromSource()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var itemsOfInterest = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var source = Fixture.CreateMany<Dummy>().Concat(itemsOfInterest).ToShuffled().ToList();
        var original = source.ToList();

        //Act
        var result = source.TryPopFirst(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != result.Value));
    }
}