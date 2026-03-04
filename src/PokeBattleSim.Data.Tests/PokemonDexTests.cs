using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class PokemonDexTests
{
    [Fact]
    public void PokemonDex_ToDex_And_AddAbilityMove()
    {
        var attrs = new Attributes(1,2,3,4);
        var skills = new Skills(1,1,1,1);
        var mobility = new MobilityTypes[] { MobilityTypes.Ground };

        var dex = new PokemonDex("TestMon", 999u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, attrs, skills, mobility);

        var dexStr = dex.ToDex();
        Assert.Contains("#999", dexStr);

        var ability = new Ability(5u, "MyAbility", "desc");
        dex.AddAbility(ability);
        Assert.Contains("MyAbility", dex.GameInfo.PossibleAbilities);

        var move = new Move(1u, "Tackle", PokemonTypes.Normal, "Hits target", new MoveStats(0, 1, 0, 1, 0), false);
        dex.AddMove(move);
        Assert.Contains("Tackle", dex.GameInfo.PossibleMoves);
    }
}
