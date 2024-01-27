namespace OPEX.Tests;

[TestClass]
public class RemoveAll
{
    [TestClass]
    public class Item : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            ICollection<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.RemoveAll(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingOnArray_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();

            var item = source.GetRandom();

            //Act
            var action = () => source.RemoveAll(item);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage($"The {nameof(ToolBX.OPEX.CollectionExtensions.RemoveAll)} method does not support arrays");
        }

        [TestMethod]
        public void WhenUsingOnList_RemoveItem()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var originalCount = source.Count;

            var item = source.GetRandom();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().NotContain(item);
            source.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingOnListThatDoesNotContainItem_DoNothing()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var original = source.ToList();

            var item = Dummy.Create<Garbage>();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenUsingOnDictionary_RemoveItem()
        {
            //Arrange
            var source = Dummy.Create<Dictionary<int, Garbage>>();
            var originalCount = source.Count;

            var item = source.GetRandom();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().NotContain(item);
            source.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingOnDictionaryThatDoesNotContainItem_DoNothing()
        {
            //Arrange
            var source = Dummy.Create<Dictionary<int, Garbage>>();
            var original = source.ToList();

            var item = Dummy.Create<KeyValuePair<int, Garbage>>();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().BeEquivalentTo(original);
        }
    }

    [TestClass]
    public class Predicate : TestBase
    {
        [TestMethod]
        public void WhenUsingOnArray_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();

            var nameToRemove = source.GetRandom()!.Name;

            //Act
            var action = () => source.RemoveAll(x => x.Name == nameToRemove);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage($"The {nameof(ToolBX.OPEX.CollectionExtensions.RemoveAll)} method does not support arrays");
        }

        [TestMethod]
        public void WhenCollectionIsNullList_Throw()
        {
            //Arrange
            IList<Garbage> source = null!;

            //Act
            var action = () => source.RemoveAll(x => x.Name == Dummy.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmptyList_DoNothing()
        {
            //Arrange
            IList<Garbage> source = new List<Garbage>();

            //Act
            source.RemoveAll(x => x.Name == Dummy.Create<string>());

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObjectOnList_RemoveThatSingleOccurence()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var originalCount = source.Count;

            var itemToRemove = source.GetRandom()!;

            //Act
            source.RemoveAll(x => x.Name == itemToRemove.Name);

            //Assert
            source.Should().NotContain(x => x.Name == itemToRemove.Name);
            source.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithMultipleCorrespondingObjectsOnList_RemoveAllOccurences()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>()
                .Concat(Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).CreateMany())
                .Concat(Dummy.CreateMany<Garbage>()).ToList();

            //Act
            source.RemoveAll(x => x.Level < 0);

            //Assert
            source.Should().NotContain(x => x.Level < 0);
            source.Should().Contain(x => x.Level >= 0);
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithNoOccurenceOnList_DoNotModifyCollection()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();

            var original = source.ToList();

            //Act
            source.RemoveAll(x => x.Name == Dummy.Create<string>());

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionIsNullDictionary_Throw()
        {
            //Arrange
            Dictionary<string, Garbage> source = null!;

            //Act
            var action = () => source.RemoveAll(x => x.Value.Name == Dummy.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmptyDictionary_DoNothing()
        {
            //Arrange
            var source = new Dictionary<string, Garbage>();

            //Act
            source.RemoveAll(x => x.Value.Name == Dummy.Create<string>());

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObjectOnDictionary_RemoveThatSingleOccurence()
        {
            //Arrange
            var source = Dummy.CreateMany<KeyValuePair<string, Garbage>>().ToDictionary(x => x.Key, x => x.Value);
            var originalCount = source.Count;

            var itemToRemove = source.GetRandom()!;

            //Act
            source.RemoveAll(x => x.Value.Name == itemToRemove.Value.Name);

            //Assert
            source.Should().NotContain(x => x.Value.Name == itemToRemove.Value.Name);
            source.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingLambdaOnKeyOverloadWithMultipleCorrespondingObjectsOnDictionary_RemoveAllOccurences()
        {
            //Arrange
            var source = new Dictionary<int, Garbage>
            {
                [1] = Dummy.Create<Garbage>(),
                [2] = Dummy.Create<Garbage>(),
                [3] = Dummy.Create<Garbage>(),
                [-1] = Dummy.Create<Garbage>(),
                [-2] = Dummy.Create<Garbage>(),
                [-3] = Dummy.Create<Garbage>(),
                [7] = Dummy.Create<Garbage>(),
                [8] = Dummy.Create<Garbage>(),
                [9] = Dummy.Create<Garbage>()
            };

            var original = source.ToDictionary(x => x.Key, x => x.Value);

            //Act
            source.RemoveAll(x => x.Key < 0);

            //Assert
            source.Should().BeEquivalentTo(new Dictionary<int, Garbage>
            {
                [1] = original[1],
                [2] = original[2],
                [3] = original[3],
                [7] = original[7],
                [8] = original[8],
                [9] = original[9]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaOnValueOverloadWithMultipleCorrespondingObjectsOnDictionary_RemoveAllOccurences()
        {
            //Arrange
            var source = new Dictionary<int, Garbage>
            {
                [1] = Dummy.Create<Garbage>(),
                [2] = Dummy.Create<Garbage>(),
                [3] = Dummy.Create<Garbage>(),
                [-1] = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).Create(),
                [-2] = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).Create(),
                [-3] = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).Create(),
                [7] = Dummy.Create<Garbage>(),
                [8] = Dummy.Create<Garbage>(),
                [9] = Dummy.Create<Garbage>()
            };

            var original = source.ToDictionary(x => x.Key, x => x.Value);

            //Act
            source.RemoveAll(x => x.Value.Level < 0);

            //Assert
            source.Should().BeEquivalentTo(new Dictionary<int, Garbage>
            {
                [1] = original[1],
                [2] = original[2],
                [3] = original[3],
                [7] = original[7],
                [8] = original[8],
                [9] = original[9]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithNoOccurenceOnDictionary_DoNotModifyCollection()
        {
            //Arrange
            var source = Dummy.CreateMany<KeyValuePair<string, Garbage>>().ToDictionary(x => x.Key, x => x.Value);

            var original = source.ToList();

            //Act
            source.RemoveAll(x => x.Value.Name == Dummy.Create<string>());

            //Assert
            source.Should().BeEquivalentTo(original);
        }
    }
}