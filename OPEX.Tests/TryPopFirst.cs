namespace OPEX.Tests;

[TestClass]
public sealed class TryPopFirst : TestBase
{
    [TestMethod]
    public void WhenUsingParameterlessWithNullSource_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;

        //Act
        var action = () => source.TryPopFirst();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnEmptyCollection_ReturnDefault()
    {
        //Arrange
        var source = new List<Garbage>();

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithOneElement_ShouldBeEmptyAfter()
    {
        //Arrange
        var source = new List<Garbage> { Dummy.Create<Garbage>() };

        //Act
        source.TryPopFirst();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithOneElement_ThenReturnRemovedElement()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = new List<Garbage> { item };

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Garbage>.Success(item));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithMultipleElements_ShouldBeRemoved()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = source.First();

        //Act
        var result = source.TryPopFirst();

        //Assert
        result.Should().Be(Result<Garbage>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;
        var item = Dummy.Create<Garbage>();

        //Act
        var action = () => source.TryPopFirst(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void Item_WhenSourceWithMultipleItemsButDoesNotContainItem_ReturnDefault()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_ReturnThatItem()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Garbage>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_RemoveFromSource()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var original = Dummy.CreateMany<Garbage>().ToList();
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
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).Append(item).Append(item).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(item);

        //Assert
        result.Should().Be(Result<Garbage>.Success(item));
    }

    [TestMethod]
    public void Item_WhenSourceContainsMultipleMatchingItems_RemoveOnlyTheFirstOccurence()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = new List<Garbage>
        {
            Dummy.Create<Garbage>(),
            item,
            Dummy.Create<Garbage>(),
            Dummy.Create<Garbage>(),
            item,
            item
        };
        var original = source.ToList();

        //Act
        source.TryPopFirst(item);

        //Assert
        source.Should().BeEquivalentTo(new List<Garbage>
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
        IList<Garbage> source = null!;
        var predicate = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.TryPopFirst(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenMatchIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.TryPopFirst(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Garbage>();
        var predicate = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var result = source.TryPopFirst(predicate);

        //Assert
        result.Should().Be(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsButNoneThatMatchPredicate_ReturnDefault()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = source.TryPopFirst(x => x.Id < 0);

        //Assert
        result.Should().Be(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_ReturnItem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.TryPopFirst(x => x.Name == item.Name);

        //Assert
        result.Should().Be(Result<Garbage>.Success(item));
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_RemoveItemFromSource()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var name = Dummy.Create<string>();
        var itemsOfInterest = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany().ToList();
        var source = Dummy.CreateMany<Garbage>().Concat(itemsOfInterest).ToShuffled().ToList();

        //Act
        var result = source.TryPopFirst(x => x.Name == name);

        //Assert
        result.Value.Should().BeOneOf(itemsOfInterest);
    }

    [TestMethod]
    public void Predicate_WhenContainsMultipleMatchingItems_RemoveItFromSource()
    {
        //Arrange
        var name = Dummy.Create<string>();
        var itemsOfInterest = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany().ToList();
        var source = Dummy.CreateMany<Garbage>().Concat(itemsOfInterest).ToShuffled().ToList();
        var original = source.ToList();

        //Act
        var result = source.TryPopFirst(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != result.Value));
    }
}