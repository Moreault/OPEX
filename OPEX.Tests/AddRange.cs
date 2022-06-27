namespace OPEX.Tests;

[TestClass]
public class AddRange
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenUsingParamsAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingEnumerableAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingParamsAndItemsAreNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            Dummy[] items = null!;

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("items");
        }

        [TestMethod]
        public void WhenUsingEnumerableAndItemsAreNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            IEnumerable<Dummy> items = null!;

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("items");
        }

        [TestMethod]
        public void WhenUsingParams_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(AddRange)));
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenUsingParamsAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingEnumerableAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingParamsAndItemsAreNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            Dummy[] items = null!;

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("items");
        }

        [TestMethod]
        public void WhenUsingEnumerableAndItemsAreNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            IEnumerable<Dummy> items = null!;

            //Act
            var action = () => collection.AddRange(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("items");
        }

        [TestMethod]
        public void WhenUsingParamsAndItemsIsEmpty_DoNothing()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = Array.Empty<Dummy>();

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenUsingEnumerableAndItemsIsEmpty_DoNothing()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = new List<Dummy>();

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenUsingParamsAndItemsContainsOnlyOneItem_AddOneItem()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = new[] { Fixture.Create<Dummy>() };

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original.Concat(items));
        }

        [TestMethod]
        public void WhenUsingEnumerableAndItemsContainsOnlyOneItem_AddOneItem()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = new List<Dummy> { Fixture.Create<Dummy>() };

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original.Concat(items));
        }

        [TestMethod]
        public void WhenUsingParamsAndMultipleItems_AddAllItems()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original.Concat(items));
        }

        [TestMethod]
        public void WhenUsingEnumerableAndMultipleItems_AddAllItems()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var items = Fixture.CreateMany<Dummy>().ToList();

            //Act
            collection.AddRange(items);

            //Assert
            collection.Should().BeEquivalentTo(original.Concat(items));
        }
    }
}