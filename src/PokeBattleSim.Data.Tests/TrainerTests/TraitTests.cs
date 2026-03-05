using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Entities;
using System.Linq;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class TraitTests
    {
        [Fact]
        public void Trait_Properties()
        {
            var tags = new []
            {
                Tags.FightingSTAB,
                Tags.CritImmune
            };
            var t = new Trait(1u, "Brave", "Fearless", tags);
            Assert.Equal(1u, t.TraitId);
            Assert.Equal("Brave", t.Name);
            Assert.Equal("Fearless", t.Description);
            Assert.Equal(2, t.Tags.Count());
        }

        [Fact]
        public void Trait_PropertiesCanBeModified()
        {
            var t = new Trait(1u, "Cold", "Unfeeling");
        
            t.Name = "Warm";
            t.TraitId = 2u;
        
            Assert.Equal("Warm", t.Name);
            Assert.Equal(2u, t.TraitId);
        }

        [Fact]
        public void Trait_MultipleTraits()
        {
            var traits = new[]
            {
                new Trait(1u, "Brave", "Fearless"),
                new Trait(2u, "Calm", "Peaceful"),
                new Trait(3u, "Hasty", "Impulsive")
            };

            Assert.Equal(3, traits.Length);
            Assert.Equal("Brave", traits[0].Name);
            Assert.Equal("Hasty", traits[2].Name);
        }

        [Fact]
        public void Trait_IdUniqueness()
        {
            var t1 = new Trait(1u, "Trait1", "Desc");
            var t2 = new Trait(2u, "Trait2", "Desc");
        
            Assert.NotEqual(t1.TraitId, t2.TraitId);
        }

        [Fact]
        public void Trait_ToDexFormatting()
        {
            var tags = new []
            {
                Tags.FightingSTAB,
                Tags.CritImmune
            };
            var t = new Trait(1u, "Brave", "Fearless", tags);
            var result = t.ToDex();

            Assert.Contains("Brave", result);
            Assert.Contains("Fearless", result);
            Assert.Contains("FightingSTAB", result);
            Assert.Contains("CritImmune", result);
        }
    }
}
