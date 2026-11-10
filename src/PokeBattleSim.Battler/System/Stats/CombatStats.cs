using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.System;

namespace PokeBattleSim.Battler.System.Stats;

public enum Ruleset
{
    AnotherLife = 0,
    Zephias = 1
}

public sealed record CombatBaseStats(
    int Wounds,
    int MaxEnergy,
    int Speed,
    int Priority,
    int Dodge,
    int Defense,
    int DamageDice,
    int HitDice,
    int AimAutos,
    int MeleeAutos,
    int AimHitDice,
    int MeleeHitDice,
    bool AimThresholdReduced,
    bool MeleeThresholdReduced);

public sealed record CombatStatInputs(
    IReadOnlyDictionary<Attributes, int> Attributes,
    IReadOnlyDictionary<Skills, int> Skills,
    int Friendship,
    double Weight)
{
    public static CombatStatInputs FromSheet(PokemonSheet sheet) => new(
        sheet.CalculateTotalAttributes().ToDictionary(a => a.Name, a => a.BaseValue),
        sheet.CalculateTotalSkills().ToDictionary(s => s.Name, s => s.BaseValue),
        (int)sheet.Friendship,
        sheet.Weight);
}

public interface ICombatStatCalculator
{
    CombatBaseStats Calculate(CombatStatInputs inputs);
}

public static class CombatStatCalculators
{
    public static ICombatStatCalculator For(Ruleset ruleset) => ruleset switch
    {
        Ruleset.Zephias => new ZephiasCalculator(),
        _ => new AnotherLifeCalculator()
    };
}

public abstract class CombatStatCalculatorBase : ICombatStatCalculator
{
    protected abstract int AutosPerLevel { get; }

    protected abstract int Wounds(int stamina, CombatStatInputs inputs);

    protected abstract int SpeedDodge(int speed);

    protected abstract int Priority(int speed);

    protected virtual bool ThresholdReduced(int skill) => false;

    public CombatBaseStats Calculate(CombatStatInputs inputs)
    {
        var power = inputs.Attributes[Attributes.Power];
        var toughness = inputs.Attributes[Attributes.Toughness];
        var speed = inputs.Attributes[Attributes.Speed];
        var stamina = inputs.Attributes[Attributes.Stamina];

        var aim = inputs.Skills[Skills.Aim];
        var melee = inputs.Skills[Skills.Brawl];
        var evasion = inputs.Skills[Skills.Evasion];

        var hitDice = 2 * speed;
        var speedDodge = SpeedDodge(speed);
        var dodge = speedDodge + evasion * Math.Max(2, (int)Math.Ceiling(speedDodge * 0.2));

        var (aimDice, aimAutos) = ConvertAutos(hitDice, aim);
        var (meleeDice, meleeAutos) = ConvertAutos(hitDice, melee);

        return new CombatBaseStats(
            Wounds(stamina, inputs),
            4,
            speed,
            Priority(speed),
            dodge,
            toughness + Tiers.Of(toughness),
            2 * power + Tiers.Of(power),
            hitDice,
            aimAutos,
            meleeAutos,
            aimDice,
            meleeDice,
            ThresholdReduced(aim),
            ThresholdReduced(melee));
    }

    // Autos beyond the dice available are converted into extra dice.
    private (int Dice, int Autos) ConvertAutos(int hitDice, int level)
    {
        var autos = level * AutosPerLevel;
        var excess = Math.Max(0, autos - hitDice);
        return (hitDice + excess, autos - excess);
    }

    public static int WeightBonus(double weight) =>
        weight < 100 ? 0 : 1 + (int)Math.Floor(Math.Log10(weight / 100));
}

public sealed class AnotherLifeCalculator : CombatStatCalculatorBase
{
    protected override int AutosPerLevel => 5;

    protected override int SpeedDodge(int speed) => speed;

    protected override int Priority(int speed) => speed;

    protected override int Wounds(int stamina, CombatStatInputs inputs)
    {
        var tier = Tiers.Of(stamina);
        var perPoint = Tiers.IsEx(stamina) ? 4 : tier == 5 ? 3 : 2;
        return perPoint * stamina + inputs.Friendship + WeightBonus(inputs.Weight);
    }
}

public sealed class ZephiasCalculator : CombatStatCalculatorBase
{
    protected override int AutosPerLevel => 4;

    // Each minus or plus grade reached adds one; base letter grades add none. Points past Ex add one each.
    protected override int SpeedDodge(int speed) =>
        speed <= 0 ? 0 : speed - (int)(Tiers.GradeOf(speed) + 1) / 3;

    protected override int Priority(int speed) => 0;

    protected override bool ThresholdReduced(int skill) => skill >= (int)Grades.S;

    protected override int Wounds(int stamina, CombatStatInputs inputs) =>
        2 * stamina + Tiers.Of(stamina) + WeightBonus(inputs.Weight);
}
