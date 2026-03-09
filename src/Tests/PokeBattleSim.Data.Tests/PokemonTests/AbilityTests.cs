using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class AbilityTests
    {
        [Fact]
        public void Ability_PropertiesAndToDex()
        {
            var a = new Ability(1u, "Levitate", "Prevents ground damage");
            Assert.Equal(1u, a.AbilityId);
            Assert.Equal("Levitate", a.Name);
            Assert.Contains("Levitate", a.ToDex());
            Assert.Empty(a.Tags);
        }

        [Fact]
        public void Ability_PropertiesInitialized()
        {
            var ability = new Ability(1u, "Static", "May paralyze on contact");

            Assert.Equal(1u, ability.AbilityId);
            Assert.Equal("Static", ability.Name);
            Assert.Equal("May paralyze on contact", ability.Description);
            Assert.Empty(ability.Tags);
        }

        [Fact]
        public void Ability_PropertiesCanBeModified()
        {
            var ability = new Ability(1u, "Static", "May paralyze on contact");

            ability.Name = "Lightning Rod";
            ability.Description = "Redirects electric moves";

            Assert.Equal("Lightning Rod", ability.Name);
            Assert.Equal("Redirects electric moves", ability.Description);
        }

        [Fact]
        public void Ability_WithTags()
        {
            var tags = new List<Tags> { Tags.ElectricSTAB };
            var ability = new Ability(1u, "Static", "Description") { Tags = tags };

            Assert.Single(ability.Tags);
        }

        [Fact]
        public void Ability_ToDexWithoutTags()
        {
            var ability = new Ability(1u, "Static", "May paralyze on contact");
            var result = ability.ToDex();

            Assert.Contains("Static", result);
            Assert.Contains("May paralyze on contact", result);
            Assert.DoesNotContain("Tags", result); // No tags, so Tags line shouldn't appear
        }

        [Fact]
        public void Ability_ToDexWithTags()
        {
            var tags = new List<Tags> { Tags.ElectricSTAB, Tags.ShockVeil };
            var ability = new Ability(1u, "Static", "Description") { Tags = tags };
            var result = ability.ToDex();

            Assert.Contains("Tags", result);
        }
    }
}
