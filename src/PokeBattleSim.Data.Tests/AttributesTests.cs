using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Data.Tests;

public class AttributesTests
{
    [Fact]
    public void Sum_AddsAttributes()
    {
        var a1 = new Attributes(1, 2, 3, 4);
        var a2 = new Attributes(5, 6, 7, 8);

        var sum = Attributes.Sum(a1, a2);

        Assert.Equal(6, sum.Power.BaseValue);
        Assert.Equal(8, sum.Speed.BaseValue);
        Assert.Equal(10, sum.Toughness.BaseValue);
        Assert.Equal(12, sum.Stamina.BaseValue);
    }
}
