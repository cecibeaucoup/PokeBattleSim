using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.StaticInfo;

public static class EffectivenessChecker
{
    public static Dictionary<PokemonTypes, int> NormalEffectiviness { get; } = new Dictionary<PokemonTypes, int>
    {
        { PokemonTypes.Normal, 1 },
        { PokemonTypes.Fire, 1 },
        { PokemonTypes.Water, 1 },
        { PokemonTypes.Electric, 1 },
        { PokemonTypes.Grass, 1 },
        { PokemonTypes.Ice, 1 },
        { PokemonTypes.Fighting, 1 },
        { PokemonTypes.Poison, 1 },
        { PokemonTypes.Ground, 1 },
        { PokemonTypes.Flying, 1 },
        { PokemonTypes.Psychic, 1 },
        { PokemonTypes.Bug, 1 },
        { PokemonTypes.Rock, -1 },
        { PokemonTypes.Ghost, 0 },
        { PokemonTypes.Dragon, 1 },
        { PokemonTypes.Dark, 1 },
        { PokemonTypes.Steel, -1 },
        { PokemonTypes.Fairy, 0 }
    };
}
