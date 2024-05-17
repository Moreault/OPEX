namespace OPEX.Tests;

[TestClass]
public class SingleIndexOf : Tester
{
    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;

        //Act
        var action = () => source.SingleIndexOf(Dummy.Create<Garbage>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Garbage>();

        //Act
        var action = () => source.SingleIndexOf(Dummy.Create<Garbage>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Item_WhenThereIsNoOccurenceOfItemInSource_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = Dummy.Create<Garbage>();

        //Act
        var action = () => source.SingleIndexOf(item);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Item_WhenThereIsMoreThanOneOccurenceInSource_Throw()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Concat(item, item).ToShuffled().ToList();

        //Act
        var action = () => source.SingleIndexOf(item);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void Item_WhenThereIsExactlyOneOccurenceInSource_ReturnIndex(int index)
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>(3).ToList();
        source.Insert(index, item);

        //Act
        var result = source.SingleIndexOf(item);

        //Assert
        result.Should().Be(index);
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Garbage> source = null!;
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.SingleIndexOf(match);

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
        var action = () => source.SingleIndexOf(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Garbage>();
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.SingleIndexOf(match);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Predicate_WhenNoItemInSourceMatchesPredicate_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        
        //Act
        var action = () => source.SingleIndexOf(x => x.Name == Dummy.Create<string>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Predicate_WhenMultipleItemInSourceMatchPredicate_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var action = () => source.SingleIndexOf(x => x.Id > 0);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void Predicate_WhenExactlyOneItemInSourceMatchesPredicate_ReturnIndexOfMatchingItem(int index)
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = Dummy.Create<Garbage>();
        source.Insert(index, item);

        //Act
        var result = source.SingleIndexOf(x => x.Id == item.Id);

        //Assert
        result.Should().Be(index);
    }
}