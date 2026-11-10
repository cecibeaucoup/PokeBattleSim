using Xunit;
using PokeBattleSim.Battler.System.Stats;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.System;

namespace PokeBattleSim.Battler.Tests.SystemTests
{
    public class CombatStatCalculatorTests
    {
        private static CombatStatInputs Inputs(int power = 5, int toughness = 5, int speed = 8, int stamina = 8,
            int aim = 0, int brawl = 0, int evasion = 0, int friendship = 0, double weight = 50) => new(
            new Dictionary<Attributes, int>
            {
                [Attributes.Power] = power,
                [Attributes.Toughness] = toughness,
                [Attributes.Speed] = speed,
                [Attributes.Stamina] = stamina
            },
            new Dictionary<Skills, int>
            {
                [Skills.Aim] = aim,
                [Skills.Brawl] = brawl,
                [Skills.Efficiency] = 0,
                [Skills.Evasion] = evasion
            },
            friendship,
            weight);

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        [InlineData(12, 4)]
        [InlineData(15, 5)]
        [InlineData(16, 6)]
        [InlineData(20, 6)]
        public void Tiers_MapFullLetterGrades(int value, int tier) => Assert.Equal(tier, Tiers.Of(value));

        [Fact]
        public void AnotherLife_BaseStats()
        {
            var stats = new AnotherLifeCalculator().Calculate(Inputs(aim: 2, evasion: 2, friendship: 1));

            Assert.Equal(12, stats.DamageDice);
            Assert.Equal(7, stats.Defense);
            Assert.Equal(16, stats.HitDice);
            Assert.Equal(8, stats.Priority);
            Assert.Equal(17, stats.Wounds);
            Assert.Equal(12, stats.Dodge);
            Assert.Equal(10, stats.AimAutos);
        }

        [Fact]
        public void AnotherLife_StaminaScalesWithTier()
        {
            var calculator = new AnotherLifeCalculator();

            Assert.Equal(3 * 14, calculator.Calculate(Inputs(stamina: 14)).Wounds);
            Assert.Equal(4 * 16, calculator.Calculate(Inputs(stamina: 16)).Wounds);
        }

        [Fact]
        public void AnotherLife_ValuesAboveEx_UseFourWoundsPerPoint()
        {
            var calculator = new AnotherLifeCalculator();

            Assert.Equal(4 * 20, calculator.Calculate(Inputs(stamina: 20)).Wounds);
            Assert.Equal(Data.Enums.AttributeGrades.Ex, Tiers.GradeOf(20));
        }

        [Fact]
        public void Zephias_ValuesAboveEx_TreatedAsEx()
        {
            var stats = new ZephiasCalculator().Calculate(Inputs(stamina: 20, power: 18));

            Assert.Equal(2 * 20 + 6, stats.Wounds);
            Assert.Equal(2 * 18 + 6, stats.DamageDice);
        }

        [Fact]
        public void Zephias_BaseStats()
        {
            var stats = new ZephiasCalculator().Calculate(Inputs(brawl: 5, weight: 1000));

            Assert.Equal(21, stats.Wounds);
            Assert.Equal(5, stats.Dodge);
            Assert.Equal(0, stats.Priority);
            Assert.Equal(16, stats.HitDice);
            Assert.True(stats.MeleeThresholdReduced);
            Assert.False(stats.AimThresholdReduced);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 3)]
        public void Zephias_DodgeCountsMinusAndPlusGrades(int speed, int dodge) =>
            Assert.Equal(dodge, new ZephiasCalculator().Calculate(Inputs(speed: speed)).Dodge);

        [Fact]
        public void ExcessAutos_BecomeDice()
        {
            var stats = new AnotherLifeCalculator().Calculate(Inputs(speed: 2, aim: 2));

            Assert.Equal(4, stats.AimAutos);
            Assert.Equal(10, stats.AimHitDice);
        }

        [Theory]
        [InlineData(50, 0)]
        [InlineData(100, 1)]
        [InlineData(999, 1)]
        [InlineData(1000, 2)]
        public void WeightBonus_UsesOrdersOfMagnitude(double weight, int bonus) =>
            Assert.Equal(bonus, CombatStatCalculatorBase.WeightBonus(weight));
    }
}
