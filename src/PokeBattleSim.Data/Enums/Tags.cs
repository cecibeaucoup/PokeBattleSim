namespace PokeBattleSim.Data.Enums;

public enum Tags
{
    CritImmune = 0,
    
    #region Movement Tags
    GroundMovement = 1,
    SwimMovement = 2,
    BurrowMovement = 3,
    HoverMovement = 4,
    FlyMovement = 5,
    #endregion

    #region STAB Tags
    NormalSTAB = 6,
    FightingSTAB = 7,
    FlyingSTAB = 8,
    PoisonSTAB = 9,
    GroundSTAB = 10,
    RockSTAB = 11,
    BugSTAB = 12,
    GhostSTAB = 13,
    SteelSTAB = 14,
    FireSTAB = 15,
    WaterSTAB = 16,
    GrassSTAB = 17,
    ElectricSTAB = 18,
    PsychicSTAB = 19,
    IceSTAB = 20,
    DragonSTAB = 21,
    DarkSTAB = 22,
    FairySTAB = 23,
    #endregion

    #region Veil Tags
    StormVeil = 24,
    SandVeil = 25,
    SnowVeil = 26,
    MistVeil = 27,
    ToxicVeil = 28,
    FlareVeil = 29,
    ShockVeil = 30,
    #endregion

}
