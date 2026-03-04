using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Data.Tests;

public class AbilityTests
{
    [Fact]
    public void Ability_PropertiesAndToDex()
    {
        var a = new Ability(1u, "Levitate", "Prevents ground damage");
        Assert.Equal(1u, a.AbilityId);
        Assert.Equal("Levitate", a.Name);
        Assert.Contains("Levitate", a.ToDex());
        Assert.Empty(a.Tags);
    }
}
