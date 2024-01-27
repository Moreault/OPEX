namespace OPEX.Tests;

[TestClass]
public class Shuffle
{
    [TestClass]
    public class WithArray : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Garbage[] source = null!;

            //Act
            var action = () => source.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var source = Array.Empty<Garbage>();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var source = new[] { Dummy.Create<Garbage>() };
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ContainTheSameItemsButInDifferentOrder()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>(10).ToArray();
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
            source.Should().NotContainInOrder(original);
        }
    }

    [TestClass]
    public class WithList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<Garbage> source = null!;

            //Act
            var action = () => source.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var source = new List<Garbage>();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var source = new List<Garbage> { Dummy.Create<Garbage>() };
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ContainTheSameItemsButInDifferentOrder()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>(10).ToList();
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
            source.Should().NotContainInOrder(original);
        }
    }
}