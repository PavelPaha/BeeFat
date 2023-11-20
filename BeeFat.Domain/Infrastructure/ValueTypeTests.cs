using BeeFat.Domain.Models.User;
using NUnit.Framework;
using FluentAssertions;

namespace BeeFat.Domain.Infrastructure;

[TestFixture]
public class ValueTypeTests
{
    [Test]
    public void AddressesWithNullsAreEqual()
    {
        var address = new Address(null, null);
        var other = new Address(null, null);
        address.Should().BeEquivalentTo(other);
    }

    [Test]
    public void AddressNotEqualToNull()
    {
        var address1 = new Address("ул. Тургенева", "4");
        var equal = address1.Equals(null);
        equal.Should().BeFalse();
    }

    [Test]
    public void AddressNotEqualToPersonName()
    {
        var address = new Address("A", "B");
        var person = new PersonName("A", "B");
        // ReSharper disable once SuspiciousTypeConversion.Global
        address.Should().NotBeEquivalentTo(person);
    }

    [Test]
    public void CompareAddressesWithoutSomeValues()
    {
        var address = new Address("A", null);
        var other = new Address(null, "Y");
        address.Should().NotBeEquivalentTo(other);
    }

    [Test]
    public void ComplexTypesAreEqual()
    {
        var person1 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 29));
        var person2 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 29));
        person1.Should().BeEquivalentTo(person2);
    }

    [Test]
    public void DifferentAddressesAreNotEqual()
    {
        var address = new Address("A", "B");
        var other = new Address("X", "Y");
        address.Should().NotBeEquivalentTo(other);
    }

    [Test]
    public void HasTypedEqualsMethod()
    {
        var method = typeof(PersonName).GetMethod("Equals", new[] { typeof(PersonName) });
        var errorMessage = "PersonName should contain method public bool Equals(PersonName name)";
        method.Should().NotBeNull(errorMessage);
        method!.IsPublic.Should().BeTrue();
        (method.ReturnType == typeof(bool)).Should().BeTrue();
        (method.GetParameters()[0].ParameterType == typeof(PersonName)).Should().BeTrue();
    }

    [Test]
    public void NotEqualComplexProperty()
    {
        var person1 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 29));
        var person2 = new Person(new PersonName("A", "XXX"), 180, new DateTime(1988, 2, 29));
        person1.Should().NotBeEquivalentTo(person2);
    }

    [Test]
    public void NotEqualDateProperties()
    {
        var person1 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 29));
        var person2 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 28));
        person1.Should().NotBeEquivalentTo(person2);
    }

    [Test]
    public void NotEqualIntProperties()
    {
        var person1 = new Person(new PersonName("A", "B"), 180, new DateTime(1988, 2, 29));
        var person2 = new Person(new PersonName("A", "B"), 181, new DateTime(1988, 2, 29));
        person1.Should().NotBeEquivalentTo(person2);
    }

    [Test]
    public void SameAddressesAreEqual()
    {
        var address1 = new Address("ул. Тургенева", "4");
        var address2 = new Address("ул. Тургенева", "4");
        address1.Should().BeEquivalentTo((object)address2);
    }

    [Test]
    public void ToStringListAllPropertiesLexicographically()
    {
        "PersonName(FirstName: A; LastName: B)".Should().BeEquivalentTo(new PersonName("A", "B").ToString());
        "Address(Building: Y; Street: X)".Should().BeEquivalentTo(new Address("X", "Y").ToString());
        "Address(Building: ; Street: )".Should().BeEquivalentTo(new Address(null, null).ToString());
    }
}

public class Person : ValueType<Person>
{
    public PersonName Name { get; }
    public decimal Height { get; }
    public DateTime BirthDate { get; }

    public Person(PersonName name, decimal height, DateTime birthDate)
    {
        Name = name;
        Height = height;
        BirthDate = birthDate;
    }
}