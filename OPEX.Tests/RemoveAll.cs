namespace OPEX.Tests;

[TestClass]
public class RemoveAll
{
    [TestClass]
    public class Item : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            ICollection<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveAll(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingOnArray_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();

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
            var source = Fixture.CreateMany<Dummy>().ToList();
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
            var source = Fixture.CreateMany<Dummy>().ToList();
            var original = source.ToList();

            var item = Fixture.Create<Dummy>();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenUsingOnDictionary_RemoveItem()
        {
            //Arrange
            var source = Fixture.Create<Dictionary<int, Dummy>>();
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
            var source = Fixture.Create<Dictionary<int, Dummy>>();
            var original = source.ToList();

            var item = Fixture.Create<KeyValuePair<int, Dummy>>();

            //Act
            source.RemoveAll(item);

            //Assert
            source.Should().BeEquivalentTo(original);
        }
    }

    [TestClass]
    public class Predicate : Tester
    {
        [TestMethod]
        public void WhenUsingOnArray_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();

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
            IList<Dummy> source = null!;

            //Act
            var action = () => source.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmptyList_DoNothing()
        {
            //Arrange
            IList<Dummy> source = new List<Dummy>();

            //Act
            source.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObjectOnList_RemoveThatSingleOccurence()
        {
            //Arrange
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
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
            var source = Fixture.CreateMany<Dummy>()
                .Concat(Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).CreateMany())
                .Concat(Fixture.CreateMany<Dummy>()).ToList();

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
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();

            var original = source.ToList();

            //Act
            source.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            source.Should().BeEquivalentTo(original);
        }

        [TestMethod]
        public void WhenCollectionIsNullDictionary_Throw()
        {
            //Arrange
            Dictionary<string, Dummy> source = null!;

            //Act
            var action = () => source.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCollectionIsEmptyDictionary_DoNothing()
        {
            //Arrange
            var source = new Dictionary<string, Dummy>();

            //Act
            source.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            source.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObjectOnDictionary_RemoveThatSingleOccurence()
        {
            //Arrange
            var source = Fixture.CreateMany<KeyValuePair<string, Dummy>>().ToDictionary(x => x.Key, x => x.Value);
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
            var source = new Dictionary<int, Dummy>
            {
                [1] = Fixture.Create<Dummy>(),
                [2] = Fixture.Create<Dummy>(),
                [3] = Fixture.Create<Dummy>(),
                [-1] = Fixture.Create<Dummy>(),
                [-2] = Fixture.Create<Dummy>(),
                [-3] = Fixture.Create<Dummy>(),
                [7] = Fixture.Create<Dummy>(),
                [8] = Fixture.Create<Dummy>(),
                [9] = Fixture.Create<Dummy>()
            };

            var original = source.ToDictionary(x => x.Key, x => x.Value);

            //Act
            source.RemoveAll(x => x.Key < 0);

            //Assert
            source.Should().BeEquivalentTo(new Dictionary<int, Dummy>
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
            var source = new Dictionary<int, Dummy>
            {
                [1] = Fixture.Create<Dummy>(),
                [2] = Fixture.Create<Dummy>(),
                [3] = Fixture.Create<Dummy>(),
                [-1] = Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).Create(),
                [-2] = Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).Create(),
                [-3] = Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).Create(),
                [7] = Fixture.Create<Dummy>(),
                [8] = Fixture.Create<Dummy>(),
                [9] = Fixture.Create<Dummy>()
            };

            var original = source.ToDictionary(x => x.Key, x => x.Value);

            //Act
            source.RemoveAll(x => x.Value.Level < 0);

            //Assert
            source.Should().BeEquivalentTo(new Dictionary<int, Dummy>
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
            var source = Fixture.CreateMany<KeyValuePair<string, Dummy>>().ToDictionary(x => x.Key, x => x.Value);

            var original = source.ToList();

            //Act
            source.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            source.Should().BeEquivalentTo(original);
        }
    }
}