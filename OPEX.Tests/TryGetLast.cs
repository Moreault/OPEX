namespace OPEX.Tests;

[TestClass]
public class TryGetLast
{
    [TestClass]
    public class WithoutPredicate : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;

            //Act
            var action = () => source.TryGetLast();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Garbage>();

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItem_ReturnThatItem()
        {
            //Arrange
            var source = new List<Garbage> { Dummy.Create<Garbage>() };

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().BeEquivalentTo(Result<Garbage>.Success(source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItemButIsWeird_ReturnThatItem()
        {
            //Arrange
            var source = new WeirdCollection<Garbage> { Dummy.Create<Garbage>() };

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().BeEquivalentTo(Result<Garbage>.Success(source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnTheLast()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var last = Dummy.Create<Garbage>();
            source.Add(last);

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(Result<Garbage>.Success(last));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButDoesNotImplementIList_ReturnTheLast()
        {
            //Arrange
            var source = new WeirdCollection<Garbage>(Dummy.CreateMany<Garbage>());
            var last = Dummy.Create<Garbage>();
            source.Add(last);

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(Result<Garbage>.Success(last));
        }
    }

    [TestClass]
    public class WithPredicate : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;

            //Act
            var action = () => source.TryGetLast(x => x.Name == "Something");

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
            var action = () => source.TryGetLast(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Garbage>();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceIsEmptyButIsWeird_ReturnFailure()
        {
            //Arrange
            var source = new WeirdCollection<Garbage>();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespondButIsWeird_ReturnFailure()
        {
            //Arrange
            var source = new WeirdCollection<Garbage>(Dummy.CreateMany<Garbage>());

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

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
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(first));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorrespondsButIsWeird_ReturnThatOneItem()
        {
            //Arrange
            var source = new WeirdCollection<Garbage>(Dummy.CreateMany<Garbage>());

            var first = Dummy.Build<Garbage>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(first));
        }

        [TestMethod]
        public void WhenAllItemsCorrespond_ReturnOnlyTheLast()
        {
            //Arrange
            var source = Dummy.Build<Garbage>().With(x => x.Name, "Something").CreateMany(3).ToArray();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(source[2]));
        }

        [TestMethod]
        public void WhenAllItemsCorrespondButIsWeird_ReturnOnlyTheLast()
        {
            //Arrange
            var source = new WeirdCollection<Garbage>(Dummy.Build<Garbage>().With(x => x.Name, "Something").CreateMany(3));

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Garbage>.Success(source[2]));
        }
    }
}