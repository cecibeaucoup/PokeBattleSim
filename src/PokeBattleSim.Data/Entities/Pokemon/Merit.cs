using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Merit (MeritTypes _type, MeritFocus _focus, Attributes _attributeBoost, Skills _skillBoost, Move? _chosenMove = null, PokemonTypes _boostedType = PokemonTypes.None)
    {
        public MeritTypes Type { get; set; } = _type;
    
        public MeritFocus Focus { get; set; } = _focus;

        public Attributes AttributeBoost { get; set; } = _attributeBoost;

        public Skills SkillBoost { get; set; } = _skillBoost;   

        public Move ChosenMove { get; set; } = _chosenMove!;

        public PokemonTypes BoostedType { get; set; } = _boostedType;
    }
}
