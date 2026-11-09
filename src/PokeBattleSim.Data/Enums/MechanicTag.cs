namespace PokeBattleSim.Data.Enums
{
    /// <summary>
    /// What the battle system must do. The exact parameters live in <see cref="ResolutionTag"/>.
    /// </summary>
    public enum MechanicTag
    {
        EmptyTag,

        Movement,
        STAB,
        Mitigation,
        NaturalGift,
        GrantImmunity,
        SetWeather,
        SetTerrain,
        ApplyCondition,
        RestrictResistance,

        RollModification,
        DiceModification,
        AutoModification,
        Check,

        ModifyPriority,
        ModifyEvasion,
        ModifyDefense,
        ModifyHit,
        ModifyDamage
    }
}
