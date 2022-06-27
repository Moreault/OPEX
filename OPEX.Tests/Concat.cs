namespace OPEX.Tests;

[TestClass]
public class Concat
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.Concat(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("first");
        }

        [TestMethod]
        public void WhenItemsIsEmpty_DoNothing()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var original = collection.ToArray();
            var items = Array.Empty<Dummy>();

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenThereAreMultipleItems_AddThemAll()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var original = collection.ToArray();
            var items = Fixture.CreateMany<Dummy>().ToArray();

            var expected = original.ToList();
            foreach (var item in items)
                expected.Add(item);

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(expected);
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
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.Concat(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("first");
        }

        [TestMethod]
        public void WhenItemsIsEmpty_DoNothing()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToArray();
            var items = Array.Empty<Dummy>();

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenThereAreMultipleItems_AddThemAll()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToArray();
            var items = Fixture.CreateMany<Dummy>().ToArray();

            var expected = original.ToList();
            foreach (var item in items)
                expected.Add(item);

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = null!;
            var items = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var action = () => collection.Concat(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("first");
        }

        [TestMethod]
        public void WhenItemsIsEmpty_DoNothing()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToArray();
            var original = collection.ToArray();
            var items = Array.Empty<Dummy>();

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenThereAreMultipleItems_AddThemAll()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToArray();
            var original = collection.ToArray();
            var items = Fixture.CreateMany<Dummy>().ToArray();

            var expected = original.ToList();
            foreach (var item in items)
                expected.Add(item);

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }

    [TestClass]
    public class WithDictionary : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dictionary<int, Dummy> collection = null!;
            var items = Fixture.CreateMany<KeyValuePair<int, Dummy>>().ToArray();

            //Act
            var action = () => collection.Concat(items);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("first");
        }

        [TestMethod]
        public void WhenItemsIsEmpty_DoNothing()
        {
            //Arrange
            var collection = Fixture.CreateMany<KeyValuePair<int, Dummy>>().ToDictionary(x => x.Key, x => x.Value);
            var original = collection.ToArray();
            var items = Array.Empty<KeyValuePair<int, Dummy>>();

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenThereAreMultipleItems_AddThemAll()
        {
            //Arrange
            var collection = Fixture.CreateMany<KeyValuePair<int, Dummy>>().ToDictionary(x => x.Key, x => x.Value);
            var original = collection.ToArray();
            var items = Fixture.CreateMany<KeyValuePair<int, Dummy>>().ToArray();

            var expected = original.ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in items)
                expected.Add(item.Key, item.Value);

            //Act
            var result = collection.Concat(items);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}