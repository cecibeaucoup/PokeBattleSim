using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class TrainerSheetTests
{
    [Fact]
    public void TrainerSheet_Properties()
    {
        var sheet = new TrainerSheet("Ash", 10u, Regions.Kanto);
        Assert.Equal("Ash", sheet.Name);
        Assert.Equal(10u, sheet.Age);
        Assert.Equal(Regions.Kanto, sheet.CurrentRegion);
        Assert.Empty(sheet.Badges);
    }
}
