namespace OPEX.Tests;

[TestClass]
public sealed class SingleOrDefault : TestBase
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_ThrowArgumentNullException()
    {
        //Arrange
        IEnumerable<Garbage> source = null!;
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => source.SingleOrDefault(customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();
        var customMessage = Dummy.Create<string>();

        //Act
        var result = source.SingleOrDefault(customMessage);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Parameterless_WhenContainsItems_ThrowWithCustomMessage()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => source.SingleOrDefault(customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Parameterless_WhenThereIsExactlyOneItem_ReturnItem()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = new List<Garbage>
        {
            item
        };
        var customMessage = Dummy.Create<string>();

        //Act
        var result = source.SingleOrDefault(customMessage);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_ThrowArgumentNullException()
    {
        //Arrange
        IEnumerable<Garbage> source = null!;
        var predicate = Dummy.Create<Func<Garbage, bool>>();
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => source.SingleOrDefault(predicate, customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_ThrowArgumentNullException()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        Func<Garbage, bool> predicate = null!;
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => source.SingleOrDefault(predicate, customMessage);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
    }

    [TestMethod] public void Predicate_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();
        var predicate = Dummy.Create<Func<Garbage, bool>>();
        var customMessage = Dummy.Create<string>();

        //Act
        var result = source.SingleOrDefault(predicate, customMessage);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenSourceHasNoMatchForPredicate_ReturnNull()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var customMessage = Dummy.Create<string>();

        //Act
        var result = source.SingleOrDefault(x => x.Id < 0, customMessage);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenSourceHasOneMatchForPredicate_ReturnMatchingItem()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToList();
        var customMessage = Dummy.Create<string>();

        //Act
        var result = source.SingleOrDefault(x => x.Id == item.Id, customMessage);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenSourceHasMultipleMatchesForPredicate_ThrowWithCustomMessage()
    {
        //Arrange
        var name = Dummy.Create<string>();
        var items = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany().ToList();
        var source = Dummy.CreateMany<Garbage>().Concat(items).ToList();
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => source.SingleOrDefault(x => x.Name == name, customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }
}