using System;

namespace PokeBattleSim.Battler.System;

public class SystemContext
{
    public Rolls RollsConfiguration { get; set; } = new Rolls();

    public BattlePatterns BattlePatternsConfiguration { get; set; } = new BattlePatterns();
}
