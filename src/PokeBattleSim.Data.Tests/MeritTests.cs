using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Tests;

public class MeritTests
{
    [Fact]
    public void Merit_Properties()
    {
        var attrs = new Attributes(1,1,1,1);
        var skills = new Skills(0,0,0,0);
        var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, attrs, skills);
        Assert.Equal(MeritTypes.Ace, m.Type);
        Assert.Equal(MeritFocus.Attributes, m.Focus);
    }
}
