namespace PokeBattleSim.Data.Enums
{
    public enum Tags
    {
        EmptyTag,
    
        #region Movement Tags   
        GroundMovement,
        SwimMovement,
        BurrowMovement,
        HoverMovement,
        FlyMovement,
        WallClimbMovement,
        TeleportMovement,
        #endregion

        #region Typing Tags 

        #region STAB Tags
        NormalSTAB,
        FightingSTAB,
        FlyingSTAB,
        PoisonSTAB,
        GroundSTAB,
        RockSTAB,
        BugSTAB,
        GhostSTAB,
        SteelSTAB,
        FireSTAB,
        WaterSTAB,
        GrassSTAB,
        ElectricSTAB,
        PsychicSTAB,
        IceSTAB,
        DragonSTAB,
        DarkSTAB,
        FairySTAB,
        #endregion

        #region Mitigation Tags

        NormalMitigation,
        FightingMitigation,
        FlyingMitigation,
        PoisonMitigation,
        GroundMitigation,
        RockMitigation,
        BugMitigation,
        GhostMitigation,
        SteelMitigation,
        FireMitigation,
        WaterMitigation,
        GrassMitigation,
        ElectricMitigation,
        PsychicMitigation,
        IceMitigation,
        DragonMitigation,
        DarkMitigation,
        FairyMitigation,

        #endregion

        #region Natural Gift Tags

        NaturalGiftNormal,
        NaturalGiftFighting,
        NaturalGiftFlying,
        NaturalGiftPoison,
        NaturalGiftGround,
        NaturalGiftRock,
        NaturalGiftBug,
        NaturalGiftGhost,
        NaturalGiftSteel,
        NaturalGiftFire,
        NaturalGiftWater,
        NaturalGiftGrass,
        NaturalGiftElectric,
        NaturalGiftPsychic,
        NaturalGiftIce,
        NaturalGiftDragon,
        NaturalGiftDark,
        NaturalGiftFairy,

        #endregion

        #endregion

        #region Veil Tags

        WaterVeil,
        SandVeil,
        SnowVeil,
        MistVeil,
        ToxicVeil,
        FlareVeil,
        ShockVeil,

        #endregion

        #region Weather Tags
    
        SunnyWeather,
        RainyWeather,
        SandyWeather,
        SnowyWeather,

        #endregion

        #region Roll Manipulation Tags

        ExplodingDice,
        RerollDice,
        Threshould,
        Minimum,
        Maximum,
        ThreshouldPlus1,
        ThreshouldPlus2,
        ThreshouldMinus1,
        ThreshouldMinus2,
        MinimumPlus1,
        MinimumPlus2,
        MaximumMinus1,
        MaximumMinus2,
    
        #endregion

        #region Dice Manipulation Tags

        DoubleDice,
        TwoAndHalfDice,
        TripleDice,
        ThreeAndHalfDice,
        QuadrupleDice,
        HalfDice,
        OneThirdDice,
        QuarterDice,
        OneFifthDice,
        OneSixthDice,
        OneEighthDice,
        ZeroDice,
        DiceIntoAutos,

        #endregion

        #region Auto-Successes Tags

        DoubleAuto,
        DoubleAndHalfAuto,
        TripleAuto,
        TripleAndHalfAuto,
        QuadrupleAuto,
        HalfAuto,
        OneThirdAuto,
        OneQuarterAuto,
        OneFifthAuto,
        OneSixthAuto,
        OneEighthAuto,
        ZeroAuto,
        AutosIntoDice,

        #endregion

        #region Attribute and Skill Checks

        PowerCheck,
        SpeedCheck,
        ToughnessCheck,
        StaminaCheck,
        AimCheck,
        EvasionCheck,
        EfficiencyCheck,
        BrawlCheck,

        #endregion

        #region 
        #endregion
    }
}
