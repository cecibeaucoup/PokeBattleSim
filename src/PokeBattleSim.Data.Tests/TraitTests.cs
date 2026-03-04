using Xunit;
using PokeBattleSim.Data.Entities.Trainer;

namespace PokeBattleSim.Data.Tests;

public class TraitTests
{
    [Fact]
    public void Trait_Properties()
    {
        var t = new Trait(1u, "Brave", "Fearless");
        Assert.Equal(1u, t.TraitId);
        Assert.Equal("Brave", t.Name);
    }
}
