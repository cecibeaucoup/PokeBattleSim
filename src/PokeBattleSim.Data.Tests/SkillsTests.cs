using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Data.Tests;

public class SkillsTests
{
    [Fact]
    public void Sum_AddsSkills()
    {
        var s1 = new Skills(1, 2, 3, 4);
        var s2 = new Skills(5, 6, 7, 8);

        var sum = Skills.Sum(s1, s2);

        Assert.Equal(6, sum.Aim.BaseValue);
        Assert.Equal(8, sum.Efficiency.BaseValue);
        Assert.Equal(10, sum.Evasion.BaseValue);
        Assert.Equal(12, sum.Brawl.BaseValue);
    }
}
