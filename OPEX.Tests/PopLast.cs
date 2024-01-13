namespace OPEX.Tests;

[TestClass]
public class PopLast : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopLast();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var action = () => source.PopLast();

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasOnlyOneItem_ShouldBeEmptyAfter()
    {
        //Arrange
        var source = new List<Dummy> { Fixture.Create<Dummy>() };

        //Act
        source.PopLast();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasOnlyOneItem_ThenReturnRemovedElement()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy> { item };

        //Act
        var result = source.PopLast();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasMultipleItems_LastItemShouldBeRemoved()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var item = source.Last();

        //Act
        source.PopLast();

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasMultipleItems_ThenReturnRemovedElement()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.Last();

        //Act
        var result = source.PopLast();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopLast((Dummy)null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceContainsOnlyOneItem_ReturnSingleItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy> { item };

        //Act
        var result = source.PopLast(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenSourceContainsOnlyOneItem_SourceShouldBeEmpty()
    {
        //Arrange
        var source = new List<Dummy> { Fixture.Create<Dummy>() };

        //Act
        source.PopLast(source.Single());

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Item_WhenSourceContainsMutlipleItems_ReturnLastItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.PopLast(source.Last());

        //Assert
        result.Should().Be(original.Last());
    }

    [TestMethod]
    public void Item_WhenSourceContainsMultipleItems_RemoveLastItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        source.PopLast(source.Last());

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != original.Last()));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var predicate = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.PopLast(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.PopLast(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceHasItemsButNoneMatchPredicate_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var action = () => source.PopLast(x => x.Name == Fixture.Create<string>());

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestMethod]
    public void Predicate_WhenSourceContainsItemsAndOnlyOneMatchesPredicate_ReturnItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.Last();

        //Act
        var result = source.PopLast(x => x.Name == item.Name);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenSourceContainsItemsAndOnlyOneMatchesPredicate_RemoveItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.Last();
        var original = source.ToList();

        //Act
        source.PopLast(x => x.Name == item.Name);
        
        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Predicate_WhenSourceContainsItemsAndMultipleMatchesWithPredicate_ReturnLastItemMatch()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var itemsOfInterest = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var source = Fixture.CreateMany<Dummy>().Concat(itemsOfInterest).Concat(Fixture.CreateMany<Dummy>()).ToList();

        //Act
        var result = source.PopLast(x => x.Name == name);

        //Assert
        result.Should().Be(itemsOfInterest.Last());
    }

    [TestMethod]
    public void Predicate_WhenSourceContainsItemsAndMultipleMatchesWithPredicate_RemoveLastItemMatch()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var itemsOfInterest = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var source = Fixture.CreateMany<Dummy>().Concat(itemsOfInterest).Concat(Fixture.CreateMany<Dummy>()).ToList();
        var original = source.ToList();

        //Act
        source.PopLast(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != itemsOfInterest.Last()));
    }
}