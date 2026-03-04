using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Dex;

public class PokemonDexGameInfo(Attributes _attributes, Skills _skills, IEnumerable<MobilityTypes> _mobility, IEnumerable<string>? _possibleAbilities = null, IEnumerable<string>? _possibleMoves = null)
{
    public Attributes Attributes { get; set; } = _attributes;

        public Skills Skills { get; set; } = _skills;

        public IEnumerable<MobilityTypes> Mobility { get; set; } = _mobility;

        public IEnumerable<string> PossibleAbilities { get; set; } = _possibleAbilities ?? [];

        public IEnumerable<string> PossibleMoves { get; set; } = _possibleMoves ?? [];
}
