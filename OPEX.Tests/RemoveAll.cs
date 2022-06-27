namespace OPEX.Tests;

[TestClass]
public class RemoveAll
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenUsingItemOverloadOnArray_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            var item = collection.GetRandom();

            //Act
            var action = () => collection.RemoveAll(item);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage($"The {nameof(ToolBX.OPEX.CollectionExtensions.RemoveAll)} method does not support arrays");
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadOnArray_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            var nameToRemove = collection.GetRandom()!.Name;

            //Act
            var action = () => collection.RemoveAll(x => x.Name == nameToRemove);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage($"The {nameof(ToolBX.OPEX.CollectionExtensions.RemoveAll)} method does not support arrays");
        }
    }

    [TestClass]
    public class WithIList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = null!;

            //Act
            var action = () => collection.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            IList<Dummy> collection = new List<Dummy>();

            //Act
            collection.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            collection.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObject_RemoveThatSingleOccurence()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var originalCount = collection.Count;

            var itemToRemove = collection.GetRandom()!;

            //Act
            collection.RemoveAll(x => x.Name == itemToRemove.Name);

            //Assert
            collection.Should().NotContain(x => x.Name == itemToRemove.Name);
            collection.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithMultipleCorrespondingObjects_RemoveAllOccurences()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>()
                .Concat(Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).CreateMany())
                .Concat(Fixture.CreateMany<Dummy>()).ToList();

            //Act
            collection.RemoveAll(x => x.Level < 0);

            //Assert
            collection.Should().NotContain(x => x.Level < 0);
            collection.Should().Contain(x => x.Level >= 0);
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithNoOccurence_DoNotModifyCollection()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            var original = collection.ToList();

            //Act
            collection.RemoveAll(x => x.Name == Fixture.Create<string>());

            //Assert
            collection.Should().BeEquivalentTo(original);
        }
    }

    [TestClass]
    public class WithDictionary : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dictionary<string, Dummy> collection = null!;

            //Act
            var action = () => collection.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_DoNothing()
        {
            //Arrange
            var collection = new Dictionary<string, Dummy>();

            //Act
            collection.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            collection.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaOverloadWithSingleCorrespondingObject_RemoveThatSingleOccurence()
        {
            //Arrange
            var collection = Fixture.CreateMany<KeyValuePair<string, Dummy>>().ToDictionary(x => x.Key, x => x.Value);
            var originalCount = collection.Count;

            var itemToRemove = collection.GetRandom()!;

            //Act
            collection.RemoveAll(x => x.Value.Name == itemToRemove.Value.Name);

            //Assert
            collection.Should().NotContain(x => x.Value.Name == itemToRemove.Value.Name);
            collection.Should().HaveCount(originalCount - 1);
        }

        [TestMethod]
        public void WhenUsingLambdaOnKeyOverloadWithMultipleCorrespondingObjects_RemoveAllOccurences()
        {
            //Arrange
            var collection = new Dictionary<int, Dummy>
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

            var original = collection.ToDictionary(x => x.Key, x => x.Value);

            //Act
            collection.RemoveAll(x => x.Key < 0);

            //Assert
            collection.Should().BeEquivalentTo(new Dictionary<int, Dummy>
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
        public void WhenUsingLambdaOnValueOverloadWithMultipleCorrespondingObjects_RemoveAllOccurences()
        {
            //Arrange
            var collection = new Dictionary<int, Dummy>
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

            var original = collection.ToDictionary(x => x.Key, x => x.Value);

            //Act
            collection.RemoveAll(x => x.Value.Level < 0);

            //Assert
            collection.Should().BeEquivalentTo(new Dictionary<int, Dummy>
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
        public void WhenUsingLambdaOverloadWithNoOccurence_DoNotModifyCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<KeyValuePair<string, Dummy>>().ToDictionary(x => x.Key, x => x.Value);

            var original = collection.ToList();

            //Act
            collection.RemoveAll(x => x.Value.Name == Fixture.Create<string>());

            //Assert
            collection.Should().BeEquivalentTo(original);
        }
    }
}