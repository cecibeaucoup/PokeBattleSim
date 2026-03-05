using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Stats
{
    public class PokeSkill(int _baseValue, Skills _name): IStat<Skills, SkillGrades>
    {
        public int BaseValue { get; set; } = _baseValue;
    
        public Skills Name { get; set; } = _name;

        public SkillGrades Grade => (SkillGrades)BaseValue;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";

        public static IEnumerable<PokeSkill> GetDefaultStats()
        {
            foreach (var skill in Enum.GetValues<Skills>())
            {
                yield return new PokeSkill(0, skill);
            }
        }
    }
}
