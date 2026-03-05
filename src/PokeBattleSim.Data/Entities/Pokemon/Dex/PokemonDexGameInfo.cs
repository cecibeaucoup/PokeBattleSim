using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Dex
{
    public class PokemonDexGameInfo(IEnumerable<PokeAttribute> _attributes, IEnumerable<PokeSkill> _skills, IEnumerable<MobilityTypes> _mobility, 
                                    IEnumerable<string>? _possibleAbilities = null, IEnumerable<string>? _possibleMoves = null)
    {
        public IEnumerable<PokeAttribute> Attributes { get; set; } = _attributes;

        public IEnumerable<PokeSkill> Skills { get; set; } = _skills;

        public IEnumerable<MobilityTypes> Mobility { get; set; } = _mobility;

        public IEnumerable<string> PossibleAbilities { get; set; } = _possibleAbilities ?? [];

        public IEnumerable<string> PossibleMoves { get; set; } = _possibleMoves ?? [];

        public string ToDex()
        {
            string strBuilder = "";

            strBuilder += $"Mobility: {string.Join(", ", Mobility)}\n\n";

            strBuilder += "Attributes:\n";
            strBuilder += $"{string.Join("\n", Attributes.Select(a => a.ToDex()))}\n\n";

            strBuilder += "Skills:\n";
            strBuilder += $"{string.Join("\n", Skills.Select(s => s.ToDex()))}\n\n";

            strBuilder += "Possible Abilities:\n";
            strBuilder += $"{(PossibleAbilities.Any() ? string.Join("\n", PossibleAbilities.Select(a => a)) : "None")}\n\n";
            strBuilder += "Possible Moves:\n";
            strBuilder += $"{(PossibleMoves.Any() ? string.Join("\n", PossibleMoves.Select(m => m)) : "None")}\n";

            return strBuilder;
        }
    }
}
