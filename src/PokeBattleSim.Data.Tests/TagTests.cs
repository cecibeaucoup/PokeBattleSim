using Xunit;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Data.Tests;

public class TagTests
{
    [Fact]
    public void Tag_Properties()
    {
        var t = new Tag("test", "desc");
        Assert.Equal("test", t.Name);
        Assert.Equal("desc", t.Description);
    }
}
