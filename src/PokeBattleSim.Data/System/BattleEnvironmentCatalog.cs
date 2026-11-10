using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.System
{
    /// <summary>
    /// Tag bindings that describe each weather and terrain. Every enum value has an entry;
    /// effect bindings are added here once the per-mechanic handlers exist.
    /// </summary>
    public static class BattleEnvironmentCatalog
    {
        private static readonly IReadOnlyList<TagBinding> None = [];

        private static readonly IReadOnlyDictionary<Weather, IReadOnlyList<TagBinding>> WeatherBindings =
            new Dictionary<Weather, IReadOnlyList<TagBinding>>
            {
                [Weather.NoWeather] = None,
                [Weather.Sunlight] = SetWeather(ResolutionTag.SunnyWeather),
                [Weather.Rain] = SetWeather(ResolutionTag.RainyWeather),
                [Weather.Sandstorm] = SetWeather(ResolutionTag.SandyWeather),
                [Weather.Hail] = SetWeather(ResolutionTag.SnowyWeather),
                [Weather.Fog] = SetWeather(ResolutionTag.FoggyWeather),
                [Weather.ToxicFog] = SetWeather(ResolutionTag.SmogWeather),
                [Weather.HarshSunlight] = SetWeather(ResolutionTag.HarshSunWeather),
                [Weather.HeavyRain] = SetWeather(ResolutionTag.StormyWeather),
                [Weather.StrongWinds] = SetWeather(ResolutionTag.WindyWeather),
                [Weather.ShadowSky] = SetWeather(ResolutionTag.ShadowWeather),
            };

        private static readonly IReadOnlyDictionary<Terrains, IReadOnlyList<TagBinding>> TerrainBindings =
            new Dictionary<Terrains, IReadOnlyList<TagBinding>>
            {
                [Terrains.Ground] = None,
                [Terrains.Water] = SetTerrain(ResolutionTag.WaterTerrain),
                [Terrains.WaterPlatforms] = SetTerrain(ResolutionTag.WaterPlatformsTerrain),
                [Terrains.DeepWater] = SetTerrain(ResolutionTag.DeepWaterTerrain),
                [Terrains.Sand] = SetTerrain(ResolutionTag.SandTerrain),
                [Terrains.Snow] = SetTerrain(ResolutionTag.SnowTerrain),
                [Terrains.Grass] = SetTerrain(ResolutionTag.GrassTerrain),
                [Terrains.Psychic] = SetTerrain(ResolutionTag.PsychicTerrain),
                [Terrains.Electric] = SetTerrain(ResolutionTag.ElectricTerrain),
                [Terrains.Misty] = SetTerrain(ResolutionTag.MistyTerrain),
                [Terrains.Ice] = SetTerrain(ResolutionTag.IceTerrain),
                [Terrains.Flaming] = SetTerrain(ResolutionTag.FlamingTerrain),
                [Terrains.Volcanic] = SetTerrain(ResolutionTag.VolcanicTerrain),
                [Terrains.Underground] = SetTerrain(ResolutionTag.UndergroundTerrain),
                [Terrains.Urban] = SetTerrain(ResolutionTag.UrbanTerrain),
                [Terrains.Mud] = SetTerrain(ResolutionTag.MudTerrain),
                [Terrains.Swamp] = SetTerrain(ResolutionTag.SwampTerrain),
            };

        public static IReadOnlyList<TagBinding> GetBindings(Weather weather) =>
            WeatherBindings.TryGetValue(weather, out var bindings)
                ? bindings
                : throw new ArgumentOutOfRangeException(nameof(weather), weather, "Weather has no catalog entry.");

        public static IReadOnlyList<TagBinding> GetBindings(Terrains terrain) =>
            TerrainBindings.TryGetValue(terrain, out var bindings)
                ? bindings
                : throw new ArgumentOutOfRangeException(nameof(terrain), terrain, "Terrain has no catalog entry.");

        private static IReadOnlyList<TagBinding> SetWeather(ResolutionTag resolution) =>
            [new TagBinding(MechanicTag.SetWeather, resolution)];

        private static IReadOnlyList<TagBinding> SetTerrain(ResolutionTag resolution) =>
            [new TagBinding(MechanicTag.SetTerrain, resolution)];
    }
}
