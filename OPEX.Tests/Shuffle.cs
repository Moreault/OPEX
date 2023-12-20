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
            Dummy[] source = null!;

            //Act
            var action = () => source.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var source = new[] { Fixture.Create<Dummy>() };
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
            var source = Fixture.CreateMany<Dummy>(10).ToArray();
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
            source.Should().NotContainInOrder(original);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> source = null!;

            //Act
            var action = () => source.Shuffle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var source = new List<Dummy>();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionContainsOneItem_DoNothing()
        {
            //Arrange
            var source = new List<Dummy> { Fixture.Create<Dummy>() };
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
            var source = Fixture.CreateMany<Dummy>(10).ToList();
            var original = source.ToList();

            //Act
            source.Shuffle();

            //Assert
            source.Should().BeEquivalentTo(original);
            source.Should().NotContainInOrder(original);
        }
    }
}