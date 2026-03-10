using System;

namespace PokeBattleSim.Data.System;

public class SystemContext
{
    public Rolls RollsConfiguration { get; set; } = new Rolls();

    public BattlePatterns BattlePatternsConfiguration { get; set; } = new BattlePatterns();
}
