using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Skill(int _baseValue, string _name)
    {
        public int BaseValue { get; set; } = _baseValue;
    
        public string Name { get; set; } = _name;

        public SkillGrades Grade => (SkillGrades)BaseValue;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";
    }
}
