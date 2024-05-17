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
            Garbage[] source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.RemoveFirst(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsArray_Throw()
        {
            //Arrange
            var source = Dummy.Create<Garbage[]>();
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
            List<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.RemoveFirst(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsListAndItemIsNullAndThereIsNoNullInCollection_Throw()
        {
            //Arrange
            var collection = Dummy.Create<List<Garbage>>();
            Garbage item = null!;

            //Act
            var action = () => collection.RemoveFirst(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.NullCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenSourceIsListAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var collection = Dummy.Create<List<Garbage>>();
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => collection.RemoveFirst(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.ItemCouldNotBeRemoved, item));
        }

        [TestMethod]
        public void WhenSourceIsListAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var collection = Dummy.CreateMany<Garbage>().ToList();
            var original = collection.ToList();
            var item = collection[1];

            //Act
            collection.RemoveFirst(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenSourceIsListAndThereAreMultipleOccurencesOfItem_RemoveTheFirstOne()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var collection = new List<Garbage>
            {
                Dummy.Create<Garbage>(),
                item,
                item,
                Dummy.Create<Garbage>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveFirst(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Garbage>
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
            Garbage[] source = null!;
            var match = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.RemoveFirst(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsArrayAndPredicateIsNull_Throw()
        {
            //Arrange
            var collection = Dummy.Create<Garbage[]>();
            Func<Garbage, bool> predicate = null!;

            //Act
            var action = () => collection.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsArray_Throw()
        {
            //Arrange
            var collection = Dummy.Create<Garbage[]>();
            var match = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => collection.RemoveFirst(match);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveFirst)));
        }

        [TestMethod]
        public void WhenSourceIsNullList_Throw()
        {
            //Arrange
            List<Garbage> source = null!;
            var predicate = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsListAndPredicateIsNull_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();
            Func<Garbage, bool> predicate = null!;

            //Act
            var action = () => source.RemoveFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsListAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();

            //Act
            var action = () => source.RemoveFirst(x => x.Name == Dummy.Create<string>());

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.PredicateItemCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.RemoveFirst(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurencesOfItem_RemoveTheFirstOne()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var collection = new List<Garbage>
            {
                Dummy.Create<Garbage>(),
                item,
                item,
                Dummy.Create<Garbage>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveFirst(x => x.Name == item.Name);

            //Assert
            collection.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0],
                item,
                original[3],
                item
            });
        }
    }
}