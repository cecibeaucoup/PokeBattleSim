using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class PokemonSheetTests
{
    [Fact]
    public void PokemonSheet_InitializesFromDex()
    {
        var attrs = new Attributes(1,1,1,1);
        var skills = new Skills(0,0,0,0);
        var mobility = new MobilityTypes[] { MobilityTypes.Ground };
        var dex = new PokemonDex("P", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, attrs, skills, mobility);

        var sheet = new PokemonSheet("Buddy", dex);
        Assert.Equal("Buddy", sheet.Nickname);
        Assert.Equal(dex.BaseInfo.Length, sheet.Length);
        Assert.Equal(dex.GameInfo.Attributes, sheet.AttributeValues["Base"]);
    }
}
