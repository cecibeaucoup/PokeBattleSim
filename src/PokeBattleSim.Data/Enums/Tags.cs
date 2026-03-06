namespace PokeBattleSim.Data.Enums;

public enum Tags
{
    EmptyTag = 0,
    
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
    WaterVeil = 24,
    SandVeil = 25,
    SnowVeil = 26,
    MistVeil = 27,
    ToxicVeil = 28,
    FlareVeil = 29,
    ShockVeil = 30,
    #endregion

    #region Mitigation Tags

    NormalMitigation = 31,
    FightingMitigation = 32,
    FlyingMitigation = 33,
    PoisonMitigation = 34,
    GroundMitigation = 35,
    RockMitigation = 36,
    BugMitigation = 37,
    GhostMitigation = 38,
    SteelMitigation = 39,
    FireMitigation = 40,
    WaterMitigation = 41,
    GrassMitigation = 42,
    ElectricMitigation = 43,
    PsychicMitigation = 44,
    IceMitigation = 45,
    DragonMitigation = 46,
    DarkMitigation = 47,
    FairyMitigation = 48,

    #endregion

    #region Weather Tags
    
    SunnyWeather = 49,
    RainyWeather = 50,
    SandyWeather = 51,
    SnowyWeather = 52,

    #endregion

    #region Roll Manipulation Tags

    ExplodingDice = 53,
    RerollDice = 54,
    Threshould = 55,
    Minimum = 56,
    Maximum = 57,
    ThreshouldPlus1 = 58,
    ThreshouldPlus2 = 59,
    ThreshouldMinus1 = 60,
    ThreshouldMinus2 = 61,
    MinimumPlus1 = 62,
    MinimumPlus2 = 63,
    MaximumMinus1 = 64,
    MaximumMinus2 = 65,
    
    #endregion

    #region Dice Manipulation Tags

    DoubleDice = 66,
    TwoAndHalfDice = 67,
    TripleDice = 68,
    ThreeAndHalfDice = 69,
    QuadrupleDice = 70,
    HalfDice = 71,
    OneThirdDice = 72,
    QuarterDice = 73,
    OneFifthDice = 74,
    OneSixthDice = 75,
    OneEighthDice = 76,
    ZeroDice = 77,
    DiceIntoAutos = 78,

    #endregion

    #region Auto-Successes Tags

    DoubleAuto = 79,
    DoubleAndHalfAuto = 80,
    TripleAuto = 81,
    TripleAndHalfAuto = 82,
    QuadrupleAuto = 83,
    HalfAuto = 84,
    OneThirdAuto = 85,
    OneQuarterAuto = 86,
    OneFifthAuto = 87,
    OneSixthAuto = 88,
    OneEighthAuto = 89,
    ZeroAuto = 90,
    AutosIntoDice = 91,

    #endregion

    #region Attribute and Skill Checks

    PowerCheck = 92,
    SpeedCheck = 93,
    ToughnessCheck = 94,
    StaminaCheck = 95,
    AimCheck = 96,
    EvasionCheck = 97,
    EfficiencyCheck = 98,
    BrawlCheck = 99,

    #endregion

    #region Natural Gift Tags

    NaturalGiftNormal = 100,
    NaturalGiftFighting = 101,
    NaturalGiftFlying = 102,
    NaturalGiftPoison = 103,
    NaturalGiftGround = 104,
    NaturalGiftRock = 105,
    NaturalGiftBug = 106,
    NaturalGiftGhost = 107,
    NaturalGiftSteel = 108,
    NaturalGiftFire = 109,
    NaturalGiftWater = 110,
    NaturalGiftGrass = 111,
    NaturalGiftElectric = 112,
    NaturalGiftPsychic = 113,
    NaturalGiftIce = 114,
    NaturalGiftDragon = 115,
    NaturalGiftDark = 116,
    NaturalGiftFairy = 117,

    #endregion

    #region 
    #endregion
}
