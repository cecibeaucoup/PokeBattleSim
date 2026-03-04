using Xunit;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Data.Tests;

public class AttributeTests
{
    [Fact]
    public void Attribute_PropertiesAndToDex()
    {
        var attr = new Attribute(10, "Pow");
        Assert.Equal(10, attr.BaseValue);
        Assert.Equal("Pow", attr.Name);
        Assert.Contains("10", attr.ToDex());
    }
}
