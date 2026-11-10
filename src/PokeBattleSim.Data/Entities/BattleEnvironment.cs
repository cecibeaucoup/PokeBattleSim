using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.System;

namespace PokeBattleSim.Data.Entities
{
    /// <summary>
    /// Local battle conditions: one weather and one terrain at a time. Setting a new one replaces the old.
    /// </summary>
    public class BattleEnvironment(Weather _weather = Weather.NoWeather, Terrains _terrain = Terrains.Ground)
    {
        public Weather Weather { get; private set; } = _weather;

        public Terrains Terrain { get; private set; } = _terrain;

        public IEnumerable<TagBinding> Tags =>
            BattleEnvironmentCatalog.GetBindings(Weather).Concat(BattleEnvironmentCatalog.GetBindings(Terrain));

        public void SetWeather(Weather weather)
        {
            BattleEnvironmentCatalog.GetBindings(weather);
            Weather = weather;
        }

        public void SetTerrain(Terrains terrain)
        {
            BattleEnvironmentCatalog.GetBindings(terrain);
            Terrain = terrain;
        }

        public void Clear()
        {
            Weather = Weather.NoWeather;
            Terrain = Terrains.Ground;
        }

        public override string ToString() => $"Weather: {Weather}, Terrain: {Terrain}";
    }
}
