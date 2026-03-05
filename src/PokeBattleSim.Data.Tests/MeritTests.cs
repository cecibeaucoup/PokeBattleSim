using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokeBattleSim.Data.Tests;

public class MeritTests
{
    [Fact]
    public void Merit_Properties()
    {
        var attrs = new List<Attribute>()
        { 
            new(1, "Power"),
            new(1, "Toughness"),
            new(1, "Speed"),
            new(1, "Stamina") 
        };

        var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, attrs);
        Assert.Equal(MeritTypes.Ace, m.MeritType);
        Assert.Equal(MeritFocus.Attributes, m.Focus);
    }
}
