using Xunit;
using PokeBattleSim.Data.System;

namespace PokeBattleSim.Data.Tests
{
    public class RollsTests
    {
        [Fact]
        public void Rolls_DefaultValues()
        {
            var rolls = new Rolls();

            Assert.Equal(6u, rolls.DieSize);
            Assert.Equal(4u, rolls.Threshold);
        }

        [Fact]
        public void Rolls_InitializeWithValues()
        {
            var rolls = new Rolls(10, 5);

            Assert.Equal(10u, rolls.DieSize);
            Assert.Equal(5u, rolls.Threshold);
        }

        [Fact]
        public void Rolls_PropertiesCanBeModified()
        {
            var rolls = new Rolls();

            rolls.DieSize = 12;
            rolls.Threshold = 6;

            Assert.Equal(12u, rolls.DieSize);
            Assert.Equal(6u, rolls.Threshold);
        }

        [Fact]
        public void Rolls_CustomDieSize()
        {
            var rolls = new Rolls(_dieSize: 20);

            Assert.Equal(20u, rolls.DieSize);
            Assert.Equal(4u, rolls.Threshold);
        }

        [Fact]
        public void Rolls_CustomThreshold()
        {
            var rolls = new Rolls(_threshold: 3);

            Assert.Equal(6u, rolls.DieSize);
            Assert.Equal(3u, rolls.Threshold);
        }
    }
}
