namespace OPEX.Tests;

[TestClass]
public sealed class Single : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_ThrowArgumentNullException()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ThrowWithCustomMessage()
    {
        //Arrange
        var source = new List<Dummy>();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Parameterless_WhenThereIsExactlyOneItem_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy>
        {
            item
        };
        var customMessage = Fixture.Create<string>();

        //Act
        var result = source.Single(customMessage);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Parameterless_WhenContainsItems_ThrowWithCustomMessage()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_ThrowArgumentNullException()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var predicate = Fixture.Create<Func<Dummy, bool>>();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(predicate, customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_ThrowArgumentNullException()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> predicate = null!;
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(predicate, customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_ThrowWithCustomMessage()
    {
        //Arrange
        var source = new List<Dummy>();
        var predicate = Fixture.Create<Func<Dummy, bool>>();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(predicate, customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Predicate_WhenSourceHasNoMatchForPredicate_ThrowWithCustomMessage()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(x => x.Id < 0, customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Predicate_WhenSourceHasOneMatchForPredicate_ReturnMatchingItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToList();
        var customMessage = Fixture.Create<string>();

        //Act
        var result = source.Single(x => x.Id == item.Id, customMessage);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenSourceHasMultipleMatchesForPredicate_ThrowWithCustomMessage()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var source = Fixture.CreateMany<Dummy>().Concat(items).ToList();
        var customMessage = Fixture.Create<string>();

        //Act
        var action = () => source.Single(x => x.Name == name, customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }
}