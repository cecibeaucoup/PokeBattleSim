namespace PokeBattleSim.Data.Enums
{
    public enum Tags
    {
        EmptyTag = 0,
    
        #region Movement Tags   
        GroundMovement = 1,
        SwimMovement = 2,
        BurrowMovement = 3,
        HoverMovement = 4,
        FlyMovement = 5,
        WallClimbMovement = 6,
        TeleportMovement = 7,
        #endregion

        #region STAB Tags
        NormalSTAB = 8,
        FightingSTAB = 9,
        FlyingSTAB = 10,
        PoisonSTAB = 11,
        GroundSTAB = 12,
        RockSTAB = 13,
        BugSTAB = 14,
        GhostSTAB = 15,
        SteelSTAB = 16,
        FireSTAB = 17,
        WaterSTAB = 18,
        GrassSTAB = 19,
        ElectricSTAB = 20,
        PsychicSTAB = 21,
        IceSTAB = 22,
        DragonSTAB = 23,
        DarkSTAB = 24,
        FairySTAB = 25,
        #endregion

        #region Veil Tags
        WaterVeil = 26,
        SandVeil = 27,
        SnowVeil = 28,
        MistVeil = 29,
        ToxicVeil = 30,
        FlareVeil = 31,
        ShockVeil = 32,
        #endregion

        #region Mitigation Tags

        NormalMitigation = 33,
        FightingMitigation = 34,
        FlyingMitigation = 35,
        PoisonMitigation = 36,
        GroundMitigation = 37,
        RockMitigation = 38,
        BugMitigation = 39,
        GhostMitigation = 40,
        SteelMitigation = 41,
        FireMitigation = 42,
        WaterMitigation = 43,
        GrassMitigation = 44,
        ElectricMitigation = 45,
        PsychicMitigation = 46,
        IceMitigation = 47,
        DragonMitigation = 48,
        DarkMitigation = 49,
        FairyMitigation = 50,

        #endregion

        #region Weather Tags
    
        SunnyWeather = 51,
        RainyWeather = 52,
        SandyWeather = 53,
        SnowyWeather = 54,

        #endregion

        #region Roll Manipulation Tags

        ExplodingDice = 55,
        RerollDice = 56,
        Threshould = 57,
        Minimum = 58,
        Maximum = 59,
        ThreshouldPlus1 = 60,
        ThreshouldPlus2 = 61,
        ThreshouldMinus1 = 62,
        ThreshouldMinus2 = 63,
        MinimumPlus1 = 64,
        MinimumPlus2 = 65,
        MaximumMinus1 = 66,
        MaximumMinus2 = 67,
    
        #endregion

        #region Dice Manipulation Tags

        DoubleDice = 68,
        TwoAndHalfDice = 69,
        TripleDice = 70,
        ThreeAndHalfDice = 71,
        QuadrupleDice = 72,
        HalfDice = 73,
        OneThirdDice = 74,
        QuarterDice = 75,
        OneFifthDice = 76,
        OneSixthDice = 77,
        OneEighthDice = 78,
        ZeroDice = 79,
        DiceIntoAutos = 80,

        #endregion

        #region Auto-Successes Tags

        DoubleAuto = 81,
        DoubleAndHalfAuto = 82,
        TripleAuto = 83,
        TripleAndHalfAuto = 84,
        QuadrupleAuto = 85,
        HalfAuto = 86,
        OneThirdAuto = 87,
        OneQuarterAuto = 88,
        OneFifthAuto = 89,
        OneSixthAuto = 90,
        OneEighthAuto = 91,
        ZeroAuto = 92,
        AutosIntoDice = 93,

        #endregion

        #region Attribute and Skill Checks

        PowerCheck = 94,
        SpeedCheck = 95,
        ToughnessCheck = 96,
        StaminaCheck = 97,
        AimCheck = 98,
        EvasionCheck = 99,
        EfficiencyCheck = 100,
        BrawlCheck = 101,

        #endregion

        #region Natural Gift Tags

        NaturalGiftNormal = 102,
        NaturalGiftFighting = 103,
        NaturalGiftFlying = 104,
        NaturalGiftPoison = 105,
        NaturalGiftGround = 106,
        NaturalGiftRock = 107,
        NaturalGiftBug = 108,
        NaturalGiftGhost = 109,
        NaturalGiftSteel = 110,
        NaturalGiftFire = 111,
        NaturalGiftWater = 112,
        NaturalGiftGrass = 113,
        NaturalGiftElectric = 114,
        NaturalGiftPsychic = 115,
        NaturalGiftIce = 116,
        NaturalGiftDragon = 117,
        NaturalGiftDark = 118,
        NaturalGiftFairy = 119,

        #endregion

        #region 
        #endregion
    }
}
