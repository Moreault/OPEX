namespace OPEX.Tests;

[TestClass]
public class TryGetSingle
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
            var action = () => source.TryGetSingle();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetSingle();

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItem_ReturnThatItem()
        {
            //Arrange
            var source = new List<Dummy> { Fixture.Create<Dummy>() };

            //Act
            var result = source.TryGetSingle();

            //Assert
            result.Should().BeEquivalentTo(new TryGetResult<Dummy>(true, source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnFailure()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var first = Fixture.Create<Dummy>();
            source.Insert(0, first);

            //Act
            var result = source.TryGetSingle();

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
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
            var action = () => source.TryGetSingle(x => x.Name == "Something");

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
            var action = () => source.TryGetSingle(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetSingle(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.TryGetSingle(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorresponds_ReturnThatOneItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            var first = Fixture.Build<Dummy>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetSingle(x => x.Name == "Something");

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, first));
        }

        [TestMethod]
        public void WhenAllItemsCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Fixture.Build<Dummy>().With(x => x.Name, "Something").CreateMany().ToArray();

            //Act
            var result = source.TryGetSingle(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }
    }
}