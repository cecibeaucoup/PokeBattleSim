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
            new(1, Attributes.Power),
            new(1, Attributes.Toughness),
            new(1, Attributes.Speed),
            new(1, Attributes.Stamina) 
        };

        var m = new Merit(MeritTypes.Ace, MeritFocus.Attributes, attrs);
        Assert.Equal(MeritTypes.Ace, m.MeritType);
        Assert.Equal(MeritFocus.Attributes, m.Focus);
    }
}
