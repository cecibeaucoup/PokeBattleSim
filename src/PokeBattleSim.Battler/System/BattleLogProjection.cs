using System.Text;

namespace PokeBattleSim.Battler.System;

public static class BattleLogProjection
{
    public static string Render(IEnumerable<BattleEvent> events, BattleState state)
    {
        var output = new StringBuilder();
        foreach (var battleEvent in events)
        {
            switch (battleEvent)
            {
                case PhaseChangedEvent phase:
                    output.AppendLine($"Phase: {phase.To}");
                    break;
                case ActionDeclaredEvent action:
                    output.AppendLine($"{state.GetCombatant(action.Action.ActorId).Name} uses {action.Action.Move.Name}.");
                    break;
                case HitResolvedEvent hit:
                    output.AppendLine(hit.Hit
                        ? $"{state.GetCombatant(hit.ActorId!).Name} hits {state.GetCombatant(hit.TargetId!).Name}."
                        : $"{state.GetCombatant(hit.ActorId!).Name} misses {state.GetCombatant(hit.TargetId!).Name}.");
                    break;
                case DamageResolvedEvent damage:
                    output.AppendLine($"{state.GetCombatant(damage.TargetId!).Name} suffers {damage.Damage} wounds.");
                    break;
                case FaintedEvent fainted:
                    output.AppendLine($"{state.GetCombatant(fainted.ActorId!).Name} has fainted.");
                    break;
                case RoundEndedEvent round:
                    output.AppendLine($"Round {round.Round} ended.");
                    break;
            }
        }

        return output.ToString();
    }
}
