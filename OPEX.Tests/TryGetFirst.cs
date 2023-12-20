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
            IEnumerable<Dummy> source = null!;

            //Act
            var action = () => source.TryGetFirst();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().Be(Result<Dummy>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItem_ReturnThatItem()
        {
            //Arrange
            var source = new List<Dummy> { Fixture.Create<Dummy>() };

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().BeEquivalentTo( Result<Dummy>.Success(source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnTheFirst()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var first = Fixture.Create<Dummy>();
            source.Insert(0, first);

            //Act
            var result = source.TryGetFirst();

            //Assert
            result.Should().Be(Result<Dummy>.Success(first));
        }
    }

    [TestClass]
    public class WithPredicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;

            //Act
            var action = () => source.TryGetFirst(x => x.Name == "Something");

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenPredicateIsNull_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> predicate = null!;

            //Act
            var action = () => source.TryGetFirst(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Dummy>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Dummy>.Failure());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorresponds_ReturnThatOneItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            var first = Fixture.Build<Dummy>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Dummy>.Success(first));
        }

        [TestMethod]
        public void WhenAllItemsCorrespond_ReturnOnlyTheFirst()
        {
            //Arrange
            var source = Fixture.Build<Dummy>().With(x => x.Name, "Something").CreateMany().ToArray();

            //Act
            var result = source.TryGetFirst(x => x.Name == "Something");

            //Assert
            result.Should().Be(Result<Dummy>.Success(source[0]));
        }
    }
}