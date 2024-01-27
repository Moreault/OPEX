namespace OPEX.Tests;

[TestClass]
public class IsNullOrEmpty
{
    [TestClass]
    public class WithArray : TestBase
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            string[] source = null!;

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var source = Array.Empty<string>();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<string>().ToArray();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithList : TestBase
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            List<string> source = null!;

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var source = new List<string>();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var source = Dummy.CreateMany<string>().ToList();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithReadOnlyList : TestBase
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<string> source = null!;

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            IReadOnlyList<string> source = new List<string>();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            IReadOnlyList<string> source = Dummy.CreateMany<string>().ToList();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class WithDictionary : TestBase
    {
        [TestMethod]
        public void WhenIsNull_ReturnTrue()
        {
            //Arrange
            Dictionary<int, string> source = null!;

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullButEmpty_ReturnTrue()
        {
            //Arrange
            var source = new Dictionary<int, string>();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIsNotNullAndContainsItems_ReturnFalse()
        {
            //Arrange
            var source = Dummy.Create<Dictionary<int, string>>();

            //Act
            var result = source.IsNullOrEmpty();

            //Assert
            result.Should().BeFalse();
        }
    }
}