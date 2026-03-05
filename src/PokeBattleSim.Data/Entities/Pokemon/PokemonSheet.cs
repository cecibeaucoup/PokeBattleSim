using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class PokemonSheet(string _nickname, PokemonDex _dexEntry)
    {
        #region Base Info

        public string Nickname { get; private set; } = _nickname;

        public PokemonDexBaseInfo DexInfo { get; private set; } = _dexEntry.BaseInfo;

        public uint Length { get; set; } = _dexEntry.BaseInfo.Length;

        public uint Weight { get; set; } = _dexEntry.BaseInfo.Weight;

        public IEnumerable<MobilityTypes> Mobility { get; private set; } = _dexEntry.GameInfo.Mobility;

        public bool IsFainted { get; set; } = false;

        public int ExpInvested { get; set; } = 0;

        #endregion

        #region Game Stats

        public Tuple<int, uint> HealthPoints { get; set; } = new Tuple<int, uint>(1, 1);

        public Tuple<int, uint> EnergyLevels { get; set; } = new Tuple<int, uint>(4, 4);

        #endregion

        #region Game Info

        public IEnumerable<PokeAttribute> BaseAttributes { get; set; } =  _dexEntry.GameInfo.Attributes;

        public IEnumerable<PokeSkill> BaseSkills { get; set; } = _dexEntry.GameInfo.Skills;

        public IEnumerable<Ability> Abilities { get; set; } = [];

        public IEnumerable<string> Moves { get; set; } = [];

        public IEnumerable<Merit> Merits { get; set; } = [];

        #endregion

        public string ToSheet()
        {
            string strBuilder = $"Nickname: {Nickname}\n";
            strBuilder += $"Length: {Length} m - ";
            strBuilder += $"Weight: {Weight} kg\n";
            strBuilder += $"Morphology: {DexInfo.Morphology}\n";
            strBuilder += $"Type: {DexInfo.PrimaryType}{(DexInfo.SecondaryType != PokemonTypes.None ? $"/{DexInfo.SecondaryType}" : "")}\n";
            strBuilder += $"Health: {HealthPoints.Item1}/{HealthPoints.Item2}\n";
            strBuilder += $"Energy: {EnergyLevels.Item1}/{EnergyLevels.Item2}\n";

            strBuilder += "Abilities:\n";
            strBuilder += $"{(Abilities.Any() ? string.Join("\n", Abilities.Select(a => a.Name)) : "None")}\n\n";

            strBuilder += "Attributes:\n";
            strBuilder += $"{string.Join("\n", CalculateTotalAttributes().Select(a => $"{a.ToDex()}"))}\n\n";

            strBuilder += "Skills:\n";
            strBuilder += $"{string.Join("\n", CalculateTotalSkills().Select(s => $"{s.ToDex()}"))}\n\n";

            strBuilder += "Moves:\n";
            strBuilder += $"{(Moves.Any() ? string.Join("\n", Moves.Select(m => m)) : "None")}\n\n";

            strBuilder += "Merits:\n";
            strBuilder += $"{(Merits.Any() ? string.Join("\n", Merits.Select(m => m.MeritType)) : "None")}\n";

            return strBuilder;
        }

        private List<PokeAttribute> CalculateTotalAttributes()
        {
            List<PokeAttribute> totalAttributes = [];

            IEnumerable<Merit> attributeMerits = Merits.Where(m => m.MeritFocus == MeritFocus.Attributes);

            foreach (var attr in Enum.GetValues<Attributes>())
            {
                var sum = BaseAttributes.Single(a => a.Name == attr).BaseValue;
                
                var meritBoost = attributeMerits
                    .SelectMany(m => m.StatBoost)
                    .OfType<PokeAttribute>()
                    .Where(stat => stat.Name.Equals(attr))
                    .Sum(stat => stat.BaseValue);

                totalAttributes.Add(new PokeAttribute(sum + meritBoost, attr));
            }            
            return totalAttributes;
        }

        private List<PokeSkill> CalculateTotalSkills()
        {
            List<PokeSkill> totalSkills = [];

            IEnumerable<Merit> skillMerits = Merits.Where(m => m.MeritFocus == MeritFocus.Skills);

            foreach (var skill in Enum.GetValues<Skills>())
            {
                var sum = BaseSkills.Single(s => s.Name == skill).BaseValue;
                
                var meritBoost = skillMerits
                    .SelectMany(m => m.StatBoost)
                    .OfType<PokeSkill>()
                    .Where(stat => stat.Name.Equals(skill))
                    .Sum(stat => stat.BaseValue);

                totalSkills.Add(new PokeSkill(sum + meritBoost, skill));
            }

            return totalSkills;
        }
    }
}
