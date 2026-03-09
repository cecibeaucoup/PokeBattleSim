using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Data.Tests
{
    public class MeritTests
    {
        private readonly List<PokeAttribute> _attrs = new()
        { 
            new(1, Attributes.Power),
            new(1, Attributes.Toughness),
            new(1, Attributes.Speed),
            new(1, Attributes.Stamina) 
        };

        private readonly List<PokeSkill> _skills = new()
        {
            new(1, Skills.Aim),
            new(1, Skills.Brawl),
            new(1, Skills.Efficiency),
            new(1, Skills.Evasion)
        };

        [Fact]
        public void Merit_Properties()
        {
            var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, _attrs);
            Assert.Equal(MeritTypes.Ace, m.MeritType);
            Assert.Equal(MeritFocus.Attributes, m.MeritFocus);
        }
    
        [Fact]
        public void Merit_ConstructorWithAttributes()
        {
            var m = new Merit(MeritTypes.Prodigy, MeritFocus.Attributes, _attrs);
        
            Assert.Equal(MeritTypes.Prodigy, m.MeritType);
            Assert.Equal(MeritFocus.Attributes, m.MeritFocus);
            Assert.NotEmpty(m.StatBoost);
        }

        [Fact]
        public void Merit_ConstructorWithSkills()
        {
            var m = new Merit(MeritTypes.Ace, MeritFocus.Skills, _skills);
        
            Assert.Equal(MeritTypes.Ace, m.MeritType);
            Assert.Equal(MeritFocus.Skills, m.MeritFocus);
            Assert.NotEmpty(m.StatBoost);
        }

        [Fact]
        public void Merit_WithOptionalMoveAndType()
        {
            var move = new Move(1u, "Test", PokemonTypes.Normal, "desc", new PokeBattleSim.Data.Entities.Pokemon.Moves.MoveStats(), true);
            var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, _attrs, move, PokemonTypes.Electric);
        
            Assert.NotNull(m.ChosenMove);
            Assert.Equal(PokemonTypes.Electric, m.BoostedType);
        }

        [Fact]
        public void Merit_PropertiesCanBeModified()
        {
            var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, _attrs);
            m.MeritType = MeritTypes.Prodigy;
        
            Assert.Equal(MeritTypes.Prodigy, m.MeritType);
        }

        [Fact]
        public void Merit_MultipleMeritTypes()
        {
            var meritTypes = new[] { MeritTypes.Ace, MeritTypes.Prodigy, MeritTypes.Specialist };
        
            foreach (var meritType in meritTypes)
            {
                var m = new Merit(meritType, MeritFocus.Attributes, _attrs);
                Assert.Equal(meritType, m.MeritType);
            }
        }

        [Fact]
        public void Merit_BoostedTypeNone()
        {
            var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, _attrs);
            Assert.Equal(PokemonTypes.None, m.BoostedType);
        }
    }
}
