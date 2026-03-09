using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.System;

public class BattleActions(Dictionary<AttackPatterns, MoveStats> _offensiveActions, 
                            Dictionary<DefensePatterns, MoveStats> _defensiveActions)
{
    public Dictionary<AttackPatterns, MoveStats> OffensiveActions { get; set; } = _offensiveActions;

    public Dictionary<DefensePatterns, MoveStats> DefensiveActions { get; set; } = _defensiveActions;
}
