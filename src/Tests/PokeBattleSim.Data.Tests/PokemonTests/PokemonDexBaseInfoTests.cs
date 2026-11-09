using Xunit;
using System;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class PokemonDexBaseInfoTests
    {
        [Fact]
        public void PokemonDexBaseInfo_PropertiesInitializedCorrectly()
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field);

            Assert.Equal("Pikachu", baseInfo.Name);
            Assert.Equal(25u, baseInfo.DexNumber);
            Assert.Equal(4u, baseInfo.Length);
            Assert.Equal(6u, baseInfo.Weight);
            Assert.Equal(Morphologies.Animal, baseInfo.Morphology);
            Assert.Equal(PokemonTypes.Electric, baseInfo.PrimaryType);
            Assert.Equal(PokemonTypes.None, baseInfo.SecondaryType);
        }

        [Fact]
        public void PokemonDexBaseInfo_PropertiesCanBeModified()
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field);

            baseInfo.Name = "Raichu";
            baseInfo.Length = 8u;
            baseInfo.Weight = 30u;

            Assert.Equal("Raichu", baseInfo.Name);
            Assert.Equal(8u, baseInfo.Length);
            Assert.Equal(30u, baseInfo.Weight);
        }

        [Fact]
        public void PokemonDexBaseInfo_AcceptsFractionalLengthAndWeight()
        {
            var baseInfo = new PokemonDexBaseInfo("Joltik", 595u, 0.1, 0.6, Morphologies.Animal, PokemonTypes.Bug, PokemonTypes.Electric, EggGroups.Field);

            Assert.Equal(0.1, baseInfo.Length);
            Assert.Equal(0.6, baseInfo.Weight);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.5)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        public void PokemonDexBaseInfo_RejectsNonPositiveLengthAndWeight(double value)
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field);

            Assert.Throws<ArgumentOutOfRangeException>(() => baseInfo.Length = value);
            Assert.Throws<ArgumentOutOfRangeException>(() => baseInfo.Weight = value);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new PokemonDexBaseInfo("X", 1u, value, 1, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, EggGroups.Field));
        }

        [Fact]
        public void PokemonDexBaseInfo_ToDexSingleType()
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field);
            var result = baseInfo.ToDex();

            Assert.Contains("#25", result);
            Assert.Contains("Pikachu", result);
            Assert.Contains("4", result); // length
            Assert.Contains("6", result); // weight
            Assert.Contains("Electric", result);
            Assert.DoesNotContain("/", result); // No secondary type display
        }

        [Fact]
        public void PokemonDexBaseInfo_ToDexDualType()
        {
            var baseInfo = new PokemonDexBaseInfo("Charizard", 6u, 17u, 90u, Morphologies.Animal, PokemonTypes.Fire, PokemonTypes.Flying, EggGroups.Monster);
            var result = baseInfo.ToDex();

            Assert.Contains("Fire/Flying", result);
        }

        [Fact]
        public void PokemonDexBaseInfo_ToDexContainsMorphology()
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Monster);
            var result = baseInfo.ToDex();

            Assert.Contains("Animal", result);
        }
    }
}
