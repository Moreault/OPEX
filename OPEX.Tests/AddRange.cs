namespace OPEX.Tests;

[TestClass]
public class AddRangeWithListOfDummyTests : AddRangeTester<List<Dummy>, Dummy>
{
    [TestMethod]
    public void WhenUsingParamsAndItemsIsEmpty_DoNothing()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = Array.Empty<Dummy>();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenUsingEnumerableAndItemsIsEmpty_DoNothing()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = new List<Dummy>();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenUsingParamsAndItemsContainsOnlyOneItem_AddOneItem()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = new[] { Fixture.Create<Dummy>() };

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndItemsContainsOnlyOneItem_AddOneItem()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = new List<Dummy> { Fixture.Create<Dummy>() };

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingParamsAndMultipleItems_AddAllItems()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = Fixture.CreateMany<Dummy>().ToArray();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }

    [TestMethod]
    public void WhenUsingEnumerableAndMultipleItems_AddAllItems()
    {
        //Arrange
        IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var items = Fixture.CreateMany<Dummy>().ToList();

        //Act
        source.AddRange(items);

        //Assert
        source.Should().BeEquivalentTo(original.Concat(items));
    }
}

[TestClass]
public class AddRangeWithArrayOfDummyTests : AddRangeTester<Dummy[], Dummy>
{
    [TestMethod]
    public void WhenUsingParams_Throw()
    {
        //Arrange
        var source = Fixture.Create<Dummy[]>();
        var items = Fixture.CreateMany<Dummy>().ToArray();

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
        var items = Fixture.CreateMany<TSource>().ToArray();

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
        var items = Fixture.CreateMany<TSource>().ToList();

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingParamsAndItemsAreNull_Throw()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();
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
        var source = Fixture.Create<TCollection>();
        IEnumerable<TSource> items = null!;

        //Act
        var action = () => source.AddRange(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(items));
    }
}