namespace OPEX.Tests;

[TestClass]
public class Split : TestBase
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Garbage> source = null!;
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.Split(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenMatchIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.Split(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_ReturnEmptyResult()
    {
        //Arrange
        var source = new List<Garbage>();

        //Act
        var result = source.Split(x => !string.IsNullOrWhiteSpace(x.Name));

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Garbage>());
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyButThereIsNoMatch_ReturnOnlyRemaining()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = source.Split(x => string.IsNullOrWhiteSpace(x.Name));

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Garbage>
        {
            Remaining = source
        });
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyAndThereAreMatches_SplitBetweenRemainingAndExcluded()
    {
        //Arrange
        var toRemain = Dummy.CreateMany<Garbage>().ToList();
        var toExclude = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).CreateMany().ToList();
        var source = toRemain.Concat(toExclude).ToList();

        //Act
        var result = source.Split(x => x.Level < 0);

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Garbage>
        {
            Remaining = toRemain,
            Excluded = toExclude
        });
    }
}