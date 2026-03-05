using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class MoveTests
    {
        [Fact]
        public void Move_PropertiesInitialized()
        {
            var moveStats = new MoveStats(1, 2, 3, 4, 5);
            var move = new Move(1u, "Thunderbolt", PokemonTypes.Electric, "A powerful electric move", moveStats, true);

            Assert.Equal(1u, move.MoveId);
            Assert.Equal("Thunderbolt", move.Name);
            Assert.Equal(PokemonTypes.Electric, move.Type);
            Assert.Equal(PokemonTypes.None, move.SecondaryType);
            Assert.Equal("A powerful electric move", move.Description);
            Assert.True(move.IsClashable);
            Assert.Empty(move.Tags);
        }

        [Fact]
        public void Move_WithSecondaryTypeAndTags()
        {
            var moveStats = new MoveStats();
            var tags = new List<Tags> { Tags.FightingSTAB };
            var move = new Move(1u, "Aqua-Electric", PokemonTypes.Water, "Hybrid move", moveStats, false, PokemonTypes.Electric, tags);

            Assert.Equal(PokemonTypes.Water, move.Type);
            Assert.Equal(PokemonTypes.Electric, move.SecondaryType);
            Assert.False(move.IsClashable);
            Assert.Single(move.Tags);
        }

        [Fact]
        public void Move_NullTagsDefaultsToEmpty()
        {
            var moveStats = new MoveStats();
            var move = new Move(1u, "Tackle", PokemonTypes.Normal, "A basic move", moveStats, true);

            Assert.Empty(move.Tags);
        }

        [Fact]
        public void Move_PropertiesCanBeModified()
        {
            var moveStats = new MoveStats();
            var move = new Move(1u, "Tackle", PokemonTypes.Normal, "A basic move", moveStats, true);

            move.Name = "Super Tackle";
            move.IsClashable = false;

            Assert.Equal("Super Tackle", move.Name);
            Assert.False(move.IsClashable);
        }

        [Fact]
        public void Move_ToDexSingleType()
        {
            var moveStats = new MoveStats(1, 2, 0, 4, 5);
            var move = new Move(1u, "Thunderbolt", PokemonTypes.Electric, "A powerful move", moveStats, true);
            var result = move.ToDex();

            Assert.Contains("Thunderbolt", result);
            Assert.Contains("Electric", result);
            Assert.Contains("A powerful move", result);
            Assert.DoesNotContain("/", result); // No secondary type
        }

        [Fact]
        public void Move_ToDexDualType()
        {
            var moveStats = new MoveStats();
            var move = new Move(1u, "Hybrid", PokemonTypes.Electric, "Dual type", moveStats, true, PokemonTypes.Water);
            var result = move.ToDex();

            Assert.Contains("Electric/Water", result);
        }

        [Fact]
        public void Move_ToDexIncludesMoveStats()
        {
            var moveStats = new MoveStats(2, 3, 1, 5, 6);
            var move = new Move(1u, "PowerStrike", PokemonTypes.Fighting, "desc", moveStats, true);
            var result = move.ToDex();

            Assert.Contains("Priority", result);
            Assert.Contains("Hit Dice", result);
            Assert.Contains("Damage", result);
        }
    }
}
