namespace Goibhniu.Shared.Tests;

public class LinqExtensionsTests
{
  [Test]
  public void MinMaxBy_WithInts_BehavesLikeMinByAndMaxBy()
  {
    // Arrange
    var source = new[] { 4, 3, 2, 6, 1, 7 };
    var selector = (int n) => n;

    // Act
    var (min, max) = source.MinMaxBy(selector);
    var expectedMin = source.MinBy(selector);
    var expectedMax = source.MaxBy(selector);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(min, Is.EqualTo(expectedMin));
      Assert.That(max, Is.EqualTo(expectedMax));
    });
  }

  [Test]
  public void MinMaxBy_WithObjects_BehavesLikeMinByAndMaxBy()
  {
    // Arrange
    var people = new[] { Person.Dagobert, Person.Christoph };
    var selector = (Person p) => p.Age;

    // Act
    var (min, max) = people.MinMaxBy(p => p.Age);
    var expectedMin = people.MinBy(selector);
    var expectedMax = people.MaxBy(selector);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(min, Is.EqualTo(expectedMin));
      Assert.That(max, Is.EqualTo(expectedMax));
    });
  }

  [Test]
  public void MinMaxBy_WithEmptySequence_ThrowsInvalidOperationException()
  {
    // Arrange
    var empty = Enumerable.Empty<int>();

    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => empty.MinMaxBy(n => n));
  }

  [Test]
  public void MinMaxBy_WithSingleElement_ReturnsSameElementForBothMinAndMax()
  {
    // Arrange
    const int item = 0xF00D;
    var singleItem = new[] { item };

    // Act
    var (min, max) = singleItem.MinMaxBy(n => n);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(min, Is.EqualTo(item));
      Assert.That(max, Is.EqualTo(item));
    });
  }

  [Test]
  public void MinMaxBy_WithDuplicateValues_BehavesLikeMinByAndMaxBy()
  {
    // Arrange
    var people = new[] { Person.Adalbert, Person.Bertram, Person.Christoph, Person.Dagobert, Person.Eckehart };
    var selector = (Person p) => p.Age;

    // Act
    var (min, max) = people.MinMaxBy(p => p.Age);
    var expectedMin = people.MinBy(selector);
    var expectedMax = people.MaxBy(selector);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(min, Is.EqualTo(expectedMin));
      Assert.That(max, Is.EqualTo(expectedMax));
    });
  }

  [Test]
  public void MinMaxBy_WithNullableValues_HandlesNullsCorrectly()
  {
    // Arrange
    var source = new int?[] { 3, null, 1, 5, null, 2 };
    var selector = (int? n) => n;

    // Act
    var (min, max) = source.MinMaxBy(selector);
    var expectedMin = source.MinBy(selector);
    var expectedMax = source.MaxBy(selector);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(min, Is.EqualTo(expectedMin));
      Assert.That(max, Is.EqualTo(expectedMax));
    });
  }

  private class Person
  {
    public readonly string Name;
    public readonly int Age;

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }

    public static Person Adalbert = new(nameof(Adalbert), 19);
    public static Person Bertram = new(nameof(Bertram), 19);
    public static Person Christoph = new(nameof(Christoph), 23);
    public static Person Dagobert = new(nameof(Dagobert), 27);
    public static Person Eckehart = new(nameof(Eckehart), 27);
  }
}