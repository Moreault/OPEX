using ToolBX.Eloquentest.Extensions;

namespace OPEX.Tests;

[TestClass]
public class IsWithinRange
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            Dummy[] source = null!;
            var index = Fixture.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var index = 0;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsEqualToLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var index = source.LastIndex();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsBetweenZeroAndLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var index = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var index = -Fixture.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            List<Dummy> source = null!;
            var index = Fixture.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var index = 0;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsEqualToLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var index = source.LastIndex();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsBetweenZeroAndLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var index = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var index = -Fixture.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> source = null!;
            var index = Fixture.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            var index = 0;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsEqualToLastIndex_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            var index = source.LastIndex();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsBetweenZeroAndLastIndex_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            var index = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            var index = -Fixture.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> source = null!;
            var index = Fixture.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var index = 0;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsEqualToLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var index = source.LastIndex();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsBetweenZeroAndLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var index = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var index = -Fixture.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }
}