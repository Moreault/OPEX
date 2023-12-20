namespace OPEX.Tests;

[TestClass]
public sealed class TryPopLast : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.TryPopLast();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnFailure()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.TryPopLast();

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasMultipleElements_ReturnLastElement()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.TryPopLast();

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(original.Last()));
    }

    [TestMethod]
    public void Parameterless_WhenSourceHasMultipleElements_LastElementIsRemovedFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        source.TryPopLast();

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != original.Last()));
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.TryPopLast(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnFailure()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.TryPopLast(Fixture.Create<Dummy>());

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Item_WhenThereAreItemsButNoMatch_ReturnFailure()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.TryPopLast(Fixture.Create<Dummy>());

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Item_WhenThereIsOneMatch_ReturnMatch()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.TryPopLast(item);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Item_WhenThereIsOneMatch_RemoveFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();
        var original = source.ToList();

        //Act
        source.TryPopLast(item);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }

    [TestMethod]
    public void Item_WhenThereAreMultipleMatches_ReturnLastMatch()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Concat(item, item, item).ToShuffled().ToList();

        //Act
        var result = source.TryPopLast(item);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Item_WhenThereAreMutlipleMatches_RemoveLastMatchFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>().ToList();
        var source = sourceWithoutItems.Concat(item, item, item).ToShuffled().ToList();

        //Act
        source.TryPopLast(item);

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
        var action = () => source.TryPopLast(match);

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
        var action = () => source.TryPopLast(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ReturnFailure()
    {
        //Arrange
        var source = new List<Dummy>();
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var result = source.TryPopLast(match);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenSourceHasNoMatch_ReturnFailure()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.TryPopLast(x => x.Name == Fixture.Create<string>());

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void Predicate_WhenSourceHasOnlyOneMatch_ReturnIt()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        var result = source.TryPopLast(x => x.Name == item.Name);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(item));
    }

    [TestMethod]
    public void Predicate_WhenSourceHasOnlyOneMatch_RemoveItFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = source.GetRandom();

        //Act
        source.TryPopLast(x => x.Name == item.Name);

        //Assert
        source.Should().NotContain(item);
    }

    [TestMethod]
    public void Predicate_WhenSourceHasMultipleMatches_ReturnLastOne()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>(3).ToList();
        var source = new List<Dummy>
        {
            items[0],
            sourceWithoutItems[0],
            items[1],
            sourceWithoutItems[1],
            items[2],
            sourceWithoutItems[2]
        };

        //Act
        var result = source.TryPopLast(x => x.Name == name);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(items.Last()));
    }

    [TestMethod]
    public void Predicate_WhenSourceHasMultipleMatches_RemoveLastOne()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>(3).ToList();
        var source = new List<Dummy>
        {
            items[0],
            sourceWithoutItems[0],
            items[1],
            sourceWithoutItems[1],
            items[2],
            sourceWithoutItems[2]
        };

        //Act
        source.TryPopLast(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(new List<Dummy>
        {
            items[0],
            sourceWithoutItems[0],
            items[1],
            sourceWithoutItems[1],
            sourceWithoutItems[2]
        });
    }

}