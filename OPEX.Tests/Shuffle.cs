namespace OPEX.Tests;

[TestClass]
public class Shuffle
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;

            //Act
            var action = () => collection.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var collection = Array.Empty<Dummy>();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var collection = new[] { Fixture.Create<Dummy>() };
            var original = collection.ToList();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ContainTheSameItemsButInDifferentOrder()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(10).ToArray();
            var original = collection.ToList();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEquivalentTo(original);
            collection.Should().NotContainInOrder(original);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;

            //Act
            var action = () => collection.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var collection = new List<Dummy>();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var collection = new List<Dummy> { Fixture.Create<Dummy>() };
            var original = collection.ToList();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ContainTheSameItemsButInDifferentOrder()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(10).ToList();
            var original = collection.ToList();

            //Act
            collection.Shuffle();

            //Assert
            collection.Should().BeEquivalentTo(original);
            collection.Should().NotContainInOrder(original);
        }
    }
}