namespace OPEX.Tests;

[TestClass]
public class SequenceEqualOrNull : Tester
{
    [TestMethod]
    public void WhenBothAreNull_ReturnTrue()
    {
        //Arrange
        IEnumerable<Dummy> first = null!;
        IEnumerable<Dummy> second = null!;

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void WhenFirstIsNullButSecondIsNot_ReturnFalse()
    {
        //Arrange
        IEnumerable<Dummy> first = null!;
        var second = Fixture.Create<List<Dummy>>();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenFirstIsNotNullButSecondIs_ReturnFalse()
    {
        //Arrange
        var first = Fixture.Create<List<Dummy>>();
        IEnumerable<Dummy> second = null!;

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenBothAreNotNullButAreAlsoNotEqual_ReturnFalse()
    {
        //Arrange
        var first = Fixture.Create<List<Dummy>>();
        var second = Fixture.Create<List<Dummy>>();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenBothAreNotNullAndContainTheSameElements_ReturnTrue()
    {
        //Arrange
        var first = Fixture.Create<List<Dummy>>();
        var second = first.ToArray();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void WhenBothAreNotNullAndContainTheSameElementsButNotInSameOrder_ReturnFalse()
    {
        //Arrange
        var first = Fixture.Create<List<Dummy>>();
        var second = first.ToList();
        second.Reverse();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }
}