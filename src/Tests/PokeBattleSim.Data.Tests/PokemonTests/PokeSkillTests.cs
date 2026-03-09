using System.Linq;
using Xunit;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Stats;


namespace PokeBattleSim.Data.Tests
{
    public class PokeSkillTests
    {
        [Fact]
        public void PokeSkill_PropertiesAndToDex()
        {
            var s = new PokeSkill(3, Skills.Aim);
            Assert.Equal(3, s.BaseValue);
            Assert.Equal(Skills.Aim, s.Name);
            Assert.Contains("3", s.ToDex());
        }

        [Fact]
        public void PokeSkill_PropertiesCanBeModified()
        {
            var PokeSkill = new PokeSkill(5, Skills.Brawl);
        
            PokeSkill.BaseValue = 10;
            PokeSkill.Name = Skills.Efficiency;

            Assert.Equal(10, PokeSkill.BaseValue);
            Assert.Equal(Skills.Efficiency, PokeSkill.Name);
        }

        [Fact]
        public void PokeSkill_AllPokeSkillTypes()
        {
            var skills = new[]
            {
                new PokeSkill(1, Skills.Aim),
                new PokeSkill(2, Skills.Brawl),
                new PokeSkill(3, Skills.Efficiency),
                new PokeSkill(4, Skills.Evasion)
            };

            Assert.Equal(4, skills.Length);
            Assert.Equal(Skills.Aim, skills[0].Name);
            Assert.Equal(Skills.Evasion, skills[3].Name);
        }

        [Fact]
        public void PokeSkill_ToDexContainBaseValue()
        {
            var skills = new PokeSkill(20, Skills.Aim);
            var result = skills.ToDex();

            Assert.Contains("20", result);
            Assert.Contains("Aim", result);
        }

        [Fact]
        public void PokeSkill_ZeroBaseValue()
        {
            var skills = new PokeSkill(0, Skills.Evasion);
            Assert.Equal(0, skills.BaseValue);
            Assert.Contains("0", skills.ToDex());
        }

        [Fact]
        public void PokeSkill_GradeCalculation()
        {
            var skill1 = new PokeSkill(1, Skills.Aim);
            var skill5 = new PokeSkill(5, Skills.Aim);

            // Grade should be based on BaseValue (cast to SkillGrades enum)
            var grade1 = skill1.Grade;
            var grade5 = skill5.Grade;
            
            // Just verify they're different (based on basevalue)
            Assert.True(grade1 != grade5 || skill1.BaseValue == skill5.BaseValue);
        }

        [Fact]
        public void PokeSkill_GetDefaultStats()
        {
            var defaultStats = PokeSkill.GetDefaultStats();

            // Should return all 4 skill types
            var statsList = defaultStats.ToList();
            Assert.Equal(4, statsList.Count);
            
            // All should have base value of 0
            Assert.All(statsList, skill => Assert.Equal(0, skill.BaseValue));
            
            // Should contain all skill types
            Assert.Contains(statsList, s => s.Name == Skills.Aim);
            Assert.Contains(statsList, s => s.Name == Skills.Brawl);
            Assert.Contains(statsList, s => s.Name == Skills.Efficiency);
            Assert.Contains(statsList, s => s.Name == Skills.Evasion);
        }

        [Fact]
        public void PokeSkill_GetDefaultStats_IsEnumerable()
        {
            var defaultStats = PokeSkill.GetDefaultStats();
            
            // Make sure it's enumerable and not null
            Assert.NotNull(defaultStats);
            Assert.True(defaultStats.Any());
        }    }
}