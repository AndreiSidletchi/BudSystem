using BudSystem.WorldBank;

namespace Tests;

public class Tests
{
    [Test]
    public void ValidIsoCodeShouldReturnCountry()
    {
        var country = WorldBankApi.GetCountry("BR");
            
        Assert.That(country, !Is.Null);
        Assert.That(country!.id, Is.EqualTo("BRA"));
        Assert.That(country.name, Is.EqualTo("Brazil"));
        Assert.That(country.region.value, Is.EqualTo("Latin America & Caribbean "));
        Assert.That(country.capitalCity, Is.EqualTo("Brasilia"));
    }

    [Test]
    public void InvalidCodeShouldThrowException()
    {
        var code = "non-existent code";
        Assert.Throws<ArgumentOutOfRangeException>(() => WorldBankApi.GetCountry(code));
    }

    [Test]
    public void NumericCodeShouldThrowException()
    {
        var code = "123";
        Assert.Throws<ArgumentOutOfRangeException>(() => WorldBankApi.GetCountry(code));
    }

    [Test]
    public void NullStringShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => WorldBankApi.GetCountry(null!));
    }
}