using System;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
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

        public Dictionary<string, IEnumerable<Attribute>> AttributeValues { get; set; } = new Dictionary<string, IEnumerable<Attribute>>()
        {
            { "Base", _dexEntry.GameInfo.Attributes },
        };

        public Dictionary<string, IEnumerable<Skill>> SkillValues { get; set; } = new Dictionary<string, IEnumerable<Skill>>()
        {
            { "Base", _dexEntry.GameInfo.Skills },
        };

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
            strBuilder += $"{AttributeValues["Base"].ToString()}\n\n";

            strBuilder += "Skills:\n";
            strBuilder += $"{SkillValues["Base"].ToString()}\n\n";           

            strBuilder += "Moves:\n";
            strBuilder += $"{(Moves.Any() ? string.Join("\n", Moves.Select(m => m)) : "None")}\n\n";

            strBuilder += "Merits:\n";
            strBuilder += $"{(Merits.Any() ? string.Join("\n", Merits.Select(m => m.MeritType)) : "None")}\n";

            return strBuilder;
        }

        private IEnumerable<Attribute> CalculateTotalAttributes()
        {
            List<Attribute> totalAttributes = new List<Attribute>(AttributeValues["Base"]);
            // Placeholder for attribute calculation logic based on base attributes, abilities, merits, etc.
            foreach (var attr in AttributeValues)
            {
                
            }

            return totalAttributes;
        }

        private IEnumerable<Skill> CalculateTotalSkills()
        {
            // Placeholder for skill calculation logic based on base skills, abilities, merits, etc.
            return SkillValues["Base"];
        }
    }
}
