namespace OPEX.Tests;

[TestClass]
public sealed class WithoutAt : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var index = Fixture.Create<int>();

        //Act
        var action = () => source.WithoutAt(index);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenIndexIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = -Fixture.Create<int>();

        //Act
        var action = () => source.WithoutAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenIndexIsOutsideOfUpperRange_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.Count + Fixture.Create<int>();

        //Act
        var action = () => source.WithoutAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenIndexIsWithinBoundaries_ReturnCollectionWithoutObjectAtIndex()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.GetRandomIndex();
        var original = source.ToList();

        //Act
        var result = source.WithoutAt(index);

        //Assert
        result.Should().NotContain(original[index]);
    }

    [TestMethod]
    public void WhenIndexIsWithinBoundaries_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.GetRandomIndex();
        var original = source.ToList();

        //Act
        source.WithoutAt(index);

        //Assert
        source.Should().BeEquivalentTo(original);
    }
}