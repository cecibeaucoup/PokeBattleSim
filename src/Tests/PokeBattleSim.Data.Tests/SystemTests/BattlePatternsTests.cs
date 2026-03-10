using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.System;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class BattlePatternsTests
    {
        [Fact]
        public void BattlePatterns_DefaultValues()
        {
            var bp = new BattlePatterns();

            Assert.Empty(bp.OffensiveActions);
            Assert.Empty(bp.DefensiveActions);
        }

        [Fact]
        public void BattlePatterns_InitializeWithOffensiveActions()
        {
            var offensive = new Dictionary<AttackPatterns, MoveStats>
            {
                { AttackPatterns.Aggresive, new MoveStats(1, 3, 0, 5, 2) }
            };

            var bp = new BattlePatterns(offensive);

            Assert.Single(bp.OffensiveActions);
            Assert.True(bp.OffensiveActions.ContainsKey(AttackPatterns.Aggresive));
            Assert.Empty(bp.DefensiveActions);
        }

        [Fact]
        public void BattlePatterns_InitializeWithDefensiveActions()
        {
            var defensive = new Dictionary<DefensePatterns, MoveStats>
            {
                { DefensePatterns.Defend, new MoveStats(0, 0, 0, 0, 0) }
            };

            var bp = new BattlePatterns(null, defensive);

            Assert.Empty(bp.OffensiveActions);
            Assert.Single(bp.DefensiveActions);
            Assert.True(bp.DefensiveActions.ContainsKey(DefensePatterns.Defend));
        }

        [Fact]
        public void BattlePatterns_InitializeWithBothActions()
        {
            var offensive = new Dictionary<AttackPatterns, MoveStats>
            {
                { AttackPatterns.Aggresive, new MoveStats(1, 4, 0, 6, 3) },
                { AttackPatterns.Rapid, new MoveStats(2, 2, 1, 3, 1) }
            };

            var defensive = new Dictionary<DefensePatterns, MoveStats>
            {
                { DefensePatterns.Evade, new MoveStats(0, 0, 0, 0, 0) },
                { DefensePatterns.Counter, new MoveStats(0, 2, 0, 3, 0) }
            };

            var bp = new BattlePatterns(offensive, defensive);

            Assert.Equal(2, bp.OffensiveActions.Count);
            Assert.Equal(2, bp.DefensiveActions.Count);
        }

        [Fact]
        public void BattlePatterns_OffensiveActionsMoveStatsAreCorrect()
        {
            var stats = new MoveStats(1, 3, 2, 5, 4);
            var offensive = new Dictionary<AttackPatterns, MoveStats>
            {
                { AttackPatterns.Guarded, stats }
            };

            var bp = new BattlePatterns(offensive);

            var retrieved = bp.OffensiveActions[AttackPatterns.Guarded];
            Assert.Equal(1, retrieved.Priority);
            Assert.Equal(3, retrieved.HitDice);
            Assert.Equal(2, retrieved.HitAutos);
            Assert.Equal(5, retrieved.DamageDice);
            Assert.Equal(4, retrieved.DamageAutos);
        }

        [Fact]
        public void BattlePatterns_DefensiveActionsMoveStatsAreCorrect()
        {
            var stats = new MoveStats(0, 1, 0, 2, 0);
            var defensive = new Dictionary<DefensePatterns, MoveStats>
            {
                { DefensePatterns.Conceal, stats }
            };

            var bp = new BattlePatterns(null, defensive);

            var retrieved = bp.DefensiveActions[DefensePatterns.Conceal];
            Assert.Equal(0, retrieved.Priority);
            Assert.Equal(1, retrieved.HitDice);
            Assert.Equal(0, retrieved.HitAutos);
            Assert.Equal(2, retrieved.DamageDice);
            Assert.Equal(0, retrieved.DamageAutos);
        }

        [Fact]
        public void BattlePatterns_PropertiesCanBeModified()
        {
            var bp = new BattlePatterns();

            bp.OffensiveActions[AttackPatterns.Judicious] = new MoveStats(0, 2, 1, 4, 2);
            bp.DefensiveActions[DefensePatterns.Defend] = new MoveStats(0, 0, 0, 0, 0);

            Assert.Single(bp.OffensiveActions);
            Assert.Single(bp.DefensiveActions);
        }

        [Fact]
        public void BattlePatterns_AllAttackPatternsCanBeUsed()
        {
            var offensive = new Dictionary<AttackPatterns, MoveStats>
            {
                { AttackPatterns.Aggresive, new MoveStats() },
                { AttackPatterns.Guarded, new MoveStats() },
                { AttackPatterns.Judicious, new MoveStats() },
                { AttackPatterns.Rapid, new MoveStats() }
            };

            var bp = new BattlePatterns(offensive);

            Assert.Equal(4, bp.OffensiveActions.Count);
        }

        [Fact]
        public void BattlePatterns_AllDefensePatternsCanBeUsed()
        {
            var defensive = new Dictionary<DefensePatterns, MoveStats>
            {
                { DefensePatterns.Counter, new MoveStats() },
                { DefensePatterns.Evade, new MoveStats() },
                { DefensePatterns.Defend, new MoveStats() },
                { DefensePatterns.Conceal, new MoveStats() }
            };

            var bp = new BattlePatterns(null, defensive);

            Assert.Equal(4, bp.DefensiveActions.Count);
        }
    }
}
