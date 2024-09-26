namespace OPEX.Tests;

[TestClass]
public class TryGetFirst
{
    [TestClass]
    public class WithoutPredicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;

            //Act
            var action = () => source.TryGetFirst();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Garbage>();

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItem_ReturnThatItem()
        {
            //Arrange
            var source = new List<Garbage> { Dummy.Create<Garbage>() };

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().BeEquivalentTo( Result<Garbage>.Success(source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnTheFirst()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var first = Dummy.Create<Garbage>();
            source.Insert(0, first);

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().Be(Result<Garbage>.Success(first));
        }
    }

    [TestClass]
    public class WithPredicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;

            //Act
            var action = () => source.TryGetFirst(x => x.Name == "Something");

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenPredicateIsNull_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            Func<Garbage, bool> predicate = null!;

            //Act
            var action = () => source.TryGetFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Garbage>();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorresponds_ReturnThatOneItem()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();

            var first = Dummy.Build<Garbage>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(first));
        }

        [TestMethod]
        public void WhenAllItemsCorrespond_ReturnOnlyTheFirst()
        {
            //Arrange
            var source = Dummy.Build<Garbage>().With(x => x.Name, "Something").CreateMany().ToArray();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(source[0]));
        }
    }
}