namespace OPEX.Tests;

[TestClass]
public sealed class With : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IReadOnlyList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.With(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenItemIsNull_AddNullToCollection()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Dummy item = null!;

        //Act
        var result = source.With(item);

        //Assert
        result.Should().BeEquivalentTo(source.Append(null));
    }

    [TestMethod]
    public void WhenItemIsNull_DoNotModifyOriginalSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Dummy item = null!;
        var original = source.ToList();

        //Act
        source.With(item);

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenMultipleItems_AddToCollection()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item1 = Fixture.Create<Dummy>();
        var item2 = Fixture.Create<Dummy>();

        //Act
        var result = source.With(item1, item2);

        //Assert
        result.Should().BeEquivalentTo(source.Append(item1).Append(item2));
    }

    [TestMethod]
    public void WhenMultipleItems_DoNotModifyOriginalSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item1 = Fixture.Create<Dummy>();
        var item2 = Fixture.Create<Dummy>();
        var original = source.ToList();

        //Act
        source.With(item1, item2);

        //Assert
        source.Should().BeEquivalentTo(original);
    }
}