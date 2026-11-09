namespace PokeBattleSim.Battler.System;

public enum EncounterKind
{
    TrainerVsTrainer,
    TrainerVsWild
}

public sealed record BattleSideSetup(
    string SideId,
    string? TrainerId,
    IReadOnlyList<CombatantState> Roster,
    IReadOnlyList<string> ActiveCombatantIds);

public sealed record BattleSetup(
    EncounterKind EncounterKind,
    IReadOnlyList<BattleSideSetup> Sides,
    int RandomSeed)
{
    public BattleState CreateState()
    {
        Validate();
        return new BattleState(Sides.SelectMany(side => side.Roster));
    }

    private void Validate()
    {
        if (Sides.Count != 2)
            throw new InvalidOperationException("A battle requires exactly two sides.");

        if (Sides.Any(side => side.Roster.Count == 0))
            throw new InvalidOperationException("Every battle side requires at least one Pokemon.");

        if (Sides.SelectMany(side => side.Roster).Select(combatant => combatant.Id).Distinct().Count() !=
            Sides.SelectMany(side => side.Roster).Count())
            throw new InvalidOperationException("Combatant IDs must be unique within a battle.");

        foreach (var side in Sides)
        {
            if (side.ActiveCombatantIds.Count == 0 ||
                side.ActiveCombatantIds.Any(id => side.Roster.All(combatant => combatant.Id != id)))
                throw new InvalidOperationException(
                    $"Side '{side.SideId}' has an invalid active Pokemon selection.");
        }

        if (EncounterKind == EncounterKind.TrainerVsWild &&
            Sides.Count(side => side.TrainerId is not null) != 1)
            throw new InvalidOperationException(
                "A trainer-versus-wild encounter requires exactly one trainer side.");

        if (EncounterKind == EncounterKind.TrainerVsTrainer &&
            Sides.Any(side => side.TrainerId is null))
            throw new InvalidOperationException(
                "A trainer-versus-trainer encounter requires a trainer for every side.");
    }
}
