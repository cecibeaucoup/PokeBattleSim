using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Skill(int _baseValue, Skills _name): IStat<Skills, SkillGrades>
    {
        public int BaseValue { get; set; } = _baseValue;
    
        public Skills Name { get; set; } = _name;

        public SkillGrades Grade => (SkillGrades)BaseValue;

        public string ToDex() => $"- {Name}: {Grade} ({BaseValue})";
    }
}
