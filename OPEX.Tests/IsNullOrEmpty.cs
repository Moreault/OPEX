namespace OPEX.Tests;

[TestClass]
public class IsNullOrEmpty
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            string[] collection = null!;

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var collection = Array.Empty<string>();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var collection = Fixture.CreateMany<string>().ToArray();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            List<string> collection = null!;

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var collection = new List<string>();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var collection = Fixture.CreateMany<string>().ToList();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<string> collection = null!;

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<string> collection = new List<string>();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<string> collection = Fixture.CreateMany<string>().ToList();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithDictionary : Tester
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            Dictionary<int, string> collection = null!;

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var collection = new Dictionary<int, string>();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var collection = Fixture.Create<Dictionary<int, string>>();

            //Act
            var result = collection.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }
}