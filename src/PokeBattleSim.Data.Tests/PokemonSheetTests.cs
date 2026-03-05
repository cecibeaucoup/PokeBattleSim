using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class PokemonSheetTests
{
    [Fact]
    public void PokemonSheet_InitializesFromDex()
    {
        var attrs = new List<Attribute>()
        {
            new(1, Attributes.Power),
            new(1, Attributes.Toughness),
            new(1, Attributes.Speed),
            new(1, Attributes.Stamina)
        };
        var skills = new List<Skill>()
        {
            new(1, Skills.Aim),
            new(1, Skills.Brawl),
            new(1, Skills.Efficiency),
            new(1, Skills.Evasion)
        };
        var mobility = new MobilityTypes[] { MobilityTypes.Ground };
        var dex = new PokemonDex("P", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, attrs, skills, mobility);

        var sheet = new PokemonSheet("Buddy", dex);
        Assert.Equal("Buddy", sheet.Nickname);
        Assert.Equal(dex.BaseInfo.Length, sheet.Length);
        Assert.Equal(dex.GameInfo.Attributes, sheet.AttributeValues["Base"]);
    }
}
