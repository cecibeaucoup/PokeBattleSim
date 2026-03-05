using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class TrainerSheetTests
    {
        [Fact]
        public void TrainerSheet_Properties()
        {
            var sheet = new TrainerSheet("Ash", 10u, Regions.Kanto);
            Assert.Equal("Ash", sheet.Name);
            Assert.Equal(10u, sheet.Age);
            Assert.Equal(Regions.Kanto, sheet.CurrentRegion);
            Assert.Empty(sheet.Badges);
        }

        [Fact]
        public void TrainerSheet_PropertiesCanBeModified()
        {
            var sheet = new TrainerSheet("Ash", 10u, Regions.Kanto);
        
            sheet.Name = "Gary";
            sheet.Age = 15u;
            sheet.CurrentRegion = Regions.Johto;
        
            Assert.Equal("Gary", sheet.Name);
            Assert.Equal(15u, sheet.Age);
            Assert.Equal(Regions.Johto, sheet.CurrentRegion);
        }

        [Fact]
        public void TrainerSheet_AddBadges()
        {
            var sheet = new TrainerSheet("Ash", 10u, Regions.Kanto);
            var badges = new List<Badge>
            {
                new("Boulder Badge", "Rock type", Regions.Kanto),
                new("Cascade Badge", "Water type", Regions.Kanto)
            };
            sheet.Badges = badges;
        
            Assert.Equal(2, sheet.Badges.Count());
        }

        [Fact]
        public void TrainerSheet_DifferentRegions()
        {
            var sheets = new[]
            {
                new TrainerSheet("Trainer1", 20u, Regions.Kanto),
                new TrainerSheet("Trainer2", 21u, Regions.Johto),
                new TrainerSheet("Trainer3", 22u, Regions.Hoenn)
            };

            Assert.Equal(Regions.Kanto, sheets[0].CurrentRegion);
            Assert.Equal(Regions.Johto, sheets[1].CurrentRegion);
            Assert.Equal(Regions.Hoenn, sheets[2].CurrentRegion);
        }

        [Fact]
        public void TrainerSheet_VaryingAges()
        {
            var young = new TrainerSheet("Young", 5u, Regions.Kanto);
            var adult = new TrainerSheet("Adult", 30u, Regions.Kanto);
        
            Assert.True(adult.Age > young.Age);
        }

        [Fact]
        public void TrainerSheet_EmptyBadgesInitially()
        {
            var sheet = new TrainerSheet("NewTrainer", 10u, Regions.Kanto);
        
            Assert.NotNull(sheet.Badges);
            Assert.Empty(sheet.Badges);
        }
    }
}
