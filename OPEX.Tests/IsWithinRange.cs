namespace OPEX.Tests;

[TestClass]
public class IsWithinRange
{
    [TestClass]
    public class WithArray : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            Garbage[] source = null!;
            var index = Dummy.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
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
            var source = Dummy.CreateMany<Garbage>().ToArray();
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
            var source = Dummy.CreateMany<Garbage>().ToArray();
            var index = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            var index = -Dummy.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnTrue()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithList : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            List<Garbage> source = null!;
            var index = Dummy.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
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
            var source = Dummy.CreateMany<Garbage>().ToList();
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
            var source = Dummy.CreateMany<Garbage>().ToList();
            var index = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var index = -Dummy.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithReadOnlyList : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Garbage> source = null!;
            var index = Dummy.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
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
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
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
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var index = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var index = -Dummy.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithWriteOnlyList : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Garbage> source = null!;
            var index = Dummy.Create<int>();

            //Act
            var action = () => source.IsWithinRange(index);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenIndexIsZero_ReturnTrue()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
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
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
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
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            var index = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIndexIsNegative_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            var index = -Dummy.Create<int>();

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIndexIsGreaterThanLastIndex_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            var index = source.LastIndex() + 1;

            //Act
            var result = source.IsWithinRange(index);

            //Assert
            result.Should().BeFalse();
        }
    }
}