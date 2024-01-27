namespace OPEX.Tests;

[TestClass]
public sealed class PopLastOrDefault : TestBase
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;

        //Act
        var action = () => source.PopLastOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnDefault()
    {
        //Arrange
        var source = new List<Garbage>();

        //Act
        var result = source.PopLastOrDefault();

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsNotEmpty_ReturnLastItem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        IList<Garbage> source = null!;
        var item = Dummy.Create<Garbage>();

        //Act
        var action = () => source.PopLastOrDefault(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenSourceDoesNotContainItem_ReturnNull()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenThereIsOnlyOneOccurenceOfItem_ReturnOccurence()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Concat(item, item, item).ToShuffled().ToList();

        //Act
        var result = source.PopLastOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenThereIsMoreThanOneOccurenceOfItem_RemoveTheLastOne()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var sourceWithoutItems = Dummy.CreateMany<Garbage>().ToList();
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
        IList<Garbage> source = null!;
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.PopLastOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.PopLastOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var result = source.PopLastOrDefault(match);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_DoNotModifySource()
    {
        //Arrange
        var source = new List<Garbage>();
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        source.PopLastOrDefault(match);

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Predicate_WhenNoItemInSourceMatchesPredicate_ReturnNull()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = source.PopLastOrDefault(x => x.Id < 0);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenNoItemInSourceMatchesPredicate_DoNotModifySource()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var source = Dummy.CreateMany<Garbage>().ToList();
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
        var name = Dummy.Create<string>();
        var items = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany(3).ToList();
        var source = new List<Garbage>
        {
            items[0],
            Dummy.Create<Garbage>(),
            items[1],
            Dummy.Create<Garbage>(),
            items[2],
            Dummy.Create<Garbage>()
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
        var name = Dummy.Create<string>();
        var items = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany(3).ToList();
        var source = new List<Garbage>
        {
            items[0],
            Dummy.Create<Garbage>(),
            items[1],
            Dummy.Create<Garbage>(),
            items[2],
            Dummy.Create<Garbage>()
        };
        var original = source.ToList();

        //Act
        source.PopLastOrDefault(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(new List<Garbage>
        {
            items[0],
            original[1],
            items[1],
            original[3],
            original[5]
        });
    }
}