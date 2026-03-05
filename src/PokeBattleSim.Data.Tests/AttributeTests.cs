using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class AttributeTests
{
    [Fact]
    public void Attribute_PropertiesAndToDex()
    {
        var attr = new Attribute(10, Attributes.Power);
        Assert.Equal(10, attr.BaseValue);
        Assert.Equal(Attributes.Power, attr.Name);
        Assert.Contains("10", attr.ToDex());
    }
}
