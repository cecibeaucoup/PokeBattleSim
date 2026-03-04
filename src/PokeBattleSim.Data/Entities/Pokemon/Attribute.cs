using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities
{
    public class Attribute (int _baseValue, string _name)
    {   
        public int BaseValue { get; set; } = _baseValue;
    
        public string Name { get; set; } = _name;

        public AttributeGrades Grade => BaseValue < 16 ? (AttributeGrades)BaseValue : AttributeGrades.Ex;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";
    }
}
