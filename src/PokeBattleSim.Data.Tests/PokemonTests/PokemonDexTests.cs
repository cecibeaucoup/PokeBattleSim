using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Entities.Pokemon.Moves;

namespace PokeBattleSim.Data.Tests
{
    public class PokemonDexTests
    {
        private PokemonDex CreateTestDex()
        {
            return new PokemonDex("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field, _attrs, _skills, _mobility, _senses);
        }

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

        private readonly MobilityTypes[] _mobility = { MobilityTypes.Ground };
        private readonly Senses[] _senses = { Senses.Vision };

        [Fact]
        public void PokemonDex_ConstructorWithBaseAndGameInfo()
        {
            var baseInfo = new PokemonDexBaseInfo("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field);
            var gameInfo = new PokemonDexGameInfo(_attrs, _skills, _mobility, _senses);

            var dex = new PokemonDex(baseInfo, gameInfo);

            Assert.Equal(baseInfo, dex.BaseInfo);
            Assert.Equal(gameInfo, dex.GameInfo);
        }

        [Fact]
        public void PokemonDex_ConstructorWithParameters()
        {
            var dex = new PokemonDex("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field, _attrs, _skills, _mobility, _senses);

            Assert.Equal("Pikachu", dex.BaseInfo.Name);
            Assert.Equal(25u, dex.BaseInfo.DexNumber);
            Assert.Equal(4u, dex.BaseInfo.Length);
            Assert.Equal(6u, dex.BaseInfo.Weight);
            Assert.Equal(Morphologies.Animal, dex.BaseInfo.Morphology);
            Assert.Equal(PokemonTypes.Electric, dex.BaseInfo.PrimaryType);
            Assert.Equal(PokemonTypes.None, dex.BaseInfo.SecondaryType);
        }

        [Fact]
        public void PokemonDex_ConstructorWithOptionalAbilitiesAndMoves()
        {
            var abilities = new[] { "Static", "Lightning Rod" };
            var moves = new[] { "Thunderbolt", "Quick Attack" };

            var dex = new PokemonDex("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, 
                                        EggGroups.Field, _attrs, _skills, _mobility, _senses,_possibleAbilities: abilities, _possibleMoves: moves);

            Assert.Equal(2, dex.GameInfo.PossibleAbilities.Count());
            Assert.Equal(2, dex.GameInfo.PossibleMoves.Count());
        }

        [Fact]
        public void PokemonDex_ToDex_ContainsExpectedInfo()
        {
            var dex = new PokemonDex("Pikachu", 25u, 4u, 6u, Morphologies.Animal, PokemonTypes.Electric, PokemonTypes.None, 
                                        EggGroups.Field, _attrs, _skills, _mobility, _senses);
            var result = dex.ToDex();

            Assert.Contains("#25", result);
            Assert.Contains("Pikachu", result);
            Assert.Contains("4", result); // length
            Assert.Contains("6", result); // weight
        }

        [Fact]
        public void PokemonDex_DualTypeHandledCorrectly()
        {
            var dex = new PokemonDex("Charizard", 6u, 17u, 90u, Morphologies.Animal, PokemonTypes.Fire, PokemonTypes.Flying, 
                                        EggGroups.Monster, _attrs, _skills, _mobility, _senses);

            Assert.Equal(PokemonTypes.Fire, dex.BaseInfo.PrimaryType);
            Assert.Equal(PokemonTypes.Flying, dex.BaseInfo.SecondaryType);
        }

        [Fact]
        public void PokemonDex_AddAbility_SingleAbility()
        {
            var dex = CreateTestDex();
            var ability = new Ability(1u, "Static", "May paralyze on contact");

            dex.AddAbility(ability);

            Assert.Contains("Static", dex.GameInfo.PossibleAbilities);
        }

        [Fact]
        public void PokemonDex_AddAbility_MultipleAbilities()
        {
            var dex = CreateTestDex();
            var ability1 = new Ability(1u, "Static", "May paralyze on contact");
            var ability2 = new Ability(2u, "Lightning Rod", "Redirects electric moves");

            dex.AddAbility(ability1);
            dex.AddAbility(ability2);

            var abilityList = dex.GameInfo.PossibleAbilities.ToList();
            Assert.Contains("Static", abilityList);
            Assert.Contains("Lightning Rod", abilityList);
        }

        [Fact]
        public void PokemonDex_AddAbility_DuplicateNotAdded()
        {
            var dex = CreateTestDex();
            var ability = new Ability(1u, "Static", "May paralyze on contact");

            dex.AddAbility(ability);
            var countAfterFirst = dex.GameInfo.PossibleAbilities.Count();
            
            dex.AddAbility(ability);
            var countAfterSecond = dex.GameInfo.PossibleAbilities.Count();

            Assert.Equal(countAfterFirst, countAfterSecond);
        }

        [Fact]
        public void PokemonDex_AddAbility_ToEmptyList()
        {
            var baseInfo = new PokemonDexBaseInfo("Test", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, EggGroups.NoGroup);
            var gameInfo = new PokemonDexGameInfo(
                new[] { new PokeAttribute(1, Attributes.Power) },
                new[] { new PokeSkill(1, Skills.Aim) },
                new[] { MobilityTypes.Ground },
                new[] { Senses.Vision }
            );
            var dex = new PokemonDex(baseInfo, gameInfo);

            var ability = new Ability(1u, "Test Ability", "A test ability");
            dex.AddAbility(ability);

            Assert.Contains("Test Ability", dex.GameInfo.PossibleAbilities);
        }

        [Fact]
        public void PokemonDex_AddMove_SingleMove()
        {
            var dex = CreateTestDex();
            var moveStats = new MoveStats(1, 2, 0, 4, 5);
            var move = new Move(1u, "Thunderbolt", PokemonTypes.Electric, "A powerful electric move", moveStats, true);

            dex.AddMove(move);

            Assert.Contains("Thunderbolt", dex.GameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDex_AddMove_MultipleMoves()
        {
            var dex = CreateTestDex();
            var moveStats1 = new MoveStats(1, 2, 0, 4, 5);
            var moveStats2 = new MoveStats(0, 1, 0, 3, 4);
            var move1 = new Move(1u, "Thunderbolt", PokemonTypes.Electric, "Electric attack", moveStats1, true);
            var move2 = new Move(2u, "Quick Attack", PokemonTypes.Normal, "Fast attack", moveStats2, true);

            dex.AddMove(move1);
            dex.AddMove(move2);

            var moveList = dex.GameInfo.PossibleMoves.ToList();
            Assert.Contains("Thunderbolt", moveList);
            Assert.Contains("Quick Attack", moveList);
        }

        [Fact]
        public void PokemonDex_AddMove_DuplicateNotAdded()
        {
            var dex = CreateTestDex();
            var moveStats = new MoveStats();
            var move = new Move(1u, "Tackle", PokemonTypes.Normal, "A basic move", moveStats, true);

            dex.AddMove(move);
            var countAfterFirst = dex.GameInfo.PossibleMoves.Count();
            
            dex.AddMove(move);
            var countAfterSecond = dex.GameInfo.PossibleMoves.Count();

            Assert.Equal(countAfterFirst, countAfterSecond);
        }

        [Fact]
        public void PokemonDex_AddMove_ToEmptyList()
        {
            var baseInfo = new PokemonDexBaseInfo("Test", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, EggGroups.NoGroup);
            var gameInfo = new PokemonDexGameInfo(
                new[] { new PokeAttribute(1, Attributes.Power) },
                new[] { new PokeSkill(1, Skills.Aim) },
                new[] { MobilityTypes.Ground },
                new[] { Senses.Vision }
            );
            var dex = new PokemonDex(baseInfo, gameInfo);

            var moveStats = new MoveStats();
            var move = new Move(1u, "Test Move", PokemonTypes.Normal, "A test move", moveStats, true);
            dex.AddMove(move);

            Assert.Contains("Test Move", dex.GameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDex_AddAbilityAndMove_Together()
        {
            var dex = CreateTestDex();
            var ability = new Ability(1u, "Static", "Paralyze ability");
            var moveStats = new MoveStats();
            var move = new Move(1u, "Thunderbolt", PokemonTypes.Electric, "Electric move", moveStats, true);

            dex.AddAbility(ability);
            dex.AddMove(move);

            Assert.Contains("Static", dex.GameInfo.PossibleAbilities);
            Assert.Contains("Thunderbolt", dex.GameInfo.PossibleMoves);
            Assert.Single(dex.GameInfo.PossibleAbilities);
            Assert.Single(dex.GameInfo.PossibleMoves);
        }

        [Fact]
        public void PokemonDex_AddAbility_WithInitialAbilities()
        {
            var attrs = new List<PokeAttribute>
            {
                new(1, Attributes.Power),
                new(1, Attributes.Toughness),
                new(1, Attributes.Speed),
                new(1, Attributes.Stamina)
            };
            var skills = new List<PokeSkill>
            {
                new(1, Skills.Aim),
                new(1, Skills.Brawl),
                new(1, Skills.Efficiency),
                new(1, Skills.Evasion)
            };
            var abilities = new[] { "Lightning Rod" };
            var senses = new[] { Senses.Vision };
            var dex = new PokemonDex("Pikachu", 25u, 4u, 6u, Morphologies.Animal, 
                PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field, attrs, skills, 
                new[] { MobilityTypes.Ground }, senses, _possibleAbilities: abilities);

            var newAbility = new Ability(2u, "Static", "Paralyze effect");
            dex.AddAbility(newAbility);

            var abilityList = dex.GameInfo.PossibleAbilities.ToList();
            Assert.Equal(2, abilityList.Count);
            Assert.Contains("Lightning Rod", abilityList);
            Assert.Contains("Static", abilityList);
        }
    }
}
