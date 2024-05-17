namespace OPEX.Tests;

[TestClass]
public class SequenceEqualOrNull : Tester
{
    [TestMethod]
    public void WhenBothAreNull_ReturnTrue()
    {
        //Arrange
        IEnumerable<Garbage> first = null!;
        IEnumerable<Garbage> second = null!;

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void WhenFirstIsNullButSecondIsNot_ReturnFalse()
    {
        //Arrange
        IEnumerable<Garbage> first = null!;
        var second = Dummy.Create<List<Garbage>>();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenFirstIsNotNullButSecondIs_ReturnFalse()
    {
        //Arrange
        var first = Dummy.Create<List<Garbage>>();
        IEnumerable<Garbage> second = null!;

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenBothAreNotNullButAreAlsoNotEqual_ReturnFalse()
    {
        //Arrange
        var first = Dummy.Create<List<Garbage>>();
        var second = Dummy.Create<List<Garbage>>();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void WhenBothAreNotNullAndContainTheSameElements_ReturnTrue()
    {
        //Arrange
        var first = Dummy.Create<List<Garbage>>();
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
        var first = Dummy.Create<List<Garbage>>();
        var second = first.ToList();
        second.Reverse();

        //Act
        var result = first.SequenceEqualOrNull(second);

        //Assert
        result.Should().BeFalse();
    }
}