using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests
{
    public class BadgeTests
    {
        [Fact]
        public void Badge_Properties()
        {
            var b = new Badge("Gym Badge", "Awarded for victory", Regions.Kanto);
            Assert.Equal("Gym Badge", b.Name);
            Assert.Equal(Regions.Kanto, b.BadgeRegion);
        }

        [Fact]
        public void Badge_DescriptionProperty()
        {
            var b = new Badge("Thunder Badge", "Defeat Brock", Regions.Kanto);
            Assert.Equal("Defeat Brock", b.Description);
        }

        [Fact]
        public void Badge_PropertiesCanBeModified()
        {
            var b = new Badge("Badge1", "Description", Regions.Kanto);
        
            b.Name = "Badge2";
            b.BadgeRegion = Regions.Johto;
        
            Assert.Equal("Badge2", b.Name);
            Assert.Equal(Regions.Johto, b.BadgeRegion);
        }

        [Fact]
        public void Badge_DifferentRegions()
        {
            var badges = new[]
            {
                new Badge("Kanto Badge", "Kanto", Regions.Kanto),
                new Badge("Johto Badge", "Johto", Regions.Johto),
                new Badge("Hoenn Badge", "Hoenn", Regions.Hoenn),
                new Badge("Sinnoh Badge", "Sinnoh", Regions.Sinnoh)
            };

            Assert.Equal(Regions.Kanto, badges[0].BadgeRegion);
            Assert.Equal(Regions.Johto, badges[1].BadgeRegion);
            Assert.Equal(Regions.Hoenn, badges[2].BadgeRegion);
            Assert.Equal(Regions.Sinnoh, badges[3].BadgeRegion);
        }
    }
}
