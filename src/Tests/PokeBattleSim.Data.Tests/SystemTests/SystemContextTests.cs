using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.System;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class SystemContextTests
    {
        [Fact]
        public void SystemContext_DefaultRollsConfiguration()
        {
            var context = new SystemContext();

            Assert.NotNull(context.RollsConfiguration);
            Assert.Equal(6u, context.RollsConfiguration.DieSize);
            Assert.Equal(4u, context.RollsConfiguration.Threshold);
        }

        [Fact]
        public void SystemContext_DefaultBattlePatternsConfiguration()
        {
            var context = new SystemContext();

            Assert.NotNull(context.BattlePatternsConfiguration);
            Assert.Empty(context.BattlePatternsConfiguration.OffensiveActions);
            Assert.Empty(context.BattlePatternsConfiguration.DefensiveActions);
        }

        [Fact]
        public void SystemContext_RollsConfigurationCanBeModified()
        {
            var context = new SystemContext();
            var customRolls = new Rolls(10, 5);

            context.RollsConfiguration = customRolls;

            Assert.Equal(10u, context.RollsConfiguration.DieSize);
            Assert.Equal(5u, context.RollsConfiguration.Threshold);
        }

        [Fact]
        public void SystemContext_BattlePatternsConfigurationCanBeModified()
        {
            var context = new SystemContext();

            var offensive = new Dictionary<AttackPatterns, MoveStats>
            {
                { AttackPatterns.Aggresive, new MoveStats(1, 3, 0, 5, 2) }
            };
            var defensive = new Dictionary<DefensePatterns, MoveStats>
            {
                { DefensePatterns.Defend, new MoveStats() }
            };

            context.BattlePatternsConfiguration = new BattlePatterns(offensive, defensive);

            Assert.Single(context.BattlePatternsConfiguration.OffensiveActions);
            Assert.Single(context.BattlePatternsConfiguration.DefensiveActions);
        }

        [Fact]
        public void SystemContext_ModifyRollsProperties()
        {
            var context = new SystemContext();

            context.RollsConfiguration.DieSize = 20;
            context.RollsConfiguration.Threshold = 10;

            Assert.Equal(20u, context.RollsConfiguration.DieSize);
            Assert.Equal(10u, context.RollsConfiguration.Threshold);
        }

        [Fact]
        public void SystemContext_AddBattlePatternActions()
        {
            var context = new SystemContext();

            context.BattlePatternsConfiguration.OffensiveActions[AttackPatterns.Rapid] = new MoveStats(2, 2, 1, 3, 1);
            context.BattlePatternsConfiguration.DefensiveActions[DefensePatterns.Evade] = new MoveStats(0, 0, 0, 0, 0);

            Assert.Single(context.BattlePatternsConfiguration.OffensiveActions);
            Assert.Single(context.BattlePatternsConfiguration.DefensiveActions);
            Assert.True(context.BattlePatternsConfiguration.OffensiveActions.ContainsKey(AttackPatterns.Rapid));
            Assert.True(context.BattlePatternsConfiguration.DefensiveActions.ContainsKey(DefensePatterns.Evade));
        }
    }
}
