using System.Linq;
using Xunit;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Stats;

namespace PokeBattleSim.Data.Tests
{
    public class AttributeTests
    {
        [Fact]
        public void PokeAttribute_PropertiesAndToDex()
        {
            var attr = new PokeAttribute(10, Attributes.Power);
            Assert.Equal(10, attr.BaseValue);
            Assert.Equal(Attributes.Power, attr.Name);
            Assert.Contains("10", attr.ToDex());
        }

        [Fact]
        public void PokeAttribute_PropertiesCanBeModified()
        {
            var attr = new PokeAttribute(5, Attributes.Toughness);
        
            attr.BaseValue = 15;
            attr.Name = Attributes.Speed;

            Assert.Equal(15, attr.BaseValue);
            Assert.Equal(Attributes.Speed, attr.Name);
        }

        [Fact]
        public void PokeAttribute_AllAttributeTypes()
        {
            var attrs = new[]
            {
                new PokeAttribute(1, Attributes.Power),
                new PokeAttribute(2, Attributes.Toughness),
                new PokeAttribute(3, Attributes.Speed),
                new PokeAttribute(4, Attributes.Stamina)
            };

            Assert.Equal(4, attrs.Length);
            Assert.Equal(Attributes.Power, attrs[0].Name);
            Assert.Equal(Attributes.Stamina, attrs[3].Name);
        }

        [Fact]
        public void PokeAttribute_ToDexFormatting()
        {
            var attr = new PokeAttribute(25, Attributes.Power);
            var result = attr.ToDex();

            Assert.Contains("Power", result);
            Assert.Contains("25", result);
        }

        [Fact]
        public void PokeAttribute_GradeCalculation()
        {
            var low = new PokeAttribute(5, Attributes.Power);
            var high = new PokeAttribute(20, Attributes.Power);

            // Grade should be Ex for values >= 16
            if (high.BaseValue >= 16)
            {
                Assert.Equal(AttributeGrades.Ex, high.Grade);
            }
        }

        [Fact]
        public void PokeAttribute_GetDefaultStats()
        {
            var defaultStats = PokeAttribute.GetDefaultStats();

            // Should return all 4 attribute types
            var statsList = defaultStats.ToList();
            Assert.Equal(4, statsList.Count);
            
            // All should have base value of 0
            Assert.All(statsList, attr => Assert.Equal(0, attr.BaseValue));
            
            // Should contain all attribute types
            Assert.Contains(statsList, a => a.Name == Attributes.Power);
            Assert.Contains(statsList, a => a.Name == Attributes.Toughness);
            Assert.Contains(statsList, a => a.Name == Attributes.Speed);
            Assert.Contains(statsList, a => a.Name == Attributes.Stamina);
        }

        [Fact]
        public void PokeAttribute_GetDefaultStats_IsEnumerable()
        {
            var defaultStats = PokeAttribute.GetDefaultStats();
            
            // Make sure it's enumerable and not null
            Assert.NotNull(defaultStats);
            Assert.True(defaultStats.Any());
        }    }
}