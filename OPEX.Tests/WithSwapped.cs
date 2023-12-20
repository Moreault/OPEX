namespace OPEX.Tests;

[TestClass]
public sealed class WithSwapped : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var current = Fixture.Create<int>();
        var destination = Fixture.Create<int>();

        //Act
        var action = () => source.WithSwapped(current, destination);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenCurrentIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var currentIndex = -Fixture.Create<int>();
        var destinationIndex = Fixture.Create<int>();

        //Act
        var action = () => source.WithSwapped(currentIndex, destinationIndex);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(currentIndex));
    }

    [TestMethod]
    public void WhenCurrentIsOutOfMaximumBounds_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(6).ToList();
        var currentIndex = source.Count + Fixture.Create<int>();
        var destinationIndex = Fixture.Create<int>();

        //Act
        var action = () => source.WithSwapped(currentIndex, destinationIndex);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(currentIndex));
    }

    [TestMethod]
    public void WhenDestinationIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var currentIndex = source.GetRandomIndex();
        var destinationIndex = -Fixture.Create<int>();

        //Act
        var action = () => source.WithSwapped(currentIndex, destinationIndex);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(destinationIndex));
    }

    [TestMethod]
    public void WhenDestinationIsOutOfMaximumBounds_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(6).ToList();
        var currentIndex = source.GetRandomIndex();
        var destinationIndex = source.Count + Fixture.Create<int>();

        //Act
        var action = () => source.WithSwapped(currentIndex, destinationIndex);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(destinationIndex));
    }

    [TestMethod]
    public void WhenCurrentAndDestinationAreWithinBoundaries_ReturnSameCollectionWithSwappedIndexes()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(6).ToList();
        var indexes = source.GetManyRandomIndexes(2);

        var currentIndex = indexes[0];
        var destinationIndex = indexes[1];

        var original = source.ToList();

        //Act
        var result = source.WithSwapped(currentIndex, destinationIndex);

        //Assert
        original.Swap(currentIndex, destinationIndex);
        result.Should().ContainInOrder(original);
    }
}