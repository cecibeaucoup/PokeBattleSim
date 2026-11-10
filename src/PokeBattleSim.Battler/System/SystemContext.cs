using System;

namespace PokeBattleSim.Battler.System;

public class SystemContext
{
    public Rolls RollsConfiguration { get; set; } = new Rolls();

    public PokeBattleSim.Battler.System.Stats.Ruleset Ruleset { get; set; } = PokeBattleSim.Battler.System.Stats.Ruleset.AnotherLife;

    public BattlePatterns BattlePatternsConfiguration { get; set; } = new BattlePatterns();
}
