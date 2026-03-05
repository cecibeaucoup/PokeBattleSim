using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities
{
    public class Merit
    {
        public Merit(MeritTypes _type, MeritFocus _focus, IEnumerable<PokeAttribute> _statBoost, Move? _chosenMove = null, PokemonTypes _boostedType = PokemonTypes.None)
        {
            MeritType = _type;
            MeritFocus = _focus;
            StatBoost = _statBoost;
            ChosenMove = _chosenMove;
            BoostedType = _boostedType;
        }

        public Merit(MeritTypes _type, MeritFocus _focus, IEnumerable<PokeSkill> _statBoost, Move? _chosenMove = null, PokemonTypes _boostedType = PokemonTypes.None)
        {
            MeritType = _type;
            MeritFocus = _focus;
            StatBoost =  _statBoost;
            ChosenMove = _chosenMove;
            BoostedType = _boostedType;
        }

        public MeritTypes MeritType { get; set; }
    
        public MeritFocus MeritFocus { get; set; }

        public IEnumerable<IStat> StatBoost { get; set; }

        public Move? ChosenMove { get; set; }

        public PokemonTypes BoostedType { get; set; }
    }
}
