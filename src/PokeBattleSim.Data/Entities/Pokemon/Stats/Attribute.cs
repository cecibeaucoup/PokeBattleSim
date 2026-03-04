using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Attribute(int _baseValue, string _name) : IStat<AttributeGrades>
    {
        public int BaseValue { get; set; } = _baseValue;

        public string Name { get; set; } = _name;

        public AttributeGrades Grade => BaseValue < 16 ? (AttributeGrades)BaseValue : AttributeGrades.Ex;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";
    }
}
