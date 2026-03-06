using System;
using System.Collections.Generic;
using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Data.Tests
{
    public class PokemonSheetTests
    {
        private PokemonDex CreateTestDex()
        {
            var attrs = new List<PokeAttribute>()
            {
                new(1, Attributes.Power),
                new(1, Attributes.Toughness),
                new(1, Attributes.Speed),
                new(1, Attributes.Stamina)
            };
            var skills = new List<PokeSkill>()
            {
                new(1, Skills.Aim),
                new(1, Skills.Brawl),
                new(1, Skills.Efficiency),
                new(1, Skills.Evasion)
            };
            var mobility = new MobilityTypes[] { MobilityTypes.Ground };
            return new PokemonDex("P", 1u, 1u, 1u, Morphologies.Animal, PokemonTypes.Normal, PokemonTypes.None, EggGroups.NoGroup, attrs, skills, mobility);
        }

        [Fact]
        public void PokemonSheet_InitializesFromDex()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            Assert.Equal("Buddy", sheet.Nickname);
            Assert.Equal(dex.BaseInfo.Length, sheet.Length);
            Assert.Equal(dex.GameInfo.Attributes, sheet.BaseAttributes);
            Assert.Equal(Grades.E, sheet.Friendship);
        }

        [Fact]
        public void PokemonSheet_InitialHealthAndEnergy()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            Assert.Equal(1, sheet.HealthPoints.Item1);
            Assert.Equal(1u, sheet.HealthPoints.Item2);
            Assert.Equal(4, sheet.EnergyLevels.Item1);
            Assert.Equal(4u, sheet.EnergyLevels.Item2);
        }

        [Fact]
        public void PokemonSheet_InitialState()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            Assert.False(sheet.IsFainted);
            Assert.Equal(0, sheet.ExpInvested);
        }

        [Fact]
        public void PokemonSheet_PropertiesCanBeModified()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            sheet.IsFainted = true;
            sheet.ExpInvested = 100;
            sheet.HealthPoints = new Tuple<int, uint>(0, 10);
            sheet.Friendship = Grades.A;
        
            Assert.True(sheet.IsFainted);
            Assert.Equal(100, sheet.ExpInvested);
            Assert.Equal(Grades.A, sheet.Friendship);
        }

        [Fact]
        public void PokemonSheet_AbilitiesAndMoves()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            Assert.Empty(sheet.Abilities);
            Assert.Empty(sheet.Moves);
            Assert.Empty(sheet.Merits);
        }

        [Fact]
        public void PokemonSheet_FormatsCorrectly()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            var result = sheet.ToSheet();
        
            Assert.Contains("Buddy", result);
            Assert.Contains("Attributes:", result);
            Assert.Contains("Skills:", result);
        }

        [Fact]
        public void PokemonSheet_CalculateTotalAttributesBasic()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            // Access through ToSheet which calls CalculateTotalAttributes
            var result = sheet.ToSheet();
            Assert.Contains("Power", result);
            Assert.Contains("Toughness", result);
        }

        [Fact]
        public void PokemonSheet_CalculateTotalSkillsBasic()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            // Access through ToSheet which calls CalculateTotalSkills
            var result = sheet.ToSheet();
            Assert.Contains("Aim", result);
            Assert.Contains("Brawl", result);
        }

        [Fact]
        public void PokemonSheet_WithMerits()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            var meritAttrs = new List<PokeAttribute>
            {
                new(1, Attributes.Power)
            };
            var merit = new Merit(MeritTypes.Ace, MeritFocus.Attributes, meritAttrs);
            sheet.Merits = new[] { merit };
        
            Assert.Single(sheet.Merits);
        }

        [Fact]
        public void PokemonSheet_DexInfoAccessible()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
        
            Assert.Equal(dex.BaseInfo.Name, sheet.DexInfo.Name);
            Assert.Equal(dex.BaseInfo.DexNumber, sheet.DexInfo.DexNumber);
        }

        [Fact]
        public void PokemonSheet_ContainsNickname()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Sparky", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Sparky", result);
        }

        [Fact]
        public void PokemonSheet_ContainsMorphology()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Morphology", result);
            Assert.Contains("Animal", result);
        }

        [Fact]
        public void PokemonSheet_ContainsType()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Type", result);
            Assert.Contains("Normal", result);
        }

        [Fact]
        public void PokemonSheet_ContainsDualType()
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
            var dex = new PokemonDex("Charizard", 6u, 17u, 90u, Morphologies.Animal, 
                PokemonTypes.Fire, PokemonTypes.Flying, EggGroups.Monster, attrs, skills, new[] { MobilityTypes.Ground });
            var sheet = new PokemonSheet("Blaze", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Fire/Flying", result);
        }

        [Fact]
        public void PokemonSheet_ContainsHealthAndEnergy()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Health:", result);
            Assert.Contains("Energy:", result);
        }

        [Fact]
        public void PokemonSheet_ContainsAttributes()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Attributes:", result);
            Assert.Contains("Power", result);
            Assert.Contains("Toughness", result);
        }

        [Fact]
        public void PokemonSheet_ContainsSkills()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Skills:", result);
            Assert.Contains("Aim", result);
            Assert.Contains("Brawl", result);
        }

        [Fact]
        public void PokemonSheet_ContainsAbilities()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Abilities:", result);
        }

        [Fact]
        public void PokemonSheet_ContainsMoves()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Moves:", result);
        }

        [Fact]
        public void PokemonSheet_ContainsMerits()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            Assert.Contains("Merits:", result);
        }

        [Fact]
        public void PokemonSheet_WithAbilities()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            sheet.Abilities = new[]
            {
                new Ability(1u, "Static", "Paralyze effect"),
                new Ability(2u, "Lightning Rod", "Redirect electricity")
            };

            var result = sheet.ToSheet();

            Assert.Contains("Static", result);
            Assert.Contains("Lightning Rod", result);
        }

        [Fact]
        public void PokemonSheet_WithMoves()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            sheet.Moves = new[] { "Thunderbolt", "Quick Attack", "Tail Whip" };

            var result = sheet.ToSheet();

            Assert.Contains("Thunderbolt", result);
            Assert.Contains("Quick Attack", result);
            Assert.Contains("Tail Whip", result);
        }

        [Fact]
        public void PokemonSheet_EmptyAbilitiesShowsNone()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            
            var result = sheet.ToSheet();

            // When abilities are empty, should show "None"
            var resultLines = result.Split('\n');
            var abilitiesIndex = Array.FindIndex(resultLines, x => x.Contains("Abilities:"));
            Assert.True(abilitiesIndex >= 0);
            Assert.Contains("None", result.Substring(result.IndexOf("Abilities:")));
        }

        [Fact]
        public void PokemonSheet_WithModifiedPhysicalStats()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            sheet.Length = 5;
            sheet.Weight = 8;

            var result = sheet.ToSheet();

            Assert.Contains("5 m", result);
            Assert.Contains("8", result);
        }

        [Fact]
        public void PokemonSheet_WithFaintedStatus()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);
            sheet.IsFainted = true;

            var result = sheet.ToSheet();

            // Verify sheet generates output even when fainted
            Assert.NotEmpty(result);
            Assert.Contains("Buddy", result);
        }

        [Fact]
        public void PokemonSheet_FormatsMultilineCorrectly()
        {
            var dex = CreateTestDex();
            var sheet = new PokemonSheet("Buddy", dex);

            var result = sheet.ToSheet();

            // Should contain newlines for proper formatting
            Assert.Contains("\n", result);
            
            // Check major sections are present
            int nicknameCount = result.Split("Nickname:").Length - 1;
            Assert.True(nicknameCount >= 1);
        }
    }
}
