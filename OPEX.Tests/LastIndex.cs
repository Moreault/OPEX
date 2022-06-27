namespace OPEX.Tests;

[TestClass]
public class LastIndex
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            string[] collection = null!;

            //Act
            var action = () => collection.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var collection = Array.Empty<int>();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var collection = new[] { Fixture.Create<string>() };

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<string>().ToArray();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(collection.Length - 1);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<string> collection = null!;

            //Act
            var action = () => collection.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var collection = new List<int>();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var collection = new List<string> { Fixture.Create<string>() };

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<string>().ToList();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(collection.Count - 1);
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<string> collection = null!;

            //Act
            var action = () => collection.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<int> collection = new List<int>();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            IReadOnlyList<string> collection = new List<string> { Fixture.Create<string>() };

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            IReadOnlyList<string> collection = Fixture.CreateMany<string>().ToList();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(collection.Count - 1);
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<string> collection = null!;

            //Act
            var action = () => collection.LastIndex();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnMinusOne()
        {
            //Arrange
            var collection = new WriteOnlyList<int>();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnZero()
        {
            //Arrange
            var collection = new WriteOnlyList<string> { Fixture.Create<string>() };

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnLastIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<string>().ToWriteOnlyList();

            //Act
            var result = collection.LastIndex();

            //Assert
            result.Should().Be(collection.Count - 1);
        }
    }
}