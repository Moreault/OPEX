namespace OPEX.Tests;

[TestClass]
public class AddRangeWithListOfDummyTests : AddRangeTester<List<Garbage>, Garbage>
{
    [TestMethod]
    public void WhenUsingParamsAndItemsIsEmpty_DoNothing()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = Array.Empty<Garbage>();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenUsingEnumerableAndItemsIsEmpty_DoNothing()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = new List<Garbage>();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenUsingParamsAndItemsContainsOnlyOneItem_AddOneItem()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = new[] { Dummy.Create<Garbage>() };

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndItemsContainsOnlyOneItem_AddOneItem()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = new List<Garbage> { Dummy.Create<Garbage>() };

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingParamsAndMultipleItems_AddAllItems()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = Dummy.CreateMany<Garbage>().ToArray();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndMultipleItems_AddAllItems()
    {
        //Arrange
        IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var items = Dummy.CreateMany<Garbage>().ToList();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }
}

[TestClass]
public class AddRangeWithArrayOfDummyTests : AddRangeTester<Garbage[], Garbage>
{
    [TestMethod]
    public void WhenUsingParams_Throw()
    {
        //Arrange
        var source = Dummy.Create<Garbage[]>();
        var items = Dummy.CreateMany<Garbage>().ToArray();

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.AddRange)));
    }
}

public abstract class AddRangeTester<TCollection, TSource> : Tester where TCollection : class, IList<TSource>
{
    [TestMethod]
    public void WhenUsingParamsAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var items = Dummy.CreateMany<TSource>().ToArray();

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var items = Dummy.CreateMany<TSource>().ToList();

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingParamsAndItemsAreNull_Throw()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
        TCollection items = null!;

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(items));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndItemsAreNull_Throw()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
        IEnumerable<TSource> items = null!;

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(items));
    }
}