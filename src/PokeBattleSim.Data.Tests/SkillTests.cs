using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Data.Tests;

public class SkillTests
{
    [Fact]
    public void Skill_PropertiesAndToDex()
    {
        var s = new Skill(3, "Aim");
        Assert.Equal(3, s.BaseValue);
        Assert.Equal("Aim", s.Name);
        Assert.Contains("3", s.ToDex());
    }
}
