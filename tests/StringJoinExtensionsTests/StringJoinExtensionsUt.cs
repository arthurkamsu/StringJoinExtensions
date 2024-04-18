using FluentAssertions;
using StringJoinExtensions;

namespace StringJoinExtensionsTests;

public class StringJoinExtensionsUt
{
    [Theory]
    [MemberData(nameof(SomeTypeScenario))]
    public void WhenNoParameterIsNull_ReturnsExpectedString<T>(
        char separator,
        IEnumerable<T> enumerableToJoin,
        Func<T, string> predicate,
        string expectedString)
    {
        // Act
        string actualJoinResult = StringJoin.Join(separator, enumerableToJoin, predicate);

        // Assert
        actualJoinResult.Should().Be(expectedString);
    }

    [Fact]
    public void WhenPredicateIsNull_ThrowsArgumentException()
    {
        // Arrange
        var enumerableToJoin = Enumerable.Range(1, 3);

        var separator = ',';

        // Delegate the error handlint to the Select function of Linq.
        var expectedArgumentNullExceptionMessage = "Value cannot be null. (Parameter 'selector')";

        // Act // Assert
        var actualException = Assert.Throws<ArgumentNullException>(() => StringJoin.Join(separator, enumerableToJoin, null));

        actualException.Message.Should().Be(expectedArgumentNullExceptionMessage);
    }

    [Fact]
    public void WhenCollectionToJoinIsNull_ThrowsArgumentException()
    {
        // Arrange
        var separator = ',';

        var predicate = (int i) => $"{Math.Pow(i, 2)}";

        // Delegate the error handlint to the Select function of Linq.
        var expectedArgumentNullExceptionMessage = "Value cannot be null. (Parameter 'source')";

        // Act // Assert
        var actualException = Assert.Throws<ArgumentNullException>(() => StringJoin.Join(separator, null, predicate));

        actualException.Message.Should().Be(expectedArgumentNullExceptionMessage);
    }

    public static IEnumerable<object[]> SomeTypeScenario()
    {
        // Arrange
        yield return new object[] 
        {
            ',',
            new List<int>{ 1, 2, 3},
            (int i) => $"{Math.Pow(i, 2)}",
            "1,4,9"
        };
        yield return new object[]
        {
            '|',
            new List<Person>{ new ("John", "Doe", 75), new ("Peter", "Doe", 20)},
            (Person p) => $"{p.FirstName}-{p.LastName}:{p.Age}",
            "John-Doe:75|Peter-Doe:20"
        };
    }
}

record struct Person(string FirstName, string LastName, int Age);
