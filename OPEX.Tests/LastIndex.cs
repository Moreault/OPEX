namespace OPEX.Tests;

[TestClass]
public class LastIndex
{
    [TestClass]
    public class WithArray : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            string[] source = null!;

            //Act
            var action = () => source.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var source = Array.Empty<int>();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var source = new[] { Dummy.Create<string>() };

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<string>().ToArray();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(source.Length - 1);
        }
    }

    [TestClass]
    public class WithList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<string> source = null!;

            //Act
            var action = () => source.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var source = new List<int>();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var source = new List<string> { Dummy.Create<string>() };

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<string>().ToList();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(source.Count - 1);
        }
    }

    [TestClass]
    public class WithReadOnlyList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<string> source = null!;

            //Act
            var action = () => source.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<int> source = new List<int>();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            IReadOnlyList<string> source = new List<string> { Dummy.Create<string>() };

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            IReadOnlyList<string> source = Dummy.CreateMany<string>().ToList();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(source.Count - 1);
        }
    }

    [TestClass]
    public class WithWriteOnlyList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<string> source = null!;

            //Act
            var action = () => source.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var source = new WriteOnlyList<int>();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var source = new WriteOnlyList<string> { Dummy.Create<string>() };

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<string>().ToWriteOnlyList();

            //Act
            var result = source.LastIndex();

            //Assert
            result.Should().Be(source.Count - 1);
        }
    }
}