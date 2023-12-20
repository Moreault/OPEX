namespace OPEX.Tests;

[TestClass]
public sealed class Without : Tester
{
    [TestMethod]
    public void Items_WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var items = Fixture.CreateMany<Dummy>().ToArray();

        //Act
        var action = () => source.Without(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Items_WhenItemsIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();
        IEnumerable<Dummy> items = null!;

        //Act
        var action = () => source.Without(items);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(items));
    }

    [TestMethod]
    public void Items_WhenItemsAreNotInSource_ReturnEquivalentToSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();
        var items = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.Without(items);

        //Assert
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void Items_WhenItemsAreNotInSource_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();
        var items = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToArray();

        //Act
        source.Without(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Items_WhenItemsAreInSource_ReturnSourceWithoutItems()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var items = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.Without(items);

        //Assert
        result.Should().BeEquivalentTo(source.Where(x => !items.Contains(x)));
    }

    [TestMethod]
    public void Items_WhenItemsAreInSource_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var items = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToArray();

        //Act
        source.Without(items);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var predicate = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.Without(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();
        Func<Dummy, bool> predicate = null!;

        //Act
        var action = () => source.Without(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
    }

    [TestMethod]
    public void Predicate_WhenNoMatchInSource_ReturnEquivalentToSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();

        //Act
        var result = source.Without(x => x.Id < 0);

        //Assert
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void Predicate_WhenNoMatchInSource_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();
        var original = source.ToArray();

        //Act
        source.Without(x => x.Id < 0);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Predicate_WhenMatchInSource_ReturnWithoutMatch()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>().ToList();
        var source = sourceWithoutItems.Concat(items).ToList();

        //Act
        var result = source.Without(x => x.Name == name);

        //Assert
        result.Should().BeEquivalentTo(sourceWithoutItems);
    }

    [TestMethod]
    public void Predicate_WhenMatchInSource_DoNotModifySource()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        var sourceWithoutItems = Fixture.CreateMany<Dummy>().ToList();
        var source = sourceWithoutItems.Concat(items).ToList();
        var original = source.ToArray();

        //Act
        source.Without(x => x.Name == name);

        //Assert
        source.Should().BeEquivalentTo(original);
    }
}