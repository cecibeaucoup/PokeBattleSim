using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;


namespace PokeBattleSim.Data.Tests;

public class SkillTests
{
    [Fact]
    public void Skill_PropertiesAndToDex()
    {
        var s = new Skill(3, Skills.Aim);
        Assert.Equal(3, s.BaseValue);
        Assert.Equal(Skills.Aim, s.Name);
        Assert.Contains("3", s.ToDex());
    }
}
