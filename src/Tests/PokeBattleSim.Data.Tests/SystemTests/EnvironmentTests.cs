using System;
using System.Collections.Generic;
using System.Linq;
using PokeBattleSim.Data.Entities;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.System;
using Xunit;

namespace PokeBattleSim.Data.Tests.SystemTests
{
    public class EnvironmentTests
    {
        [Fact]
        public void Defaults_AreNoWeatherAndGround()
        {
            var environment = new BattleEnvironment();

            Assert.Equal(Weather.NoWeather, environment.Weather);
            Assert.Equal(Terrains.Ground, environment.Terrain);
            Assert.Empty(environment.Tags);
        }

        [Fact]
        public void Setting_ReplacesPreviousWeatherAndTerrain()
        {
            var environment = new BattleEnvironment(Weather.Rain, Terrains.Grass);

            environment.SetWeather(Weather.Hail);
            environment.SetTerrain(Terrains.Ice);

            Assert.Equal(Weather.Hail, environment.Weather);
            Assert.Equal(Terrains.Ice, environment.Terrain);
        }

        [Fact]
        public void Tags_ListWeatherBindingsThenTerrainBindings()
        {
            var environment = new BattleEnvironment(Weather.Rain, Terrains.Misty);

            Assert.Equal(
                [
                    new TagBinding(MechanicTag.SetWeather, ResolutionTag.RainyWeather),
                    new TagBinding(MechanicTag.SetTerrain, ResolutionTag.MistyTerrain)
                ],
                environment.Tags);
        }

        [Fact]
        public void Clear_RestoresDefaults()
        {
            var environment = new BattleEnvironment(Weather.Sandstorm, Terrains.Sand);

            environment.Clear();

            Assert.Equal(Weather.NoWeather, environment.Weather);
            Assert.Equal(Terrains.Ground, environment.Terrain);
        }

        [Fact]
        public void Setting_UndefinedValueThrows()
        {
            var environment = new BattleEnvironment();

            Assert.Throws<ArgumentOutOfRangeException>(() => environment.SetWeather((Weather)999));
            Assert.Throws<ArgumentOutOfRangeException>(() => environment.SetTerrain((Terrains)999));
            Assert.Equal(Weather.NoWeather, environment.Weather);
        }

        [Fact]
        public void Catalog_HasEntryForEveryWeatherAndTerrain()
        {
            foreach (var weather in Enum.GetValues<Weather>())
                Assert.Equal(weather == Weather.NoWeather ? 0 : 1, BattleEnvironmentCatalog.GetBindings(weather).Count);

            foreach (var terrain in Enum.GetValues<Terrains>())
                Assert.Equal(terrain == Terrains.Ground ? 0 : 1, BattleEnvironmentCatalog.GetBindings(terrain).Count);
        }

        [Fact]
        public void Catalog_UsesDistinctResolutionPerValue()
        {
            var weatherTags = Enum.GetValues<Weather>().SelectMany(BattleEnvironmentCatalog.GetBindings).ToList();
            var terrainTags = Enum.GetValues<Terrains>().SelectMany(BattleEnvironmentCatalog.GetBindings).ToList();

            Assert.Equal(weatherTags.Count, weatherTags.Distinct().Count());
            Assert.Equal(terrainTags.Count, terrainTags.Distinct().Count());
        }
    }
}
