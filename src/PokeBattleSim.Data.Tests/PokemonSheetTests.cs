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
        var attrs = new List<Attribute>()
        {
            new(1, "Power"),
            new(1, "Toughness"),
            new(1, "Speed"),
            new(1, "Stamina")
        };
        var skills = new List<Skill>()
        {
            new(1, "Aim"),
            new(1, "Brawl"),
            new(1, "Efficiency"),
            new(1, "Evasion")
        };
        var mobility = new MobilityTypes[] { MobilityTypes.Ground };
        var dex = new PokemonDex("P", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, attrs, skills, mobility);

        var sheet = new PokemonSheet("Buddy", dex);
        Assert.Equal("Buddy", sheet.Nickname);
        Assert.Equal(dex.BaseInfo.Length, sheet.Length);
        Assert.Equal(dex.GameInfo.Attributes, sheet.AttributeValues["Base"]);
    }
}
