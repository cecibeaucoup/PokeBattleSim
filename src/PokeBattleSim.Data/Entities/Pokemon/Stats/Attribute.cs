using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Attribute(int _baseValue, Attributes _name) : IStat<Attributes, AttributeGrades>
    {
        public int BaseValue { get; set; } = _baseValue;

        public Attributes Name { get; set; } = _name;

        public AttributeGrades Grade => BaseValue < 16 ? (AttributeGrades)BaseValue : AttributeGrades.Ex;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";
    }
}
