namespace OPEX.Tests;

[TestClass]
public class RemoveFirst
{
    [TestClass]
    public class Item : Tester
    {
        [TestMethod]
        public void WhenSourceIsNullArray_Throw()
        {
            //Arrange
            Dummy[] source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveFirst(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsArray_Throw()
        {
            //Arrange
            var source = Fixture.Create<Dummy[]>();
            var item = source.GetRandom();

            //Act
            var action = () => source.RemoveFirst(item);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveFirst)));
        }

        [TestMethod]
        public void WhenSourceIsNullList_Throw()
        {
            //Arrange
            List<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveFirst(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsListAndItemIsNullAndThereIsNoNullInCollection_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();
            Dummy item = null!;

            //Act
            var action = () => collection.RemoveFirst(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.NullCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenSourceIsListAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.RemoveFirst(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.ItemCouldNotBeRemoved, item));
        }

        [TestMethod]
        public void WhenSourceIsListAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var item = collection[1];

            //Act
            collection.RemoveFirst(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenSourceIsListAndThereAreMultipleOccurencesOfItem_RemoveTheFirstOne()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveFirst(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0],
                item,
                original[3],
                item
            });
        }
    }

    [TestClass]
    public class Predicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNullArray_Throw()
        {
            //Arrange
            Dummy[] source = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.RemoveFirst(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsArrayAndPredicateIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.Create<Dummy[]>();
            Func<Dummy, bool> predicate = null!;

            //Act
            var action = () => collection.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsArray_Throw()
        {
            //Arrange
            var collection = Fixture.Create<Dummy[]>();
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.RemoveFirst(match);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveFirst)));
        }

        [TestMethod]
        public void WhenSourceIsNullList_Throw()
        {
            //Arrange
            List<Dummy> source = null!;
            var predicate = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsListAndPredicateIsNull_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();
            Func<Dummy, bool> predicate = null!;

            //Act
            var action = () => source.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsListAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();

            //Act
            var action = () => source.RemoveFirst(x => x.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.PredicateItemCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.RemoveFirst(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurencesOfItem_RemoveTheFirstOne()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveFirst(x => x.Name == item.Name);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0],
                item,
                original[3],
                item
            });
        }
    }
}