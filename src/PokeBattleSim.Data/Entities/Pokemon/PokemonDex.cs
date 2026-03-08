using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class PokemonDex
    {   

        #region Constructors
        public PokemonDex(PokemonDexBaseInfo _baseInfo, PokemonDexGameInfo _gameInfo)
        {
            BaseInfo = _baseInfo;
            GameInfo = _gameInfo;
        }

        public PokemonDex(string _name, uint _dexNumber, uint _length, uint _weight, Morphologies _morphology, PokemonTypes _priType, PokemonTypes _secType,
                            EggGroups _eggGroup1, IEnumerable<PokeAttribute> _attributes, IEnumerable<PokeSkill> _skills, IEnumerable<MobilityTypes> _mobility, 
                            IEnumerable<Senses> _senses, EggGroups _eggGroup2 = EggGroups.NoGroup, IEnumerable<string>? _possibleAbilities = null, 
                            IEnumerable<string>? _possibleMoves = null)
        {
            BaseInfo = new PokemonDexBaseInfo(_name, _dexNumber, _length, _weight, _morphology, _priType, _secType, _eggGroup1, _eggGroup2);
            GameInfo = new PokemonDexGameInfo(_attributes, _skills, _mobility, _senses, _possibleAbilities, _possibleMoves);
        }
        #endregion
        
        public PokemonDexBaseInfo BaseInfo { get; set; }

        public PokemonDexGameInfo GameInfo { get; set; }
    
        public string ToDex()
        {
            string strBuilder = "";

            strBuilder += $"#{BaseInfo.DexNumber} - {BaseInfo.Name}\n";
            strBuilder += $"Length: {BaseInfo.Length} m - ";
            strBuilder += $"Weight: {BaseInfo.Weight} kg\n";
            strBuilder += $"Morphology: {BaseInfo.Morphology}\n";
            strBuilder += $"Primary Type: {BaseInfo.PrimaryType}\n";
            strBuilder += $"Secondary Type: {BaseInfo.SecondaryType}\n\n";
            strBuilder += $"Mobility: {string.Join(", ", GameInfo.Mobility)}\n\n";
            strBuilder += $"Senses: {string.Join(", ", GameInfo.Senses)}\n\n";

            strBuilder += "Attributes:\n";

            strBuilder += "Skills:\n";

            strBuilder += "Possible Abilities:\n";
            strBuilder += $"{(GameInfo.PossibleAbilities.Any() ? string.Join("\n", GameInfo.PossibleAbilities.Select(a => a)) : "None")}\n\n";
            strBuilder += "Possible Moves:\n";
            strBuilder += $"{(GameInfo.PossibleMoves.Any() ? string.Join("\n", GameInfo.PossibleMoves.Select(m => m)) : "None")}\n";

            return strBuilder;
        }

        public void AddAbility(Ability ability)
        {
            if (!GameInfo.PossibleAbilities.Contains(ability.Name))
            {
                GameInfo.PossibleAbilities = GameInfo.PossibleAbilities.Append(ability.Name);
            }
        }

        public void AddMove(Move move)
        {
            if (!GameInfo.PossibleMoves.Contains(move.Name))
            {
                GameInfo.PossibleMoves = GameInfo.PossibleMoves.Append(move.Name);
            }
        }   
    }
}
