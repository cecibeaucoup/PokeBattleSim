using System.Text.Json;
using System.Collections.Generic;
using PokeBattleSim.Data.Enums;
using Xunit;

namespace PokeBattleSim.Data.Tests.SystemTests
{
    public class TagBindingTests
    {
        private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

        [Fact]
        public void Equality_ComparesMechanicAndResolutionSequence()
        {
            var a = new TagBinding(MechanicTag.SetWeather, ResolutionTag.SunnyWeather);
            var b = new TagBinding(MechanicTag.SetWeather, ResolutionTag.SunnyWeather);
            var c = new TagBinding(MechanicTag.SetWeather, ResolutionTag.RainyWeather);

            Assert.Equal(a, b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.NotEqual(a, c);
            Assert.Single(new HashSet<TagBinding> { a, b });
        }

        [Fact]
        public void Deconstruct_ReturnsMechanicAndResolutions()
        {
            var (mechanic, resolutions) = new TagBinding(MechanicTag.RestrictResistance,
                ResolutionTag.FireType, ResolutionTag.WaterType);

            Assert.Equal(MechanicTag.RestrictResistance, mechanic);
            Assert.Equal(2, resolutions.Count);
        }

        [Fact]
        public void ToString_FormatsMechanicWithResolutions()
        {
            Assert.Equal("SetWeather(SunnyWeather)",
                new TagBinding(MechanicTag.SetWeather, ResolutionTag.SunnyWeather).ToString());
            Assert.Equal("EmptyTag", new TagBinding(MechanicTag.EmptyTag).ToString());
        }

        [Fact]
        public void Json_RoundTrips()
        {
            var original = new List<TagBinding>
            {
                new(MechanicTag.GrantImmunity, ResolutionTag.WaterVeil, ResolutionTag.WaterType),
                new(MechanicTag.EmptyTag)
            };

            var result = JsonSerializer.Deserialize<List<TagBinding>>(
                JsonSerializer.Serialize(original, Options), Options);

            Assert.Equal(original, result);
        }
    }
}
