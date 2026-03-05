using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Stats
{
    public class PokeAttribute(int _baseValue, Attributes _name) : IStat<Attributes, AttributeGrades>
    {
        public int BaseValue { get; set; } = _baseValue;

        public Attributes Name { get; set; } = _name;

        public AttributeGrades Grade => BaseValue < 16 ? (AttributeGrades)BaseValue : AttributeGrades.Ex;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";

        public static IEnumerable<PokeAttribute> GetDefaultStats()
        {
            foreach (var attr in Enum.GetValues<Attributes>())
            {
                yield return new PokeAttribute(0, attr);
            }
        }
    }
    
}
