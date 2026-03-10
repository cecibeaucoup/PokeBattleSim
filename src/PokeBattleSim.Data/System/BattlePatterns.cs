using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.System;

public class BattlePatterns(Dictionary<AttackPatterns, MoveStats>? _offensiveActions = null, 
                            Dictionary<DefensePatterns, MoveStats>? _defensiveActions = null)
{
    public Dictionary<AttackPatterns, MoveStats> OffensiveActions { get; set; } = _offensiveActions ?? [];

    public Dictionary<DefensePatterns, MoveStats> DefensiveActions { get; set; } = _defensiveActions ?? [];
}
