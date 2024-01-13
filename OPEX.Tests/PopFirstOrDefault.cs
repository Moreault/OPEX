namespace OPEX.Tests;

[TestClass]
public sealed class PopFirstOrDefault : Tester
{
    [TestMethod]
    public void WhenUsingParameterlessWithNullSource_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopFirstOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingParameterlessOnEmptyCollection_ReturnDefault()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.PopFirstOrDefault();

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithOneElement_ShouldBeEmptyAfter()
    {
        //Arrange
        var source = new List<Dummy> { Fixture.Create<Dummy>() };

        //Act
        source.PopFirstOrDefault();

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
        var result = source.PopFirstOrDefault();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void WhenUsingParameterlessOnCollectionWithMultipleElements_ShouldBeRemoved()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var item = source.First();

        //Act
        source.PopFirstOrDefault();

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
        var result = source.PopFirstOrDefault();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.PopFirstOrDefault(item);

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
        var result = source.PopFirstOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenSourceWithMultipleItemsButDoesNotContainItem_ReturnDefault()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopFirstOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_ReturnThatItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopFirstOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenSourceContainsOneMatchingItem_RemoveFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var original = Fixture.CreateMany<Dummy>().ToList();
        var source = original.Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopFirstOrDefault(item);

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
        var result = source.PopFirstOrDefault(item);

        //Assert
        result.Should().Be(item);
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
        source.PopFirstOrDefault(item);

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
        var action = () => source.PopFirstOrDefault(predicate);

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
        var action = () => source.PopFirstOrDefault(match);

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
        var result = source.PopFirstOrDefault(predicate);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsButNoneThatMatchPredicate_ReturnDefault()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.PopFirstOrDefault(x => x.Id < 0);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_ReturnItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.PopFirstOrDefault(x => x.Name == item.Name);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenContainsItemsAndOneMatchesPredicate_RemoveItemFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();
        var original = source.ToList();

        //Act
        source.PopFirstOrDefault(x => x.Name == item.Name);

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
        var result = source.PopFirstOrDefault(x => x.Name == name);

        //Assert
        result.Should().BeOneOf(itemsOfInterest);
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
        var result = source.PopFirstOrDefault(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != result));
    }
}