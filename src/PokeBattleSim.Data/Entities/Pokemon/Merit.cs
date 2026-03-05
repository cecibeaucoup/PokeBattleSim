using System;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Merit
    {
        public Merit(MeritTypes _type, MeritFocus _focus, IEnumerable<Attribute> _statBoost, Move? _chosenMove = null, PokemonTypes _boostedType = PokemonTypes.None)
        {
            MeritType = _type;
            Focus = _focus;
            StatBoost = _statBoost.Cast<IStat<Enum, Enum>>();
            ChosenMove = _chosenMove;
            BoostedType = _boostedType;
        }

        public Merit(MeritTypes _type, MeritFocus _focus, IEnumerable<Skill> _statBoost, Move? _chosenMove = null, PokemonTypes _boostedType = PokemonTypes.None)
        {
            MeritType = _type;
            Focus = _focus;
            StatBoost =  _statBoost.Cast<IStat<Enum, Enum>>();
            ChosenMove = _chosenMove;
            BoostedType = _boostedType;
        }

        public MeritTypes MeritType { get; set; }
    
        public MeritFocus Focus { get; set; }

        public IEnumerable<IStat<Enum, Enum>> StatBoost { get; set; }

        public Move? ChosenMove { get; set; }

        public PokemonTypes BoostedType { get; set; }
    }
}
