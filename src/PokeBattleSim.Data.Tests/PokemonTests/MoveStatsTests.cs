using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PokeBattleSim.Data.Entities;
using PokeBattleSim.Data.Entities.Pokemon.Moves;

namespace PokeBattleSim.Data.Tests
{
    public class MoveStatsTests
    {
        [Fact]
        public void MoveStats_DefaultValues()
        {
            var moveStats = new MoveStats();

            Assert.Equal(0, moveStats.Priority);
            Assert.Equal(0, moveStats.HitDice);
            Assert.Equal(0, moveStats.HitAutos);
            Assert.Equal(0, moveStats.DamageDice);
            Assert.Equal(0, moveStats.DamageAutos);
        }

        [Fact]
        public void MoveStats_InitializeWithValues()
        {
            var moveStats = new MoveStats(2, 3, 1, 5, 6);

            Assert.Equal(2, moveStats.Priority);
            Assert.Equal(3, moveStats.HitDice);
            Assert.Equal(1, moveStats.HitAutos);
            Assert.Equal(5, moveStats.DamageDice);
            Assert.Equal(6, moveStats.DamageAutos);
        }

        [Fact]
        public void MoveStats_PropertiesCanBeModified()
        {
            var moveStats = new MoveStats();

            moveStats.Priority = 3;
            moveStats.DamageDice = 10;

            Assert.Equal(3, moveStats.Priority);
            Assert.Equal(10, moveStats.DamageDice);
        }

        [Fact]
        public void MoveStats_ToDexContainsAllValues()
        {
            var moveStats = new MoveStats(2, 3, 1, 5, 6);
            var result = moveStats.ToDex();

            Assert.Contains("Priority: 2", result);
            Assert.Contains("Hit Dice: 3", result);
            Assert.Contains("Auto Hits: 1", result);
            Assert.Contains("Damage Dice: 5", result);
            Assert.Contains("Auto Damage 6", result);
        }
        
        [Fact]
        public void MoveStats_ToDexWithoutTagsNoTagsLine()
        {
            var moveStats = new MoveStats(1, 2, 0, 4, 5);
            var result = moveStats.ToDex();

            Assert.DoesNotContain("Tags:", result);
        }
    }
}
