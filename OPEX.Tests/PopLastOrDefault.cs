namespace OPEX.Tests;

[TestClass]
public sealed class PopLastOrDefault : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopLastOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.PopLastOrDefault();

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsNotEmpty_ReturnLastItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.PopLastOrDefault();

        //Assert
        result.Should().Be(original.Last());
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsNotEmpty_SourceShouldContainAllButLastItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        source.PopLastOrDefault();

        //Assert
        source.Should().BeEquivalentTo(original.Take(original.Count - 1));
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.PopLastOrDefault(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenSourceDoesNotContainItem_ReturnNull()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenThereIsOnlyOneOccurenceOfItem_ReturnOccurence()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenThereIsOnlyOneOccurenceOfItem_RemoveItemFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();
        var original = source.ToList();

        //Act
        source.PopLastOrDefault(item);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Item_WhenThereIsMoreThanOneOccurenceOfItem_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Concat(item, item, item).ToShuffled().ToList();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenThereIsMoreThanOneOccurenceOfItem_RemoveTheLastOne()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>().ToList();
        var source = sourceWithoutItems.Concat(item, item, item).ToShuffled().ToList();

        //Act
        source.PopLastOrDefault(item);

        //Assert
        source.Should().BeEquivalentTo(sourceWithoutItems.Concat(item, item));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.PopLastOrDefault(match);

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
        var action = () => source.PopLastOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Dummy>();
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var result = source.PopLastOrDefault(match);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_DoNotModifySource()
    {
        //Arrange
        var source = new List<Dummy>();
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        source.PopLastOrDefault(match);

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Predicate_WhenNoItemInSourceMatchesPredicate_ReturnNull()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.PopLastOrDefault(x => x.Id < 0);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenNoItemInSourceMatchesPredicate_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        source.PopLastOrDefault(x => x.Id < 0);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Predicate_WhenOneItemMatchesPredicate_ReturnItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.PopLastOrDefault(x => x.Id == item.Id);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenOneItemMatchesPredicate_RemoveItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();
        var original = source.ToList();

        //Act
        source.PopLastOrDefault(x => x.Id == item.Id);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Predicate_WhenMultipleItemsMatchPredicate_ReturnLastOccurenceOnly()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany(3).ToList();
        var source = new List<Dummy>
        {
            items[0],
            Fixture.Create<Dummy>(),
            items[1],
            Fixture.Create<Dummy>(),
            items[2],
            Fixture.Create<Dummy>()
        };

        //Act
        var result = source.PopLastOrDefault(x => x.Name == name);

        //Assert
        result.Should().Be(items.Last());
    }

    [TestMethod]
    public void Predicate_WhenMultipleItemsMatchPredicate_RemoveItem()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany(3).ToList();
        var source = new List<Dummy>
        {
            items[0],
            Fixture.Create<Dummy>(),
            items[1],
            Fixture.Create<Dummy>(),
            items[2],
            Fixture.Create<Dummy>()
        };
        var original = source.ToList();

        //Act
        source.PopLastOrDefault(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(new List<Dummy>
        {
            items[0],
            original[1],
            items[1],
            original[3],
            original[5]
        });
    }
}