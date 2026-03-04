using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class BadgeTests
{
    [Fact]
    public void Badge_Properties()
    {
        var b = new Badge("Gym Badge", "Awarded for victory", Regions.Kanto);
        Assert.Equal("Gym Badge", b.Name);
        Assert.Equal(Regions.Kanto, b.BadgeRegion);
    }
}
