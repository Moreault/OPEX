namespace OPEX.Tests;

[TestClass]
public class GetRandomIndex : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;

        //Act
        var action = () => source.GetRandomIndex();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceIsArray_ReturnRandomIndexWithinBoundaries()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(15).ToArray();

        //Act
        var results = new List<int>();
        for (var i = 0; i < 100; i++)
            results.Add(source.GetRandomIndex());

        //Assert
        results.Should().OnlyContain(x => x >= 0 && x <= 14);
    }

    [TestMethod]
    public void WhenSourceIsIList_ReturnRandomIndexWithinBoundaries()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(15).ToList();

        //Act
        var results = new List<int>();
        for (var i = 0; i < 100; i++)
            results.Add(source.GetRandomIndex());

        //Assert
        results.Should().OnlyContain(x => x >= 0 && x <= 14);
    }

    [TestMethod]
    public void WhenSourceIsWriteOnlyList_ReturnRandomIndexWithinBoundaries()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(15).ToWriteOnlyList();

        //Act
        var results = new List<int>();
        for (var i = 0; i < 100; i++)
            results.Add(source.GetRandomIndex());

        //Assert
        results.Should().OnlyContain(x => x >= 0 && x <= 14);
    }

    [TestMethod]
    public void WhenSourceIsReadOnlyList_ReturnRandomIndexWithinBoundaries()
    {
        //Arrange
        IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>(15).ToList();

        //Act
        var results = new List<int>();
        for (var i = 0; i < 100; i++)
            results.Add(source.GetRandomIndex());

        //Assert
        results.Should().OnlyContain(x => x >= 0 && x <= 14);
    }
}