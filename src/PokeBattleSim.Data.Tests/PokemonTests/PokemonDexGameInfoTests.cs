using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Stats;

namespace PokeBattleSim.Data.Tests
{
    public class PokemonDexGameInfoTests
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
            new(1, Skills.Brawl)
        };

        private readonly MobilityTypes[] _mobility = { MobilityTypes.Ground };

        [Fact]
        public void PokemonDexGameInfo_PropertiesInitialized()
        {
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility);

            Assert.Equal(_attrs, gameInfo.Attributes);
            Assert.Equal(_skills, gameInfo.Skills);
            Assert.Equal(_mobility, gameInfo.Mobility);
            Assert.Empty(gameInfo.PossibleAbilities);
            Assert.Empty(gameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDexGameInfo_WithAbilitiesAndMoves()
        {
            var abilities = new[] { "Static" };
            var moves = new[] { "Thunderbolt" };

            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility, abilities, moves);

            Assert.Single(gameInfo.PossibleAbilities);
            Assert.Single(gameInfo.PossibleMoves);
            Assert.Contains("Static", gameInfo.PossibleAbilities);
            Assert.Contains("Thunderbolt", gameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDexGameInfo_NullAbilitiesDefaultsToEmpty()
        {
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility, null, null);

            Assert.Empty(gameInfo.PossibleAbilities);
            Assert.Empty(gameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDexGameInfo_ToDexContainsAllSections()
        {
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility);
            var result = gameInfo.ToDex();

            Assert.Contains("Mobility:", result);
            Assert.Contains("Attributes:", result);
            Assert.Contains("Skills:", result);
            Assert.Contains("Possible Abilities:", result);
            Assert.Contains("Possible Moves:", result);
        }

        [Fact]
        public void PokemonDexGameInfo_ToDexWithAbilitiesAndMoves()
        {
            var abilities = new[] { "Static", "Lightning Rod" };
            var moves = new[] { "Thunderbolt" };

            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility, abilities, moves);
            var result = gameInfo.ToDex();

            Assert.Contains("Static", result);
            Assert.Contains("Lightning Rod", result);
            Assert.Contains("Thunderbolt", result);
        }

        [Fact]
        public void PokemonDexGameInfo_ToDexWithNone_WhenNoAbilitiesOrMoves()
        {
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility);
            var result = gameInfo.ToDex();

            Assert.Contains("None", result); // Should appear for abilities and moves if empty
        }

        [Fact]
        public void PokemonDexGameInfo_PropertiesCanBeModified()
        {
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility);
            var newAttrs = new List<PokeAttribute> { new(5, Attributes.Power) };

            gameInfo.Attributes = newAttrs;

            Assert.Single(gameInfo.Attributes);
        }
    }
}
